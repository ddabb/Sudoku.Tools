using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(9,"R5C9","000806172817923546062070983006000400100680730003000600054090867708460301601708204")]
    public class XRSize6Type2Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize6Type2;

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
