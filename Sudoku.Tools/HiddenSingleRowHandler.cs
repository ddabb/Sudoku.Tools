﻿using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(4,"R8C9","000000000000040329000651840000000000000000473008473296004000900000005180060180700")]
    public class HiddenSingleRowHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.HiddenSingleRow;

        public override MethodClassify methodClassify => MethodClassify.SudokuRules;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var direction = Direction.Row;
            foreach (var index in G.baseIndexs)
            {
                cells.AddRange(GetHiddenSingleCellInfo(qSudoku, c => G.GetFilter(c, direction, index)&&c.Value==0));
            }
            return cells;
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
