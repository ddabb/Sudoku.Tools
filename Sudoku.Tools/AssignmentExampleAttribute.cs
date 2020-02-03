﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sudoku.Core;

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
        public SolveMethodEnum[] SolveHandlers;

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

        public AssignmentExampleAttribute(string queryString)
        {
            this.queryString = queryString;
        }
    }
}
