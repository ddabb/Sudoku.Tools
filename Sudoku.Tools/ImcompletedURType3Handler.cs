using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(6,"R1C6","005000018371908000800100000004581372518070600723400185400800901180600400009014800")]
    public class ImcompletedURType3Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ImcompletedURType3;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
