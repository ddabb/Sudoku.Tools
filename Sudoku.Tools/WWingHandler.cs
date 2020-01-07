using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Sudoku.Tools
{
    [AssignmentExample("012009600600012094749635821428397100397156482156020009904573210070261940201900007")]
    public class WWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var pairs = qSudoku.AllUnSetCell.Where(c => c.GetRest().Count == 2);
            var cellPairs = (from a in pairs
                             join b in pairs on 1 equals 1
                             where a.Index < b.Index     
                             && !IsSameBlock(a,b)
                             && a.GetRestString() == b.GetRestString()
                             select new { a, b }).ToList();
            int times = 2;
            List<PossibleIndex> allPossibleindex1 = GetAllPossibleIndexInRowOrColumn(qSudoku, times);
            foreach (var ab in cellPairs)
            {
                var restString = ab.a.GetRestString();
                var restInts = ab.a.GetRest();
                var restValue = ConvertToInts(restString);
                var filterIndexs = allPossibleindex1.Where(c => restValue.Contains(c.SpeacialValue)).ToList();
                foreach (var IndexPairs in filterIndexs.Where(c=>!c.indexs.Contains(ab.a.Index)&& !c.indexs.Contains(ab.b.Index)))
                {
                    if (IndexPairs.direction==Direction.Row)
                    {
   
                        if (IsSameColumn(ab.a.Index,IndexPairs.indexs[0])&& IsSameColumn(ab.b.Index, IndexPairs.indexs[1])   ||(
                            IsSameColumn(ab.a.Index, IndexPairs.indexs[1]) && IsSameColumn(ab.b.Index, IndexPairs.indexs[0])
                            )                         )
                        {
                            var rest= restInts.First(c=>c!= IndexPairs.SpeacialValue);
                        var removeCells=    GetIntersectCells(qSudoku.AllUnSetCell, ab.a, ab.b);
                            foreach (var cell in removeCells)
                            {
                                var rests = cell.GetRest();
                                if (rests.Count==2&& rests.Contains(rest))
                                {
                                    cells.Add(new PositiveCellInfo(cell.Index, rests.First(c => c != rest)));
                                }
                            }
                        }
                    }
                    if (IndexPairs.direction == Direction.Column)
                    {

                        if (IsSameRow(ab.a.Index, IndexPairs.indexs[0]) && IsSameRow(ab.b.Index, IndexPairs.indexs[1])||
                            (IsSameRow(ab.a.Index, IndexPairs.indexs[1]) && IsSameRow(ab.b.Index, IndexPairs.indexs[0])))
                        {
                            var rest = restInts.First(c => c != IndexPairs.SpeacialValue);
                            var removeCells = GetIntersectCells(qSudoku.AllUnSetCell, ab.a, ab.b);
                            foreach (var cell in removeCells)
                            {
                                var rests = cell.GetRest();
                                if (rests.Count == 2 && rests.Contains(rest))
                                {
                                    cells.Add(new PositiveCellInfo(cell.Index, rests.First(c => c != rest)));
                                }
                            }
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
