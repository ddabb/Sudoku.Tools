using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(7, "R6C4", "000040601001560204006100079607010900019600007020090816532971468978436100164258793")]
    public class IncompleteWXYZWingHandler : SolverHandlerBase
    {
        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();

        }

        public override SolveMethodEnum methodType => SolveMethodEnum.IncompleteWXYZWing;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCell;
            var checkCells = allUnSetCell.Where(c => c.GetRest().Count == 3).ToList();
            foreach (var checkCell in checkCells)
            {
                var checkCellRest = checkCell.GetRest();
                var relatedCell = checkCell.RelatedUnsetCells
                    .Where(c => c.GetRest().Count == 2 && c.GetRest().Intersect(checkCellRest).Count() == 1).ToList();
                var filter = (from a in relatedCell
                              join b in relatedCell on 1 equals 1
                              join c in relatedCell on 1 equals 1
                              let indexs = new List<int> { a.Index, b.Index, c.Index, checkCell.Index }
                              let arest = a.GetRest()
                              let brest = b.GetRest()
                              let crest = c.GetRest()
                              let removeValue = arest.Except(checkCellRest).First()
                              where arest.Except(checkCellRest).First() == brest.Except(checkCellRest).First()
                                    && arest.Except(checkCellRest).First() == crest.Except(checkCellRest).First()
                                    && a.Index < b.Index && b.Index < c.Index
                              select new { a, b, c, removeValue, indexs }).ToList();
                foreach (var item in filter)
                {
                    var subfilter = allUnSetCell.Where(c =>
                        !item.indexs.Contains(c.Index) && c.GetRest().Count == 2 &&
                        c.GetRest().Contains(item.removeValue)).ToList();
                    foreach (var findCell in subfilter)
                    {
                        if (GetIntersectCellIndexs(subfilter, item.a, item.b).Contains(findCell.Index) && GetIntersectCellIndexs(subfilter, item.a, item.c).Contains(findCell.Index))
                        {
                            cells.Add(new PositiveCellInfo(findCell.Index, findCell.GetRest().First(c => c != item.removeValue)));
                        }
                    }

                }

            }


            return cells;
        }
    }
}
