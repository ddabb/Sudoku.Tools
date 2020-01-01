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
    public class HiddenSingleBlockHandler :SolverHandlerBase
    {
        public override List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var direction = Direction.Block;
            foreach (var index in QSudoku.baseIndexs)
            {
                cells.AddRange(GetHiddenSingleCellInfo(qSoduku, c => GetFilter(c, direction, index)));
            }
            return cells;
        }
    }
}
