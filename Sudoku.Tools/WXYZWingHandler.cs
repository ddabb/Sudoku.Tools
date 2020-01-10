using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample("000109030190700006300286001581472003900568002600391785700915008210007050050020000")]
    public class WXYZWingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.WXYZWing;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCell.Where(c => c.GetRest().Count <= 4).ToList();

            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
