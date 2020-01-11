using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("396002451451090782782451090645230008030000045100045030003500064500904020004020500")]
    //400061070057004001160702084926007138745318000010629457580103726001076845670085010
    //318652007497381005600070138849010000531026009700890051900108500100200006203067014
    public class URType4Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.URType4;

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
