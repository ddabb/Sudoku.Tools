using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample(5, "R1C6", "390000700000000650507000349049380506601054983853000400900800134002940865400000297")] //已调整
    public class NakedQuadrupleHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.NakedQuadruple;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {

            return AssignmentCellByEliminationCell(qSudoku);
        }


        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;
            List<int> possibleCount = new List<int> { 2, 3,4 };
            //只有2或3个候选数的单元格
            var twoOrThreeRests = allUnSetCell.Where(c => possibleCount.Contains(c.RestCount)).ToList();
            foreach (var direction in G.AllDirection)
            {
                foreach (var index in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkCells = twoOrThreeRests.Where(G.GetDirectionCells(direction, index)).ToList();
                    if (checkCells.Count() > 3)
                    {
                        var list = (from a in checkCells
                                    join b in checkCells on 1 equals 1
                                    join c in checkCells on 1 equals 1
                                    join d in checkCells on 1 equals 1
                                    let indexs = G.MergeCellIndexs(a, b, c,d)
                                    let rests = G.MergeCellRest(a, b, c,d)
                                    where indexs.Count() == 4
                                       && rests.Count() == 4
                                    select new { a, b, c,d, indexs, rests }).ToList();
                        foreach (var item in list)
                        {
                            var a = item.a;
                            var b = item.b;
                            var c = item.c;
                            var d = item.d;
                            var rests = item.rests;
                            rests.Sort();
                            var drawCells = GetDrawNakedCell(new List<CellInfo> { a, b, c,d }, rests);
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
                                        var negativeCell = new NegativeCell(cell.Index, removeValue, qSudoku)
                                        {
                                            drawCells = drawCells
                                        };
                                        negativeCell.SolveMessages = new List<SolveMessage> { "在", GetDirectionMessage(direction, index), "中", G.MergeLocationDesc(a, b,c,d),  "只出现了" + rests.JoinString() + "四个数\r\n", negativeCell.Location, "不能为" + removeValue+"\r\n" };
                                        cells.Add(negativeCell);
                                    }
                                    else
                                    {
                                        var removeValue = interactList;
                                        var negativeCell = new NegativeValuesGroup(cell.Index, removeValue, qSudoku)
                                        {
                                            drawCells = drawCells
                                        };
                                        negativeCell.SolveMessages = new List<SolveMessage> { "在", GetDirectionMessage(direction, index), "中", G.MergeLocationDesc(a, b, c, d), "只出现了" + rests.JoinString() + "四个数\r\n", negativeCell.Location, "不能为" + removeValue.JoinString() + "\r\n" };
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
            return @"在某个行/列/宫内,若四个单元格只能填入a、b、c、d四个数,则该行/列/宫的其余单元格不能填入a、b、c、d。";
        }
    }
}
