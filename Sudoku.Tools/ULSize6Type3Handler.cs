using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(4,"R2C7","417329586056010000038050000581290640629540801374100295193405700745032000862971354")]
    public class ULSize6Type3Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize6Type3;

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
