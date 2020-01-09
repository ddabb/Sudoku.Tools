using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("030150209000360050700490603001273800000519000003684700100000008320040000409001060")]
    public class FinnedJeffyfishHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.FinnedJeffyfish;

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
