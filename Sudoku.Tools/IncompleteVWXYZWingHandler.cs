using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [EliminationExample(6, "R8C4", "080051700010027540570930002425700000708002056601500027060005000150000200847200095")]

    public class IncompleteVWXYZWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;
            var checkCells = allUnSetCell.Where(c => c.RestCount == 4).ToList();
            foreach (var checkCell in checkCells)
            {
                var checkCellRest = checkCell.RestList;
                var relatedCell = checkCell.RelatedUnsetCells
                    .Where(c => c.RestCount == 2 && c.RestList.Intersect(checkCellRest).Count() == 1).ToList();
                var filter = (from a in relatedCell
                              join b in relatedCell on 1 equals 1
                              join c in relatedCell on 1 equals 1
                              join d         in relatedCell on 1 equals 1
                              let indexs = new List<int> { a.Index, b.Index, c.Index,d.Index, checkCell.Index }
                              let arest = a.RestList
                              let brest = b.RestList
                              let crest = c.RestList
                              let dcrest = d.RestList
                              let removeValue = arest.Except(checkCellRest).First()
                              where arest.Except(checkCellRest).First() == brest.Except(checkCellRest).First()
                                    && arest.Except(checkCellRest).First() == crest.Except(checkCellRest).First()
                                    && arest.Except(checkCellRest).First() == dcrest.Except(checkCellRest).First()
                                    && a.Index < b.Index && b.Index < c.Index&&c.Index<d.Index
                              select new { a, b, c,d, removeValue, indexs }).ToList();
                foreach (var item in filter)
                {

                    var publicIndexs = item.a.RelatedUnsetIndexs
                        .Intersect(item.b.RelatedUnsetIndexs)
                        .Intersect(item.c.RelatedUnsetIndexs)
                        .Intersect(item.d.RelatedUnsetIndexs).ToList();
                    var intersectValue = item.removeValue;
             
                    foreach (var index in publicIndexs)
                    {
                        if (qSudoku.GetRest(index).Contains(intersectValue))
                        {
                            var cell = new NegativeCell(index, intersectValue) {Sudoku = qSudoku};
                            cells.Add(cell);
                        }
          
                    }
                }

            }


            
            return cells;
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.IncompleteVWXYZWing;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
    }
}
