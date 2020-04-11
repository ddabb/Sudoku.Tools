using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core;
using Sudoku.Core.Model;

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
                            var result = new PositiveCell(cell.Index, value, qSudoku) { SolveMessages = new List<SolveMessage> { cell.Block.BlockDesc(), "只有", cell.Location, "能填入", value } };
                            result.drawCells.Add(result);

                            cells.Add(result);
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

        public override string GetDesc()
        {
            return "宫内只有一个位置可以填入候选数x，则该位置的值为x。";
        }
    }
}
