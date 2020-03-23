using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [EliminationExample(2, "R8C1", "000058793093074185578931462010582934005169000982347651054726019009015006060093500")]
    public class SplitWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {

            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCells = qSudoku.AllUnSetCells;
            var pairCells = allUnsetCells.Where(c => c.RestCount == 2).ToList();
            foreach (var item in pairCells)
            {
                var rest = item.RestList;
                var value1 = rest[0];//2
                var value2 = rest[1];//7
                var filterCells = allUnsetCells.Where(c => c.RestList.Contains(value1) && c.RestList.Contains(value2) && c.Index != item.Index).ToList();
                if (filterCells.Count > 3)
                {

                    //a R8C8 7
                    //b R9C8 2
                    //c R8C1 7
                    //d R9C1 2
                    var keyCells = (from a in filterCells
                                    join b in filterCells on 1 equals 1
                                    join c in filterCells on 1 equals 1
                                    join d in filterCells on 1 equals 1
                                    let indexs = G.MergeCellIndexs(a, b, c, d).ToList()
                                    let cellLists = G.MergeCells(a, b, c, d).ToList()
                                    let abCells = G.MergeCells(a, b, item).ToList()
                                    let aNextCell = new NegativeCell(a.Index, value2, qSudoku).NextCells
                                    let bNextCell = new NegativeCell(b.Index, value1, qSudoku).NextCells
                                    let cNextCell = new NegativeCell(c.Index, value2, qSudoku).NextCells
                                    let dNextCell = new NegativeCell(d.Index, value1, qSudoku).NextCells

                                    where indexs.Count() == 4

                                          && aNextCell.Exists(x => x.Index == c.Index && x.Value == value2)
                                          && bNextCell.Exists(x => x.Index == d.Index && x.Value == value1)
                                          && cNextCell.Exists(x => x.Index == a.Index && x.Value == value2)
                                          && dNextCell.Exists(x => x.Index == b.Index && x.Value == value1)
                                          //&& (c.Row == d.Row || c.Block == d.Block || c.Column == d.Column)
                                          && a.Block == b.Block
                                          && c.Block == d.Block
                                          && a.Index < b.Index
                                          && c.Index < d.Index
                                          && cellLists.Select(c => c.Row).Distinct().Count() == 2
                                          && cellLists.Select(c => c.Column).Distinct().Count() == 2
                                          && cellLists.Select(c => c.Block).Distinct().Count() == 2
                                          && (abCells.Select(c => c.Row).Distinct().Count() == 1 || abCells.Select(c => c.Column).Distinct().Count() == 1)
                                          && abCells.Select(c => c.Block).Distinct().Count() == 2
                                    select new { a, b, c, d, cellLists }
                      ).ToList();



                    foreach (var keyCell in keyCells)
                    {
                        var a = keyCell.a;
                        var b = keyCell.b;
                        var c = keyCell.c;
                        var d = keyCell.d;

                        var pa = new PositiveCell(a.Index, value2, qSudoku) .NextCells;  //a R8C8 7
                        var pb = new PositiveCell(b.Index, value1, qSudoku) .NextCells; //b R9C8 2
                        if (pa.Exists(x => x.Index == item.Index && x.Value == value2) && pb.Exists(x => x.Index == item.Index && x.Value == value1))


                        {
                            var cell1 = new NegativeCell(c.Index, value1, qSudoku)
                            {
             
                                SolveMessages = MergeCellSolveLocationMessage(item, a, b, c, d)
                            };
                            cell1.SolveMessages.AddRange(new List<SolveMessage> { "value1是", value1 + "\r\n" });
                            cell1.SolveMessages.AddRange(new List<SolveMessage> { "value2是", value2 + "\r\n" });
                            cell1.SolveMessages.AddRange(new List<SolveMessage> { c.Index.LoctionDesc(), "不能为", value1 });
                            var cell2 = new NegativeCell(d.Index, value2, qSudoku)
                            {
                       
                                SolveMessages = MergeCellSolveLocationMessage(item, a, b, c, d)
                            };
                            cell2.SolveMessages.AddRange(new List<SolveMessage> { "value1是", value1 + "\r\n" });
                            cell2.SolveMessages.AddRange(new List<SolveMessage> { "value2是", value2 + "\r\n" });
                            cell2.SolveMessages.AddRange(new List<SolveMessage> { d.Index.LoctionDesc(), "不能为", value2 });

                            cells.Add(cell1);
                            cells.Add(cell2);
                        }


                    }
                }
            }

            return cells;
        }

        public override string GetDesc()
        {
            return "";
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.SplitWing;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
    }
}
