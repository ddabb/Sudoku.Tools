using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample("390002804120864039048739012273000008080327145451698327032080006814976253760203081")] //出数
                                                                                                             //060000725257000000409572806045007000726308504000050670592700000004925367673000259

    public class ULSize6Type2Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize6Type2;

        /// <summary>
        /// 满足 xyz,除去z 有xy 与该xyz 同行 或同列
        /// 满足 xyz,除去z 有xy 与该xyz 同行 或同列
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <returns></returns>
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCell;
            var restPair = qSudoku.AllUnSetCell.Where(c => c.GetRest().Count == 2);

            var filter = (from x in checkCells
                          join y in checkCells on x.GetRestString() equals y.GetRestString()
                          where x.GetRest().Count == 3
                          && x.Column != y.Column
                            && x.Row != y.Row
                          select new { x, y }
            ).ToList();
            foreach (var item in filter)
            {
                var cellX = item.x;
                var cellY = item.y;
                var testLinq = (from a in restPair
                                join b in restPair on a.GetRestString() equals b.GetRestString()
                                join c in restPair on a.GetRestString() equals c.GetRestString()
                                join d in restPair on a.GetRestString() equals d.GetRestString()
                                where a.Column == cellX.Column && b.Column == cellY.Column
                                      && a.Row == b.Row
                                      && c.Column == d.Column
                                      && c.Row == cellY.Row
                                      && d.Row == cellX.Row
                                select new { a,b,c,d}).ToList();
                if (testLinq.Count()!=0)
                {
                    var rest = testLinq.First().a.GetRest();
                    var value = cellX.GetRest().Except(rest).First();
                    var tempResult = GetIntersectCells(qSudoku.AllUnSetCell, cellX, cellY).Where(c => c.GetRest().Count == 2 && c.GetRest().Contains(value));
                    foreach (var cell in tempResult)
                    {
                        cells.Add(new PositiveCellInfo(cell.Index, cell.GetRest().First(c => c != value)));
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
