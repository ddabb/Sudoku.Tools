using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core
{
    public class PossibleCell : CellInfo
    {
        public override string Desc => throw new NotImplementedException();

        public override bool IsError => throw new NotImplementedException();

        public override List<CellInfo> NextCells => throw new NotImplementedException();

        public override List<CellInfo> GetNextCells()
        {
            throw new NotImplementedException();
        }

        public PossibleCell(int index, int value, QSudoku sudoku) : base(index, value,  sudoku)
        {
            this.CellType = CellType.Possible;



        }
    }
}
