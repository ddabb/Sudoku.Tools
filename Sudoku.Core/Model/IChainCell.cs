using System.Collections.Generic;

namespace Sudoku.Core.Model
{
    public interface IChainCell
    {
        ThenType thentype { get; set; }
        CellInfo negativeCell { get; set; }
       CellInfo PostiveCell { get; set; }
       List<IChainCell> Children { get; set; }
       IChainCell Parent { get; set; }
    }

    public enum ThenType
    {
        ThenYes,
        ThenNo,
    }


}