using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Sudoku.Tools
{
    /// <summary>
    /// two hidden pair and two fake naked triple
    /// </summary>
    [AssignmentExample(9,"R5C9","000806172817923546062070983006000400100680730003000600054090867708460301601708204")]
    public class XRSize6Type2Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize6Type2;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            return cells;
            var allUnsetCells = qSudoku.AllUnSetCells;
            var pairCells = allUnsetCells.Where(c => c.GetRest().Count == 2).ToList();
            var ab = (from a in pairCells
                      join b in pairCells on 1 equals 1
                let arest = a.GetRest()
                let brest = b.GetRest()
                where a.Index < b.Index
                      && a.Block != b.Block
                      && arest.Intersect(brest).Count() == 1
                select new {a, b, arest, brest}).ToList();
            foreach (var item in ab)
            {
                var a = item.a;
                var b = item.b;
                var arest = item.arest;
                var brest = item.brest;
                var publicCells = qSudoku.GetPublicUnsetAreas(a, b);
                var cd = (from c in publicCells
                    from d in publicCells
                    let inRow = a.Column == c.Column
                    where c.GetRestString() == arest.JoinString()
                          && d.GetRestString() == brest.JoinString()
                    select new {inRow, c, d}).ToList();
                foreach (var item1 in cd)
                {
                    
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
