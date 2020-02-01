using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Sudoku.Tools
{
    [AssignmentExample(5, "R5C1", "748359126001726840026400705200040387074030009180070004402507608017003452805204070")]
    public class LockedURType1Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.LockedURType1;

        public override MethodClassify methodClassify => throw new NotImplementedException();

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCell = qSudoku.AllUnSetCells;
            var ab = (from a in allUnsetCell
                      join b in allUnsetCell on a.RestString equals b.RestString
                      let sameRow = a.Row == b.Row
                      let indexs = G.MergeCellIndexs(a, b)
                      let restList = a.RestList
                      where a.Index < b.Index
                      && restList.Count == 2
                      && (a.Block != b.Block)
                      && (a.Row == b.Row || a.Column == b.Column)
                      select new { a, b, sameRow, indexs, restList }).ToList();
            foreach (var item in ab)
            {
                var sameRow = item.sameRow;
                var a = item.a;
                var b = item.b;
                var abindexs = item.indexs;
                var restList = item.restList;
                var cdGroup = allUnsetCell.Where(c => !abindexs.Contains(c.Index) && c.RestList.Intersect(restList).Count() == 2).ToList();
                var cd = (from c in cdGroup
                          join d in cdGroup on 1 equals 1
                          let cdIndexs = G.MergeCellIndexs(c, d)
                          where sameRow ? c.Column == a.Column && d.Column == b.Column && c.Row == d.Row : c.Row == a.Row && d.Row == b.Row && c.Column == d.Column
                          select new { c, d, cdIndexs }).ToList();
                foreach (var item1 in cd)
                {
                    var c = item1.c;
                    var d = item1.d;
                    var cdIndexs = item1.cdIndexs;
                    foreach (var rest in restList)
                    {
                        var haveRestValueCell = allUnsetCell.Where(cell => (sameRow ? cell.Row == a.Row || cell.Row == c.Row : cell.Column == a.Column || cell.Column == c.Column) && cell.RestList.Contains(rest)).ToList();
                        if (haveRestValueCell.Count == 4)
                        {
                            var positiveValue = restList.ToList().First(c => c != rest);
                            var findCells = allUnsetCell.Where(cell => cell.RestList.Contains(positiveValue) && (sameRow ? cell.Row == c.Row : cell.Column == c.Column) && !cdIndexs.Contains(cell.Index)).ToList();
                            if (findCells.Count == 1)
                            {

                                cells.Add(new PositiveCell(findCells.First().Index, positiveValue));
                            }

                        }
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
