using Sudoku.Core;
using System.Collections.Generic;

namespace Sudoku.Core
{

    public interface ISudokuSolveHelper
    {
        List<CellInfo> Assignment(QSudoku qSudoku);
    }
}
