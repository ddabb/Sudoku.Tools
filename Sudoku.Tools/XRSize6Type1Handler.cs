using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(6,"R3C1","907024305842365917030907400004009600000246009009000040000492876796050234428673591")]
    public class XRSize6Type1Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize6Type1;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCells = qSudoku.AllUnSetCells;
            var ab = (from a in allUnsetCells
                join b in allUnsetCells on 1 equals 1
                let arest = a.RestList
                let brest = b.RestList
                let sameRow = a.Row == b.Row
                where a.Index != b.Index
                      && (a.Row == b.Row || a.Column == b.Column)
                      && arest.Except(brest).Count() == 1
                      && arest.Intersect(brest).Count() == brest.Count
                select new {a, b, arest, brest, sameRow}).ToList();

            foreach (var item in ab)
            {
                var a = item.a;
                var b = item.b;
                var arest = item.arest;
                var brest = item.brest;
                var sameRow = item.sameRow;

                var brelatedCell = (from c in allUnsetCells
                    join d in allUnsetCells on 1 equals 1
                    let crest = c.RestList
                    let drest = d.RestList
                    where c.Index < d.Index
                          && (sameRow
                              ? (c.Column == d.Column && c.Column == b.Column)
                              : c.Row == d.Row && c.Row == b.Row)
                          && G.DinstinctInt(brest, crest, drest).Count == 3
                    select new {c, d, crest, drest}).ToList();
                foreach (var item2 in brelatedCell)
                {
                    var c = item2.c;
                    var d = item2.d;
                    var crest = item2.crest;
                    var drest = item2.drest;
                    var list = (from e in allUnsetCells
                        join f in allUnsetCells on 1 equals 1
                        where e.RestString == crest.JoinString()
                              && f.RestString == drest.JoinString()
                              && (sameRow
                                  ? e.Column == a.Column && f.Column == a.Column && e.Row == c.Row && f.Row == d.Row
                                  : e.Row == a.Row && f.Row == a.Row && e.Column == c.Column && f.Column == d.Column)
                        select new {e, f}).ToList();
                    foreach (var pair in list)
                    {
                        cells.Add(new PositiveCell(a.Index, arest.Except(brest).First()));
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
