using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Sudoku.Tools
{
    [Example("980006375376850140000700860569347218000000537723581496000205780000000950000008620")]
    public class NakedPairHandller :SolverHandlerBase
    {
        public override List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSoduku.GetFilterCell(c => c.Value == 0 && qSoduku.GetRest(c).Count ==2);
            foreach (var direction in allDirection)
            {
                foreach (var index in baseIndexs)
                {
                    var subcells = qSoduku.AllUnSetCell.Where(c => GetFilter(c, direction, index)).ToList();
                    if (subcells.Count>2)
                    {
                        var temp = checkCells.Where(c => GetFilter(c, direction, index)).GroupBy(c => qSoduku.GetRestString(c)).Where(c => c.Count() == 2);
                        foreach (var sub in temp)
                        {
                            var removeCells = subcells.Where(c => qSoduku.GetRestString(c) != sub.Key);
                            var removeValues = ConvertToInts(sub.Key);
                            foreach (var cell in removeCells)
                            {
                                var rests = qSoduku.GetRest(cell);
                                if (rests.Count>1&&rests.Intersect(removeValues).Count()>0)
                                {
                                    foreach (var value in removeValues)
                                    {
                                        rests.Remove(value);
                                    }
                                    if (rests.Count==1)
                                    {
                                        cells.Add(new CellInfo(cell.Index, rests[0]));
                                    }
                                }
                            }                  
                       
                        }
                    }
  
                }

            }


            return cells;
        }
    }
}
