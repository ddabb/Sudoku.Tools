using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core;

namespace Sudoku.Tools
{
    public class DymanicForcingChainHandler : SolverHandlerBase
    {
        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.DymanicForcingChain;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
