using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    
    [EliminationExample("300800060109500000050030000030000206600308109005067000000603408703000000040002030")]
    [AssignmentExample(3,"R9C9", "469500271185762439723900856647859312318427695952006784004090560001605900596070100")]
    public class URType2Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.URType2;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allcheckCell = qSudoku.AllUnSetCells.Where(c => new List<int> { 2, 3 }.Contains(c.RestCount)).ToList();

            var filter = (from a in allcheckCell
                          join b in allcheckCell on a.RestString equals b.RestString
                          join c in allcheckCell on 1 equals 1
                          join d in allcheckCell on c.RestString equals d.RestString
                          where a.Index < b.Index
                             && c.Index < d.Index
                             && d.RestCount == 3
                             && a.RestCount == 2
                             && d.RestList.Except(a.RestList).Count()==1
                              && d.RestList.Intersect(a.RestList).Count() == 2
                          select new List<CellInfo> { a, b, c, d }).ToList();
            foreach (var item in filter)
            {
                if (item.Select(c => c.Row).Distinct().Count() == 2 && item.Select(c => c.Column).Distinct().Count() == 2 && item.Select(c => c.Block).Distinct().Count() == 2)
                {
                  
                    var a = item.First();
                    var d = item.Last();
                    var arest = a.RestList;
                    var drest = d.RestList;
                    var value = d.RestList.Except(a.RestList).First();
                 var tempResult = qSudoku.GetPublicUnsetAreas(item[2], item[3]).Where(c => c.RestCount == 2 && c.RestList.Contains(value));
                    foreach (var cell in tempResult)
                    {
                        cells.Add(new PositiveCellInfo(cell.Index, cell.RestList.First(c => c != value)));
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
