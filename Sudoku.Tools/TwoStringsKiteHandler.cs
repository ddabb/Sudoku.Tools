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

            foreach (var direction in allDirection)
            {
                foreach (var directionIndex in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkDirectionCells = qSudoku.AllUnSetCell.Where(GetDirectionCells(direction, directionIndex)).ToList();

                    var temp = (from value in G.AllBaseValues
                                where qSudoku.GetPossibleIndex(value, checkDirectionCells).Count == 2
                                select new {direction,directionIndex,value }
                                ).ToList();
                    if(temp.Count>0)
                    {

                    }

                } }

            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
