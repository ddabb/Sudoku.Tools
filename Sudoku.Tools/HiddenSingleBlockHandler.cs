using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Soduku.Tools
{
    [Example("900400613320190700000000009000017008000000000700360000800000000009045086253001004")]
    public class HiddenSingleBlockHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var direction = Direction.Block;
            foreach (var index in baseIndexs)
            {
                cells.AddRange(GetHiddenSingleCellInfo(qSudoku, c => GetFilter(c, direction, index) && c.Value == 0));
            }
            return cells;
        }
    }
}
