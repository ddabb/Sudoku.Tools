using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [Example("012009600600012094749635821428397100397156482156020009904573210070261940201900007")]
    public  class WWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
