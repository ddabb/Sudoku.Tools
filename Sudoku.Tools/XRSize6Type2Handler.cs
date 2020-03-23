using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{

    [AssignmentExample(9, "R5C9", "000806172817923546062070983006000400100680730003000600054090867708460301601708204"
        , SolveMethodEnum.HiddenPair
        , SolveMethodEnum.HiddenPair
        )]
    public class XRSize6Type2Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize6Type2;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCells = qSudoku.AllUnSetCells;
            var pairCells = allUnsetCells.Where(c => c.RestCount == 2).ToList();
            var ab = (from a in pairCells
                      join b in pairCells on 1 equals 1
                      let arest = a.RestList
                      let brest = b.RestList
                      where a.Index < b.Index
                            && a.Block != b.Block
                            && arest.Intersect(brest).Count() == 1
                      select new { a, b, arest, brest }).ToList();
            foreach (var item in ab)
            {
                var a = item.a;
                var b = item.b;
                var arest = item.arest;
                var brest = item.brest;
                var interRest = arest.Intersect(brest).ToList();
                var other = arest.Except(interRest).Union(brest.Except(interRest)).ToList();
                var publicCells = qSudoku.GetPublicUnsetAreas(a, b);
                var cd = (from c in publicCells
                          from d in publicCells
                          let inRow = a.Column == c.Column
                          where c.RestString == arest.JoinString()
                                && d.RestString == brest.JoinString()
                          select new { inRow, c, d }).ToList();
                foreach (var item1 in cd)
                {
                    var inRow = item1.inRow;
                    var filte = allUnsetCells.Where(c =>
                        c.RestList.Count == 3 && c.RestList.Intersect(other).Count() == other.Count()).ToList();
                    if (inRow)
                    {
                        var ef = (from e in filte
                                  join f in filte on e.Column equals f.Column
                                  where e.Row == a.Row
                                        && f.Row == b.Row
                                        && e.RestString==f.RestString
                                  select new { e, f }).ToList();
                        foreach (var item2 in ef)
                        {
                            var e = item2.e;
                            var f = item2.f;
                            var removeValue = e.RestList.Except(other).First();
                            var list = allUnsetCells.Where(
                                c => c.Column == e.Column && c.Row != e.Row && c.Row != f.Row).ToList();
                            foreach (var item3 in list)
                            {
                                if (item3.RestList.Contains(removeValue))
                                {
                                    var cell = new NegativeCell(item3.Index, removeValue, qSudoku) ;
                                    cells.Add(cell);
                                }
                                
                            }
                        }

                    }
                    else
                    {
                        var ef = (from e in filte
                                  join f in filte on e.Row equals f.Row
                                  where e.Column == a.Column
                                  && f.Column == b.Column
                                  && e.RestString == f.RestString
                                  select new { e, f }).ToList();
                        foreach (var item2 in ef)
                        {
                            var e = item2.e;
                            var f = item2.f;
                            var removeValue = e.RestList.Except(other).First();
                            var list = allUnsetCells.Where(
                                c => c.Row == e.Row && c.Column != e.Column && c.Column != f.Column);
                            foreach (var item3 in list)
                            {
                                if (item3.RestList.Contains(removeValue))
                                {
                                    var cell = new NegativeCell(item3.Index, removeValue, qSudoku);
                                    cells.Add(cell);
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
