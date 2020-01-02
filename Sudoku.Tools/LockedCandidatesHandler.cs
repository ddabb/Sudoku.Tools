using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soduku.Tools
{
    [Example("030150209000360050700490603001273800000519000003684700100000008320040000409001060")]

    public class LockedCandidatesHandler :SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
