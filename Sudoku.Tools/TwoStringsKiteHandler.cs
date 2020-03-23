using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{

    /// <summary>
    /// https://www.cnblogs.com/asdyzh/p/10145026.html
    /// </summary>
    [AssignmentExample(7, "R2C4", "081020600042060089056800240693142758428357916175689324510036892230008460860200000")]
    public class TwoStringsKiteHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.TwoStringsKite;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allunsetCell = qSudoku.AllUnSetCells;
            var possileIndex = GetAllPossibleIndexInRowOrColumn(qSudoku, 2);

            var pairs = (from chaina in possileIndex
                         join chainb in possileIndex on chaina.SpeacialValue equals chainb.SpeacialValue
                         where chaina.direction != chainb.direction
                         select new { chaina, chainb }
                         ).ToList();
            foreach (var item in pairs)
            {
                var rest = item.chaina.SpeacialValue;
                var allIndex = new List<int> { item.chaina.indexs[0], item.chaina.indexs[1], item.chainb.indexs[0], item.chainb.indexs[1] };
                var blocks = allIndex.Select(c => new PositiveCell(c, 0, qSudoku)).Select(c => c.Block);
                if (allIndex.Distinct().Count() == 4 && blocks.Distinct().Count() == 3)
                {
                    Dictionary<int, int> blockCount = new Dictionary<int, int>();
                    foreach (var index in allIndex)
                    {
                        var eachBlock = new PositiveCell(index, 0, qSudoku).Block;
                        if (blockCount.ContainsKey(eachBlock))
                        {
                            blockCount[eachBlock] += 1;
                        }
                        else
                        {
                            blockCount.Add(eachBlock, 1);
                        }
                    }

                    var intersectIndex = allIndex.Where(c => blockCount[new PositiveCell(c, 0, qSudoku).Block] != 2).ToList();
                    var restCells = qSudoku.GetPublicUnsetAreas(intersectIndex[0], intersectIndex[1]);
                    foreach (var cell in restCells)
                    {
                        var rests = cell.RestList;

                        if (rests.Contains(rest))
                        {
                            cells.Add(new NegativeCell(cell.Index, rest, qSudoku) );
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
