using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(1,"R5C2","000400025009005706125700094200000000007042560004310972003004000002000680001080009")]
    public class XRSize14Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize14;

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
