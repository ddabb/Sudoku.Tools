using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample("450106237073000165261573489316000578724865913005731642542317896130650724607002351")]
    public class RemotePairHandler : SolverHandlerBase
    {
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
