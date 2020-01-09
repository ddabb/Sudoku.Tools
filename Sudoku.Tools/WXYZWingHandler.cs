using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("000037501152000600000500000070102000400750100218000750000305000829476315000090000")]
    public class WXYZWingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.WXYZWing;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
