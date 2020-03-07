using Sudoku.Core;
using System.Collections.Generic;
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
            return AssignmentCellByEliminationCell(qSudoku);
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
                            var removeCells = subcells.Where(c => c.RestString != sub.Key).ToList();
                            var removeValues = ConvertToInts(sub.Key);
                            foreach (var cell in removeCells)
                            {
                                var rests = cell.RestList;
                                if (rests.Intersect(removeValues).Count() == removeValues.Count)
                                {
                                    var cell1 = new NegativeValuesGroup(cell.Index, removeValues) { Sudoku = qSudoku };
                                    cell1.SolveMessages = new List<SolveMessage> { removeCells[0].Location, "和", removeCells[1].Location, "只能填入" + removeValues.JoinString(),"\r\n", cell.Location, "不能填入" + removeValues.JoinString(), "\r\n" };
                                    cells.Add(cell1);
                                }
                                else
                                {
                                    foreach (var removeValue in removeValues)
                                    {
                                        if (rests.Contains(removeValue))
                                        {
                                            var cell1 = new NegativeCell(cell.Index, removeValue) { Sudoku = qSudoku };
                                            cell1.SolveMessages = new List<SolveMessage> { removeCells[0].Location, "和", removeCells[1].Location, "只能填入" + removeValues.JoinString(), "\r\n", cell.Location, "不能填入" + removeValue, "\r\n" };
                                            cells.Add(cell1);
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
