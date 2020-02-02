using System;
using System.Collections.Generic;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample(8, "R8C7", "002019008140005300000000000010050000507020080000008006800003002003041060000980000")]
    public class HiddenSingleBlockHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.HiddenSingleBlock;

        public override MethodClassify methodClassify => MethodClassify.SudokuRules;

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

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
