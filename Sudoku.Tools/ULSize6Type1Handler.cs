using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Sudoku.Tools
{
    [AssignmentExample(8, "R7C4","489172005651403002273056004300718459597364821814529003905001047748005006130047598")]
    public class ULSize6Type1Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize6Type1;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells=new List<CellInfo>();
            var allUnsetCell = qSudoku.AllUnSetCells;
            var checkCells = allUnsetCell.Where(c => c.RestCount == 3).ToList();
            var pairCells = allUnsetCell.Where(c => c.RestCount == 2).ToList();
            foreach (var a0 in checkCells)
            {
                var step = (from a1 in pairCells
                    join a2 in pairCells on a1.RestString equals a2.RestString
                    let restString = a1.RestString
                    let indexs=new List<int> {a1.Index,a2.Index }
                 where a1.Column == a0.Column
                       && a2.Row == a0.Row
                       && a0.RestList.Intersect(a1.RestList).Count() == 2
                    select new { a1, a2, restString, indexs }).ToList();

                foreach (var item in step)
                {
                    var a1 = item.a1;
                    var a2 = item.a2;
                    var restString = item.restString;
                    var exceptIndex = item.indexs;
                    var leftCells = pairCells.Where(c =>
                        !exceptIndex.Contains(c.Index) && c.RestString == restString).ToList();
                    var results = (from a3 in leftCells
                        join a4 in leftCells on a3.Column equals a4.Column
                        join a5 in leftCells on a4.Row equals a5.Row
                        where a3.Index != a4.Index
                              && a4.Index != a5.Index
                              && a3.Row == a1.Row
                              && a3.Row == a1.Row
                              && a5.Column == a2.Column
                              && a5.Row == a4.Row
                        select new {a3, a4, a5}).ToList();
                    foreach (var item1 in results)
                    {
                        cells.Add(new PositiveCellInfo(a0.Index,a0.RestList.Except(a1.RestList).First()));
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
