using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{

    public class RSudoku
    {
        public string answerString;

        public RSudoku(string sudokuString)
        {
            this.answerString = sudokuString;
        }

        /// <summary>
        /// 
        /// </summary>
        private List<CellInfo> cellInfos { get; set; }
    }
}
