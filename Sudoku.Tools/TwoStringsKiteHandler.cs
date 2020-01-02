using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{

    /// <summary>
    /// https://www.cnblogs.com/asdyzh/p/10145026.html
    /// </summary>
    [AssignmentExample("081020600042060089056800240693142758428357916175689324510036892230008460860200000")]
    public class TwoStringsKiteHandler : ISudokuSolveHelper
    {
        public List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            return cells;
        }

        public List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
