using Sudoku.Core;
using System;
using System.Collections.Generic;

namespace Sudoku.Tools
{
    [AssignmentExample(5, "R2C9", "900400613320190700000000009000017008000000000700360000800000000009045086253001004")]
    public class NakedSingleHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.NakedSingle;

        public override MethodClassify methodClassify => MethodClassify.SudokuRules;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cellInfo = new List<CellInfo>();
            foreach (var index in qSudoku.AllUnSetCells)
            {
                var restList = index.RestList;
                if (restList.Count == 1)
                {
                    var cell = new PositiveCell(index.Index, restList[0])
                    {
                        SolveMessages = new List<SolveMessage> { index.Location, " 只能够填入 ", restList[0] }
                    };
                    cell.drawCells.Add(cell);

                    cellInfo.Add(cell);
                }
            }
            return cellInfo;

        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override string GetDesc()
        {
            return "单元格只有一个数可以填";
        }
    }
}
