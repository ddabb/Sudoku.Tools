using System.Collections.Generic;

namespace Sudoku.Tools
{

    public interface ISudokuSolveHelper
    {
        List<CellInfo> Excute(QSudoku qSoduku);
    }
}
