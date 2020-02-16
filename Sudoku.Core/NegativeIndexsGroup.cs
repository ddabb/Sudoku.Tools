using System.Collections.Generic;

namespace Sudoku.Core
{
    public class NegativeIndexsGroup : CellInfo
    {


        public NegativeIndexsGroup(List<int> index, int value) : base(index, value)
        {
            this.CellType = CellType.NegativeIndexsGroup;

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

        public override string Desc => throw new System.NotImplementedException();
    }
}