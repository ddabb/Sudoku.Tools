﻿using System;
using System.Collections.Generic;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample("900400613320190700000000009000017008000000000700360000800000000009045086253001004")]
    public class HiddenSingleBlockHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.HiddenSingleBlock;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var direction = Direction.Block;
            foreach (var index in G.baseIndexs)
            {
                cells.AddRange(GetHiddenSingleCellInfo(qSudoku, c => G.GetFilter(c, direction, index) && c.Value == 0));
            }
            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
