using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(7, "R1C9", "000109030190700006300286001581472003900568002600391785700915008210007050050020000")]
    public class WXYZWingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.WXYZWing;
        /// <summary>
        /// 142ms
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <returns></returns>
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCell.Where(c => c.GetRest().Count == 4).ToList();
            List<int> countRange=new List<int>{2,3};
            foreach (var checkcell in checkCells)
            {
                var checkcellRest = checkcell.GetRest();
                var relatedCell = checkcell.RelatedUnsetCells.Where(c => countRange.Contains(c.GetRest().Count) && c.GetRest().Intersect(checkcellRest).Any()).ToList();

                var filter = (from x in relatedCell
                              join y in relatedCell on 1 equals 1
                              join z in relatedCell on 1 equals 1
         
                              let indexs = new List<int> { x.Index, y.Index, z.Index, checkcell.Index}
                              let xrest = x.GetRest()
                              let yrest = y.GetRest()
                              let zrest = z.GetRest()
                                    where indexs.Distinct().Count() == 4
                                    && x.Index < y.Index && y.Index < z.Index
                                    && xrest.Intersect(yrest).Intersect(zrest).Count() == 1
                                    && xrest.All(c => checkcellRest.Contains(c))
                                    && yrest.All(c => checkcellRest.Contains(c))
                                    && zrest.All(c => checkcellRest.Contains(c))
                                    && xrest.JoinString() != yrest.JoinString()
                                    && yrest.JoinString() != zrest.JoinString()
                              select new { indexs,  x,y,z, xrest, yrest, zrest }).Distinct().ToList();
                foreach (var item in filter)
                {
                    var xyzRest = item.xrest.Intersect(item.yrest).Intersect(item.zrest).ToList();
                    if (xyzRest.Count == 1)
                    {
                        var intersectValue = xyzRest.First();
                        var whereCells = relatedCell.Where(c =>
                            !item.indexs.Contains(c.Index)
                            && GetIntersectCellIndexs(relatedCell, item.x, checkcell).Contains(c.Index)
                            && GetIntersectCellIndexs(relatedCell, item.y, checkcell).Contains(c.Index)
                            && GetIntersectCellIndexs(relatedCell, item.z, checkcell).Contains(c.Index)).ToList();
                        foreach (var findCell in whereCells)
                        {
                            var findCellRest = findCell.GetRest();
                            if (checkcellRest.Contains(intersectValue) && findCellRest.Contains(intersectValue))
                            {
                                cells.Add(new PositiveCellInfo(findCell.Index, findCellRest.First(c => c != intersectValue))); 
                            }
                        }
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
