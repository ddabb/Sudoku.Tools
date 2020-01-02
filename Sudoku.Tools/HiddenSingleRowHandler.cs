using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [Example("000000000000040329000651840000000000000000473008473296004000900000005180060180700")]
    public class HiddenSingleRowHandler :SolverHandlerBase
    {
        public override List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var direction = Direction.Row;
            foreach (var index in baseIndexs)
            {
                cells.AddRange(GetHiddenSingleCellInfo(qSoduku, c => GetFilter(c, direction, index)&&c.Value==0));
            }
            return cells;
        }


    }
}
