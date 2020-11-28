using Sudoku.Core;
using Sudoku.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Sudoku.Tools
{
    [EliminationExample(7, "R2C4", "000800400203060050070050060092480010718526943000009280020090070537248691901005000")]
    public class FinnedXwingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.FinnedXwing;
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
            var valueRestMap = G.AllBaseValues.ToDictionary(value => value, value => allUnsetCells.Where(c => c.RestList.Contains(value)).ToList());
            foreach (var direction in G.AllDirection.Where(c => c != Direction.Block))
            {
                var keys = (from key1 in G.RowblockMaps
                            join key2 in G.RowblockMaps on 1 equals 1
                            where key1.Key < key2.Key
                            select new { key1, key2 }).ToList();
                foreach (var keyitems in keys)
                {
                    var key1 = keyitems.key1;
                    var key2 = keyitems.key2;
                    var result = (from index1 in key1.Value
                                  join index2 in key2.Value on 1 equals 1
                                  let indexs = G.MergeInt(index1, index2)
                                  select indexs).ToList();
                    foreach (var indexsItem in result)
                    {
                        foreach (var value in G.AllBaseValues)
                        {
                 
                            var valueCells = valueRestMap[value];
                            if (valueCells.Count>5)
                            {
                                Func<CellInfo, bool> predicate = null;
                                if (direction == Direction.Row)
                                {
                                    predicate = c => indexsItem.Contains(c.Row);
                                }
                                else
                                {
                                    predicate = c => indexsItem.Contains(c.Column);
                                }
                                var filterCell = valueCells.Where(predicate).ToList();
                                if (filterCell.Count() == 5)
                                {
                                    var blocks = filterCell.Select(c => c.Block).Distinct().ToList();
                                    var distinctBlock = blocks.Count();
                                    if (distinctBlock == 4)
                                    {
                                        var distinctRow = filterCell.Select(c => c.Row).Distinct().Count();
                                        var distinctColumn = filterCell.Select(c => c.Column).Distinct().Count();
                                        var keyBlock = blocks.First(c => filterCell.Count(x => x.Block == c) == 2);
                                        var keyCells = new List<CellInfo>();
                                        if (direction == Direction.Column && distinctRow == 3 && distinctColumn == 2)
                                        {
                                            keyCells = filterCell.Where(x => filterCell.Where(c => c.Block == keyBlock).Select(c => c.Row).ToList().Contains(x.Row)).ToList();
                                        }
                                        else if (direction == Direction.Row && distinctRow == 2 && distinctColumn == 3)
                                        {
                                            keyCells = filterCell.Where(x => filterCell.Where(c => c.Block == keyBlock).Select(c => c.Column).ToList().Contains(x.Column)).ToList();
                                        }
                                        if (keyCells.Count == 3)
                                        {
                                            var indexs = qSudoku.GetPublicUnsetAreaIndexs(keyCells);
                                            var removeCells = valueCells
                                                .Where(c => c.Block == keyBlock && indexs.Contains(c.Index)).ToList();
                                            foreach (var removeCell in removeCells)
                                            {
                                                var negativeCell = new NegativeCell(removeCell.Index, value, qSudoku);
                                                cells.Add(negativeCell);
                                            }
                                            if (removeCells.Count > 1)
                                            {
                                                var negativeCell1 = new NegativeIndexsGroup(removeCells.Select(c => c.Index).ToList(), value, qSudoku);
                                                cells.Add(negativeCell1);
                                            }
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
