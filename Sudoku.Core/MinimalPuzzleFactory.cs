using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{
    /// <summary>
    /// 精简题工厂类
    /// </summary>
    public class MinimalPuzzleFactory
    {
        public QSudoku Make(RSudoku rsudoku)
        {

            return new QSudoku(rsudoku.answerString);
        }
    }
}
