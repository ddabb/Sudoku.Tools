using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(7,"R1C3","120846003008193200900275081400050008002081000890460300080524100219738060700619832",SolveMethodEnum.HiddenPair)]
    public class XRSize6Type4Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize6Type4;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells=new List<CellInfo>();

            var pairCell = qSudoku.AllUnSetCells.Where(c => c.RestCount == 2).ToList();
            var keyCells = (from a in pairCell
                join b in pairCell on a.RestString equals b.RestString
                join c in pairCell on 1 equals 1
                join d in pairCell on c.RestString equals d.RestString
                let sameRow = a.Row == c.Row
                where a.RestString != c.RestString
                      && a.RestList.Intersect(c.RestList).Count() == 1
                      && ((a.Row == c.Row && a.Column == b.Column && b.Row == d.Row && c.Column == d.Column) ||
                          (a.Column == c.Column && a.Row == b.Row && b.Column == d.Column && c.Row == d.Row))
                      && G.MergeCellIndexs(a, b, c, d).Distinct().Count() == 4
                      && a.Index<c.Index
                      && a.Index < b.Index
                      && b.Index < d.Index
                            select new {a, b, c, d, sameRow }).ToList();
            foreach (var item in keyCells)
            {
                
            }

            return cells;
        }
    }
}
