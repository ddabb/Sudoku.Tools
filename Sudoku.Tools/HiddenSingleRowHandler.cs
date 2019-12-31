using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [Example("900400613320190700000000009000017008000000000700360000800000000009045086253001004")]
    public class HiddenSingleRowHandler :SolverHandlerBase
    {
        public override List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cells = new List<CellInfo>();

            for (int i = 0; i < 9; i++)
            {
                Func<CellInfo, bool> predicate = c => c.Row == i;
                var rests = qSoduku.GetUnSetInfo(predicate);
                List<int> cues = new List<int>();
                foreach (var item in rests)
                {
                    if (item.Value.Count > 1)
                    {
                        cues.AddRange(item.Value);
                    }


                }
                var temp = cues.GroupBy(c => c).Where(c => c.Count() == 1).ToList();
                foreach (var item in temp)
                {
                    var cellValue = item.Key;
                    foreach (var restCell in rests)
                    {
                        if (restCell.Value.Contains(cellValue))
                        {
                            cells.Add(new CellInfo(restCell.Key, cellValue));
                        }
                    }


                }

            }
            return cells;// qSoduku.GetBlockSetInfo();
        }
    }
}
