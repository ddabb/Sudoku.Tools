using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Sudoku.Core.Model
{
   public class ChainCell : CellInfo
    {
        public ChainCell(int index, int value, QSudoku sudoku) : base(index, value, sudoku)
        {
            this.CellType = CellType.Chain;
        }
        public override string Desc { get; } = "";
        public override bool IsError { get; } = false;
        public override List<CellInfo> InitNextCells()
        {
            return new List<CellInfo>();
        }
        public override List<CellInfo> GetNextCellsFromSudokuCache()
        {
            return new List<CellInfo>();
        }
        public override List<CellInfo> NextCells { get; }
    }
}
