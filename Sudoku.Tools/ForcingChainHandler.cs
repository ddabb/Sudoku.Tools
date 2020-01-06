using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sudoku.Tools
{

    [AssignmentExample("716000289298176543534928761000490108080607904945812376403089612809201437020000895")]
    //200607984700480230048000710000070598017800623580000471395000847126748359874000162
    public class ForcingChainHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            
            
            var possibleIndexs= GetAllPossibleIndex(qSudoku, 2);
            var temp = (from a in possibleIndexs
                        join b in possibleIndexs on 1 equals 1
                        where a.indexs.Intersect(b.indexs).Count() > 0
                        select new { a, b }).ToList();
            //71 6

            NegativeCellInfo test = new NegativeCellInfo(71, 6);
            //Dictionary<int, List<CellInfo>> levelCells = new Dictionary<int, List<CellInfo>>();
            //levelCells.Add(1, new List<CellInfo> { test });
            //var temp2= GetPositiveCellInfos(possibleIndexs, test);
            //test.reductionAfterCells = temp2;

            //levelCells.Add(2, temp2.ToList<CellInfo>());

            //levelCells.Add(3, new List<CellInfo>());
            //foreach (var sub in test.reductionAfterCells)
            //{
            //    var temp3 = GetNegativeCells(qSudoku, sub, sub.reductionBeforeCell);
            //    levelCells[3].AddRange(temp3.ToList<CellInfo>());
            //    sub.reductionAfterCells = temp3;
            //}





            return cells;

        }


        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
