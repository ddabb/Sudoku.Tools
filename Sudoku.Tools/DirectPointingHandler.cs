using System;
using Sudoku.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sudoku.Tools
{

    [AssignmentExample(9,"R7C4","000436517000280000006170000000061070001000000050804100000043761003610000000000394")]
    public class DirectPointingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.DirectPointing;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allunsetcell = qSudoku.AllUnSetCells;
            var blocks = allunsetcell.Select(c => c.Block).Distinct().ToList();

            foreach (var blockindex in blocks)
            {
                foreach (var value in G.AllBaseValues)
                {
                    foreach (var direction in G.AllDirection.Where(c=>c!= Direction.Block))
                    {
                        var blockUnSetCell = allunsetcell.Where(c => c.Block == blockindex).ToList();
                        var directionIndex = blockUnSetCell.Where(c => c.GetRest().Contains(value))
                            .Select(FindDirectionCondtion(direction)).Distinct().ToList();

                        if (directionIndex.Count == 1 && blockUnSetCell.Count(c => c.GetRest().Contains(value)) != 1)
                        {
                            var index = directionIndex.First();
                            var checks = allunsetcell.Where(c => G.GetFilter(c, direction, index) && c.Block != blockindex).ToList();
                            foreach (var cell in checks)
                            {
                                var cellrest = cell.GetRest();
                                if (cellrest.Contains(value) && cellrest.Count(c => c != value) == 1)
                                {

                                    cells.Add(new PositiveCellInfo(cell.Index, cellrest.First(c => c != value)));
                                }
                            }

                            var otherBlocks = checks.Select(c => c.Block).Distinct().ToList();
                            foreach (var block in otherBlocks)
                            {
                                var otherCell = allunsetcell.Where(c => c.Block == block && !G.GetFilter(c, direction, index));
                                var containsCell = otherCell.Where(c => c.GetRest().Contains(value)).ToList();
                                if (containsCell.Count() == 1)
                                {

                                    cells.Add(new PositiveCellInfo(containsCell.First().Index, value));
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
            List<NegativeCellInfo> cells = new List<NegativeCellInfo>();
            return cells;
        }


    }
}
