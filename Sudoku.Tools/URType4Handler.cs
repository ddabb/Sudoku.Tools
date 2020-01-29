using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(6,"R9C2", "396002451451090782782451090645230008030000045100045030003500064500904020004020500")]
    //400061070057004001160702084926007138745318000010629457580103726001076845670085010
    //318652007497381005600070138849010000531026009700890051900108500100200006203067014
    public class URType4Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.URType4;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var unCheckSell = qSudoku.AllUnSetCells;
            var pairs = (from a in unCheckSell
                         join b in unCheckSell on a.RestList.JoinString() equals b.RestString
                         where (a.Row == b.Row || a.Column == b.Column) && a.Index < b.Index
                                                                        && a.RestCount == 2
                         select new { a, b }).ToList();
            foreach (var cell in pairs)
            {
                var a = cell.a;
                var b = cell.b;
                var restString = a.RestString;
                var restInt = a.RestList;

                if (a.Row == b.Row)
                {

                    var pairs1 = (from c in unCheckSell
                                  join d in unCheckSell on 1 equals 1
                                  where c.Row != a.Row && c.Row == d.Row
                                                       && c.RestList.Intersect(restInt).Count() == 2
                                                       && d.RestList.Intersect(restInt).Count() == 2
                                                       && c.Column == a.Column
                                                       && d.Column == b.Column
                                                       && c.RestCount > 2
                                                       && d.RestCount > 2
                                  select new { c, d }).ToList();
                    foreach (var cell2 in pairs1)
                    {
                                                    cells.AddRange(
                            //a b 中 restvalue的x y在 c d 列只存在于对应的两行
                            from value in restInt
                            where (qSudoku.GetPossibleIndex(value, c => c.Value == 0 && c.Column == cell2.c.Column).Count == 2 &&
                                         qSudoku.GetPossibleIndex(value, c => c.Value == 0 && c.Column == cell2.d.Column).Count == 2) || qSudoku.GetPossibleIndex(value, c => c.Value == 0 && c.Row == cell2.c.Row).Count == 2
                            let cellValue = restInt.First(c => c != value)
                            let indexs = qSudoku.GetPossibleIndex(cellValue,
                            c => c.Value == 0 && c.Row == cell2.c.Row && c.Index != cell2.c.Index && c.Index != cell2.d.Index)
                            where indexs.Count == 1
                            select new PositiveCell(indexs.First(), cellValue));
                    }
                }

                if (a.Column == b.Column)
                {
                    var pairs1 = (from c in unCheckSell
                                  join d in unCheckSell on 1 equals 1
                                  where c.Column != a.Column && c.Column == d.Column
                                                             && c.RestList.Intersect(restInt).Count() == 2
                                                             && d.RestList.Intersect(restInt).Count() == 2
                                                             && c.Row == a.Row
                                                             && d.Row == b.Row
                                                             && c.RestCount > 2
                                                             && d.RestCount > 2
                                  select new { c, d }).ToList();
                    foreach (var cell2 in pairs1)
                    {

                        cells.AddRange(
                                //a b 中 restvalue的x y在 c d 列只存在于对应的两行
                                from value in restInt
                                where (qSudoku.GetPossibleIndex(value, c => c.Value == 0 && c.Row == cell2.c.Row).Count == 2 &&
                                             qSudoku.GetPossibleIndex(value, c => c.Value == 0 && c.Row == cell2.d.Row).Count == 2) || qSudoku.GetPossibleIndex(value, c => c.Value == 0 && c.Column == cell2.c.Column).Count == 2
                                let cellValue = restInt.First(c => c != value)
                                let indexs = qSudoku.GetPossibleIndex(cellValue,
                                c => c.Value == 0 && c.Column == cell2.c.Column && c.Index != cell2.c.Index && c.Index != cell2.d.Index)
                                where indexs.Count == 1
                                select new PositiveCell(indexs.First(), cellValue));
                    }
                }

            }
            return cells;
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
