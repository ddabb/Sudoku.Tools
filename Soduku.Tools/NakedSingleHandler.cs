using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Soduku.Tools
{
    [Example("900400613320190700000000009000017008000000000700360000800000000009045086253001004")]
    public  class NakedSingleHandler: ISudokuSolveHelper
    {

        public List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cellInfo = new List<CellInfo>();
            foreach (var index in QSudoku.allLocations)
            {
                var restList = qSoduku.GetRest(index);
                if (restList.Count==1)
                {
                    cellInfo.Add(new CellInfo(index, restList[0]));
                }
            }
            return cellInfo;

        }
    }
}
