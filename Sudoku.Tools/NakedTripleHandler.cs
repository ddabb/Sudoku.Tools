using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [Example("390000700000000650507000349049380506601054983853000400900800134002940865400000297")]
    public class NakedTripleHandler :SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.GetFilterCell(c => c.Value == 0 && qSudoku.GetRest(c).Count == 3);
            foreach (var direction in allDirection)
            {
                foreach (var index in baseIndexs)
                {
                    var subcells = qSudoku.AllUnSetCell.Where(c => GetFilter(c, direction, index)).ToList();
                    if (subcells.Count > 3)
                    {
                        var temp = checkCells.Where(c => GetFilter(c, direction, index)).GroupBy(c => qSudoku.GetRestString(c)).Where(c => c.Count() == 3);
                        foreach (var sub in temp)
                        {
                            var removeCells = subcells.Where(c => qSudoku.GetRestString(c) != sub.Key);
                            var removeValues = ConvertToInts(sub.Key);
                            foreach (var cell in removeCells)
                            {
                                var rests = qSudoku.GetRest(cell);
                                if (rests.Count > 1 && rests.Intersect(removeValues).Count() > 0)
                                {
                                    foreach (var value in removeValues)
                                    {
                                        rests.Remove(value);
                                    }
                                    if (rests.Count == 1)
                                    {
                                        cells.Add(new CellInfo(cell.Index, rests[0]));
                                    }
                                }
                            }

                        }
                    }

                }

            }
            return cells;
        }
    }
}
