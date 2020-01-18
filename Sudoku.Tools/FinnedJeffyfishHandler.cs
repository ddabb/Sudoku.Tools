using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(8,"R9C5","200000003080030050003402100001205400000090000009308600002506900090020070400000001")]
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
