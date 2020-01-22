using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(8, "R9C2", "869453721000921568215800439621534987407610352000200146000102803932785614100340205")]
    public class XYZWingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XYZWing;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var unsetsell = qSudoku.AllUnSetCells;
            var checkCells = qSudoku.AllUnSetCells.Where(c => c.GetRest().Count == 3).ToList();
            foreach (var checkCell in checkCells)
            {
                var cellrest = checkCell.GetRest();
                var relatedCell = checkCell.RelatedUnsetCells.Where(c => c.GetRest().Count == 2 && c.GetRest().Intersect(checkCell.GetRest()).Count() > 1).ToList();
                var cell = (from a in relatedCell
                            join b in relatedCell on 1 equals 1
                            join cellInfo in qSudoku.AllUnSetCells.Where(c => c.GetRest().Count == 2) on 1 equals 1
                            let arest = a.GetRest()
                            let brest = b.GetRest()
                            let cellRest = cellInfo.GetRest()
                            let comvalue = cellrest.Intersect(a.GetRest()).Intersect(b.GetRest()).First()
                            let cellInforest = cellInfo.GetRest()
                            let restvalue = cellRest.First(c => c != comvalue)
                            where a.Index < b.Index
                          && cellInfo.Index != a.Index
                          && cellInfo.Index != b.Index
                          && cellInfo.Index != checkCell.Index
                          && cellInforest.Contains(comvalue)
                          && qSudoku.GetPublicUnsetAreaIndexs(a, checkCell).Contains(cellInfo.Index)
                          && qSudoku.GetPublicUnsetAreaIndexs(b, checkCell).Contains(cellInfo.Index)
                            select new { cellInfo, comvalue, a, b, restvalue, checkCells }).ToList();
                cells.AddRange(cell.Select(item => new PositiveCellInfo(item.cellInfo.Index, item.restvalue)).Cast<CellInfo>());
            }

            return cells;

        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
