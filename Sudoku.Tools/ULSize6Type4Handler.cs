using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(7, "R3C8", "640010300890300000230480500579132000362948715418567932156004893983651247724893156")]
    public class ULSize6Type4Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize6Type4;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;
            var pairCells = allUnSetCell.Where(c => c.RestCount == 2).ToList();
            var ab = (from a in pairCells
                      join b in pairCells on a.RestString equals b.RestString
                      let indexs = new List<int> { a.Index, b.Index }
                      where a.Block == b.Block && a.Index < b.Index
                      select new { a, b, a.Block, a.RestList }).ToList();
            foreach (var item in ab)
            {
                var block = item.Block;
                var restList = item.RestList;
                var a = item.a;
                var b = item.b;
                var filter = allUnSetCell.Where(c => c.Block != block && c.RestList.Intersect(restList).Count() == 2).ToList();
                var cd = (from c in filter
                          join d in filter on c.RestString equals d.RestString
                          let sameRow = c.Row == d.Row
                          where c.RestCount == 2
                                && c.Block == d.Block
                                && (c.Row == d.Row && c.Column == a.Column && d.Column == b.Column || (c.Column == d.Column && c.Row == a.Row && d.Row == b.Row))
                          select new { c, d, sameRow }).ToList();
                foreach (var item2 in cd)
                {
                    var sameRow = item2.sameRow;
                    var ef = (from e in filter
                              join f in filter on 1 equals 1
                              where sameRow
                                  ? e.Column == f.Column && e.Row == a.Row && f.Row == b.Row
                                  : e.Row == f.Row && e.Column == a.Column && f.Column == b.Column
                              select new { e, f }).ToList();

                    foreach (var item3 in ef)
                    {
                        var e = item3.e;
                        var f = item3.f;
                        foreach (var value in restList)
                        {
                            Func<CellInfo, bool> whereCondition = c => c.Value == 0 && (sameRow ? c.Column == e.Column : c.Row == e.Row) && c.Index != e.Index && c.Index != f.Index;
                            if (qSudoku.GetPossibleIndex(value, whereCondition).Count == 0)
                            {
                                var removeValue = restList.First(c => c != value);//removalue=7
                                var temp = qSudoku.GetFilterCell(whereCondition).Where(c =>
                                    c.RestList.Count == 2 && c.RestList.Contains(removeValue)).ToList();
                                if (temp.Count == 1)
                                {
                                    cells.Add(new PositiveCell(temp.First().Index, removeValue, qSudoku));
                                }
                            }

                        }
                    }

                }
            }
            return cells;
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
