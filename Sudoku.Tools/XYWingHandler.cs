using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(6,"R8C7","860035900700068351530074020070810530005307100183540200020650703057400002010700495")]
    public class XYWingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XYWing;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCells.Where(c => c.RestCount == 2).ToList();
            foreach (var checkCell in checkCells)
            {

                var checkCellRest = checkCell.RestList;
                var relatedCell = checkCell.RelatedUnsetCells;
                var xy = (from x in relatedCell
                          join y in relatedCell on 1 equals 1
                          let xrest = x.RestList
                          let yrest = y.RestList
                          where x.Index < y.Index
                                && xrest.Count == 2
                                && yrest.Count == 2
                                && checkCellRest.Intersect(xrest).Count() == 1
                                && checkCellRest.Intersect(yrest).Count() == 1
                                && xrest.Except(checkCellRest).First() == yrest.Except(checkCellRest).First()
                                && xrest.JoinString() != yrest.JoinString()
                          select new { x, y, xrest, yrest }).ToList();
                foreach (var item in xy)
                {
                    var x = item.x;
                    var y = item.y;
                    var xrest = item.xrest;
                    var yrest = item.yrest;
                    var removes = xrest.Intersect(yrest).ToList();
                    if (checkCellRest.Contains(xrest.Except(removes).First()) && checkCellRest.Contains(yrest.Except(removes).First()))
                    {
                        var removeValue = removes.First();
                        foreach (var unsetCell in qSudoku.GetPublicUnsetAreas(item.x, item.y).Where(c => c.Index != checkCell.Index))
                        {
                            var unsetCellRest = unsetCell.RestList;
                            if (unsetCellRest.Contains(removeValue))
                            {
                                cells.Add(new NegativeCell(unsetCell.Index, removeValue) { Sudoku = qSudoku });
                            }
                        }
                    }



                }
            }

            return cells;
        }
    }
}
