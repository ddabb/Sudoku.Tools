using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    
    [EliminationExample("300800060109500000050030000030000206600308109005067000000603408703000000040002030")]
    [AssignmentExample("030150209000360050700490603001273800000519000003684700100000008320040000409001060")]
    public class URType2Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.URType2Handler;

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
