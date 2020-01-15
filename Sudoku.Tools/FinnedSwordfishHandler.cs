using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample("030150209000360050700490603001273800000519000003684700100000008320040000409001060")]
    public class FinnedSwordfishHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.FinnedSwordfish;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <returns></returns>
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = (from value in G.AllBaseValues
                                    let filterCells =
                                        (from index1 in G.baseIndexs
                                         join index2 in G.baseIndexs on 1 equals 1
                                         join index3 in G.baseIndexs on 1 equals 1
                                         join cellInfo in qSudoku.AllUnSetCell on 1 equals 1
                                         where    index1 < index2
                                               && index2 < index3
                                               && cellInfo.GetRest().Contains(value)
                                               && new List<int> { index1, index2, index3 }.Contains(cellInfo.Row)
                                         select cellInfo).ToList()
                                    where filterCells.Count > 5 && filterCells.Count < 9
                                    let columns = filterCells.Select(c => c.Column).Distinct().ToList()
                                    let rows = filterCells.Select(c => c.Row).Distinct().ToList()
                                    where columns.Count == 3 && rows.Count == 3
                                    let checkCells = qSudoku.AllUnSetCell.Where(c =>
                                            columns.Contains(c.Column)
                                            && !rows.Contains(c.Row)
                                           && c.GetRest().Count == 2
                                           && c.GetRest().Contains(value))
                                        .ToList()
                                    from checkCell in checkCells
                                    select new PositiveCellInfo(checkCell.Index, checkCell.GetRest().First(c => c != value)))
                .Cast<CellInfo>().ToList();
            cells.AddRange((from value in G.AllBaseValues
                            let filterCells =
                                (from index1 in G.baseIndexs
                                 join index2 in G.baseIndexs on 1 equals 1
                                 join index3 in G.baseIndexs on 1 equals 1
                                 join cellInfo in qSudoku.AllUnSetCell on 1 equals 1
                                 where index1 < index2 && index2 < index3 && cellInfo.GetRest().Contains(value) &&
                               new List<int> { index1, index2, index3 }.Contains(cellInfo.Column)
                                 select cellInfo).ToList()
                            where filterCells.Count > 5 && filterCells.Count < 9
                            let columns = filterCells.Select(c => c.Column).Distinct().ToList()
                            let rows = filterCells.Select(c => c.Row).Distinct().ToList()
                            where columns.Count == 3 && rows.Count == 3
                            let checkCells =
                                qSudoku.AllUnSetCell.Where(c =>
                                    !columns.Contains(c.Column) && rows.Contains(c.Row) && c.GetRest().Count == 2 &&
                                    c.GetRest().Contains(value)).ToList()
                            from checkCell in checkCells
                            select new PositiveCellInfo(checkCell.Index, checkCell.GetRest().First(c => c != value)))
                .Cast<CellInfo>());

            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
