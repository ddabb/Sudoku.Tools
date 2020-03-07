using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Sudoku.Tools
{
    [AssignmentExample(5,"R5C4","000000000000040329000651847000000500000000473008473296004000900000005180060180700")]
    public  class HiddenSingleColumnHandler:SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.HiddenSingleColumn;

        public override MethodClassify methodClassify => MethodClassify.SudokuRules;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;

            foreach (var index in G.baseIndexs)
            {
                List<int> allRest = new List<int>();
                var conditionCells = allUnSetCell.Where(c => c.Column == index).ToList();
                foreach (var cell in conditionCells)
                {
                    allRest.AddRange(cell.RestList);
                }

                foreach (var value in G.AllBaseValues)
                {
                    if (allRest.Count(c => c == value) == 1)
                    {
                        var findCells = conditionCells.Where(c => c.RestList.Contains(value) && c.RestCount != 1)//和显性数对作区分
                            .ToList();
                        if (findCells.Count != 0)
                        {
                            var cell = findCells.First();
                            cells.Add(new PositiveCell(cell.Index, value) { Sudoku = qSudoku, SolveMessages = new List<SolveMessage> { cell.Column.ColumnDesc(), "只有", cell.Location, "能填入", value } });
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
            return "";
        }
    }
}
