using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{

    [AssignmentExample(9,"R1C7","805630070003705800027081305236894157481257030579163284702300000304000700198076023")]
    public class SkyscraperHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.Skyscraper;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allunsetCell = qSudoku.AllUnSetCells;
            int times = 2;
           
            List<PossibleIndex> allPossibleindex1 = GetAllPossibleIndexInRowOrColumn(qSudoku, times);
            var pair = (from a in allPossibleindex1

                          join c in allPossibleindex1 on a.SpeacialValue equals c.SpeacialValue
                          where a.indexs.JoinString() != c.indexs.JoinString()
                          && !IsSameBlock(a.indexs[0],a.indexs[1])
                          && !IsSameBlock(c.indexs[0], c.indexs[1])
                          && a.direction == c.direction
                          &&a.indexs[0]<c.indexs[0] //不重复计算
                          && ((IsSameRowOrSameColumn(a.indexs[0], c.indexs[0])&&!IsSameRowOrSameColumn(a.indexs[1], c.indexs[1]))
                          ||(IsSameRowOrSameColumn(a.indexs[1], c.indexs[1]) && !IsSameRowOrSameColumn(a.indexs[0], c.indexs[0])))
                          select new { a,c}
                          ).ToList();
            foreach (var item in pair)
            {
                var rest = new List<CellInfo>();
                var SpeacialValue = item.a.SpeacialValue;
                if (IsSameRowOrSameColumn(item.a.indexs[0], item.c.indexs[0]))
                {
                 
                    rest= qSudoku.GetPublicUnsetAreas(item.a.indexs[1], item.c.indexs[1]);
                }
                else
                {
           
                    rest = qSudoku.GetPublicUnsetAreas(item.a.indexs[0], item.c.indexs[0]); 
                }
                var filter = rest.Where(c => c.RestCount==2&& c.RestList.Contains(SpeacialValue)).ToList();
                foreach (var cell in filter)
                {
                    cells.Add(new PositiveCellInfo(cell.Index, cell.RestList.First(c=>c!= SpeacialValue)));
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
