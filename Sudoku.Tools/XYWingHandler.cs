using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Sudoku.Core.Model;

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
                        var filter = qSudoku.GetPublicUnsetAreas(item.x, item.y).Where(c => c.Index != checkCell.Index).ToList();
                        var drawCells = GetDrawNegativeCell(filter, removes);
                        drawCells.AddRange(GetDrawPossibleCell(new List<CellInfo> { x, y, checkCell }, G.MergeCellRest(x, y, checkCell)));
                        foreach (var unsetCell in filter)
                        {
                            var unsetCellRest = unsetCell.RestList;
                            if (unsetCellRest.Contains(removeValue))
                            {
                                var negativeCell = new NegativeCell(unsetCell.Index, removeValue, qSudoku) ;
                                negativeCell.SolveMessages = new List<SolveMessage>
                                {
                                    G.MergeLocationDesc(checkCell,x,y),"构成xy-Wing\r\n",G.MergeLocationDesc(x,y),"共同影响区域不能为"+removes.JoinString()+"\r\n"
                                };
                                negativeCell.drawCells = drawCells;
                                cells.Add(negativeCell);
                            }
                        }
                    }



                }
            }

            return cells;
        }

        public override string GetDesc()
        {
            return "若三个双候选单元格的候选数满足XZ,YZ,XY形式，且XY位于XZ,YZ的共同区域上，则XZ,YZ的其余共同区域不包含候选数Z。";
        }
    }
}
