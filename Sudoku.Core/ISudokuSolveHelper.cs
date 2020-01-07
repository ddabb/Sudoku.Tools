using Sudoku.Core;
using System.Collections.Generic;

namespace Sudoku.Core
{

    public interface ISudokuSolveHelper
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
        List<NegativeCellInfo> Elimination(QSudoku qSudoku);

        SolveMethodEnum methodType { get; }
        
    }
}
