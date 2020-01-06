using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{
    public class PositiveCellInfo : CellInfo
    {


        public PositiveCellInfo(int index, int value) : base(index, value)
        {
            this.cellType = CellType.Positive;

        }

        public override CellInfo parent { get ; set ; }
        public override List<CellInfo> NextCells { get; set; }


        public override List<CellInfo> GetNextCells(QSudoku sudoku)
        {
            throw new NotImplementedException();
        }
    }
}
