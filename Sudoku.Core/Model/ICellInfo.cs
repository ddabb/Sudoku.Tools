using System.Collections.Generic;
namespace Sudoku.Core.Model
{
    public interface ICellInfo
    {
        CellInfo Parent { get; set; }
        
        List<CellInfo> InitNextCells();
        List<CellInfo> NextCells { get; }
    }
}
