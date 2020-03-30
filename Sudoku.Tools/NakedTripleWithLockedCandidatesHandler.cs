using Sudoku.Core;
using Sudoku.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{

    [AssignmentExample(8, "R9C7", "154703200072100004030402000040028000280917046010340020020071090790004102061239057")]
    public class NakedTripleWithLockedCandidatesHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.NakedTripleWithLockedCandidates;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCell = qSudoku.AllUnSetCells;
            var filterCells = allUnsetCell.Where(c => c.RestCount == 3).ToList();

            var pairCells = (from a in filterCells
                             join b in filterCells on a.Block equals b.Block
                             let rests = a.RestList
                             let indexs = G.MergeCellIndexs(a, b)
                             where a.Index < b.Index
                             && a.RestString == b.RestString
                             select new { a, b, rests, indexs });
            foreach (var item in pairCells)
            {
                var a = item.a;
                var b = item.b;
                var block = a.Block;
                var indexs = item.indexs;
                var rests = item.rests;
                var commonAreas = qSudoku.GetPublicUnsetAreas(a, b).Where(c => c.Block != block && c.RestCount == 2 && c.RestList.Intersect(rests).Count() == 2).ToList();
                foreach (var cell in commonAreas)
                {
                    var removeValue = rests.Except(cell.RestList).First();
                    var cellIndexs = allUnsetCell.Where(c => c.RestList.Contains(removeValue) && c.Block == a.Block && !indexs.Contains(c.Index)).ToList();
                    if (cellIndexs.Count != 0)
                    {
                        var removeIndexs = cellIndexs.Select(c => c.Index).ToList();
                        var locations = cellIndexs.Select(c => c.Location).ToList();
                        var group = new NegativeIndexsGroup(removeIndexs, removeValue, qSudoku) { Sudoku = qSudoku };
                        group.SolveMessages = new List<SolveMessage>
                                        {G.MergeLocationDesc(a,b,cell),   "构成"+a.RestString+"的显性三数组\r\n",
                                      "因为",G.MergeLocationDesc(a,b),"位于" ,block.BlockDesc(),"中\r\n",
                                   "所以",   G.MergeLocationDesc(cellIndexs),     "不能为"+removeValue+"\r\n"
                                        };
                        var drawCells = GetDrawPossibleCell(new List<CellInfo> { a, b, cell }, a.RestList);
                        drawCells.AddRange(GetDrawNegativeCell(cellIndexs, new List<int> { removeValue }));
                        group.drawCells = drawCells;
                        cells.Add(group);
                    }
                }



            }


            return cells;
        }

        public override string GetDesc()
        {
            return "若一个行内的显性三数组中，候选数a只出现在了某宫中，则该宫的其余行不包含该候选数。\r\n" +
                   "若一个列内的显性三数组中，候选数a只出现在了某宫中，则该宫的其余列不包含该候选数。\r\n";
        }
    }
}
