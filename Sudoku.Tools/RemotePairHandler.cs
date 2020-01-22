using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(4,"R2C6","450106237073000165261573489316000578724865913005731642542317896130650724607002351")]
    public class RemotePairHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.RemotePair;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var filter = qSudoku.AllUnSetCells;
            var checkCells = filter.Where(c => c.GetRest().Count == 2).ToList();
            var cd = (from a in checkCells
                      join b in checkCells on a.GetRestString() equals b.GetRestString()
                      join c in checkCells on b.GetRestString() equals c.GetRestString()
                      join d in checkCells on b.GetRestString() equals d.GetRestString()
                      where new List<int>() { a.Index, b.Index, c.Index, d.Index }.Distinct().Count() == 4
                            && ((a.Column == b.Column && a.Row == c.Row && b.Row == d.Row) || (a.Row == b.Row
                                                                                               && a.Column == c.Column &&
                                                                                               b.Column == d.Column)
                            )
                            && c.Row != d.Row && c.Column != d.Column

                      select new { c, d }).ToList();
            foreach (var item in cd)
            {
                var c = item.c;
                var d = item.d;
                var rest = c.GetRest();
                cells.AddRange((from cell in qSudoku.GetPublicUnsetAreas(c, d) 
                    let cellrest = cell.GetRest() 
                    where cellrest.Intersect(rest).Any() && cellrest.Except(rest).Count() == 1 
                    select new PositiveCellInfo(cell.Index, cellrest.Except(rest).First())).Cast<CellInfo>());
            }
            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
