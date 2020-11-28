using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    [AssignmentExample(7, "R4C9", "000020080040009003000005700000000030805070020037004000070080056090000300100040000")]
    public class ClaimingInColumnHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ClaimingInColumn;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            var temp = AssignmentCellByEliminationCell(qSudoku);
            return temp;
        }
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allunsetCells = qSudoku.AllUnSetCells;
            foreach (var index in G.baseIndexs)
            {
                foreach (var value in G.AllBaseValues)
                {
                    var blockinfo = allunsetCells.Where(c => c.Column == index && c.RestList.Contains(value)).ToList();
                    var blocks = blockinfo.Select(c => c.Block).Distinct();
                    if (blockinfo.Count > 1 && blocks.Count() == 1) //若blockinfo.Count==1 则是唯余法。
                    {
                        var block = blocks.First();
                        var existsRows = blockinfo.Select(c => c.Row).Distinct();
                        #region 同宫不同列
                        var negativeCells = allunsetCells.Where(c => c.Block == block && c.Column != index && c.RestList.Contains(value)).ToList();
                        foreach (var item1 in negativeCells)
                        {
                            var cell = new NegativeCell(item1.Index, value, qSudoku)
                            {
                                SolveMessages = new List<SolveMessage>
                                {
                                    index.ColumnDesc(),
                                    "只有",
                                    block.BlockDesc(),
                                    "可以填入" + value + "\t\t\r\n",
                                    "所以",
                                    item1.Location,
                                    "不能填入" + value + "\t\t\r\n"
                                }
                            };
                            var drawCells = GetDrawPossibleCell( new List<int> { value }, blockinfo);
                            drawCells.Add(cell);
                            cell.drawCells = drawCells;
                            cells.Add(cell);
                        }
                        #endregion
                        #region 第三行
                        var checkrow = allunsetCells.Where(c => c.Block == block && !existsRows.Contains(c.Row)).Select(c => c.Row).ToList();
                        foreach (var row in checkrow)
                        {
                            var cells1 = allunsetCells.Where(c => c.Block == block && c.Row == row && c.RestList.Contains(value)).ToList();
                            var list1 = cells1.Select(c => c.Index).ToList();
                            var cell = new NegativeIndexsGroup(list1, value, qSudoku)
                            {
                                SolveMessages = new List<SolveMessage>
                                {
                                    index.ColumnDesc(),
                                    "只有",
                                    block.BlockDesc(),
                                    "可以填入" + value + "\t\t\r\n",
                                    "所以"            ,G.MergeLocationDesc(cells1),"不能填入" + value+"\t\t\r\n"
                                }
                            };
                            var drawCells = GetDrawPossibleCell(value,blockinfo);
                            drawCells.AddRange(GetDrawNegativeCell(value,cells1));
                            drawCells.Add(cell);
                            cell.drawCells = drawCells;
                            cells.Add(cell);
                        }
                        #endregion
                        #region 其余列
                        var otherColumn = negativeCells.Select(c => c.Column).Distinct().ToList();
                        foreach (var column in otherColumn)
                        {
                            var cells1 = allunsetCells.Where(c => c.Block == block && c.Column == column && c.RestList.Contains(value)).ToList();
                            var list1 = cells1.Select(c => c.Index).ToList();
                            var cell = new NegativeIndexsGroup(list1, value, qSudoku)
                            {
                                SolveMessages = new List<SolveMessage>
                                {
                                    index.ColumnDesc(),
                                    "只有",
                                    block.BlockDesc(),
                                    "可以填入" + value + "\t\t\r\n",
                                    G.MergeLocationDesc(cells1),"不能填入" + value+"\t\t\r\n"
                                }
                            };
                            var drawCells = GetDrawPossibleCell(value,blockinfo);
                            drawCells.AddRange(GetDrawNegativeCell(value , cells1));
                            drawCells.Add(cell);
                            cell.drawCells = drawCells;
                            cells.Add(cell);
                        }
                        #endregion
                    }
                }
            }
            return cells;
        }
        public override string GetDesc()
        {
            return "若特定列C中只有特定宫B中，包含候选数a，则该宫的其余列不包含该候选数。";
        }
    }
}
