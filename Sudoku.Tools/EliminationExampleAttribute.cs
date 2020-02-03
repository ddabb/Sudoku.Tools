using Sudoku.Core;
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
        public SolveMethodEnum[] SolveHandlers;
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

        public EliminationExampleAttribute(int positionValue, string positionString, string queryString, params SolveMethodEnum[] solveHandlers)
        {
            this.queryString = queryString;
            this.value = positionValue;
            this.positionString = positionString;
            this.SolveHandlers = solveHandlers;
        }

       
    }
}