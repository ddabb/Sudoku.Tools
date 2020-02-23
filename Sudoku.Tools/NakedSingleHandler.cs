using System;
using System.Collections.Generic;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample(5,"R2C9","900400613320190700000000009000017008000000000700360000800000000009045086253001004")]
    public  class NakedSingleHandler:SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.NakedSingle;

        public override MethodClassify methodClassify => MethodClassify.SudokuRules;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cellInfo = new List<CellInfo>();
            foreach (var index in qSudoku.AllUnSetCells)
            {
                var restList = index.RestList;
                if (restList.Count==1)
                {
                    var cell = new PositiveCell(index.Index, restList[0]);
                    cell.SolveMessages=new List<SolveMessage>
                    {
                       
                        new SolveMessage(index.Location,MessageType.Location),
                        new SolveMessage(" 只能够填入 "),
                        new SolveMessage(" "+restList[0],MessageType.Important)
                    };
                    cellInfo.Add(cell);
                }
            }
            return cellInfo;

        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
