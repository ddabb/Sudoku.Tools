using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(3, "R8C2", "980006375376850140000700860569347218000000537723581496000205780000000950000008620")]
    public class NakedPairHandller : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.NakedPair;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var eliminationCells = Elimination(qSudoku);
            foreach (var cellInfo in eliminationCells)
            {
                foreach (var postiveCell in cellInfo.NextCells)
                {
                    cells.Add(postiveCell);
                }

            }

            return cells;
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.GetFilterCell(c => c.Value == 0 && c.RestCount == 2);
            foreach (var direction in G.AllDirection)
            {
                foreach (var index in G.baseIndexs)
                {
                    var subcells = qSudoku.AllUnSetCells.Where(c => G.GetFilter(c, direction, index)).ToList();
                    if (subcells.Count > 2)
                    {
                        var temp = checkCells.Where(c => G.GetFilter(c, direction, index)).GroupBy(c => c.RestString)
                            .Where(c => c.Count() == 2);
                        foreach (var sub in temp)
                        {
                            var removeCells = subcells.Where(c => c.RestString != sub.Key);
                            var removeValues = ConvertToInts(sub.Key);
                            foreach (var cell in removeCells)
                            {
                                var rests = cell.RestList;
                                if (rests.Intersect(removeValues).Count() == removeValues.Count)
                                {
                                    cells.Add(new NegativeValuesGroup(cell.Index, removeValues) { Sudoku = qSudoku });
                                }
                                else
                                {
                                    foreach (var removeValue in removeValues)
                                    {
                                        if (rests.Contains(removeValue))
                                        {
                                            cells.Add(new NegativeCell(cell.Index, removeValue) { Sudoku = qSudoku });
                                        }
                                    }
                                }
                            }

                        }
                    }

                }

            }

            return cells;
        }
    }
}
