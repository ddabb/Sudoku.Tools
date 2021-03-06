﻿using Sudoku.Core;
using System.Collections.Generic;
using Sudoku.Core.Model;
namespace Sudoku.Core
{
    public interface ISudokuSolveHandler
    {
        /// <summary>
        /// 出数
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <returns></returns>
        List<CellInfo> Assignment(QSudoku qSudoku);
        /// <summary>
        /// 删数
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <returns></returns>
        List<CellInfo> Elimination(QSudoku qSudoku);
        SolveMethodEnum methodType { get; }
        MethodClassify methodClassify { get; }
        public string GetDesc();
    }
}
