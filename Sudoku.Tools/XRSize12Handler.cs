using Sudoku.Core;
using System;
using System.Collections.Generic;

namespace Sudoku.Tools
{
    [AssignmentExample(5, "R6C7", "395000001107503900800900753500020006634158297200060000483090000901604008700000049")]
    public class XRSize12Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize12;

        public override MethodClassify methodClassify => throw new NotImplementedException();

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var result = XRSizeCommonMethod(qSudoku, 6);
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
