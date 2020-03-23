using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [EliminationExample(2,"R4C2","000400006200000970000005038006040003000601002800050760592104087074500009100002045",SolveMethodEnum.NakedPair)]
    [AssignmentExample(2, "R6C2", "000400006200000970000005038006040003000601002800050760592104087074500009100002045", SolveMethodEnum.NakedPair)]
    public class XRSize6Type3Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize6Type3;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCell = qSudoku.AllUnSetCells;
            var pairCell = qSudoku.AllUnSetCells.Where(c=>c.RestCount==2).ToList();
            var tripleCell = qSudoku.AllUnSetCells.Where(c => c.RestCount == 3).ToList();
            var ab = (from a in pairCell
                join b in pairCell on 1 equals 1
                let sameRow=a.Row==b.Row
                where a.Index < b.Index && (a.Row == b.Row || a.Column == b.Column)
                select new {a, b, sameRow }).ToList();
            foreach (var item in ab)
            {
                var sameRow = item.sameRow;
                var a = item.a;
                var b = item.b;
                if (sameRow)
                {
                    var keyCells = (from c in tripleCell
                        join d in tripleCell on c.RestString equals d.RestString
                        join e in tripleCell on d.RestString equals e.RestString
                        where c.Row == d.Row
                              && c.RestList.Intersect(a.RestList).Count() == a.RestCount
                              &&c.Column==a.Column
                              &&d.Column==b.Column
                              &&e.Row!=c.Row
                              &&e.Row!=a.Row
                        select new {c, d, e}).ToList();
                    foreach (var keyCellList in keyCells)
                    {
                        var e = keyCellList.e;
                        var fgs = (from f in tripleCell
                            join g in tripleCell on f.Row equals g.Row
                            where f.Column == a.Column && b.Column == g.Column
                                                       && f.Row == e.Row && g.Row == e.Row
                                                       && f.RestList.Intersect(e.RestList).Count() == 2
                                                       && g.RestList.Intersect(e.RestList).Count() == 2
                                                       && f.RestList.Intersect(e.RestList).JoinString() == g.RestList.Intersect(e.RestList).JoinString()
                                   select new {f, g}).ToList();
                        foreach (var fg in fgs)
                        {
                            var f = fg.f;
                            var removeValue = e.RestList.Except(e.RestList.Intersect(f.RestList)).First();
                            cells.Add(new NegativeCell(e.Index,removeValue, qSudoku)) ;
                        }
                    }
                }
                else
                {
                    var keyCells = (from c in tripleCell
                        join d in tripleCell on c.RestString equals d.RestString
                        join e in tripleCell on d.RestString equals e.RestString
                        where c.Column == d.Column
                              && c.RestList.Intersect(a.RestList).Count() == a.RestCount
                              && c.Row == a.Row
                              && d.Row == b.Row
                              && e.Column != c.Column
                              && e.Column != a.Column
                                    select new { c, d, e }).ToList();
                    foreach (var keyCellList in keyCells)
                    {
                        var e = keyCellList.e;
                        var fgs = (from f in tripleCell
                            join g in tripleCell on f.Column equals g.Column
                                   where f.Row == a.Row && b.Row == g.Row
                                                        && f.Column == e.Column && g.Column == e.Column
                                                        && f.RestList.Intersect(e.RestList).Count() == 2
                                                        && g.RestList.Intersect(e.RestList).Count() == 2
                                                        && f.RestList.Intersect(e.RestList).JoinString() == g.RestList.Intersect(e.RestList).JoinString()
                                   select new { f, g }).ToList();
                        foreach (var fg in fgs)
                        {
                            var f = fg.f;
                            var removeValue = e.RestList.Except(e.RestList.Intersect(f.RestList)).First();
                            cells.Add(new NegativeCell(e.Index, removeValue, qSudoku));
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
