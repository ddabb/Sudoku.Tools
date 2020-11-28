using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    [AssignmentExample(8, "R9C4", "900164080070983215813200964080020000500001070001000042040716000000000000007092000")] //已调整
    public class HiddenQuadrupleHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.HiddenQuadruple;
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
                    if (checkCells.Count > 4)
                    {
                        Dictionary<CellInfo, List<int>> indexRest = new Dictionary<CellInfo, List<int>>();
                        var allRests = new List<int>();
                        foreach (var cell in checkCells)
                        {
                            indexRest.Add(cell, cell.RestList);
                            allRests.AddRange(cell.RestList);
                        }
                        var restValues = allRests.Distinct().ToList();
                        if (restValues.Count > 4)
                        {
                            var keyCell = (from a in restValues
                                           join b in restValues on 1 equals 1
                                           join c in restValues on 1 equals 1
                                           join d in restValues on 1 equals 1
                                           let kvs = checkCells.Where(cell => cell.RestList.Contains(a) || cell.RestList.Contains(b) || cell.RestList.Contains(c) || cell.RestList.Contains(d)).ToList()
                                           let values = new List<int> { a, b, c, d }
                                           where a < b && b < c && c < d
                                           && kvs.Count() == 4
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
                                var cell4 = groupCells[3];
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
                                            var negativeCell = new NegativeCell(cell.Index, rest, qSudoku) ;
                                            negativeCell.SolveMessages = new List<SolveMessage>
                                            {
                                                "在",GetDirectionMessage(direction,index),"中",values.JoinString()+"只出现在",G.MergeLocationDesc(cell1,cell2,cell3,cell4),"之中\t\t\r\n所以",
                                                cell.Location,"不能填入"+rest+"\t\t\r\n"
                                            };
                                            var drawCells = GetDrawNakedCell(groupCells, values);
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
                                        var negativeCell = new NegativeIndexsGroup(indexs, other, qSudoku) ;
                                        negativeCell.SolveMessages = new List<SolveMessage>
                                            {
                                                "在",GetDirectionMessage(direction,index),"中",values.JoinString()+"只出现在",G.MergeLocationDesc(cell1,cell2,cell3,cell4),"之中\t\t\r\n所以",
                                            };
                                        negativeCell.SolveMessages.Add(G.MergeLocationDesc(cell1, cell2, cell3, cell4));
                                        negativeCell.SolveMessages.Add("不能填入" + other + "\t\t\r\n");
                                        var drawCells = GetDrawNakedCell(groupCells, values);
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
            return @"若在某行/列/宫中，候选者a,b,c,d只在单元格A,B,C,D中出现,则A,B,C,D只能填入a,b,c,d;可以删除A,B,C,D单元格中a,b,c,d以外的候选数。";
        }
    }
}
