using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(7, "R1C3", "120846003008193200900275081400050008002081000890460300080524100219738060700619832", SolveMethodEnum.HiddenPair)]
    public class XRSize6Type4Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize6Type4;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCells = qSudoku.AllUnSetCells;
            var pairCell = qSudoku.AllUnSetCells.Where(c => c.RestCount == 2).ToList();
            var keyCells = (from a in pairCell
                            join b in pairCell on a.RestString equals b.RestString
                            join c in pairCell on 1 equals 1
                            join d in pairCell on c.RestString equals d.RestString
                            let acSameRow = a.Row == c.Row
                            let indexs = G.MergeCellIndexs(a, b, c, d)
                            where a.RestString != c.RestString
                                  && a.RestList.Intersect(c.RestList).Count() == 1
                                  && (a.Row == c.Row && a.Column == b.Column && b.Row == d.Row && c.Column == d.Column ||
                                      a.Column == c.Column && a.Row == b.Row && b.Column == d.Column && c.Row == d.Row)
                                  && G.MergeCellIndexs(a, b, c, d).Distinct().Count() == 4
                                  && a.Index < c.Index
                                  && a.Index < b.Index
                                  && b.Index < d.Index
                            select new { a, b, c, d, acSameRow, indexs }).ToList();
            foreach (var item in keyCells)
            {
                var acSameRow = item.acSameRow;
                var a = item.a;
                var b = item.b;
                var c = item.c;
                var d = item.d;
                var indexs = item.indexs;
                var tempValue = a.RestList.Union(c.RestList).Except(a.RestList.Intersect(c.RestList)).ToList();
                var value1 = tempValue[0];
                var value2 = tempValue[1];
                if (acSameRow)
                {

                    var cellList = (from cell1 in allUnsetCells
                                    join cell2 in allUnsetCells on cell1.Column equals cell2.Column
                                    let column = cell1.Column
                                    let cell1Rest = cell1.RestList
                                    let cell2Rest = cell2.RestList
                                    where cell1.Row == a.Row
                                          && cell2.Row == b.Row
                                          && cell1Rest.Contains(value1)
                                          && cell1Rest.Contains(value2)
                                          && cell2Rest.Contains(value1)
                                          && cell2Rest.Contains(value2)
                                          && cell1.Index < cell2.Index
                                    select new { cell1, cell2, column }).ToList();
                    foreach (var item2 in cellList)
                    {
                        var column = item2.column;
                        var cell1 = item2.cell1;
                        var cell2 = item2.cell2;
                        var filterCell = allUnsetCells.Where(c1 => c1.Column == column).ToList();
                        if (filterCell.Count(cell => cell.RestList.Contains(value1)) == 2)
                        {
                            cells.Add(new NegativeCell(cell1.Index, value2) { Sudoku = qSudoku });
                            cells.Add(new NegativeCell(cell2.Index, value2) { Sudoku = qSudoku });
                            cells.Add(new NegativeIndexsGroup(new List<int> { cell1.Index, cell2.Index }, value2) { Sudoku = qSudoku });
                        }
                        if (filterCell.Count(cell => cell.RestList.Contains(value2)) == 2)
                        {
                            cells.Add(new NegativeCell(cell1.Index, value1) { Sudoku = qSudoku });
                            cells.Add(new NegativeCell(cell2.Index, value1) { Sudoku = qSudoku });
                            cells.Add(new NegativeIndexsGroup(new List<int> { cell1.Index, cell2.Index }, value1) { Sudoku = qSudoku });
                        }
                    }


                }
                else
                {
                    var cellList = (from cell1 in allUnsetCells
                                    join cell2 in allUnsetCells on cell1.Row equals cell2.Row
                                    let row = cell1.Row
                                    let cell1Rest = cell1.RestList
                                    let cell2Rest = cell2.RestList
                                    where cell1.Column == a.Column
                                          && cell2.Column == b.Column
                                          && cell1Rest.Contains(value1)
                                          && cell1Rest.Contains(value2)
                                          && cell2Rest.Contains(value1)
                                          && cell2Rest.Contains(value2)
                                          && cell1.Index < cell2.Index
                                    select new { cell1, cell2, row }).ToList();
                    foreach (var item2 in cellList)
                    {
                        var row = item2.row;
                        var cell1 = item2.cell1;
                        var cell2 = item2.cell2;
                        var filterCell = allUnsetCells.Where(c1 => c1.Row == row).ToList();
                        if (filterCell.Count(cell => cell.RestList.Contains(value1)) == 2)
                        {
                            cells.Add(new NegativeCell(cell1.Index, value2) { Sudoku = qSudoku });
                            cells.Add(new NegativeCell(cell2.Index, value2) { Sudoku = qSudoku });
                            cells.Add(new NegativeIndexsGroup(new List<int> { cell1.Index, cell2.Index }, value2) { Sudoku = qSudoku });
                        }
                        if (filterCell.Count(cell => cell.RestList.Contains(value2)) == 2)
                        {
                            cells.Add(new NegativeCell(cell1.Index, value1) { Sudoku = qSudoku });
                            cells.Add(new NegativeCell(cell2.Index, value1) { Sudoku = qSudoku });
                            cells.Add(new NegativeIndexsGroup(new List<int> { cell1.Index, cell2.Index }, value1) { Sudoku = qSudoku });
                        }
                    }


                }
            }

            return cells;
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
