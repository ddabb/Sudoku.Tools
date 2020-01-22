using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(7,"R3C8","640010300890300000230480500579132000362948715418567932156004893983651247724893156")]
    public class ULSize6Type4Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize6Type4;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;
            var pairCells = allUnSetCell.Where(c => c.RestCount == 2).ToList();
            var ab = (from a in pairCells
                join b in pairCells on a.RestString equals b.RestString
                let indexs=new List<int> {a.Index,b.Index }
                where a.Block == b.Block && a.Index < b.Index
                select new {a, b,a.Block, a.RestList }).ToList();
            foreach (var item in ab)
            {
                var block = item.Block;

            }


            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
