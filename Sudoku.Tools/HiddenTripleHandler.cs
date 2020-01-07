using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("015070000900040105834651000050097018108065000000180526403518000060030851581026043")]
    public class HiddenTripleHandler :SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var direction in G.AllDirection)
            {
                foreach (var index in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkCells = qSudoku.AllUnSetCell.Where(G.GetDirectionCells(direction, index)).ToList();

                    if (checkCells.Count>3)
                    {

                        var allRests = new List<int>();
                        foreach (var cell in checkCells)
                        {
                            allRests.AddRange(qSudoku.GetRest(cell));
                  
                        }
                        var restValues = allRests.Distinct().ToList();
                        if (restValues.Count > 3)
                        {
                            var values = (from a in restValues
                                          join b in restValues on 1 equals 1
                                          join c in restValues on 1 equals 1
                                          where new List<int> { a, b, c }.Select(c => c).Distinct().Count() == 3 &&a<b&&b<c
                                          select new List<int> { a, b, c }).ToList();
                            foreach (var eachTriple in values)
                            {
                                Dictionary<int, List<int>> a = new Dictionary<int, List<int>>();
                                foreach (var value in eachTriple)
                                {
                                    a.Add(value, qSudoku.GetPossibleIndex(value, checkCells));
                                }

                                var allindexs = new List<int>();
                                foreach (var kv in a)
                                {
                                    allindexs.AddRange(kv.Value);
                                }

                                var exceptIndexs = allindexs.Distinct().ToList();
                                if (exceptIndexs.Count() == 3)
                                {
                                    foreach (var item in restValues.Where(c => !eachTriple.Contains(c)))
                                    {
                                        var leftIndexs = qSudoku.GetPossibleIndex(item, checkCells).Where(c => !exceptIndexs.Contains(c)).ToList();
                                        if (leftIndexs.Count == 1)
                                        {

                                            var cellinfo = new PositiveCellInfo(leftIndexs.First(), item);
                                            cells.Add(cellinfo);
                                        }

                                    }


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
