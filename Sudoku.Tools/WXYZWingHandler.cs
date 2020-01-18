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
        /// 230mm
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <returns></returns>
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCell.Where(c => c.GetRest().Count == 4).ToList();

            foreach (var checkcell in checkCells)
            {
                var checkcellRest = checkcell.GetRest();
                var relatedCell = checkcell.RelatedUnsetCells.Where(c => c.GetRest().Count > 1 && c.GetRest().Intersect(checkcellRest).Any()).ToList();

                var filter = (from x in relatedCell
                              join y in relatedCell on 1 equals 1
                              join z in relatedCell on 1 equals 1
                              join cellInfo in relatedCell.Where(c => c.GetRest().Count == 2) on 1 equals 1
                              let indexs = new List<int> { x.Index, y.Index, z.Index, checkcell.Index, cellInfo.Index }
                              let xrest = x.GetRest()
                              let yrest = y.GetRest()
                              let zrest = z.GetRest()
                              let intersectCellRest = cellInfo.GetRest()
                              where indexs.Distinct().Count() == 5
                                    && x.Index < y.Index && y.Index < z.Index
                                    && xrest.Intersect(yrest).Intersect(zrest).Count() == 1
                                    && xrest.All(c => checkcellRest.Contains(c))
                                    && yrest.All(c => checkcellRest.Contains(c))
                                    && zrest.All(c => checkcellRest.Contains(c))
                                    && xrest.JoinString() != yrest.JoinString()
                                    && yrest.JoinString() != zrest.JoinString()
                                    && GetIntersectCellIndexs(relatedCell, x, checkcell).Contains(cellInfo.Index)
                                    && GetIntersectCellIndexs(relatedCell, y, checkcell).Contains(cellInfo.Index)
                                    && GetIntersectCellIndexs(relatedCell, z, checkcell).Contains(cellInfo.Index)
                              select new { cellInfo, x,y,z,
                                   intersectCellRest }).Distinct().ToList();
                cells.AddRange((from item in filter 
                    let xyzRest = item.x.GetRest().Intersect(item.y.GetRest()).Intersect(item.z.GetRest()).ToList() 
                    where xyzRest.Count == 1 && checkcellRest.Contains(xyzRest.First()) 
                    let intersectValue = xyzRest.First() 
                    where item.intersectCellRest.Contains(intersectValue) 
                    select new PositiveCellInfo(item.cellInfo.Index, item.intersectCellRest.First(c => c != intersectValue))
                    ).Cast<CellInfo>());
            }

            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
