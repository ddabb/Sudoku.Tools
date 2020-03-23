using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [EliminationExampleAttribute(4, "R1C4", "000010900001209870029306451697023100482061000135000260000002610056134700010697000")]
    public class AlignedTripleExclusionHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.AlignedTripleExclusion;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();

            var allUnsetCell = qSudoku.AllUnSetCells;
            var triples = allUnsetCell.Where(c => c.RestCount == 3).ToList();
            foreach (var item in triples)
            {

                var itemRest = item.RestList;
                var filterCells = item.RelatedUnsetCells.Where(c => c.RestCount == 2).ToList();
                var ab = (from a in filterCells
                          join b in filterCells on 1 equals 1
                          let aexcept = a.RestList.Except(itemRest).ToList()
                          let bexcept = b.RestList.Except(itemRest).ToList()
                          where a.Index < b.Index
                                && a.RestString != b.RestString
                                && aexcept.Count == 1
                                && bexcept.Count == 1
                                && aexcept.First() == bexcept.First()
                                && a.RestList.Intersect(itemRest).Count() == 1
                                && b.RestList.Intersect(itemRest).Count() == 1
                          select new { a, b, aexcept, bexcept }).ToList();
                foreach (var item1 in ab)
                {
                    var a = item1.a;
                    var b = item1.b;
                    var aexcept = item1.aexcept;
                    var bexcept = item1.bexcept;
                    var removalue = itemRest.ToList().Except(a.RestList).Except(b.RestList).First();
                    var comonValue = aexcept.First();
                    var conditionCells = qSudoku.GetPublicUnsetAreas(a, b).Where(c =>
                          c.RestCount == 2 && c.RestList.Contains(removalue) && c.RestList.Contains(comonValue)).ToList();
                    foreach (var cell in conditionCells)
                    {
                        foreach (var checkCell in qSudoku.GetPublicUnsetAreas(item, cell))
                        {
                            if (checkCell.RestList.Contains(removalue))
                            {
                                cells.Add(new NegativeCell(checkCell.Index, removalue, qSudoku));
                            }
                        }

                    }



                }



            }
            return cells;
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
