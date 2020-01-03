using Sudoku.Core;
using System;
using System.Collections.Generic;
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
            foreach (var direction in allDirection)
            {
                foreach (var index in baseIndexs)
                {
                    //待检查的单元格
                    var checkCells = qSudoku.AllUnSetCell.Where(GetDirectionCells(direction, index)).ToList();

                    if (checkCells.Count>3)
                    {

                        var allRests = new List<int>();
                        foreach (var cell in cells)
                        {
                            allRests.AddRange(qSudoku.GetRest(cell));
                            var restValues = allRests.Distinct().ToList();
                            if (restValues.Count>3)
                            {
                                var values = (from a in restValues
                                    join b in restValues on 1 equals 1
                                    join c in restValues on 1 equals 1
                                    where new List<int> { a, b, c }.Select(c => c).Distinct().Count() == 3
                                    select new List<int> { a, b, c }).ToList();
                                foreach (var eachTriple in values)
                                {
                                    Dictionary<int,List<int>> a=new Dictionary<int, List<int>>();
                                    foreach (var value in eachTriple)
                                    {
                                        a.Add(value, qSudoku.GetPossibleIndex(value, checkCells)); 
                                    }

                                    var allindexs=new List<int>();
                                    foreach (var kv in a)
                                    {
                                        allindexs.AddRange(kv.Value);
                                    }

                                    var exceptIndexs = allindexs.Distinct();
                                    if (exceptIndexs.Count()==3)
                                    {
                                        
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
