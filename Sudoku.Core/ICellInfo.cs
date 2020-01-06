using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{
    public interface ICellInfo
    {
        CellInfo parent { get; set; }
        List<CellInfo> GetNextCells(QSudoku sudoku);

        List<CellInfo> NextCells { get; set; }
    }
}
