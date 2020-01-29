using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(7, "R6C4", "000040601001560204006100079607010900019600007020090816532971468978436100164258793")]
    public class IncompleteWXYZWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();

        }

        public override SolveMethodEnum methodType => SolveMethodEnum.IncompleteWXYZWing;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;
            var checkCells = allUnSetCell.Where(c => c.RestCount == 3).ToList();
            foreach (var checkCell in checkCells)
            {
                var checkCellRest = checkCell.RestList;
                var relatedCell = checkCell.RelatedUnsetCells
                    .Where(c => c.RestCount == 2 && c.RestList.Intersect(checkCellRest).Count() == 1).ToList();
                var filter = (from a in relatedCell
                    join b in relatedCell on 1 equals 1
                    join c in relatedCell on 1 equals 1
                    let indexs = new List<int> {a.Index, b.Index, c.Index, checkCell.Index}
                    let arest = a.RestList
                    let brest = b.RestList
                    let crest = c.RestList
                    let removeValue = arest.Except(checkCellRest).First()
                    where arest.Except(checkCellRest).First() == brest.Except(checkCellRest).First()
                          && arest.Except(checkCellRest).First() == crest.Except(checkCellRest).First()
                          && a.Index < b.Index && b.Index < c.Index
                    select new {a, b, c, removeValue, indexs}).ToList();
                foreach (var item in filter)
                {

                    var publicIndexs = item.a.RelatedUnsetIndexs
                        .Intersect(item.b.RelatedUnsetIndexs)
                        .Intersect(item.c.RelatedUnsetIndexs).ToList();
                    var intersectValue = item.removeValue;
                    foreach (var index in publicIndexs)
                    {
                        var findCellRest = qSudoku.GetRest(index);
                        if (findCellRest.Contains(intersectValue) && findCellRest.Count == 2)
                        {
                            cells.Add(new PositiveCell(index, findCellRest.First(c => c != intersectValue)));
                        }
                    }
                }

            }


            return cells;
        }
    }
}
