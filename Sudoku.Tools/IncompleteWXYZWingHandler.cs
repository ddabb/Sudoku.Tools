﻿using Sudoku.Core;
using Sudoku.Core.Model;
using System.Collections.Generic;
using System.Linq;
namespace Sudoku.Tools
{
    [AssignmentExample(7, "R6C4", "000040601001560204006100079607010900019600007020090816532971468978436100164258793")]
    public class IncompleteWXYZWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;
            var checkCells = allUnSetCell.Where(c => c.RestCount == 3).ToList();
            foreach (var checkCell in checkCells)
            {
                var checkCellRest = checkCell.RestList;
                var relatedCell = checkCell.RelatedUnsetCells
                    .Where(c => c.RestCount == 2 && c.RestList.Intersect(checkCellRest).Count() == 1).ToList();
                var filter = (from a in relatedCell
                              join b in relatedCell on 1 equals 1
                              join c in relatedCell on 1 equals 1
                              let indexs = new List<int> { a.Index, b.Index, c.Index, checkCell.Index }
                              let arest = a.RestList
                              let brest = b.RestList
                              let crest = c.RestList
                              let removeValue = arest.Except(checkCellRest).First()
                              where arest.Except(checkCellRest).First() == brest.Except(checkCellRest).First()
                                    && arest.Except(checkCellRest).First() == crest.Except(checkCellRest).First()
                                    && a.Index < b.Index && b.Index < c.Index
                              select new { a, b, c, removeValue, indexs }).ToList();
                foreach (var item in filter)
                {
                    var a = item.a;
                    var b = item.b;
                    var c = item.c;
                    var publicIndexs = item.a.RelatedUnsetIndexs
                        .Intersect(item.b.RelatedUnsetIndexs)
                        .Intersect(item.c.RelatedUnsetIndexs).ToList();
                    var intersectValue = item.removeValue;
                    var message = G.MergeLocationDesc(checkCell, a, b, c);
                    var message1 = G.MergeLocationDesc(a, b, c);
                    foreach (var index in publicIndexs)
                    {
                        if (qSudoku.GetRest(index).Contains(intersectValue))
                        {
                            var cell = new NegativeCell(index, intersectValue, qSudoku);
                            cell.drawCells.AddRange(GetDrawPossibleCell(a.RestList, a));
                            cell.drawCells.AddRange(GetDrawPossibleCell(b.RestList, b));
                            cell.drawCells.AddRange(GetDrawPossibleCell(c.RestList, c));
                            cell.drawCells.AddRange(GetDrawPossibleCell(checkCell.RestList, checkCell));
                            cell.drawCells.Add(cell);
                            var checkCellRest1 = checkCellRest.ToList();
                            checkCellRest1.Add(intersectValue);
                            checkCellRest1.Sort();
                            cell.SolveMessages = new List<SolveMessage>
                            {
                                message,"的候选数满足WZ,XZ,YZ,WXY形式。","其中Z为"+intersectValue+"\t\t\r\n","且"+checkCell.Location+"位于",message1,"共同相关格上；"
                                ,message1,"的其余共同相关格不包含候选数"+intersectValue+"\t\t\r\n"
                            };
                            cells.Add(cell);
                        }
                    }
                    var list1 = qSudoku.AllUnSetCells
                        .Where(c => publicIndexs.Contains(c.Index) && c.RestList.Contains(intersectValue)).ToList();
                    if (list1.Count > 1)
                    {
                        var group = new NegativeIndexsGroup(list1.Select(cellInfo => cellInfo.Index).ToList(), intersectValue,
                            qSudoku);
                        group.drawCells.AddRange(GetDrawPossibleCell(a.RestList, a));
                        group.drawCells.AddRange(GetDrawPossibleCell(b.RestList, b));
                        group.drawCells.AddRange(GetDrawPossibleCell(c.RestList, c));
                        group.drawCells.AddRange(GetDrawPossibleCell(checkCell.RestList, checkCell));
                        group.drawCells.AddRange(GetDrawNegativeCell(intersectValue, list1));
                        group.SolveMessages = new List<SolveMessage>
                        {
                            message,"的候选数满足VZ,WZ,XZ,YZ,VWXY形式。","其中Z为"+intersectValue+"\t\t\r\n","且"+checkCell.Location+"位于",message1,"共同相关格上；"
                            ,message1,"的其余共同相关格不包含候选数"+intersectValue+"\t\t\r\n"
                        };
                        cells.Add(group);
                    }
                }
            }
            return cells;
        }
        public override SolveMethodEnum methodType => SolveMethodEnum.IncompleteWXYZWing;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }
        public override string GetDesc()
        {
            return "若四个单元格的候选数满足WZ,XZ,YZ,WXY形式，且WXY位于WZ,XZ,YZ的共同相关格上，则WZ,XZ,YZ的其余共同相关格不包含候选数Z。";
        }
    }
}
