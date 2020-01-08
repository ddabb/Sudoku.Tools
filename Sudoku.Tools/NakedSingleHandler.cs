using System;
using System.Collections.Generic;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample("900400613320190700000000009000017008000000000700360000800000000009045086253001004")]
    public  class NakedSingleHandler:SolverHandlerBase
    {
        public override SolveMethodEnum methodType => throw new NotImplementedException();

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cellInfo = new List<CellInfo>();
            foreach (var index in qSudoku.AllUnSetCell)
            {
                var restList = index.GetRest();
                if (restList.Count==1)
                {
                    cellInfo.Add(new PositiveCellInfo(index.Index, restList[0]));
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
