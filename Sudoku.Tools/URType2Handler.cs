using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    
    [EliminationExample("300800060109500000050030000030000206600308109005067000000603408703000000040002030")]
    [AssignmentExample("030150209000360050700490603001273800000519000003684700100000008320040000409001060")]
    public class URType2Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.URType2Handler;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allcheckCell = qSudoku.AllUnSetCell.Where(c => new List<int> { 2, 3 }.Contains(c.GetRest().Count)).ToList();

            var filter = (from a in allcheckCell
                          join b in allcheckCell on a.GetRestString() equals b.GetRestString()
                          join c in allcheckCell on 1 equals 1
                          join d in allcheckCell on c.GetRestString() equals d.GetRestString()
                          where a.Index < b.Index
                             && c.Index < d.Index
                             && d.GetRest().Count() == 3
                             && a.GetRest().Count() == 2
                             && d.GetRest().Except(a.GetRest()).Count()==1
                          select new List<CellInfo> { a, b, c, d }).ToList();
            foreach (var item in filter)
            {
                if (item.Select(c => c.Row).Distinct().Count() == 2 && item.Select(c => c.Column).Distinct().Count() == 2 && item.Select(c => c.Block).Distinct().Count() == 2)
                {
                  
                    var a = item.First();
                    var d = item.Last();
                    var arest = a.GetRest();
                    var drest = d.GetRest();
                    var value = d.GetRest().Except(a.GetRest()).First();
                 var tempResult =   GetIntersectCells(qSudoku.AllUnSetCell, item[2], item[3]).Where(c => c.GetRest().Count == 2 && c.GetRest().Contains(value));
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
