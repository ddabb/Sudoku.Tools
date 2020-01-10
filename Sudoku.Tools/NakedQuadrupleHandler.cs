using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("390000700000000650507000349049380506601054983853000400900800134002940865400000297")] //已调整
    public class NakedQuadrupleHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.NakedQuadruple;

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
