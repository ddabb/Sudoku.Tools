using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("000074200200906740004520009100457928547892000002600574400705092005209403629040057")]
    public class SwordfishHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.Swordfish;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {

            List<CellInfo> cells = new List<CellInfo>();
            List<int> range = new List<int> {  3 };
            List<int> sumrange = new List<int> { 9 };
            foreach (var value in G.AllBaseValues)
            {
                var checkCell = qSudoku.AllUnSetCells.Where(c => c.RestList.Contains(value)).ToList();
                var filter = (from index1 in G.baseIndexs
                              join index2 in G.baseIndexs on 1 equals 1
                              join index3 in G.baseIndexs on 1 equals 1
                              let rows = new List<int> { index1, index2, index3 }
                              let columns = G.DistinctColumn(checkCell.Where(c => rows.Contains(c.Row)).ToList())
                              let count1 = checkCell.Count(c => c.Row == index1)
                              let count2 = checkCell.Count(c => c.Row == index2)
                              let count3 = checkCell.Count(c => c.Row == index3)
                              where index1 < index2
                                    && index2 < index3
                                    && columns.Count == 3
                                    && sumrange.Contains(count1 + count2 + count3)
                                    && range.Contains(count1)
                                    && range.Contains(count2)
                                    && range.Contains(count3)
                              select new { rows, columns }).ToList();
                foreach (var item in filter)
                {
                    cells.AddRange(checkCell.Where(c => !item.rows.Contains(c.Row)
                                                        && item.columns.Contains(c.Column) &&
                                                        c.RestList.Contains(value) &&
                                                        c.RestCount == 2).Select(cell => new PositiveCell(cell.Index, cell.RestList.First(c => c != value), qSudoku)
                    ).Cast<CellInfo>());
                }

                var filter2 = (from index1 in G.baseIndexs
                               join index2 in G.baseIndexs on 1 equals 1
                               join index3 in G.baseIndexs on 1 equals 1
                               let columns = new List<int> { index1, index2, index3 }
                               let distinctRows = G.DistinctRow(checkCell.Where(c => columns.Contains(c.Column)).ToList())
                               let count1 = checkCell.Count(c => c.Column == index1)
                               let count2 = checkCell.Count(c => c.Column == index2)
                               let count3 = checkCell.Count(c => c.Column == index3)
                               where index1 < index2
                                     && index2 < index3
                                     && distinctRows.Count == 3
                                     && sumrange.Contains(count1 + count2 + count3)
                                     && range.Contains(count1)
                                     && range.Contains(count2)
                                     && range.Contains(count3)
                               select new { distinctRows, columns }).ToList();
                foreach (var item in filter2)
                {
                    cells.AddRange(checkCell.Where(c => item.distinctRows.Contains(c.Row)
                                                        && !item.columns.Contains(c.Column) &&
                                                        c.RestList.Contains(value) &&
                                                        c.RestCount == 2).Select(cell => new PositiveCell(cell.Index, cell.RestList.First(c => c != value), qSudoku)
                    ).Cast<CellInfo>());
                }
            }

            return cells;
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
