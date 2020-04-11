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
                       
                            let arest = a.RestList
                            let brest = b.RestList
                            let comvalue = cellrest.Intersect(a.RestList).Intersect(b.RestList).First()
                            where a.Index < b.Index
                                  && arest.All(c => cellrest.Contains(c))
                                  && brest.All(c => cellrest.Contains(c))
                                  && a.RestString != b.RestString
                            select new {  comvalue, a, b, checkCells }).ToList();

                foreach (var item in cell)
                {
                    var a = item.a;
                    var b = item.b;
                    var comvalue = item.comvalue;
                    var filterCell = qSudoku.GetPublicUnsetAreaIndexs(a, checkCell, b)
                        .Where(c => qSudoku.GetRest(c).Contains(comvalue)).ToList();
                    if (filterCell.Count>0)
                    {
                        var drawCells = new List<CellInfo>();
                        drawCells.AddRange(GetDrawPossibleCell(a.RestList, a));
                        drawCells.AddRange(GetDrawPossibleCell(b.RestList, b));
                        drawCells.AddRange(GetDrawPossibleCell(checkCell.RestList, checkCell));
                        drawCells.AddRange(GetDrawNegativeCell(comvalue, qSudoku.AllCell.Where(c=>filterCell.Contains(c.Index)).ToList()));
                        
                        foreach (var cellinfo in qSudoku.GetPublicUnsetAreaIndexs(a, checkCell, b))
                        {
                            var cellSingle = new NegativeCell(cellinfo, comvalue, qSudoku);
                            var message1 = G.MergeLocationDesc(a, b, checkCell);
                            cellSingle.SolveMessages=new List<SolveMessage>
                            {
                                message1 ,"包含了",checkCell.RestString,"三个候选数，"
                                ,"且",checkCell.Location,"位于",G.MergeLocationDesc(a,b),"的共同相关格上\t\t\r\n",
                                "所以",message1,"的共同相关格上不包含共同值"+comvalue+"\t\t\r\n"
                            };
                            cellSingle.drawCells.AddRange(drawCells);
                            cells.Add(cellSingle);
                        }

                        if (filterCell.Count > 1)
                        {
                            var c = new NegativeIndexsGroup(filterCell, comvalue, qSudoku);
                            var message1 = G.MergeLocationDesc(a, b, checkCell);
                            c.SolveMessages = new List<SolveMessage>
                            {
                                message1 ,"包含了",checkCell.RestString,"三个候选数，"
                                ,"且",checkCell.Location,"位于",G.MergeLocationDesc(a,b),"的共同相关格上\t\t\r\n",
                                "所以",message1,"的共同相关格上不包含共同值"+comvalue+"\t\t\r\n"
                            };
                            c.drawCells.AddRange(drawCells);
                            cells.Add(c);
                        }

                    }




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
