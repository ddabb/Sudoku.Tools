using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(4, "R9C4","914526300620081459508940162209008510486159723150200890361095240045002901092010635")]
    public class ULSize8Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize8;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCell;
            int cellCount = 8;
            Dictionary<int, List<int>> dic = checkCells.ToDictionary(cell => cell.Index, cell => cell.GetRest());
            List<int> values = G.AllBaseValues.Where(value => dic.Values.Count(c => c.Contains(value)) > cellCount)
                .ToList();
            foreach (var x in values)
            {
                foreach (var y in values)
                {
                    if (x >= y) continue;
                    var pair = new List<int> {x, y};
                    var speacilCells = checkCells.Where(c =>
                        c.GetRest().Intersect(pair).Count() == 2 && c.GetRest().Count == 3).ToList();
                    foreach (var a0 in speacilCells)
                    {
                        foreach (var a3A4A5 in from pair1 in from a1 in checkCells
                                join a2 in checkCells on a1.GetRestString() equals a2.GetRestString()
                                where a1.Index != a2.Index
                                      && a1.Row == a0.Row
                                      && a1.GetRest().Count == 2
                                      && a2.Column == a0.Column
                                select new {a1, a2}
                            let a1 = pair1.a1
                            let a2 = pair1.a2
                            select (from a3 in checkCells
                                join a4 in checkCells on a3.GetRestString() equals a4.GetRestString()
                                join a5 in checkCells on a4.GetRestString() equals a5.GetRestString()
                                where a3.Index != a4.Index
                                      && a5.Index != a4.Index
                                      && a3.GetRest().Count == 2
                                      && a3.Row == a4.Row
                                      && a2.Row == a5.Row
                                      && a3.Column == a1.Column
                                select new {a3, a4, a5}).ToList())
                        {
                            cells.AddRange(from triple1 in a3A4A5
                                let a3 = triple1.a3
                                let a4 = triple1.a4
                                let a5 = triple1.a5
                                select (
                                    from a6 in checkCells
                                    join a7 in checkCells on a6.GetRestString() equals a7.GetRestString()
                                    where a6.Index < a7.Index
                                          && a6.Row == a7.Row
                                          && a7.GetRestString() == pair.JoinString()
                                          && a6.Column == a4.Column
                                          && a7.Column == a5.Column
                                    select new {a6, a7}).ToList()
                                into last
                                where last.Count == 1
                                select new PositiveCellInfo(a0.Index, a0.GetRest().Except(pair).First()));
                        }
                    }
                }
            }
            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
