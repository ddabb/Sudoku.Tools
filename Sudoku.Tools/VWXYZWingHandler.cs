using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [EliminationExample(1,"R6C6","893000076756080304142673859580020003429036705630000002214568937368297541975314008")]
    public class VWXYZWingHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.VWXYZWing;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCells.Where(c => c.RestCount == 5).ToList();
            List<int> countRange = new List<int> { 2, 3 };
            foreach (var checkcell in checkCells)
            {
                var checkcellRest = checkcell.RestList;
                var relatedCell = checkcell.RelatedUnsetCells.Where(c =>
                    countRange.Contains(c.RestCount) && c.RestList.Intersect(checkcellRest).Any()).ToList();

                var filter = (from x in relatedCell
                              join y in relatedCell on 1 equals 1
                              join z in relatedCell on 1 equals 1
                              join w in relatedCell on 1 equals 1
                              let indexs = new List<int> { w.Index,x.Index, y.Index, z.Index, checkcell.Index }
                              let wrest = w.RestList
                              let xrest = x.RestList
                              let yrest = y.RestList
                              let zrest = z.RestList
                              where indexs.Distinct().Count() == 5
                                    && w.Index < x.Index && x.Index < y.Index && y.Index < z.Index
                                    && xrest.Intersect(yrest).Intersect(zrest).Count() == 1
                                    && wrest.Count == 2
                                    && xrest.Count == 2
                                    && yrest.Count == 2
                                    && zrest.Count == 2
                                    && wrest.All(c => checkcellRest.Contains(c))
                                    && xrest.All(c => checkcellRest.Contains(c))
                                    && yrest.All(c => checkcellRest.Contains(c))
                                    && zrest.All(c => checkcellRest.Contains(c))
                                    && new List<string> {w.RestString, x.RestString, y.RestString, z.RestString }.Distinct().Count() == 4
                              select new { indexs,w, x, y, z, xrest, yrest, zrest }).Distinct().ToList();
                foreach (var item in filter)
                {
                    var xyzRest = item.xrest.Intersect(item.yrest).Intersect(item.zrest).ToList();
                    if (xyzRest.Count == 1)
                    {
                        var x = item.x;
                        var y = item.y;
                        var z = item.z;
                        var w = item.w;
                        var intersectValue = xyzRest.First();
                        var publicIndexs = qSudoku.GetPublicUnsetAreaIndexs(checkcell, item.w, item.x, item.y, item.z);
                        foreach (var index in publicIndexs)
                        {
                            var findCellRest = qSudoku.GetRest(index);
                            if (findCellRest.Contains(intersectValue))
                            {
                                var negativeCell = new NegativeCell(index, intersectValue, qSudoku);
                                negativeCell.drawCells.AddRange(GetDrawPossibleCell(x.RestList, x));
                                negativeCell.drawCells.AddRange(GetDrawPossibleCell(y.RestList, y));
                                negativeCell.drawCells.AddRange(GetDrawPossibleCell(z.RestList, z));
                                negativeCell.drawCells.AddRange(GetDrawPossibleCell(w.RestList, w));
                                negativeCell.drawCells.AddRange(GetDrawPossibleCell(checkcell.RestList, checkcell));
                                var message1 = G.MergeLocationDesc(w,x, y, z, checkcell);
                                negativeCell.SolveMessages = new List<SolveMessage>
                                {
                                    message1 ,"包含了",checkcell.RestString,"五个候选数，"
                                    ,"且",checkcell.Location,"位于",G.MergeLocationDesc(w,x, y, z),"的共同相关格上\t\t\r\n",
                                    "所以",message1,"的共同相关格上不包含共同值"+intersectValue+"\t\t\r\n"
                                };
                                negativeCell.drawCells.Add(negativeCell);
                                cells.Add(negativeCell);
                            }
                        }

                        var list1 = qSudoku.AllUnSetCells
                                    .Where(c => publicIndexs.Contains(c.Index) && c.RestList.Contains(intersectValue)).ToList();
                        if (list1.Count > 1)
                        {
                            var indexs = list1.Select(c => c.Index).ToList();
                            var cellgroup = new NegativeIndexsGroup(indexs, intersectValue,
                                qSudoku);
                            cellgroup.drawCells.AddRange(GetDrawPossibleCell(x.RestList, x));
                            cellgroup.drawCells.AddRange(GetDrawPossibleCell(y.RestList, y));
                            cellgroup.drawCells.AddRange(GetDrawPossibleCell(z.RestList, z));
                            cellgroup.drawCells.AddRange(GetDrawPossibleCell(checkcell.RestList, checkcell));
                            foreach (var index in indexs)
                            {
                                cellgroup.drawCells.AddRange(GetDrawNegativeCell(intersectValue, qSudoku.GetCell(index)));
                            }
                            var message1 = G.MergeLocationDesc(w,x, y, z, checkcell);
                            cellgroup.SolveMessages = new List<SolveMessage>
                            {
                                message1 ,"包含了",checkcell.RestString,"五个候选数，"
                                ,"且",checkcell.Location,"位于",G.MergeLocationDesc(w,x, y, z),"的共同相关格上\t\t\r\n",
                                "所以",message1,"的共同相关格上不包含共同值"+intersectValue+"\t\t\r\n"
                            };
                            cells.Add(cellgroup);
             
                        }



                    }

                }

            }

            return cells;
        }

        public override string GetDesc()
        {
            return "若五个单元格的候选数满足VZ,WZ,XZ,YZ,WXYZ形式，且VWXYZ位于VZ,WZ,XZ,YZ的共同相关格上，则VZ,WZ,XZ,YZ,XYZ的其余共同相关格不包含候选数Z。";
        }
    }
}
