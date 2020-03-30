using Sudoku.Core;
using Sudoku.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(9, "R4C9", "598643002003759648674128593457200830906307425032405060005904380341872956009530204")]
    public class EmptyRectangleHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCell = qSudoku.AllUnSetCells;

            foreach (var value in G.AllBaseValues)
            {
                var cellInfos = allUnsetCell.Where(c => c.RestList.Contains(value)).ToList();
                foreach (var index in G.baseIndexs)
                {
                    var blockCells = cellInfos.Where(c => c.Block == index).ToList();
                    if (blockCells.Count <= 2) continue;
                    var result = blockCells.Where(c =>
                        blockCells.All(c1 => c1.Column == c.Column || c1.Row == c.Row)).ToList();
                    foreach (var interectCell in result)
                    {
                        var otherBlockCell = cellInfos.Where(c => c.Block != index).ToList();

                        var abc = (from a in otherBlockCell
                                   join b in otherBlockCell on 1 equals 1
                                   join c in otherBlockCell on 1 equals 1
                                   where a.Row == interectCell.Row
                                         && b.Column == interectCell.Column
                                         && c.Row == b.Row
                                         && c.Column == a.Column
                                         && a.Block != b.Block
                                   select new { a, b, c }).ToList();
                        foreach (var item in abc)
                        {
                            var aCell = item.a;
                            var bCell = item.b;
                            var cCell = item.c;
                            if (qSudoku.GetPossibleIndex(value, c => c.Value == 0 && c.Row == cCell.Row).Count == 2)
                            {
                                var cell = new NegativeCell(aCell.Index,
                                    value, qSudoku);
                                cell.SolveMessages = new List<SolveMessage>
                                {
                                    G.MergeLocationDesc(interectCell,aCell,bCell,cCell),

                                    new SolveMessage("中的"),
                                    new SolveMessage(" "+ value+" ", MessageType.Postive),
                                    new SolveMessage("构成空矩形"+"\r\n", MessageType.Important),
                                    new SolveMessage("所以"),
                                    new SolveMessage(cell.Location+"不为"+value+"\r\n", MessageType.Nagetive),
                                };

                                var drawCells = GetDrawPossibleCell(blockCells, new List<int> { value });

                                drawCells.AddRange(GetDrawPossibleCell(G.MergeCells(aCell, bCell, cCell), new List<int> { value }));
                                drawCells.Add(cell);
                                cell.drawCells = drawCells;
                                cells.Add(cell);
                            }
                            else if (qSudoku.GetPossibleIndex(value, c => c.Value == 0 && c.Column == cCell.Column)
                                         .Count == 2 && bCell.RestCount == 2)
                            {
                                var cell = new NegativeCell(bCell.Index, value, qSudoku);
                                cell.SolveMessages = new List<SolveMessage>
                                {
                                    G.MergeLocationDesc(interectCell,aCell,bCell,cCell),
                                    new SolveMessage("中的"),
                                    new SolveMessage(" "+ value+" ", MessageType.Postive),
                                    new SolveMessage("构成空矩形"+"\r\n", MessageType.Important),
                                    new SolveMessage("所以"),
                                    new SolveMessage(cell.Location+"不为"+value+"\r\n", MessageType.Nagetive),
                                };
                                var drawCells = GetDrawPossibleCell(blockCells, new List<int> { value });
                                drawCells.AddRange(GetDrawPossibleCell(G.MergeCells(aCell, bCell, cCell), new List<int> { value }));
                                drawCells.Add(cell);
                                cell.drawCells = drawCells;
                                cells.Add(cell);

                            }
                        }
                    }
                }
            }
            return cells;
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.EmptyRectangle;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
