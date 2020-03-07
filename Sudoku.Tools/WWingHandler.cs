using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
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
            var pairs = qSudoku.AllUnSetCells.Where(c => c.RestCount == 2);
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
                    if (IndexPairs.direction == Direction.Row)
                    {

                        if (IsSameColumn(ab.a.Index, IndexPairs.indexs[0]) && IsSameColumn(ab.b.Index, IndexPairs.indexs[1]) || (
                            IsSameColumn(ab.a.Index, IndexPairs.indexs[1]) && IsSameColumn(ab.b.Index, IndexPairs.indexs[0])
                            ))
                        {
                            var rest = restInts.First(c => c != IndexPairs.SpeacialValue);
                            var removeCells = qSudoku.GetPublicUnsetAreas(ab.a, ab.b);
                            foreach (var cell in removeCells)
                            {
                                var rests = cell.RestList;
                                if (rests.Contains(rest))
                                {
                                    cells.Add(new NegativeCell(cell.Index, rest) { Sudoku = qSudoku });
                                }
                            }
                        }
                    }
                    if (IndexPairs.direction == Direction.Column)
                    {
                        if (IsSameRow(ab.a.Index, IndexPairs.indexs[0]) && IsSameRow(ab.b.Index, IndexPairs.indexs[1]) ||
                            (IsSameRow(ab.a.Index, IndexPairs.indexs[1]) && IsSameRow(ab.b.Index, IndexPairs.indexs[0])))
                        {
                            var rest = restInts.First(c => c != IndexPairs.SpeacialValue);
                            var removeCells = qSudoku.GetPublicUnsetAreas(ab.a, ab.b);
                            foreach (var cell in removeCells)
                            {
                                var rests = cell.RestList;
                      
                                if (rests.Contains(rest))
                                {
                                    cells.Add(new NegativeCell(cell.Index, rest) { Sudoku = qSudoku });
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
