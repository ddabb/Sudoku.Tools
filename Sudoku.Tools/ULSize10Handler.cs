using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(5, "R6C4","003400879008093002902068534009800007325047908087009000750284093294316785830975240")]
    public class ULSize10Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize10;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCells;
            int cellCount = 10;
            Dictionary<int, List<int>> dic = checkCells.ToDictionary(cell => cell.Index, cell => cell.RestList);
            List<int> values = G.AllBaseValues.Where(value => dic.Values.Count(c => c.Contains(value)) > cellCount)
                .ToList();
            foreach (var x in values)
            {
                foreach (var y in values)
                {
                    if (x >= y) continue;
                    var pair = new List<int> {x, y};
                    var subcheckcells = checkCells.Where(c =>
                        c.RestList.Intersect(pair).Count() == 2).ToList();
                    var speacilCells = subcheckcells.Where(c => c.RestCount == 3).ToList();
                    foreach (var a0 in speacilCells)
                    {
                        var a1a2 = (from a1 in subcheckcells
                            join a2 in subcheckcells on a1.RestString equals a2.RestString
                            where a1.RestString == pair.JoinString()
                                  && a1.Column == a0.Column
                                  && a2.Row == a0.Row
                                  && a1.Index != a0.Index
                                  && a2.Index != a0.Index
                            select new {a1, a2}).ToList();
                        foreach (var a3a4a5 in from pair2 in a1a2 let a1 = pair2.a1 let a2 = pair2.a2 select (from a3 in subcheckcells
                            join a4 in subcheckcells on a3.RestString equals a4.RestString
                            join a5 in subcheckcells on a4.RestString equals a5.RestString
                            where
                                a3.Column == a2.Column
                                && a3.RestString == pair.JoinString()
                                && a4.Column == a5.Column
                                && a4.Index != a5.Index
                                && a3.Index != a2.Index
                                && a4.Row == a1.Row
                                && a4.Index != a1.Index
                            select new {a3, a4, a5}).ToList())
                        {
                            cells.AddRange((from pair3 in a3a4a5 let a3 = pair3.a3 let a4 = pair3.a4 let a5 = pair3.a5 select (from a6 in subcheckcells join a7 in subcheckcells on a6.RestString equals a7.RestString join a8 in subcheckcells on a7.RestString equals a8.RestString join a9 in subcheckcells on a8.RestString equals a9.RestString where a6.Row == a5.Row && a5.Index != a6.Index && a6.Column == a9.Column && a6.Index != a9.Index && a9.Row == a8.Row && a8.Index != a9.Index && a8.Column == a7.Column && a8.Index != a7.Index && a7.Row == a3.Row && a7.Index != a3.Index && a7.RestString == pair.JoinString() select new {a6, a7, a8, a9}).ToList() into a6789 where a6789.Count == 1 select new PositiveCellInfo(a0.Index, a0.RestList.Except(pair).First())).Cast<CellInfo>());
                        }
                    }
                }
            }

            return cells;
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
