using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{
    public interface ICellInfo
    {
        CellInfo Parent { get; set; }
        List<CellInfo> GetNextCells();

        List<CellInfo> NextCells { get; set; }
    }
}
