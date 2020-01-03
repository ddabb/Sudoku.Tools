using System;
using Sudoku.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sudoku.Tools
{

    [AssignmentExample("000436517000280000006170000000061070001000000050804100000043761003610000000000394")]
    public class DirectPointingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allunsetcell = qSudoku.AllUnSetCell;
            var blocks = allunsetcell.Select(c => c.Block).Distinct().ToList();

            foreach (var blockindex in blocks)
            {
                foreach (var value in QSudoku.AllBaseValues)
                {
                    foreach (var direction in allDirection.Where(c=>c!= Direction.Block))
                    {
                        var blockUnSetCell = allunsetcell.Where(c => c.Block == blockindex).ToList();
                        var directionIndex = blockUnSetCell.Where(c => qSudoku.GetRest(c).Contains(value))
                            .Select(Selector(direction)).Distinct().ToList();

                        if (directionIndex.Count == 1 && blockUnSetCell.Count(c => qSudoku.GetRest(c).Contains(value)) != 1)
                        {
                            var index = directionIndex.First();
                            var checks = allunsetcell.Where(c =>GetFilter(c, direction, index) && c.Block != blockindex).ToList();
                            foreach (var cell in checks)
                            {
                                var cellrest = qSudoku.GetRest(cell);
                                if (cellrest.Contains(value) && cellrest.Count(c => c != value) == 1)
                                {

                                    cells.Add(new CellInfo(cell.Index, cellrest.First(c => c != value)));
                                }
                            }

                            var otherBlocks = checks.Select(c => c.Block).Distinct().ToList();
                            foreach (var block in otherBlocks)
                            {
                                var otherCell = allunsetcell.Where(c => c.Block == block && !GetFilter(c, direction, index));
                                var containsCell = otherCell.Where(c => qSudoku.GetRest(c).Contains(value)).ToList();
                                if (containsCell.Count() == 1)
                                {

                                    cells.Add(new CellInfo(containsCell.First().Index, value));
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
