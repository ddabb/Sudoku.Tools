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

        public CellType cellType { get; set; }
        private QSudoku sudoku;

        /// <summary>
        /// 推导层级
        /// </summary>
        public int level;


        public CellInfo(int index, int value)
        {
            this.Index = index;
            Value = value;
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

        public abstract CellInfo parent { get; set; }
        public abstract List<CellInfo> NextCells { get; set; }
        
        public override string ToString()
        {
            return "index  " + Index + "  row  " + Row + "  column  " + Column + "  block  " + Block + "  value  " + Value;
        }


        public List<CellInfo> GetAllRelatedCell(List<CellInfo> allCellInfo)
        {
            return allCellInfo.Where(c => c.Index != this.Index && (c.Block == this.Block || c.Row == this.Row || c.Column == this.Column)).ToList();

        }
        public int Value;

        public override bool Equals(object obj)
        {
            if (obj is CellInfo cell)
            {
                if (cell.Index == Index && cell.Value == Value)
                {
                    return true;

                }
            }
            return false; ;
        }

        public abstract List<CellInfo> GetNextCells(QSudoku sudoku);
   
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