using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Sudoku.Core;

namespace Sudoku.Tools
{
    /// <summary>
    /// 
    /// 候选数X若在R行仅存在一个宫B中，则该B宫中的非R行排除候选数X。
    /// </summary>
    [AssignmentExample(1,"R1C5","200007450537420008419050723000040075170000046640070000004060537700084092000700004")]
    public class ClaimingInRowHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ClaimingInRow;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var AllunsetCells = qSudoku.AllUnSetCells;

            foreach (var index in G.baseIndexs)
            {
                foreach (var value in G.AllBaseValues)
                {
                    var blockinfo = AllunsetCells.Where(c => c.Row == index && c.GetRest().Contains(value)).ToList();
                    var blocks = blockinfo.Select(c => c.Block).Distinct();
                    if (blockinfo.Count>1&&blocks.Count()==1) //若blockinfo.Count==1 则是唯余法。
                    {
                        var block = blocks.First();
                        var ExistsColumns = blockinfo.Select(c => c.Column).Distinct();
                        #region 同宫不同行
                        var negativeCells = AllunsetCells.Where(c => c.Block == block && c.Row != index).ToList();
                        foreach (var item1 in negativeCells)
                        {
                            var cellrest = item1.GetRest();
                            if (cellrest.Count == 2 && cellrest.Contains(value))
                            {
                                item1.Value = cellrest.First(c => c != value);
                                cells.Add(item1);
                            }
                        }
                        #endregion

                        #region 第三列
                        var checkcolumn = AllunsetCells.Where(c => c.Block == block && !ExistsColumns.Contains(c.Column)).Select(c => c.Column).ToList();
                        foreach (var column in checkcolumn)
                        {
                            var list1 = AllunsetCells.Where(c => c.Block != block && c.Column == column && c.GetRest().Contains(value)).ToList();
                            if (list1.Count() == 1)
                            {
                                var result = list1.First();
                                result.Value = value;
                                cells.Add(result);
                            }
                        }
                        #endregion

                        var otherRows = negativeCells.Select(c => c.Row).Distinct().ToList();
                        foreach (var result in from row in otherRows select AllunsetCells.Where(c => c.Block != block && c.Row == row && c.GetRest().Contains(value)).ToList() into list1 where list1.Count() == 1 select list1.First())
                        {
                            result.Value = value;
                            cells.Add(result);
                        }



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
