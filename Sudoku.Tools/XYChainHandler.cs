using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample("060300570052041006000605100580004267429006315607002498006207000005410620290063700")]
    public class XYChainHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XYChain;

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
