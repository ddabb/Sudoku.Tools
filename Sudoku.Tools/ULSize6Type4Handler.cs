using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(7,"R3C8","640010300890300000230480500579132000362948715418567932156004893983651247724893156")]
    public class ULSize6Type4Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize6Type4;

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
