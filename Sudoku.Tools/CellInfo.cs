namespace Sudoku.Tools
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
        public CellInfo(int index,int value)
        {
            this.index = index;
            this.Value = value;
        }

        public int index
        {
            get { return mIndex; }
            set
            {
                mIndex = value;
                Row = value / 9;
                Column = value % 9;
                Block = (Row / 3) * 3 + (Column / 3);
            }

        }
        public override string ToString()
        {
            return "index  " + this.index + "  row  " + Row + "  column  " + Column + "  block  " + Block + "  value  " + Value;
        }
        public int Value;
    }
}