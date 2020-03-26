using System;
using System.Collections.Generic;

namespace Sudoku.Core.Model
{
    /// <summary>
    /// 出数用例
    /// </summary>
    public class AssignmentExampleAttribute : Attribute
    {
        public string queryString;
        public int value;
        public string positionString;
        public SolveMethodEnum[] SolveHandlers;
        public KeyValuePair<int, int>[] removeValues;

        public AssignmentExampleAttribute(int positionValue,string positionString, string queryString)
        {
            this.queryString = queryString;
            this.value = positionValue;
            this.positionString = positionString;

        }

        public AssignmentExampleAttribute(int positionValue, string positionString, string queryString,params SolveMethodEnum[] solveHandlers)
        {
            this.queryString = queryString;
            this.value = positionValue;
            this.positionString = positionString;
            this.SolveHandlers = solveHandlers;
        }

        public AssignmentExampleAttribute(int positionValue, string positionString, string queryString, params KeyValuePair<int, int>[] removeValues)
        {
            this.queryString = queryString;
            this.value = positionValue;
            this.positionString = positionString;
            this.removeValues = removeValues;
        }

        public AssignmentExampleAttribute(string queryString)
        {
            this.queryString = queryString;
        }
    }
}
