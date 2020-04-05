using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [EliminationExample(1,"R4C1", "000840070010000805007105690076582000000471000000693700035914207709000510020050000")]
  public  class NakedQuadrupleWithLockedCandidatesHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;
            List<int> possibleCount = new List<int> { 2, 3, 4 };
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
                                    let indexs = G.MergeCellIndexs(a, b, c, d)
                                    let rests = G.MergeCellRest(a, b, c, d)
                                    where indexs.Count() == 4
                                       && rests.Count() == 4
                                    select new { a, b, c, d, indexs, rests }).ToList();
                        foreach (var item in list)
                        {
                            var a = item.a;
                            var b = item.b;
                            var c = item.c;
                            var d = item.d;
                            var rests = item.rests;
                            rests.Sort();
                            var drawCells = GetDrawNakedCell(new List<CellInfo> { a, b, c, d }, rests);
                            var indexs = item.indexs;
                            var filterCell = allUnSetCell.Where(G.GetDirectionCells(direction, index)).Where(c => !indexs.Contains(c.Index)).ToList();
                            drawCells.AddRange(GetDrawNegativeCell(rests,filterCell ));
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
                                        negativeCell.SolveMessages = new List<SolveMessage> { "在", GetDirectionMessage(direction, index), "中", G.MergeLocationDesc(a, b, c, d), "只出现了" + rests.JoinString() + "四个数\t\t\r\n", negativeCell.Location, "不能为" + removeValue + "\t\t\r\n" };
                                    
                                        //cells.Add(negativeCell);
                                    }
                                    else
                                    {
                                        var removeValue = interactList;
                                        var negativeCell = new NegativeValuesGroup(cell.Index, removeValue, qSudoku)
                                        {
                                            drawCells = drawCells
                                        };
                                        negativeCell.SolveMessages = new List<SolveMessage> { "在", GetDirectionMessage(direction, index), "中", G.MergeLocationDesc(a, b, c, d), "只出现了" + rests.JoinString() + "四个数\t\t\r\n", negativeCell.Location, "不能为" + removeValue.JoinString() + "\t\t\r\n" };
                                   //     cells.Add(negativeCell);
                                    }
                                }


                            }

                        }


                    }
                }

            }
            return cells;
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.NakedQuadrupleWithLockedCandidates;
        public override MethodClassify methodClassify { get; }
        public override string GetDesc()
        {
            return "若一个宫内的显性四数组中，候选数a只出现在了某行/某列，则其余宫的该行该列不包含该候选数。\t\t\r\n" +
                   "若一个行内的显性四数组中，候选数a只出现在了某宫中，则该宫的其余行不包含该候选数。\t\t\r\n" +
                   "若一个列内的显性四数组中，候选数a只出现在了某宫中，则该宫的其余列不包含该候选数。\t\t\r\n";
        }

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }
    }
}
