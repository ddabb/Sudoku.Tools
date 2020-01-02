using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{

    [Example("000436517000280000006170000000061070001000000050804100000043761003610000000000394")]
    public class DirectPointingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            List<NegativeCellInfo> cells = new List<NegativeCellInfo>();
            return cells;
        }

        
    }
}
