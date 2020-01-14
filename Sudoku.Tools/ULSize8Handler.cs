using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample("914526300620081459508940162209008510486159723150200890361095240045002901092010635")]
    public class ULSize8Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize8;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCell;
            int cellCount = 8;
            List<int> values = new List<int>();
            Dictionary<int, List<int>> dic = checkCells.ToDictionary(cell => cell.Index, cell => cell.GetRest());
            foreach (var value in G.AllBaseValues)
            {
                if (dic.Values.Count(c => c.Contains(value)) > cellCount)
                {
                    values.Add(value);
                }
            }

            foreach (var x in values)
            {
                foreach (var y in values)
                {
                    if (x < y)
                    {
                        var pair = new List<int> {x, y};
                        Debug.WriteLine(" checkCells.Count " + checkCells.Count+"x"+x+"y"+y);
                        var containsCells = checkCells.Where(c => c.GetRest().Intersect(pair).Count() == 2).ToList();
                        Debug.WriteLine(" containsCells.Count " + containsCells.Count );
                        var equalCells = checkCells.Where(c => c.GetRest().Except(pair).Count() == 2).ToList();
                        Debug.WriteLine(" equalCells.Count " + equalCells.Count+DateTime.Now);
                        //从A8的a8.RrCc 入手
                        if (equalCells.Count==7)
                        {
                            //var temp = (from a1 in containsCells 
                            //        join a2 in containsCells on a1.GetRestString() equals a2.GetRestString()
                            //    join a3 in containsCells on a1.GetRestString() equals a3.GetRestString()
                            //    join a4 in containsCells on a1.GetRestString() equals a4.GetRestString()
                            //    join a5 in containsCells on a1.GetRestString() equals a5.GetRestString()
                            //    join a6 in containsCells on a1.GetRestString() equals a6.GetRestString()
                            //    join a7 in containsCells on a1.GetRestString() equals a7.GetRestString()
                            //    join a8 in containsCells on 2 equals a8.GetRest().Intersect(pair).Count()
                            //    where a1.GetRestString() == pair.JoinString()
                            //          && a8.GetRest().Count == 3
                            //          && a1.Index<a2.Index
                            //          && a2.Index<a3.Index
                            //          && a3.Index<a4.Index
                            //          && a4.Index<a5.Index
                            //          && a5.Index<a6.Index
                            //          && a6.Index<a7.Index
                            //            select new { a1, a2, a3, a4, a5, a6, a7, a8 }
                            //    ).ToList();
                            //Debug.WriteLine(" temp.Count " + temp.Count + "x  " + x + " y " + y + DateTime.Now+ "RrCc" + temp.First().a8.RrCc);
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
