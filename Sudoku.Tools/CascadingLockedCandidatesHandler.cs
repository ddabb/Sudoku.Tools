using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample(3, "R8C6", "005238090000900600000700000826005901450192086100800452000007000004000000080519200")]
    public class CascadingLockedCandidatesHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.CascadingLockedCandidates;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            var temp= AssignmentCellByEliminationCell(qSudoku);
            return temp;
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCells = qSudoku.AllUnSetCells;
            Dictionary<int, List<int>> blockMaps = new Dictionary<int, List<int>>
            {
                { 1, new List<int> { 0, 1, 2 } },
                { 2, new List<int> { 3, 4, 5 } },
                { 3, new List<int> { 6, 7, 8 } },
                { 4, new List<int> { 0, 3, 6 } },
                { 5, new List<int> { 1, 4, 7 } },
                { 6, new List<int> { 2, 5, 8 } }
            };

            foreach (var kv in blockMaps)
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
                                    var drawCells = GetDrawPossibleCell(containsList, new List<int> { value });
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
                                                    cells.Add(singleCell);
                                                }
                                                var indexs = keyCells.Select(c => c.Index).ToList();
                                                var nagetiveCell = new NegativeIndexsGroup(indexs, value, qSudoku);
                                                drawCells.Add(nagetiveCell);
                                                nagetiveCell.drawCells = drawCells;
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
                                                    var singleCell = new NegativeCell(keyCell.Index, value, qSudoku) ;
                                                    drawCells.Add(singleCell);
                                                    singleCell.drawCells = drawCells;
                                                    cells.Add(singleCell);
                                                }

                                                var indexs = keyCells.Select(c => c.Index).ToList();
                                                var nagetiveCell = new NegativeIndexsGroup(indexs, value, qSudoku) ;
                         
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
            return "";
        }
    }
}
