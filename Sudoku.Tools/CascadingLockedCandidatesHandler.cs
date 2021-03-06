﻿using Sudoku.Core;
using Sudoku.Core.Model;
using System.Collections.Generic;
using System.Linq;
namespace Sudoku.Tools
{
    [AssignmentExample(3, "R8C6", "005238090000900600000700000826005901450192086100800452000007000004000000080519200")]
    public class CascadingLockedCandidatesHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.CascadingLockedCandidates;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            var temp = AssignmentCellByEliminationCell(qSudoku);
            return temp;
        }
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCells = qSudoku.AllUnSetCells;
            foreach (var kv in G.blockMaps)
            {
                var key = kv.Key;
                var blocks = kv.Value;
                var pairBlocks = (from a in blocks
                                  join b in blocks on 1 equals 1
                                  let blockList = new List<int> { a, b }
                                  where a < b
                                  select new { a, b, blockList });
                foreach (var item in pairBlocks)
                {
                    var blockList = item.blockList;
                    var blocka = item.a;
                    var blockb = item.b;
                    var otherBlock = blocks.Except(blockList).First();
                    var filterCell = allUnsetCells.Where(c => blockList.Contains(c.Block)).ToList();
                    if (filterCell.Count > 3)
                    {
                        foreach (var value in G.AllBaseValues)
                        {
                            var containsList = filterCell.Where(c => c.RestList.Contains(value)).ToList();
                            if (containsList.Count == 4)
                            {
                                if (containsList.Count(c => c.Block == blocka) == 2)//a宫2个数,b宫2个数
                                {
                                    var drawCells = GetDrawPossibleCell( new List<int> { value }, containsList);
                                    var distinctRow = containsList.Select(c => c.Row).Distinct().ToList();
                                    var distinctColumn = containsList.Select(c => c.Column).Distinct().ToList();
                                    if (distinctRow.Count() == 2
                                     && distinctColumn.Count() == 2)
                                    {
                                        if (key < 4)
                                        {
                                            #region 删除 第三宫 distinctRow 的value
                                            var keyCells = allUnsetCells.Where(c => c.Block == otherBlock && distinctRow.Contains(c.Row) && c.RestList.Contains(value)).ToList();
                                            if (keyCells.Any())
                                            {
                                                foreach (var keyCell in keyCells)
                                                {
                                                    var singleCell = new NegativeCell(keyCell.Index, value, qSudoku);
                                                    drawCells.Add(singleCell);
                                                    singleCell.drawCells = drawCells;
                                                    singleCell.SolveMessages = new List<SolveMessage>
                                                    {
                                                        G.MergeLocationDesc(containsList),"中的"+value,"构成"+G.GetEnumDescription(this.methodType)+"\t\t\r\n",
                                                        "所以",singleCell.Location,"不能为"+value+ "\t\t\r\n",
                                                    };
                                                    cells.Add(singleCell);
                                                }
                                                var indexs = keyCells.Select(c => c.Index).ToList();
                                                var nagetiveCell = new NegativeIndexsGroup(indexs, value, qSudoku);
                                                drawCells.Add(nagetiveCell);
                                                nagetiveCell.drawCells = drawCells;
                                                nagetiveCell.SolveMessages = new List<SolveMessage>
                                                {
                                                    G.MergeLocationDesc(containsList),"中的"+value,"构成"+G.GetEnumDescription(this.methodType)+"\t\t\r\n",
                                                    "所以",nagetiveCell.Location,"不能为"+value+ "\t\t\r\n",
                                                };
                                                cells.Add(nagetiveCell);
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region 删除 第三宫 distinctRow 的value
                                            var keyCells = allUnsetCells.Where(c => c.Block == otherBlock && distinctColumn.Contains(c.Column) && c.RestList.Contains(value)).ToList();
                                            if (keyCells.Any())
                                            {
                                                foreach (var keyCell in keyCells)
                                                {
                                                    var singleCell = new NegativeCell(keyCell.Index, value, qSudoku);
                                                    drawCells.Add(singleCell);
                                                    singleCell.drawCells = drawCells;
                                                    singleCell.SolveMessages = new List<SolveMessage>
                                                    {
                                                        G.MergeLocationDesc(containsList),"中的"+value,"构成"+G.GetEnumDescription(this.methodType)+"\t\t\r\n",
                                                        "所以",singleCell.Location,"不能为"+value+ "\t\t\r\n",
                                                    };
                                                    cells.Add(singleCell);
                                                }
                                                var indexs = keyCells.Select(c => c.Index).ToList();
                                                var nagetiveCell = new NegativeIndexsGroup(indexs, value, qSudoku)
                                                {
                                                    SolveMessages = new List<SolveMessage>
                                                    {
                                                        G.MergeLocationDesc(containsList),
                                                        "中的" + value,
                                                        "构成" + G.GetEnumDescription(this.methodType) + "\t\t\r\n",
                                                        "所以",
                                                        G.MergeLocationDesc(keyCells),
                                                        "不能为" + value+ "\t\t\r\n",
                                                    }
                                                };
                                                drawCells.Add(nagetiveCell);
                                                nagetiveCell.drawCells = drawCells;
                                                cells.Add(nagetiveCell);
                                            }
                                            #endregion
                                        }
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
            return "若候选数a在两个宫中，只出现在了特定两行的特定两列，则这两列的其余宫不包含候选数\t\t\r\n" +
                   "若候选数a在两个宫中，只出现在了特定两列的特定两行，则这两行的其余宫不包含候选数\t\t\r\n";
        }
    }
}
