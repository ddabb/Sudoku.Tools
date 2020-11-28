using Sudoku.Core;
using Sudoku.Core.Model;
using System.Collections.Generic;
using System.Linq;
namespace Sudoku.Tools
{
    [AssignmentExample(8, "R1C1", "012009600600012094749635821428397100397156482156020009904573210070261940201900007")]
    public class WWingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.WWing;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var pairs = qSudoku.AllUnSetCells.Where(c => c.RestCount == 2).ToList();
            var cellPairs = (from a in pairs
                             join b in pairs on 1 equals 1
                             where a.Index < b.Index
                             && !IsSameBlock(a, b)
                             && a.RestString == b.RestString
                             select new { a, b }).ToList();
            int times = 2;
            List<PossibleIndex> allPossibleindex1 = GetAllPossibleIndexInRowOrColumn(qSudoku, times);
            foreach (var ab in cellPairs)
            {
                var restString = ab.a.RestString;
                var restInts = ab.a.RestList;
                var restValue = ConvertToInts(restString);
                var filterIndexs = allPossibleindex1.Where(c => restValue.Contains(c.SpeacialValue)).ToList();
                foreach (var IndexPairs in filterIndexs.Where(c => !c.indexs.Contains(ab.a.Index) && !c.indexs.Contains(ab.b.Index)))
                {
                    var index1 = IndexPairs.indexs[0];
                    var index2 = IndexPairs.indexs[1];
                    var speacialValue = IndexPairs.SpeacialValue;
                    if (IndexPairs.direction == Direction.Row)
                    {
                        if (IsSameColumn(ab.a.Index, index1) && IsSameColumn(ab.b.Index, index2) || (
                            IsSameColumn(ab.a.Index, index2) && IsSameColumn(ab.b.Index, index1)
                            ))
                        {
                            var rest = restInts.First(c => c != speacialValue);
                            var removeCells = qSudoku.GetPublicUnsetAreas(ab.a, ab.b).Where(c => c.RestList.Contains(rest)).ToList();
                            foreach (var cell in removeCells)
                            {
                                var rests = cell.RestList;
                                var negativeCell = new NegativeCell(cell.Index, rest, qSudoku);
                                negativeCell.drawCells.AddRange(GetDrawChainCell(restInts, ab.a, ab.b));
                                var chaina = new ChainCell(index1, speacialValue, qSudoku);
                                var chainb = new ChainCell(index2, speacialValue, qSudoku);
                                negativeCell.drawCells.Add(chaina);
                                negativeCell.drawCells.Add(chainb);
                                negativeCell.drawCells.AddRange(GetDrawNegativeCell(rest, removeCells));
                                negativeCell.SolveMessages = new List<SolveMessage>
                                {
                                    G.MergeLocationDesc(ab.a,ab.b, chaina, chainb),"构成"+speacialValue+"和"+rest+"的"+G.GetEnumDescription(methodType),"\t\t\r\n所以" ,
                                    ab.a.Location,"和", ab.b.Location,"的共同相关格",   G.MergeLocationDesc(removeCells),"中不包含"+rest+"\t\t\r\n"
                                };
                                cells.Add(negativeCell);
                            }
                        }
                    }
                    if (IndexPairs.direction == Direction.Column)
                    {
                        if (IsSameRow(ab.a.Index, index1) && IsSameRow(ab.b.Index, index2) ||
                            (IsSameRow(ab.a.Index, index2) && IsSameRow(ab.b.Index, index1)))
                        {
                            var rest = restInts.First(c => c != speacialValue);
                            var removeCells = qSudoku.GetPublicUnsetAreas(ab.a, ab.b).Where(c => c.RestList.Contains(rest)).ToList(); ;
                            foreach (var cell in removeCells)
                            {
                                var rests = cell.RestList;
                                var negativeCell = new NegativeCell(cell.Index, rest, qSudoku);
                                negativeCell.drawCells.AddRange(GetDrawChainCell(restInts, ab.a, ab.b));
                                var chaina = new ChainCell(index1, speacialValue, qSudoku);
                                var chainb = new ChainCell(index2, speacialValue, qSudoku);
                                negativeCell.drawCells.Add(chaina);
                                negativeCell.drawCells.Add(chainb);
                                negativeCell.drawCells.AddRange(GetDrawNegativeCell(rest, removeCells));
                                negativeCell.SolveMessages = new List<SolveMessage>
                                {
                                    G.MergeLocationDesc(ab.a,ab.b, chaina, chainb),"构成"+speacialValue+"和"+rest+"的"+G.GetEnumDescription(methodType),"\t\t\r\n所以" ,
                                    ab.a.Location,"和", ab.b.Location,"的共同相关格",    G.MergeLocationDesc(removeCells),"中不包含"+rest+"\t\t\r\n"
                                };
                                cells.Add(negativeCell);
                            }
                        }
                    }
                }
            }
            return cells;
        }
        public override string GetDesc()
        {
            return "参考链文本";
        }
    }
}
