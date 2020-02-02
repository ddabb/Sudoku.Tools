using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{

    [AssignmentExample(7,"R4C9","000020080040009003000005700000000030805070020037004000070080056090000300100040000")]
    public class ClaimingInColumnHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ClaimingInColumn;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var AllunsetCells = qSudoku.AllUnSetCells;
            foreach (var index in G.baseIndexs)
            {
                foreach (var value in G.AllBaseValues)
                {
                    var cellList = AllunsetCells.Where(c => c.Column == index && c.RestList.Contains(value)).ToList();
                    var blocks = cellList.Select(c => c.Block).Distinct().ToList();
                    if (cellList.Count > 1 && blocks.Count() == 1) //若cellList.Count==1 则是唯余法。
                    {
                        var block = blocks.First();
                        var existsRows = cellList.Select(c => c.Row).Distinct().ToList();
                        #region 同宫不同列
                        var negativeCells = AllunsetCells.Where(c => c.Block == block && c.Column != index).ToList();
                        foreach (var item1 in negativeCells)
                        {
                            var cellrest = item1.RestList;
                            if (cellrest.Count == 2 && cellrest.Contains(value))
                            {
                                item1.Value = cellrest.First(c => c != value);
                                cells.Add(item1);
                            }
                        }
                        #endregion

                        #region 第三行
                        var checkRow = AllunsetCells.Where(c => c.Block == block && !existsRows.Contains(c.Row)).Select(c => c.Row).ToList();
                        foreach (var row in checkRow)
                        {
                            var list1 = AllunsetCells.Where(c => c.Block != block && c.Row == row && c.RestList.Contains(value)).ToList();
                            if (list1.Count() == 1)
                            {
                                var result = list1.First();
                                result.Value = value;
                                cells.Add(result);
                            }
                        }
                        #endregion

                        var otherColumns = negativeCells.Select(c => c.Column).Distinct().ToList();
                        foreach (var result in from column in otherColumns select AllunsetCells.Where(c => c.Block != block && c.Column == column && c.RestList.Contains(value)).ToList() into list1 where list1.Count() == 1 select list1.First())
                        {
                            result.Value = value;
                            cells.Add(result);
                        }



                    }
                }

            }
            return cells;
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
