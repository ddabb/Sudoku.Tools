using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(7, "R1C1", "000080200005000040020005000962837000003214697174500832001000000697348521248751369")]
    public class DiscontinuousNiceLoopHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.DiscontinuousNiceLoop;
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

                            Debug.WriteLine("  index1   " + index1 + "   testValue1   " + testValue1);
                            NegativeCellInfo cell = new NegativeCellInfo(index1, testValue1);
                            cell.Sudoku = qSudoku;
                            var nacells = new List<CellInfo>();
                            loop(cell, ref nacells, ref relatedIndex, ref otherValues);

                            List<int> removeValues = new List<int>();
                            foreach (var value in nacells.Select(c => c.Value))
                            {
                                if (!removeValues.Contains(value))
                                {
                                    removeValues.Add(value);

                                    NegativeCellInfo cell1 = new NegativeCellInfo(index1, value);
                                    cell1.Sudoku = qSudoku;
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
    }
}
