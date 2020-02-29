using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample(8, "R8C7", "002019008140005300000000000010050000507020080000008006800003002003041060000980000")]
    public class HiddenSingleBlockHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.HiddenSingleBlock;

        public override MethodClassify methodClassify => MethodClassify.SudokuRules;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;

            foreach (var index in G.baseIndexs)
            {
                List<int> allRest = new List<int>();
                var conditionCells = allUnSetCell.Where(c => c.Block == index).ToList();
                foreach (var cell in conditionCells)
                {
                    allRest.AddRange(cell.RestList);
                }

                foreach (var value in G.AllBaseValues)
                {
                    if (allRest.Count(c=>c==value)==1)
                    {
                        var findCells = conditionCells.Where(c => c.RestList.Contains(value) && c.RestCount != 1)//和显性数对作区分
                            .ToList();
                        if (findCells.Count!=0)
                        {
                            var cell = findCells.First();
                            cells.Add(new PositiveCell(cell.Index,value){Sudoku = qSudoku, SolveMessages = new List<SolveMessage> { cell.Block + 1 + "宮只有", cell.Location, "能填入", value } });
                        }
                    }
                }
            }
            return cells;
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>(); return cells;
        }
    }
}
