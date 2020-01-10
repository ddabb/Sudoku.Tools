using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{

    [AssignmentExample("030150209000360050700490603001273800000519000003684700100000008320040000409001060")]
    public class ClaimingInColumnHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ClaimingInColumn;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var AllunsetCells = qSudoku.AllUnSetCell;
            foreach (var index in G.baseIndexs)
            {
                foreach (var value in G.AllBaseValues)
                {
                    var blockinfo = AllunsetCells.Where(c => c.Column == index && c.GetRest().Contains(value)).ToList();
                    var blocks = blockinfo.Select(c => c.Block).Distinct();
                    if (blockinfo.Count > 1 && blocks.Count() == 1) //若blockinfo.Count==1 则是唯余法。
                    {
                        var block = blocks.First();
                        var ExistsRows = blockinfo.Select(c => c.Row).Distinct();
                        #region 同宫不同行
                        var negativeCells = AllunsetCells.Where(c => c.Block == block && c.Column != index).ToList();
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

                        #region 第三行
                        var checkRow = AllunsetCells.Where(c => c.Block == block && !ExistsRows.Contains(c.Row)).Select(c => c.Row).ToList();
                        foreach (var row in checkRow)
                        {
                            var list1 = AllunsetCells.Where(c => c.Block != block && c.Row == row && c.GetRest().Contains(value)).ToList();
                            if (list1.Count() == 1)
                            {
                                var result = list1.First();
                                result.Value = value;
                                cells.Add(result);
                            }
                        }
                        #endregion



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
