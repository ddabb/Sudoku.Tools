using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(4, "R2C7", "417329586056010000038050000581290640629540801374100295193405700745032000862971354")]
    public class ULSize6Type3Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize6Type3;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCell;
            var pairCells = allUnSetCell.Where(c => c.GetRest().Count == 2).ToList();
            var otherCells = allUnSetCell.Where(c => c.GetRest().Count > 2).ToList();

            var a0a1 = (from a0 in pairCells
                        join a1 in pairCells on a0.Block equals a1.Block
                        let restString = a0.GetRestString()
                        let exceptIndexs = new List<int> { a0.Index, a1.Index }
                        where a0.Index < a1.Index
                              && a0.GetRestString() == a1.GetRestString()
                        select new { a0, a1, restString, exceptIndexs }).ToList();
            foreach (var item in a0a1)
            {

                var restString = item.restString;

                var exceptIndexs = item.exceptIndexs;
                var leftCells = allUnSetCell
                    .Where(c => !exceptIndexs.Contains(c.Index) && c.GetRestString() == restString).ToList();
                var a0 = item.a0;
                var a1 = item.a1;
                var restList = a0.GetRest();
                var item1s = (from a2 in leftCells
                              join a3 in leftCells on a2.Block equals a3.Block
                              let sameRow = a2.Row == a0.Row && a1.Row == a3.Row
                              where (a2.Row == a0.Row && a1.Row == a3.Row)
                                    || (a2.Column == a0.Column && a1.Column == a3.Column)
                              select new { a2, a3, sameRow }
                    ).ToList();
                foreach (var items in item1s)
                {
                    var sameRow = items.sameRow;
                    var a2 = items.a2;
                    var a3 = items.a3;
                    if (!sameRow)
                    {
                        var a4a5 = (from a4 in otherCells
                                    join a5 in otherCells on a4.Column equals a5.Column
                                    where a4.Row == a0.Row
                                          && a5.Row == a1.Row
                                          && a4.GetRest().Intersect(restList).Count() == 2
                                          && a5.GetRest().Intersect(restList).Count() == 2
                                    select new { a4, a5 }).ToList();
                        foreach (var item3 in a4a5)
                        {
                            var a4 = item3.a4;
                            var a5 = item3.a5;
                            cells.AddRange(NewMethod(qSudoku, a4, restList, a5, allUnSetCell));
                        }
                    }
                    else
                    {
                        var a4a5 = (from a4 in otherCells
                                    join a5 in otherCells on a4.Row equals a5.Row
                                    where a4.Column == a0.Column
                                          && a5.Column == a1.Column
                                          && a4.GetRest().Intersect(restList).Count() == 2
                                          && a5.GetRest().Intersect(restList).Count() == 2
                                    select new { a4, a5 }).ToList();
                        foreach (var item3 in a4a5)
                        {
                            var a4 = item3.a4;
                            var a5 = item3.a5;
                            cells.AddRange(NewMethod(qSudoku, a4, restList, a5, allUnSetCell));
                        }

                    }
                }





            }

            return cells;
        }

        private List<CellInfo> NewMethod(QSudoku qSudoku, CellInfo a4, List<int> restList, CellInfo a5,
            List<CellInfo> allUnSetCell)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var restCount = new List<int> { 1, 2 };
            var sumRest = new List<int>();
            var rest1 = a4.GetRest().Except(restList).ToList();
            var rest2 = a5.GetRest().Except(restList).ToList();
            sumRest.AddRange(rest1);
            sumRest.AddRange(rest2);
            sumRest = sumRest.Distinct().ToList();
            sumRest.Sort();
            if (restCount.Contains(rest1.Count)
                && restCount.Contains(rest2.Count)
                && restCount.Contains(sumRest.Count))
            {
                var a4RelatedIndex = a4.RelatedUnsetIndexs;
                var a5RelatedIndex = a5.RelatedUnsetIndexs;
                var intersecta4a5Cells = a4RelatedIndex.Intersect(a5RelatedIndex).ToList();
                var foundResult = allUnSetCell.Where(c =>
                        intersecta4a5Cells.Contains(c.Index) &&
                        c.GetRestString() == sumRest.JoinString())
                    .ToList();
                if (foundResult.Count != 0)
                {
                    foreach (var cell in foundResult)
                    {
                        var effectIndex = cell.RelatedUnsetIndexs.Intersect(intersecta4a5Cells)
                            .ToList();
                        foreach (var index in effectIndex)
                        {
                            var indexRest = qSudoku.GetRest(index);
                            if (indexRest.Count > 1 && indexRest.Except(sumRest).Count() == 1)
                            {
                                cells.Add(new PositiveCellInfo(index, indexRest.Except(sumRest).First()));
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
