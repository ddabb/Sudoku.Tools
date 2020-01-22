using Sudoku.Core;
using System;
using System.Collections.Generic;

using System.Linq;
namespace Sudoku.Tools
{
    [AssignmentExample("534618729297534861600297400760009080009800076853761942976000018000976204000180697")] //已调整
    public class URType1Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.URType1;

        /// <summary>
        /// ab,ab,ab,abc 结构，且属于两个block。
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <returns></returns>
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allcheckCell = qSudoku.AllUnSetCells.Where(c => new List<int> { 2, 3 }.Contains(c.GetRest().Count)).ToList();

            var filter = (from a in allcheckCell
                          join b in allcheckCell on a.GetRestString() equals b.GetRestString()
                          join c in allcheckCell on a.GetRestString() equals c.GetRestString()
                          join d in allcheckCell on 1 equals 1
                          where a.Index < b.Index
                             && b.Index < c.Index
                             && d.GetRest().Count() == 3
                             && a.GetRest().Count() == 2

                          select new List<CellInfo> { a, b, c, d }).ToList();
            foreach (var item in filter)
            {
                if (item.Select(c => c.Row).Distinct().Count() == 2 && item.Select(c => c.Column).Distinct().Count() == 2 && item.Select(c => c.Block).Distinct().Count() == 2)
                {
                    var d = item.Last();
                    var a = item.First();
                    var arest = a.GetRest();
                    var drest = d.GetRest();
                    if (drest.Intersect(arest).Count() == 2)
                    {
                        var result = new PositiveCellInfo(d.Index, drest.First(c => !arest.Contains(c)));
                        result.CellType = CellType.Positive;
                        result.Sudoku = qSudoku;

                        cells.Add(result);
                    }
                }
            }






            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
