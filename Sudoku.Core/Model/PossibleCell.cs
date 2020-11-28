using System;
using System.Collections.Generic;
namespace Sudoku.Core.Model
{
    public class PossibleCell : CellInfo
    {
        public override string Desc => throw new NotImplementedException();
        public override bool IsError => throw new NotImplementedException();
        public override List<CellInfo> NextCells => throw new NotImplementedException();
        public override List<CellInfo> InitNextCells()
        {
            return new List<CellInfo>();
        }
        public override List<CellInfo> GetNextCellsFromSudokuCache()
        {
            return new List<CellInfo>();
        }
        public PossibleCell(int index, int value, QSudoku sudoku) : base(index, value,  sudoku)
        {
            this.CellType = CellType.Possible;
        }
    }
}
