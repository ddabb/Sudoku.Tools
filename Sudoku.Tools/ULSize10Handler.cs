﻿using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("003400879008093002902068534009800007325047908087009000750284093294316785830975240")]
    public class ULSize10Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize10;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCell;
            int cellCount = 10;
            Dictionary<int, List<int>> dic = checkCells.ToDictionary(cell => cell.Index, cell => cell.GetRest());
            List<int> values = G.AllBaseValues.Where(value => dic.Values.Count(c => c.Contains(value)) > cellCount)
                .ToList();
            foreach (var x in values)
            {
                foreach (var y in values)
                {
                    if (x >= y) continue;
                    var pair = new List<int> {x, y};
                    var subcheckcells = checkCells.Where(c =>
                        c.GetRest().Intersect(pair).Count() == 2).ToList();
                    var speacilCells = subcheckcells.Where(c => c.GetRest().Count == 3).ToList();
                    foreach (var a0 in speacilCells)
                    {
                        var a1a2 = (from a1 in subcheckcells
                            join a2 in subcheckcells on a1.GetRestString() equals a2.GetRestString()
                            where a1.GetRestString() == pair.JoinString()
                                  && a1.Column == a0.Column
                                  && a2.Row == a1.Row
                                  && a1.Index != a0.Index
                                  && a2.Index != a0.Index
                            select new {a1, a2}).ToList();
                        foreach (var pair2 in a1a2)
                        {
                            var a1 = pair2.a1;
                            var a2 = pair2.a2;
                            var a3a4a5 = (from a3 in subcheckcells
                                join a4 in subcheckcells on a3.GetRestString() equals a4.GetRestString()
                                join a5 in subcheckcells on a4.GetRestString() equals a5.GetRestString()
                                where
                                    a3.Column == a2.Column
                                    && a3.GetRestString() == pair.JoinString()
                                    && a4.Column == a5.Column
                                    && a4.Index != a5.Index
                                    && a3.Index != a2.Index
                                    && a4.Row == a1.Row
                                    && a4.Index != a1.Index
                                select new {a3, a4, a5}).ToList();
                            foreach (var pair3 in a3a4a5)
                            {
                                var a3 = pair3.a3;
                                var a4 = pair3.a4;
                                var a5 = pair3.a5;
                                var a6789 = (from a6 in subcheckcells
                                        join a7 in subcheckcells on a6.GetRestString() equals a7.GetRestString()
                                        join a8 in subcheckcells on a7.GetRestString() equals a8.GetRestString()
                                        join a9 in subcheckcells on a8.GetRestString() equals a9.GetRestString()
                                             where a6.Row==a5.Row
                                                   &&a5.Index!=a6.Index
                                                   &&a6.Column==a9.Column
                                                   &&a6.Index!=a9.Index
                                                   &&a9.Row==a8.Row
                                                   &&a8.Index!=a9.Index
                                                   &&a8.Column==a7.Column
                                                   &&a8.Index!=a7.Index
                                                   &&a7.Row==a3.Row
                                                   &&a7.Index!=a3.Index
                                                   &&a7.GetRestString()==pair.JoinString()
                                        select new {a6, a7, a8, a9}
                                    ).ToList();
                                if (a6789.Count==0)
                                {
                                    cells.Add(new PositiveCellInfo(a0.Index, a0.GetRest().Except(pair).First())); 
                                }
                            }

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
