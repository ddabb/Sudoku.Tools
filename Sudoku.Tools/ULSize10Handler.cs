using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("003400879008093002902068534009800007325047908087009000750284093294316785830975240")]
    public class ULSize10Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize10;

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
