using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(1,"R6C5", "000074200200906740004520009100457928547892000002600574400705092005209403629040057")]
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
            var allUnSetCell = qSudoku.AllUnSetCell;
            List<CellInfo> cells = (from value in G.AllBaseValues
                                    let filterCells = FindCell(allUnSetCell, value)
                                    let checkCells = allUnSetCell.Where(c =>
                                            G.DistinctColumn(filterCells).Contains(c.Column)
                                            && !G.DistinctRow(filterCells).Contains(c.Row)
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
                                 join cellInfo in allUnSetCell on 1 equals 1
                                 where index1 < index2 && index2 < index3 && cellInfo.GetRest().Contains(value) &&
                               new List<int> { index1, index2, index3 }.Contains(cellInfo.Column)
                                 select cellInfo).ToList()
                            where filterCells.Count > 5 && filterCells.Count < 9
                            let columns = filterCells.Select(c => c.Column).Distinct().ToList()
                            let rows = filterCells.Select(c => c.Row).Distinct().ToList()
                            where columns.Count == 3 && rows.Count == 3
                            let checkCells =
                                allUnSetCell.Where(c =>
                                    !columns.Contains(c.Column) && rows.Contains(c.Row) && c.GetRest().Count == 2 &&
                                    c.GetRest().Contains(value)).ToList()
                            from checkCell in checkCells
                            select new PositiveCellInfo(checkCell.Index, checkCell.GetRest().First(c => c != value)))
                .Cast<CellInfo>());

            return cells;
        }

        public List<CellInfo> FindCell(List<CellInfo> allUnSetCell, int value)
        {
            List<CellInfo> results=new List<CellInfo>();

            var containsell = allUnSetCell.Where(c => c.GetRest().Contains(value)).ToList();

               var indexss = (from index1 in G.baseIndexs
                join index2 in G.baseIndexs on 1 equals 1
                join index3 in G.baseIndexs on 1 equals 1
                where index1 < index2
                      && index2 < index3
                select new List<int> {index1, index2, index3}).ToList();

               foreach (var fiter in indexss.Select(indexlist => containsell.Where(c=>indexlist.Contains(c.Row)).ToList()).Where(fiter => fiter.Count()>5&&fiter.Count()<9&&G.DistinctRow(fiter).Count == 3 && G.DistinctColumn(fiter).Count == 3))
               {
                   results= fiter;
               }
               return results;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
