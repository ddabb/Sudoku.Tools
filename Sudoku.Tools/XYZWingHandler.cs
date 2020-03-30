using Sudoku.Core;
using Sudoku.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(8, "R9C2", "869453721000921568215800439621534987407610352000200146000102803932785614100340205")]
    public class XYZWingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XYZWing;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);

        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var unsetsell = qSudoku.AllUnSetCells;
            var checkCells = qSudoku.AllUnSetCells.Where(c => c.RestCount == 3).ToList();
            foreach (var checkCell in checkCells)
            {
                var cellrest = checkCell.RestList;
                var relatedCell = checkCell.RelatedUnsetCells.Where(c => c.RestCount == 2 && c.RestList.Intersect(checkCell.RestList).Count() == 2).ToList();
                var cell = (from a in relatedCell
                            join b in relatedCell on 1 equals 1
                            join cellInfo in qSudoku.AllUnSetCells on 1 equals 1
                            let arest = a.RestList
                            let brest = b.RestList
                            let cellRest = cellInfo.RestList
                            let comvalue = cellrest.Intersect(a.RestList).Intersect(b.RestList).First()
                            let cellInforest = cellInfo.RestList
                            let restvalue = cellRest.First(c => c != comvalue)
                            where a.Index < b.Index
                                  && cellInfo.Index != a.Index
                                  && cellInfo.Index != b.Index
                                  && arest.All(c => cellrest.Contains(c))
                                  && brest.All(c => cellrest.Contains(c))
                                  && cellInfo.Index != checkCell.Index
                                  && a.RestString != b.RestString
                                  && cellInforest.Contains(comvalue)
                                  && qSudoku.GetPublicUnsetAreaIndexs(a, checkCell, b).Contains(cellInfo.Index)
                            select new { cellInfo, comvalue, a, b, restvalue, checkCells }).ToList();

                foreach (var item in cell)
                {
                    var cellSingle = new NegativeCell(item.cellInfo.Index, item.comvalue, qSudoku);
                    cells.Add(cellSingle);
                }

                var indexs = cell.Select(c => c.cellInfo.Index).ToList();
                if (indexs.Count > 1)
                {
                    var comvalue = cell.First().comvalue;
                    cells.Add(new NegativeIndexsGroup(indexs, comvalue, qSudoku));
                }

            }

            return cells;
        }

        public override string GetDesc()
        {
            return "若三个单元格的候选数满足XZ,YZ,XYZ形式，且XYZ位于XZ,YZ的共同相关格上，则XZ,YZ,XYZ的其余共同相关格不包含候选数Z。";
        }
    }
}
