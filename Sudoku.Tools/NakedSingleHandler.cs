using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
namespace Soduku.Tools
{
    [AssignmentExample("900400613320190700000000009000017008000000000700360000800000000009045086253001004")]
    public  class NakedSingleHandler:SolverHandlerBase
    {

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cellInfo = new List<CellInfo>();
            foreach (var index in qSudoku.GetFilterCell(c=>c.Value==0).Select(c=>c.Index))
            {
                var restList = qSudoku.GetRest(index);
                if (restList.Count==1)
                {
                    cellInfo.Add(new CellInfo(index, restList[0]));
                }
            }
            return cellInfo;

        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
