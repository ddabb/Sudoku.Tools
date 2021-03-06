﻿using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    /// <summary>
    /// 
    /// 候选数X若在R行仅存在一个宫B中，则该B宫中的非R行排除候选数X。
    /// </summary>
    [AssignmentExample(1, "R1C5", "200007450537420008419050723000040075170000046640070000004060537700084092000700004")]
    public class ClaimingInRowHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ClaimingInRow;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var AllunsetCells = qSudoku.AllUnSetCells;
            foreach (var index in G.baseIndexs)
            {
                foreach (var value in G.AllBaseValues)
                {
                    var blockinfo = AllunsetCells.Where(c => c.Row == index && c.RestList.Contains(value)).ToList();
                    var blocks = blockinfo.Select(c => c.Block).Distinct();
                    if (blockinfo.Count > 1 && blocks.Count() == 1) //若blockinfo.Count==1 则是唯余法。
                    {
                        var block = blocks.First();
                        var ExistsColumns = blockinfo.Select(c => c.Column).Distinct();
                        #region 同宫不同行
                        var negativeCells = AllunsetCells.Where(c => c.Block == block && c.Row != index && c.RestList.Contains(value)).ToList();
                        foreach (var item1 in negativeCells)
                        {
                            var cell = new NegativeCell(item1.Index, value, qSudoku)
                            {
                                SolveMessages = new List<SolveMessage>
                                {
                                    index.RowDesc(),
                                    "只有",
                                    block.BlockDesc(),
                                    "可以填入" + value + "\t\t\r\n",
                                    "所以",
                                    item1.Location,
                                    "不能填入" + value + "\t\t\r\n"
                                }
                            };
                            var drawCells = GetDrawPossibleCell(value,blockinfo);
                            drawCells.Add(cell);
                            cell.drawCells = drawCells;
                            cells.Add(cell);
                        }
                        #endregion
                        #region 第三列
                        var checkcolumn = AllunsetCells.Where(c => c.Block == block && !ExistsColumns.Contains(c.Column)).Select(c => c.Column).ToList();
                        foreach (var column in checkcolumn)
                        {
                            var cells1 = AllunsetCells.Where(c => c.Block == block && c.Column == column && c.RestList.Contains(value)).ToList();
                            var list1 = cells1.Select(c => c.Index).ToList();
                            var cell = new NegativeIndexsGroup(list1, value, qSudoku)
                            {
                                SolveMessages = new List<SolveMessage>
                                {
                                    index.RowDesc(),
                                    "只有",
                                    block.BlockDesc(),
                                    "可以填入" + value + "\t\t\r\n",
                                    "所以" ,         G.MergeLocationDesc(cells1),"不能填入" + value+"\t\t\r\n"
                                }
                            };
                            var drawCells = GetDrawPossibleCell(new List<int> { value },blockinfo );
                            drawCells.AddRange(GetDrawNegativeCell( new List<int> { value },cells1));
                            drawCells.Add(cell);
                            cell.drawCells = drawCells;
                            cells.Add(cell);
                        }
                        #endregion
                        #region 其余行
                        var otherRows = negativeCells.Select(c => c.Row).Distinct().ToList();
                        foreach (var row in otherRows)
                        {
                            var cells1 = AllunsetCells.Where(c => c.Block == block && c.Row == row && c.RestList.Contains(value)).ToList();
                            var list1 = cells1.Select(c => c.Index).ToList();
                            var cell = new NegativeIndexsGroup(list1, value, qSudoku)
                            {
                                SolveMessages = new List<SolveMessage>
                                {
                                    index.RowDesc(),
                                    "只有",
                                    block.BlockDesc(),
                                    "可以填入" + value + "\t\t\r\n",
                                    "所以"              ,         G.MergeLocationDesc(cells1),"不能填入" + value+"\t\t\r\n"
                                }
                            };
                            var drawCells = GetDrawPossibleCell(value,blockinfo);
                            drawCells.AddRange(GetDrawNegativeCell(value,cells1));
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
            return "若特定行R中只有特定宫B中，包含候选数a，则该宫的其余行不包含该候选数。";
        }
    }
}
