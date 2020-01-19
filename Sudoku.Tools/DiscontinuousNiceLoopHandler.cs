using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample(7, "R1C1", "000080200005000040020005000962837000003214697174500832001000000697348521248751369")]
    public  class DiscontinuousNiceLoopHandler : SolverHandlerBase
    {
        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.DiscontinuousNiceLoop;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
