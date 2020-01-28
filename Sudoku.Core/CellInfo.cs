﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Sudoku.Core
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CellInfo: ICellInfo
    {
        public int Row;
        public int Column;
        public int Block;
        private int mIndex;
        
        public CellType CellType { get; set; }

        public QSudoku Sudoku;

        public bool IsRoot = false;

        public List<int> mRest = null;

        
        public int RestCount => GetRest().Count;

        public string RestString => GetRest().JoinString();

        public List<int> RestList => GetRest();

        private List<int> GetRest()
        {
            if (mRest == null)
            {
                var relatedCells = Sudoku.AllSetCell.Where(c => c.Value != 0 && (c.Row == Row || c.Column == Column || c.Block == Block));
                var result = G.AllBaseValues.Except(relatedCells.Select(c => c.Value)).ToList();
                result.Sort();
                mRest = result;
            }
   
            return mRest;
        }
        

        /// <summary>
        /// 推导层级
        /// </summary>
        public int Level { get; set; }=0;
        public abstract  bool IsError { get; }
        /// <summary>
        /// 关联的未设置值的单元格
        /// </summary>
        public List<CellInfo> RelatedUnsetCells
        {
            get
            {
                return this.Sudoku.AllUnSetCells.Where(c=>c.Index != this.Index 
                                                          && (c.Block == this.Block || c.Row == this.Row || c.Column == this.Column)
                                                          ).ToList();
            }
        }


        /// <summary>
        /// 关联的未设置值的单元格
        /// </summary>
        public List<int> RelatedUnsetIndexs
        {
            get { return RelatedUnsetCells.Select(c => c.Index).ToList(); }
        }

        public CellInfo(int index, int value)
        {
            this.Index = index;
            Value = value;
        }

        public void SetSudoku(QSudoku qSudoku)
        {
            this.Sudoku = qSudoku;
        }

        public string RrCc {
            get { return "R"+(this.Row+1)+"C" + (this.Column+1); }
        }
        public int Index
        {
            get { return mIndex; }
            set
            {
                mIndex = value;
                Row = value / 9;
                Column = value % 9;
                Block = Row / 3 * 3 + Column / 3;
            }

        }

        public CellInfo AnalysisRoot;

        public CellInfo Parent { get; set; }


        public FromTo Fromto = new FromTo();

        private List<CellInfo> parentCache;

        public List<CellInfo> GetAllParents()
        {
            if (parentCache!=null)
            {
                return parentCache;
            }
            else
            {
                List<CellInfo> refCellInfos = new List<CellInfo>();
                if (Parent == null)
                {
                    parentCache = refCellInfos;
                    return refCellInfos;
                }
                else
                {
                    refCellInfos.AddRange(Parent.GetAllParents());
                    refCellInfos.Insert(0,Parent);
                    parentCache = refCellInfos;
                    return refCellInfos;
                }
             
             
             
          
            }


        }

        public abstract List<CellInfo> NextCells { get; }
        
        public override string ToString()
        {
            return "index  " + Index +" "+ RrCc + "  value  " + Value+ "  类型："+G.GetEnumDescription(CellType)+ "  层级" + Level;
        }

        public Func<CellInfo, bool> UnSetCellInSameColumn()
        {
            return c => c.Value == 0 && c.Index != this.Index && c.Column == this.Column;
        }

        public Func<CellInfo, bool> UnSetCellInSameRow()
        {
            return c => c.Value == 0 && c.Index != this.Index && c.Row == this.Row;
        }

        public Func<CellInfo, bool> UnSetCellInSameBlock()
        {
            return c => c.Value == 0 && c.Index != this.Index && c.Block == this.Block;
        }

        public int Value;

        public override bool Equals(object obj)
        {
            if (obj is CellInfo cell)
            {
                if (cell.Index == Index && cell.Value == Value&&cell.CellType==CellType)
                {
                    return true;

                }
            }
            return false; ;
        }

        public abstract List<CellInfo> GetNextCells();
   
    }
    /// <summary>
    /// 单元格类型
    /// </summary>
    public enum CellType
    {
        /// <summary>
        /// 否定的，即指定Index单元格的候选数一定不是Value
        /// </summary>
        [Description("否定的")]
        Negative,
        /// <summary>
        /// 肯定的，即指定Index单元格的候选数一定是Value
        /// </summary>
        [Description("肯定的")]
        Init
    }
}