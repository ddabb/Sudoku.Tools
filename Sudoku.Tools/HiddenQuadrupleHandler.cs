using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("900164080070983215813200964080020000500001070002000042040716000000000000007892000")] //已调整
    public class HiddenQuadrupleHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.HiddenQuadruple;

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
