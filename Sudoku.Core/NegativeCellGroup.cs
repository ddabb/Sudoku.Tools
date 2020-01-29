using System.Collections.Generic;

namespace Sudoku.Core
{
    public class NegativeCellGroup : CellInfo
    {


        public NegativeCellGroup(List<int> index, int value) : base(index, value)
        {
            this.CellType = CellType.NegativeCellGroup;

            if (Index == 70 && Value == 5)
            {

            }

        }


        public override bool IsError { get; }
        public override List<CellInfo> GetNextCells()
        {
            throw new System.NotImplementedException();
        }

        public override List<CellInfo> NextCells { get; }
    }
}