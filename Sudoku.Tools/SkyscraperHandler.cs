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

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
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
                var index1 = item.a.indexs[0];
                var index2 = item.c.indexs[0];
                var index3 = item.a.indexs[1];
                var index4 = item.c.indexs[1];
                int cellindex1;
                int cellindex2;
                if (IsSameRowOrSameColumn(item.a.indexs[0], item.c.indexs[0]))
                {
                    cellindex1 = item.a.indexs[1];
                    cellindex2 = item.c.indexs[1];
  
                }
                else
                {
                    cellindex1 = item.a.indexs[0];
                    cellindex2 = item.c.indexs[0];
               
                }
                rest = qSudoku.GetPublicUnsetAreas(item.a.indexs[1], item.c.indexs[1]);
                var filter = rest.Where(c => c.RestCount==2&& c.RestList.Contains(SpeacialValue)).ToList();
                foreach (var cell in filter)
                {
                    var cell1 = new PositiveCell(cell.Index, cell.RestList.First(c => c != SpeacialValue))
                    {
                        SolveMessages = new List<SolveMessage>
                        {
                            index1.LoctionDesc(),
                            "、",
                            index2.LoctionDesc(),
                            "、",
                            index3.LoctionDesc(),
                            "、",
                            index4.LoctionDesc(),
                            "的值",
                            SpeacialValue,
                            "构成摩天楼",
                            cellindex1.LoctionDesc(),
                            "、",
                            cellindex2.LoctionDesc(),
                            "共同影响区域删除",
                            SpeacialValue
                        }
                    };
                    cells.Add(cell1); ;
                }
        
            }
          


            return cells;
        }



        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
