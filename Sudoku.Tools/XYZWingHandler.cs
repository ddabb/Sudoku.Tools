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
            var checkCells = qSudoku.AllUnSetCell.Where(c => qSudoku.GetRest(c).Count == 2).ToList();

            var temp = (from x in checkCells
                        join y in checkCells on 1 equals 1
                        where qSudoku.GetRest(x).Intersect(qSudoku.GetRest(y)).Count() == 1
                        && GetIntersectCells(checkCells,x, y).Count>0
                          && qSudoku.GetRestString(x)!= qSudoku.GetRestString(y)
                        && x.Index<y.Index
                        && x.Block!=y.Block
                        select new { x, y }).ToList();
            foreach (var xy in temp)
            {
                var allrest = qSudoku.GetRest(xy.x);
                var removeValue = qSudoku.GetRest(xy.x).Intersect(qSudoku.GetRest(xy.y)).First();
                allrest.AddRange(qSudoku.GetRest(xy.y));
                allrest= allrest.Distinct().ToList();
                allrest.Sort();
                var intersectCells = GetIntersectCells(qSudoku.AllUnSetCell, xy.x, xy.y);
                if (xy.x.Index == 10)
                {

                }
                if (intersectCells.Where(c=>qSudoku.GetRestString(c)== allrest.JoinString()).Count()>0)
                {
                    var intersectCell = intersectCells.First(c => qSudoku.GetRestString(c) == allrest.JoinString());
   
                    foreach (var cell in intersectCells.Where(c=>c.Index!= intersectCell.Index && c.Block== intersectCell.Block))
                    {
                
                            var rests = qSudoku.GetRest(cell);
                            if (rests.Contains(removeValue) && rests.Count == 2)
                            {
     
                                cells.Add(new CellInfo(cell.Index, rests.First(c => c != removeValue)));
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
