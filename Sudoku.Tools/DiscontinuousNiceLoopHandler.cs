using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(7, "R1C1", "000080200005000040020005000962837000003214697174500832001000000697348521248751369")]
    public class DiscontinuousNiceLoopHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.DiscontinuousNiceLoop;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCell = qSudoku.AllUnSetCells;
            foreach (var unsetCell in allUnsetCell)
            {
                //if (unsetCell.Index == 7)
                {
                    var relatedIndex = unsetCell.RelatedUnsetCells.Select(c => c.Index).ToList();
                    var rests = unsetCell.RestList;
                    foreach (var restValue in rests)
                    {
                        var index1 = unsetCell.Index;
                        var testValue1 = restValue;
                        var otherValues = rests.ToList().Except(new List<int> { restValue }).ToList();
                        //if (testValue1 == 5)
                        {

                            NegativeCell cell = new NegativeCell(index1, testValue1, qSudoku);
                            var nacells = new List<CellInfo>();
                            loop(cell, ref nacells, ref relatedIndex, ref otherValues);

                            List<int> removeValues = new List<int>();
                            foreach (var value in nacells.Select(c => c.Value))
                            {
                                if (!removeValues.Contains(value))
                                {
                                    removeValues.Add(value);

                                    NegativeCell cell1 = new NegativeCell(index1, value, qSudoku);

                                    foreach (var item in cell1.NextCells)
                                    {
                                        cells.Add(item);
                                    }


                                }
                            }


                        }





                    }

                }


            }

            return cells;
        }

        private void loop(CellInfo cell, ref List<CellInfo> nacells, ref List<int> relatedIndex, ref List<int> otherValues)
        {

            if (nacells.Any()) return;

            if (cell.GetAllParents()
      .Any(c => c.Index == cell.Index
      && c.CellType == cell.CellType
      && c.Value == cell.Value))
            {
                return;
            }
            foreach (var nextCell in cell.NextCells)
            {

                if (nextCell.CellType == CellType.Positive)
                {
                    if (otherValues.Contains(nextCell.Value) && relatedIndex.Contains(nextCell.Index))
                    {
                        if (!nacells.Exists(c => c.Value == nextCell.Value && c.Index == nextCell.Index))
                        {
                            nacells.Add(nextCell);
                        }

                    }
                }
                loop(nextCell, ref nacells, ref relatedIndex, ref otherValues);
            }
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
