using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample(3, "R8C2", "980006375376850140000700860569347218000000537723581496000205780000000950000008620")]
    public class NakedPairHandller : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.NakedPair;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }



        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;
            foreach (var direction in G.AllDirection)
            {
                foreach (var index in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkCells = allUnSetCell.Where(G.GetDirectionCells(direction, index)).ToList();
                    if (checkCells.Count() > 2)
                    {
                        var list = (from a in checkCells
                                    join b in checkCells on 1 equals 1
                             
                                    let indexs = G.MergeCellIndexs(a, b)
                                    let rests = G.MergeCellRest(a, b)
                                    where indexs.Count() == 2
                                       && rests.Count() == 2
                                    select new { a, b, indexs, rests }).ToList();
                        foreach (var item in list)
                        {
                            var a = item.a;
                            var b = item.b;
               
                            var rests = item.rests;
                            rests.Sort();
                            var drawCells = GetDrawNakedCell(new List<CellInfo> { a, b}, rests);
                            var indexs = item.indexs;
                            var filterCell = allUnSetCell.Where(G.GetDirectionCells(direction, index)).Where(c => !indexs.Contains(c.Index)).ToList();
                            drawCells.AddRange(GetDrawNegativeCell(filterCell, rests));
                            foreach (var cell in filterCell)
                            {
                                var interactList = cell.RestList.Intersect(rests).ToList();
                                if (interactList.Any())
                                {
                                    if (interactList.Count() == 1)
                                    {
                                        var removeValue = interactList.First();
                                        var negativeCell = new NegativeCell(cell.Index, removeValue, qSudoku);
                                        negativeCell.drawCells = drawCells;
                                        negativeCell.SolveMessages = new List<SolveMessage> { "在", GetDirectionMessage(direction, index), "中", G.MergeLocationDesc(a, b),"只出现了" + rests.JoinString() + "两个数\r\n", negativeCell.Location, "不能为" + removeValue + "\r\n" };
                                        cells.Add(negativeCell);
                                    }
                                    else
                                    {
                                        var removeValue = interactList;
                                        var negativeCell = new NegativeValuesGroup(cell.Index, removeValue, qSudoku)
                                        {
                                          drawCells = drawCells
                                        };
                                        negativeCell.SolveMessages = new List<SolveMessage> { "在", GetDirectionMessage(direction, index), "中", G.MergeLocationDesc(a, b), "只出现了" + rests.JoinString() + "两个数\r\n", negativeCell.Location, "不能为" + removeValue.JoinString() + "\r\n" };
                                        cells.Add(negativeCell);
                                    }
                                }


                            }

                        }


                    }
                }

            }

            return cells;
        }

        public override string GetDesc()
        {
            return @"在某个行/列/宫内,若两个单元格都只能填入a或者b,则该行/列/宫的其余单元格不能填入a或者b。";
        }
    }
}
