using System;
using System.Collections.Generic;
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

        public abstract CellInfo Parent { get; set; }
        public abstract List<CellInfo> NextCells { get; set; }
        
        public override string ToString()
        {
            return "index  " + Index + "  row  " + Row + "  column  " + Column + "  block  " + Block + "  value  " + Value;
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
        Negative,
        /// <summary>
        /// 肯定的，即指定Index单元格的候选数一定是Value
        /// </summary>
        Positive
    }
}