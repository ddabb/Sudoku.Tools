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
        public override SolveMethodEnum methodType => SolveMethodEnum.XYWing;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCell.Where(c => c.GetRest().Count == 2).ToList();

            var temp = (from x in checkCells
                        join y in checkCells on 1 equals 1
                        where x.GetRest().Intersect(y.GetRest()).Count()==1
                        select new { x, y }).ToList();
            foreach (var pair in temp)
            {
             var intestectCells=   checkCells.Intersect(GetIntersectCells(checkCells, pair.x, pair.y)).ToList();
                if (intestectCells.Any())
                {


                    var restX = pair.x.GetRest();
                    var restY = pair.y.GetRest();
                    List<int> all = new List<int>();
                    all.AddRange(restX);
                    all.AddRange(restY);
                    var intersectValue = restX.Intersect(restY).First();
                    all = all.Where(x => x != intersectValue).OrderBy(c => c).ToList();
                    var allString = string.Join(",", all);
                    
                    if (intestectCells.Exists(c=> c.GetRestString()==allString))
                    {
                        foreach (var cell in intestectCells)
                        {
                            if (cell.GetRestString() != allString)
                            {
                                var rests = cell.GetRest();
                                if (rests.Contains(intersectValue)&&rests.Count==2)
                                {
                                    Debug.WriteLine("allString" + allString);
                                    Debug.WriteLine("pair.x" + pair.x + "  restX" + string.Join(",", restX));
                                    Debug.WriteLine("pair.y" + pair.y + "  restY" + string.Join(",", restY));
                                    cells.Add(new PositiveCellInfo(cell.Index, rests.First(c => c != intersectValue)));
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
