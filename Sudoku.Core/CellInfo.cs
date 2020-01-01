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
            this.index = index;
            Value = value;
        }

        public int index
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
            return "index  " + index + "  row  " + Row + "  column  " + Column + "  block  " + Block + "  value  " + Value;
        }
        public int Value;

        public override bool Equals(object obj)
        {
            if (obj is CellInfo cell)
            {
                if (cell.index == index && cell.Value == Value)
                {
                    return true;

                }
            }
            return false; ;
        }
    }
}