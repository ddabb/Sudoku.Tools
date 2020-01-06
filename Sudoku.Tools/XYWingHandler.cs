using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("860035900700068351530074020070810530005307100183540200020650703057400002010700495")]
    public class XYWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCell.Where(c => qSudoku.GetRest(c).Count == 2).ToList();

            var temp = (from x in checkCells
                        join y in checkCells on 1 equals 1
                        where qSudoku.GetRest(x).Intersect(qSudoku.GetRest(y)).Count()==1
                        select new { x, y }).ToList();
            foreach (var pair in temp)
            {
             var intestectCells=   checkCells.Intersect(GetIntersectCells(checkCells, pair.x, pair.y)).ToList();
                if (intestectCells.Count()>0)
                {


                    var restX = qSudoku.GetRest(pair.x);
                    var restY = qSudoku.GetRest(pair.y);
                    List<int> all = new List<int>();
                    all.AddRange(restX);
                    all.AddRange(restY);
                    var IntersectValue = restX.Intersect(restY).First();
                    all = all.Where(x => x != IntersectValue).OrderBy(c => c).ToList();
                    var allString = string.Join(",", all);
                    
                    if (intestectCells.Exists(c=> qSudoku.GetRestString(c)==allString))
                    {
                        foreach (var cell in intestectCells)
                        {
                            if (qSudoku.GetRestString(cell) != allString)
                            {
                                var rests = qSudoku.GetRest(cell);
                                if (rests.Contains(IntersectValue)&&rests.Count==2)
                                {
                                    //Debug.WriteLine("allString"+ allString);
                                    //Debug.WriteLine("pair.x" + pair.x + "  restX" + string.Join(",",restX) );
                                    //Debug.WriteLine("pair.y" + pair.y+  "  restY" + string.Join(",", restY));
                                    cells.Add(new PositiveCellInfo(cell.Index, rests.First(c => c != IntersectValue)));
                                }
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
