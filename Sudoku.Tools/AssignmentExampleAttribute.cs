using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    /// <summary>
    /// 出数用例
    /// </summary>
    public class AssignmentExampleAttribute : Attribute
    {
        public string queryString;
        public int value;
        public string positionString;

        public AssignmentExampleAttribute(int positionValue,string positionString, string queryString)
        {
            this.queryString = queryString;
            this.value = positionValue;
            this.positionString = positionString;

        }

        public AssignmentExampleAttribute(string queryString)
        {
            this.queryString = queryString;
        }
    }
}
