using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    [AssignmentExample(9, "R7C4", "000436517000280000006170000000061070001000000050804100000043761003610000000000394")]
    public class DirectPointingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.DirectPointing;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allunsetcell = qSudoku.AllUnSetCells;
            var blocks = allunsetcell.Select(c => c.Block).Distinct().ToList();
            foreach (var blockindex in blocks)
            {
                foreach (var value in G.AllBaseValues)
                {
                    foreach (var direction in G.AllDirection.Where(c => c != Direction.Block))
                    {
                        var blockUnSetCell = allunsetcell.Where(c => c.Block == blockindex).ToList();
                        var directionIndex = blockUnSetCell.Where(c => c.RestList.Contains(value))
                            .Select(FindDirectionCondtion(direction)).Distinct().ToList();
                        if (directionIndex.Count == 1 && blockUnSetCell.Count(c => c.RestList.Contains(value)) != 1)
                        {
                            var index = directionIndex.First();
                            var checks = allunsetcell.Where(c => G.GetFilter(c, direction, index) && c.Block != blockindex).ToList();
                            foreach (var cell in checks)
                            {
                                var cellrest = cell.RestList;
                                if (cellrest.Contains(value))
                                {
                                    var cell1 = new NegativeCell(cell.Index, value, qSudoku)
                                    {
                                        SolveMessages = new List<SolveMessage>
                                        {
                                            blockindex.BlockDesc(),
                                            "只有",
                                            GetDirectionMessage(direction, index),
                                            "可以填入" + value + "\t\t\r\n",
                                            "所以",
                                            cell.Location,
                                            "不能填入" + value + "\t\t\r\n"
                                        }
                                    };
                                    var drawCells = GetDrawPossibleCell(new List<int> { value },blockUnSetCell);
                                    drawCells.Add(cell1);
                                    cell1.drawCells = drawCells;
                                    cells.Add(cell1);
                                }
                            }
                            var otherBlocks = checks.Select(c => c.Block).Distinct().ToList();// 该行/列其他宫
                            foreach (var block in otherBlocks)
                            {
                                var otherCell = allunsetcell.Where(c => c.Block == block && G.GetFilter(c, direction, index)&&c.RestList.Contains(value)).ToList();
                                var indexs = otherCell.Select(c => c.Index).ToList();
                                var cell1 = new NegativeIndexsGroup(indexs, value, qSudoku)
                                {
                                    SolveMessages = new List<SolveMessage>
                                    {
                                        blockindex.BlockDesc(),
                                        "只有",
                                        GetDirectionMessage(direction, index),
                                        "可以填入" + value + "\t\t\r\n",
                                        "所以",
                                        G.MergeLocationDesc(otherCell),
                                        "不能填入" + value + "\t\t\r\n"
                                    }
                                };
                                var drawCells = GetDrawPossibleCell(value,blockUnSetCell);
                                drawCells.AddRange(GetDrawNegativeCell(value,otherCell));
                                drawCells.Add(cell1);
                                cell1.drawCells = drawCells;
                                cells.Add(cell1);
                            }
                        }
                    }
                }
            }
            return cells;
        }
        public override string GetDesc()
        {
            return "若宫B中只有特定行R可以填入候选数a，则该行R的其余宫不能填入候选数a；\t\t\r\n" +
                   "若宫B中只有特定列C可以填入候选数a，则该列C的其余宫不能填入候选数a；\t\t\r\n";
        }
    }
}
