using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample("030150209000360050700490603001273800000519000003684700100000008320040000409001060")]
    public class JellyfishHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.Jellyfish;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            List<int> range = new List<int> { 4 };
            List<int> sumrange = new List<int> { 16 };
            foreach (var value in G.AllBaseValues)
            {
                var checkCell = qSudoku.AllUnSetCells.Where(c => c.RestList.Contains(value)).ToList();
                var filter = (from index1 in G.baseIndexs
                              join index2 in G.baseIndexs on 1 equals 1
                              join index3 in G.baseIndexs on 1 equals 1
                              join index4 in G.baseIndexs on 1 equals 1
                              let rows = new List<int> { index1, index2, index3, index4 }
                              let columns = G.DistinctColumn(checkCell.Where(c => rows.Contains(c.Row)).ToList())
                              let count1 = checkCell.Count(c => c.Row == index1)
                              let count2 = checkCell.Count(c => c.Row == index2)
                              let count3 = checkCell.Count(c => c.Row == index3)
                              let count4 = checkCell.Count(c => c.Row == index4)
                              where index1 < index2
                                    && index2 < index3
                                    && index3 < index4
                                    && columns.Count == 4
                                    && sumrange.Contains(count1 + count2 + count3 + count4)
                                    && range.Contains(count1)
                                    && range.Contains(count2)
                                    && range.Contains(count3)
                                    && range.Contains(count4)
                              select new { rows, columns }).ToList();
                foreach (var item in filter)
                {
                    cells.AddRange(checkCell.Where(c => !item.rows.Contains(c.Row)
                                                        && item.columns.Contains(c.Column) &&
                                                        c.RestList.Contains(value) &&
                                                        c.RestCount>1).Select(cell => new NegativeCell(cell.Index, value, qSudoku)
                    ).Cast<CellInfo>());
                }

                var filter2 = (from index1 in G.baseIndexs
                               join index2 in G.baseIndexs on 1 equals 1
                               join index3 in G.baseIndexs on 1 equals 1
                               join index4 in G.baseIndexs on 1 equals 1
                               let columns = new List<int> { index1, index2, index3, index4 }
                               let distinctRows = G.DistinctRow(checkCell.Where(c => columns.Contains(c.Column)).ToList())
                               let count1 = checkCell.Count(c => c.Column == index1)
                               let count2 = checkCell.Count(c => c.Column == index2)
                               let count3 = checkCell.Count(c => c.Column == index3)
                               let count4 = checkCell.Count(c => c.Column == index4)
                               where index1 < index2
                                     && index2 < index3
                                     && index3 < index4
                                     && distinctRows.Count == 4
                                     && sumrange.Contains(count1 + count2 + count3 + count4)
                                     && range.Contains(count1)
                                     && range.Contains(count2)
                                     && range.Contains(count3)
                                     && range.Contains(count4)
                               select new { distinctRows, columns }).ToList();
                foreach (var item in filter2)
                {
                    cells.AddRange(checkCell.Where(c => item.distinctRows.Contains(c.Row)
                                                        && !item.columns.Contains(c.Column) &&
                                                        c.RestList.Contains(value) &&
                                                        c.RestCount > 1).Select(cell => new NegativeCell(cell.Index, value, qSudoku)
                    ).Cast<CellInfo>());
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
