using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample(9, "R5C6", "000040900080905600097160000009001200300000007008600100800794352035216089902583000")]
    public class URType3HiddenTripleHandller : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.URType3HiddenTriple;
        public override MethodClassify methodClassify { get; }
    }
}
