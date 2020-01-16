using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(5, "R5C5","748359126001726840026400705200040087074030000180070004402007608017003402805204070")]
    public class LockedURType1Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.LockedURType1;

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
