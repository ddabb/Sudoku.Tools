using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Sudoku.Tools
{
    /// <summary>
    /// 
    /// 候选数X若在R行仅存在一个宫B中，则该B宫中的非R行排除候选数X。
    /// </summary>
    [Example("200007450537420008419050723000040075170000046640070000004060537700084092000700004")]
    public class ClaimingInRowHandler : ISudokuSolveHelper
    {
        public List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            Func<CellInfo, bool> predicate = c => c.Value == 0;
            var rests = qSoduku.GetFilterCell(predicate);
 
            //候选数X若在R行仅存在一个宫B中，则该B宫中的非R行排除候选数X。

            var temp=(from c in rests
            select (new { c.Block, c.index,c.Row })).OrderBy(c=>c.Row).ThenBy(c=>c.Block).ToList();
            foreach (var item in temp)
            {
                //Debug.WriteLine("ClaimingInRowHandler 位置  " + item.index + "  行号  "+item.Row + " 宫 " + item.Block + " 候选数 " +string.Join(",",qSoduku.GetRest(item.index)));
            }


            return cells;
        }

    }
}
