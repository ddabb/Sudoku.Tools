using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("914526300620081459508940162209008510486159723150200890361095240045002901092010635")]
    public class ULSize8Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize8;

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
