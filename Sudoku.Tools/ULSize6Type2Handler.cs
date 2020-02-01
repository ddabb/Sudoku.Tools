using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(1,"R3C2", "060000725257000000409572806045007000726308504000050670592700000004925367673000259")] //出数
                                                                                                             //060000725257000000409572806045007000726308504000050670592700000004925367673000259

    public class ULSize6Type2Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize6Type2;

        public override MethodClassify methodClassify => throw new NotImplementedException();

        /// <summary>
        /// 满足 xyz,除去z 有xy 与该xyz 同行 或同列
        /// 满足 xyz,除去z 有xy 与该xyz 同行 或同列
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <returns></returns>
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCells;
            var restPair = qSudoku.AllUnSetCells.Where(c => c.RestCount == 2);

            var filter = (from x in checkCells
                          join y in checkCells on x.RestString equals y.RestString
                          where x.RestCount == 3
                          && x.Column != y.Column
                            && x.Row != y.Row
                          select new { x, y }
            ).ToList();
            foreach (var item in filter)
            {
                var cellX = item.x;
                var cellY = item.y;
                var testLinq = (from a in restPair
                                join b in restPair on a.RestString equals b.RestString
                                join c in restPair on a.RestString equals c.RestString
                                join d in restPair on a.RestString equals d.RestString
                                where a.Column == cellX.Column && b.Column == cellY.Column
                                      && a.Row == b.Row
                                      && c.Column == d.Column
                                      && c.Row == cellY.Row
                                      && d.Row == cellX.Row
                                select new { a,b,c,d}).ToList();
                if (testLinq.Count()!=0)
                {
                    var rest = testLinq.First().a.RestList;
                    var value = cellX.RestList.Except(rest).First();
                    var tempResult = qSudoku.GetPublicUnsetAreas(cellX, cellY).Where(c => c.RestCount == 2 && c.RestList.Contains(value));
                    foreach (var cell in tempResult)
                    {
                        cells.Add(new PositiveCell(cell.Index, cell.RestList.First(c => c != value)));
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
