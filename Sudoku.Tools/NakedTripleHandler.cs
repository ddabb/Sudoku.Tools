using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("390000700000000650507000349049380506601054983853000400900800134002940865400000297")]
    public class NakedTripleHandler :SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCell;
            List<int> possibleCount=new List<int>{2,3};
            //只有2或3个候选数的单元格
            var twoOrThreeRests = allUnSetCell.Where(c=>possibleCount.Contains(qSudoku.GetRest(c).Count)).ToList();
            foreach (var direction in allDirection)
            {
                foreach (var index in baseIndexs)
                {
                    //待检查的单元格
                    var checkCells = twoOrThreeRests.Where(c => GetFilter(c, direction, index)).ToList() ;
                    if (checkCells.Count() <= 2) continue;
                    {
                        foreach (var cell1 in checkCells)
                        {
                            foreach (var cell2 in checkCells)
                            {
                                foreach (var cell3 in checkCells)
                                {

                                    cells.AddRange(GetCells(qSudoku, new List<CellInfo> {cell1, cell2, cell3},
                                        direction, index)); 
                                }
                            }
           
                        }
                    }
                }

            }
            return cells;
        }

        private List<CellInfo> GetCells(QSudoku qSudoku, List<CellInfo> checkCellInfos, Direction direction, int index
            )
        {
            var allUnSetCell = qSudoku.AllUnSetCell;
            List<CellInfo> cells =new List<CellInfo>();
            if (checkCellInfos.Select(c => c.Index).Distinct().Count() == 3) 
            {
                var exceptIndexs = checkCellInfos.Select(c => c.Index).ToList();
                var allRest = new List<int>();
                foreach (var checkCellInfo in checkCellInfos)
                {
                    allRest.AddRange(qSudoku.GetRest(checkCellInfo));
                }

                if (allRest.Distinct().Count() == 3 && allRest.Count >=8) 
                {
                    var subCheckCells = allUnSetCell
                        .Where(c => GetFilter(c, direction, index) && !exceptIndexs.Contains(c.Index))
                        .ToList();

                    foreach (var subCheckCell in subCheckCells)
                    {
                        var rests = qSudoku.GetRest(subCheckCell);
                        if (rests.Intersect(allRest).Count() <= 1) continue;
                        foreach (var value in allRest)
                        {
                            rests.Remove(value);
                        }

                        if (rests.Count == 1)
                        {
                            cells.Add(new CellInfo(subCheckCell.Index, rests[0]));
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
