using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(4, "R1C9", "015070000900040105834651000050097018108065000000180526403518000060030851581026043")]
    public class HiddenTripleHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.HiddenTriple;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var direction in G.AllDirection)
            {
                foreach (var index in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkCells = qSudoku.AllUnSetCells.Where(G.GetDirectionCells(direction, index)).ToList();

                    if (checkCells.Count > 3)
                    {
                        Dictionary<CellInfo, List<int>> indexRest = new Dictionary<CellInfo, List<int>>();
                        var allRests = new List<int>();
                        foreach (var cell in checkCells)
                        {
                            indexRest.Add(cell, cell.RestList);
                            allRests.AddRange(cell.RestList);
                        }
                        var restValues = allRests.Distinct().ToList();
                        if (restValues.Count > 3)
                        {
                            var keyCell = (from a in restValues
                                           join b in restValues on 1 equals 1
                                           join c in restValues on 1 equals 1
                                           let kvs = checkCells.Where(cell => cell.RestList.Contains(a) || cell.RestList.Contains(b) || cell.RestList.Contains(c)).ToList()
                                           let values = new List<int> { a, b, c }
                                           where a < b && b < c
                                           && kvs.Count() == 3
                                           select new { a, b, c, kvs, values }).ToList();
                            foreach (var item in keyCell)
                            {
                                var value1 = item.a;
                                var value2 = item.b;
                                var value3 = item.c;
                                var values = item.values;
                                values.Sort();
                                var kvs = item.kvs;
                                var groupCells = kvs;
                                var cell1 = groupCells[0];
                                var cell2 = groupCells[1];
                                var cell3 = groupCells[2];
                                var drawCell = new List<CellInfo>();
                                var allValues = new List<int>();

                                foreach (var kv in kvs)
                                {
                                    var cell = kv;
                                    allValues.AddRange(kv.RestList);
                                    foreach (var rest in kv.RestList)
                                    {
                                        if (!values.Contains(rest))
                                        {
                                            var negativeCell = new NegativeCell(cell.Index, rest) { Sudoku = qSudoku };
                                            negativeCell.SolveMessages = new List<SolveMessage>
                                            {
                                                "在",GetDirectionMessage(direction,index),"中",values.JoinString()+"只出现在",G.MergeLocationDesc(cell1,cell2,cell3),"之中\r\n所以",
                                                cell.Location,"不能填入"+rest+"\r\n"
                                            };
                                            var drawCells = GetDrawPossibleCell(groupCells, values);
                                            drawCells.Add(negativeCell);
                                            negativeCell.drawCells = drawCells;
                                            cells.Add(negativeCell);
                                        }

                                    }

                                }
                                var otherValues = allValues.Where(c => !values.Contains(c)).Distinct().ToList();
                                foreach (var other in otherValues)
                                {
                                    var filter = groupCells.Where(c => c.RestList.Contains(other)).ToList();
                                    if (filter.Count > 1)
                                    {
                                        var indexs = filter.Select(c => c.Index).ToList();
                                        var negativeCell = new NegativeIndexsGroup(indexs, other) { Sudoku = qSudoku };
                                        negativeCell.SolveMessages = new List<SolveMessage>
                                            {
                                                "在",GetDirectionMessage(direction,index),"中",values.JoinString()+"只出现在",G.MergeLocationDesc(cell1,cell2,cell3),"之中\r\n所以",

                                            };
                                        for (int i = 0; i < indexs.Count; i++)
                                        {
                                            negativeCell.SolveMessages.Add(indexs[i].LoctionDesc());
                                            if (i < indexs.Count - 1)
                                            {
                                                negativeCell.SolveMessages.Add("、");
                                            }
                                        }
                                        negativeCell.SolveMessages.Add("不能填入" + other + "\r\n");
                                        var drawCells = GetDrawPossibleCell(groupCells, values);
                                        negativeCell.drawCells = drawCells;
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

            return @"若在某行/列/宫中，候选者a,b,c只在单元格A,B,C中出现,则A,B,C只能填入a,b,c;可以删除A,B,C单元格中a,b,c以外的候选数。";
        }
    }
}
