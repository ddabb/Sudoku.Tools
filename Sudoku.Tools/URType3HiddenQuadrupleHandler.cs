using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample(9,"R8C3", "193524800850376900007000000000051230001043600035960000000000500500607048006485092")]
  public  class URType3HiddenQuadrupleHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.URType3HiddenQuadruple;
        public override MethodClassify methodClassify { get; }
    }
}
