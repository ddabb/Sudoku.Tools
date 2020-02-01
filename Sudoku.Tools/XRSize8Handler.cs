using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(5, "R8C1", "004851327253647198008923654820096031006030502340210906081069203002380019439172865")]
    public class XRSize8Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize8;

        public override MethodClassify methodClassify => throw new NotImplementedException();

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var result = XRSizeCommonMethod(qSudoku, 4);
            if (result.Count > 0)
            {
                cells.AddRange(result);
            }
            return cells;
        }



        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
