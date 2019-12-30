using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sudoku.Tools
{

    [Example("030150209000360050700490603001273800000519000003684700100000008320040000409001060")]
    public class ClaimingInColumnHandler: ISudokuSolveHelper
    {

        public List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            Func<CellInfo, bool> predicate = c => c.Value==0;
            var rests = qSoduku.GetFilterCell(predicate);
            foreach (var item in rests)
            {
                Debug.WriteLine("ClaimingInColumnHandler 位置  " + item.index + "  候选数  " + string.Join(",", qSoduku.GetRest(item.index)));
            }
            return cells;
        }
    }
}
