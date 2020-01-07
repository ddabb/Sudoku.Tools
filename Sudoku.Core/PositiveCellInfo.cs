using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{
    public class PositiveCellInfo : CellInfo
    {


        public PositiveCellInfo(int index, int value) : base(index, value)
        {
            this.CellType = CellType.Positive;

        }

        public override CellInfo Parent { get ; set ; }
        public override List<CellInfo> NextCells { get; set; }


        public override List<CellInfo> GetNextCells()
        {
            throw new NotImplementedException();
        }
    }
}
