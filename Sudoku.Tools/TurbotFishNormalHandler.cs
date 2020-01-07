using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Sudoku.Tools
{

    
     [EliminationExample("094782130782103009100094728041920873920807514807001962279318000318000297465279381")]
     [AssignmentExample("700002600000000900090615074009100703400020859073059100040000500927500400600001097")]
    public class TurbotFishNormalHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
           

            List<PossibleIndex> list = GetAllPossibleIndexInRow(qSudoku, 2);
            cells.AddRange( AssignmentInOnedirection(qSudoku, list,Direction.Row));
            List<PossibleIndex> list1 = GetAllPossibleIndexInColumn(qSudoku, 2);
            cells.AddRange(AssignmentInOnedirection(qSudoku, list1, Direction.Column));

            return cells;
        }

        private List<CellInfo> AssignmentInOnedirection(QSudoku qSudoku,  List<PossibleIndex> list, Direction direction)
        {
            List<CellInfo> cells = new List<CellInfo>();
               var allunsetCell = qSudoku.AllUnSetCell;
            var chains = (from a in list
                          join b in GetAllPossibleIndexInBlock(qSudoku, 2) on a.SpeacialValue equals b.SpeacialValue
                          select new { a, b }).ToList();

            foreach (var ab in chains)
            {
                var rest = ab.a.SpeacialValue;
                var indexa = ab.a.indexs;
                var indexb = ab.b.indexs;

                foreach (var ai in indexa)
                {
                    cells.AddRange(from bi in indexb.Where(bi => direction == Direction.Row ? IsSameColumn(ai, bi) : IsSameRow(ai, bi)).Select(bi => bi)
                                   let restCells = GetIntersectCells(allunsetCell, ab.a.indexs.First(c => c != ai), ab.b.indexs.First(c => c != bi))
                                   from cell in restCells
                                   let rests = cell.GetRest()
                                   where rests.Count == 2 && rests.Contains(rest)
                                   && ab.a.indexs.Intersect(ab.b.indexs).Count()== 0 //四个单元格
                                   select new PositiveCellInfo(cell.Index, rests.First(c => c != rest)));
                }



            }
            return cells;
        }

        //000080000506030080980500102200758000004090058058614000400070860000805000800020009
        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
