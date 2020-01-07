using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("869453721000921568215800439621534987407610352000200146000102803932785614100340205")]
 public   class XYZWingHandler :SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCell.Where(c => c.GetRest().Count == 2).ToList();

            var temp = (from x in checkCells
                        join y in checkCells on 1 equals 1
                        where x.GetRest().Intersect(y.GetRest()).Count() == 1
                              && GetIntersectCells(checkCells,x, y).Count>0
                              && x.GetRestString()!= y.GetRestString()
                              && x.Index<y.Index
                              && x.Block!=y.Block
                        select new { x, y }).ToList();
            foreach (var xy in temp)
            {
                var allrest = xy.x.GetRest();
                var removeValue = xy.x.GetRest().Intersect(xy.y.GetRest()).First();
                allrest.AddRange(xy.y.GetRest());
                allrest= allrest.Distinct().ToList();
                allrest.Sort();
                var intersectCells = GetIntersectCells(qSudoku.AllUnSetCell, xy.x, xy.y);
                if (xy.x.Index == 10)
                {

                }
                if (intersectCells.Where(c=>c.GetRestString()== allrest.JoinString()).Count()>0)
                {
                    var intersectCell = intersectCells.First(c => c.GetRestString() == allrest.JoinString());
   
                    foreach (var cell in intersectCells.Where(c=>c.Index!= intersectCell.Index && c.Block== intersectCell.Block))
                    {
                
                            var rests = cell.GetRest();
                            if (rests.Contains(removeValue) && rests.Count == 2)
                            {
     
                                cells.Add(new PositiveCellInfo(cell.Index, rests.First(c => c != removeValue)));
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
