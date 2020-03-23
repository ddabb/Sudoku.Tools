using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample(8, "R3C6", "390000700000000650507000349049380506601054983853000400900800134002940865400000297")]
    public class NakedTripleHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.NakedTriple;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }




        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {

            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;
            List<int> possibleCount = new List<int> { 2, 3 };
            //只有2或3个候选数的单元格
            var twoOrThreeRests = allUnSetCell.Where(c => possibleCount.Contains(c.RestCount)).ToList();
            foreach (var direction in G.AllDirection)
            {
                foreach (var index in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkCells = twoOrThreeRests.Where(G.GetDirectionCells(direction, index)).ToList();
                    if (checkCells.Count() > 2)
                    {
                        var list = (from a in checkCells
                                    join b in checkCells on 1 equals 1
                                    join c in checkCells on 1 equals 1
                                    let indexs = G.MergeCellIndexs(a, b, c)
                                    let rests = G.MergeCellRest(a, b, c)
                                    where indexs.Count() == 3
                                       && rests.Count() == 3
                                    select new { a, b, c, indexs, rests }).ToList();
                        foreach (var item in list)
                        {
                            var a = item.a;
                            var b = item.b;
                            var c = item.c;
                            var rests = item.rests;
                            rests.Sort();
                            var drawCells = GetDrawNakedCell(new List<CellInfo> { a, b, c }, rests);
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
                                        negativeCell.SolveMessages = new List<SolveMessage> { "在", GetDirectionMessage(direction, index), "中", G.MergeLocationDesc(a, b, c), "只出现了" + rests.JoinString() + "三个数\r\n", negativeCell.Location, "不能为" + removeValue };
                                        cells.Add(negativeCell);
                                    }
                                    else
                                    {
                                        var removeValue = interactList;
                                        var negativeCell = new NegativeValuesGroup(cell.Index, removeValue, qSudoku) ;
                                        negativeCell.drawCells = drawCells;
                                        negativeCell.SolveMessages = new List<SolveMessage> { "在", GetDirectionMessage(direction, index), "中", G.MergeLocationDesc(a, b, c), "只出现了" + rests.JoinString() + "三个数\r\n", negativeCell.Location, "不能为" + removeValue.JoinString() };
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
            return @"在某个行/列/宫内,若三个单元格只能填入a、b、c三个数,则该行/列/宫的其余单元格不能填入a、b、c。";
        }
    }
}
