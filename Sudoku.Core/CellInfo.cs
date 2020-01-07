using System;
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

        public List<int> GetRest()
        {
            var relatedCells = Sudoku.AllSetCell.Where(c => c.Value != 0 && (c.Row == Row || c.Column == Column || c.Block == Block));
            var result = G.AllBaseValues.Except(relatedCells.Select(c => c.Value)).ToList();
            result.Sort();
            return result;
        }
        
        public string GetRestString()
        {
            return string.Join(",", GetRest());
        }
        /// <summary>
        /// 推导层级
        /// </summary>
        public int Level { get; set; }=0;

        /// <summary>
        /// 关联的未设置值的单元格
        /// </summary>
        public List<CellInfo> RelatedUnsetCells
        {
            get
            {
                return this.Sudoku.AllUnSetCell.Where(c=>c.Index != this.Index 
                                                          && (c.Block == this.Block || c.Row == this.Row || c.Column == this.Column)
                                                          ).ToList();
            }
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

        public CellInfo Parent { get; set; }


        public FromTo Fromto = new FromTo();

        public List<CellInfo> GetAllParents()
        {
            List<CellInfo> refCellInfos = new List<CellInfo>();
            if (Parent == null) return refCellInfos;
            refCellInfos.Add(Parent);
            refCellInfos.AddRange(Parent.GetAllParents());
            return refCellInfos;

        }

        public abstract List<CellInfo> NextCells { get; }
        
        public override string ToString()
        {
            return "index  " + Index + "  row  " + Row + "  column  " + Column + "  block  " + Block + "  value  " + Value+ "  类型："+G.GetEnumDescription(CellType)+ "  层级" + Level;
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
        Positive
    }
}