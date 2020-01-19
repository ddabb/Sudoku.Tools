using System;

namespace Sudoku.Tools
{
    /// <summary>
    /// 删数用例
    /// </summary>
    public class EliminationExampleAttribute : Attribute
    {
        public string queryString;
        public int value;
        public string positionString;

        public EliminationExampleAttribute(string queryString)
        {
            this.queryString = queryString;

        }

        public EliminationExampleAttribute(int positionValue, string positionString, string queryString)
        {
            this.queryString = queryString;
            this.value = positionValue;
            this.positionString = positionString;

        }
    }
}