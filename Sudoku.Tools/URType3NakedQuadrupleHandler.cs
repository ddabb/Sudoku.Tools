using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{

    [AssignmentExample(8, "R5C8", "425000800638725941719684235003002694000430500540000103267148359354269718001000400")]
    public class URType3NakedQuadrupleHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.URType3NakedQuadruple;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCells = qSudoku.AllUnSetCells;

            var pairCells = (from a in allUnsetCells
                             join b in allUnsetCells on a.RestString equals b.RestString
                             let sameRow = a.Row == b.Row
                             let indexs = G.MergeCellIndexs(a, b)
                             where a.Index < b.Index
                             && a.RestCount == 2 && (a.Row == b.Row || a.Column == b.Column)
                             select new { a, b, sameRow, indexs }).ToList();
            foreach (var item in pairCells)
            {
                var a = item.a;
                var b = item.b;
                var rest = a.RestList;
                var indexs = item.indexs;
                var sameRow = item.sameRow;
                var filterCell = allUnsetCells.Where(c => !indexs.Contains(c.Index)).ToList();

                var filter2 = (from c in filterCell
                               join d in filterCell on 1 equals 1

                               let interc = c.RestList.Intersect(rest).ToList()
                               let interd = d.RestList.Intersect(rest).ToList()
                               let exceptC = c.RestList.Except(rest).ToList()
                               let exceptD = d.RestList.Except(rest).ToList()
                               let indexCD = G.MergeCellIndexs(c, d)
                               where c.Index < d.Index

                               && (sameRow ?
                                 c.Column == a.Column && d.Column == b.Column && c.Row == d.Row
                               : c.Row == a.Row && d.Row == b.Row && c.Column == d.Column)

                                && interc.Count == 2
                               && interd.Count == 2
                               select new { c, d, exceptC, exceptD, indexCD }).ToList();
                foreach (var keyCells in filter2)
                {
                    var c = keyCells.c;
                    var d = keyCells.d;
                    var indexCD = keyCells.indexCD;
                    var exceptC = keyCells.exceptC;
                    var exceptD = keyCells.exceptD;
                    var filterCells = allUnsetCells.Where(cell => sameRow ? cell.Row == c.Row : cell.Column == c.Column && !indexCD.Contains(cell.Index)).ToList();
                    var efg = (from e in filterCells
                               join f in filterCells on 1 equals 1
                               join g in filterCells on 1 equals 1
                               let exceptIndexs = G.MergeCellIndexs(c, d, e, f, g)
                               let otherValues = G.MergeInt(exceptC, exceptD, e.RestList, f.RestList, g.RestList).Distinct().ToList()
                               where e.Index < f.Index && f.Index < g.Index
                               && otherValues.Count == 4
                               select new { exceptIndexs, otherValues ,e, f, g }).ToList();
                    foreach (var removeCells in efg)
                    {
                        var exceptIndexs = removeCells.exceptIndexs;
                        var otherValues = removeCells.otherValues;
                        var cellss = filterCells.Where(c => !exceptIndexs.Contains(c.Index)).ToList();
                        foreach (var removeCell in cellss)
                        {
                            var interRest = removeCell.RestList.Intersect(otherValues).ToList();
                            if (interRest.Any())
                            {
                                if (interRest.Count == 1)
                                {
                                    var nagetiveCell = new NegativeCell(removeCell.Index, interRest.First(), qSudoku);
                                    cells.Add(nagetiveCell);
                                }
                                else
                                {
                                    var nagetiveCell = new NegativeValuesGroup(removeCell.Index, interRest, qSudoku);
                                    cells.Add(nagetiveCell);
                                }

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
