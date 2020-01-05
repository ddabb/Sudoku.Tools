using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{

    /// <summary>
    /// https://www.cnblogs.com/asdyzh/p/10145026.html
    /// </summary>
    [AssignmentExample("081020600042060089056800240693142758428357916175689324510036892230008460860200000")]
    public class TwoStringsKiteHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allunsetCell = qSudoku.AllUnSetCell;
            var possileIndex = GetAllPairInRowOrColumnIndex(qSudoku, 2);

            var pairs = (from chaina in possileIndex
                         join chainb in possileIndex on chaina.SpeacialValue equals chainb.SpeacialValue
                         where chaina.direction!=chainb.direction
                         select new { chaina , chainb }
                         ).ToList();
            foreach (var item in pairs)
            {
                var rest = item.chaina.SpeacialValue;
                var allIndex = new List<int> { item.chaina.indexs[0], item.chaina.indexs[1], item.chainb.indexs[0], item.chainb.indexs[1] };
                var Blocks = allIndex.Select(c => new CellInfo(c, 0)).Select(c => c.Block);
                if (allIndex.Distinct().Count()==4&& Blocks.Distinct().Count() == 3)
                {
                    Dictionary<int, int> blockCount = new Dictionary<int, int>();
                    foreach (var index in allIndex)
                    {
                        var eachBlock = new CellInfo(index, 0).Block;
                        if (blockCount.ContainsKey(eachBlock))
                        {
                            blockCount[eachBlock] += 1;
                        }
                        else
                        {
                            blockCount.Add(eachBlock, 1);
                        }
                    }

                   var intersectIndex= allIndex.Where(c => blockCount[new CellInfo(c, 0).Block] != 2).ToList();
                    var restCells = GetIntersectCells(allunsetCell, intersectIndex[0], intersectIndex[1]);
                    foreach (var cell in restCells)
                    {
                        var rests = qSudoku.GetRest(cell);
                        if (rests.Count == 2 && rests.Contains(rest))
                        {
                            cells.Add(new CellInfo(cell.Index, rests.First(c => c != rest)));
                        }
                    }





                }   
            }




            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
