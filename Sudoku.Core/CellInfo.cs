namespace Sudoku.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class CellInfo
    {
        public int Row;
        public int Column;
        public int Block;
        private int mIndex;
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
        public override string ToString()
        {
            return "index  " + Index + "  row  " + Row + "  column  " + Column + "  block  " + Block + "  value  " + Value;
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
    }
}