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
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var AllunsetCells = qSudoku.AllUnSetCells;

            foreach (var index in G.baseIndexs)
            {
                foreach (var value in G.AllBaseValues)
                {
                    var blockinfo = AllunsetCells.Where(c => c.Column == index && c.RestList.Contains(value)).ToList();
                    var blocks = blockinfo.Select(c => c.Block).Distinct();
                    if (blockinfo.Count > 1 && blocks.Count() == 1) //若blockinfo.Count==1 则是唯余法。
                    {
                        var block = blocks.First();
                        var ExistsRows = blockinfo.Select(c => c.Row).Distinct();
                        #region 同宫不同列
                        var negativeCells = AllunsetCells.Where(c => c.Block == block && c.Column != index && c.RestList.Contains(value)).ToList();
                        foreach (var item1 in negativeCells)
                        {
                            var cell = new NegativeCell(item1.Index, value) { Sudoku = qSudoku };
                            cell.SolveMessages = new List<SolveMessage> { index.ColumnDesc(), "只有", block.BlockDesc(), "可以填入" + value +"\r\n", "所以", item1.Location ,"不能填入" + value +"\r\n"};
                            cells.Add(cell);

                        }
                        #endregion

                        #region 第三行
                        var checkrow = AllunsetCells.Where(c => c.Block == block && !ExistsRows.Contains(c.Row)).Select(c => c.Row).ToList();
                        foreach (var row in checkrow)
                        {
                            var list1 = AllunsetCells.Where(c => c.Block == block && c.Row == row && c.RestList.Contains(value)).Select(c => c.Index).ToList();
                            var cell = new NegativeIndexsGroup(list1, value) { Sudoku = qSudoku };
                            
                            cell.SolveMessages = new List<SolveMessage> { index.ColumnDesc(), "只有", block.BlockDesc(), "可以填入" + value+"\r\n", "所以" };
                            foreach (var item in list1)
                            {
                                cell.SolveMessages.AddRange(new List<SolveMessage> { item.LoctionDesc()," "});
                            }
                           
                            cell.SolveMessages.Add("不能填入" + value+"\r\n");
                            cells.Add(cell);
                        }
                        #endregion

                        #region 其余列
                        var otherColumn = negativeCells.Select(c => c.Column).Distinct().ToList();
             
                        foreach (var column in otherColumn)
                        {
                            var list1 = AllunsetCells.Where(c => c.Block == block && c.Column == column && c.RestList.Contains(value)).Select(c => c.Index).ToList();
                            var cell = new NegativeIndexsGroup(list1, value) { Sudoku = qSudoku };
                            cell.SolveMessages = new List<SolveMessage> { index.ColumnDesc(), "只有" , block.BlockDesc(), "可以填入" +value +"\r\n", "所以" };
                            foreach (var item in list1)
                            {
                                cell.SolveMessages.AddRange(new List<SolveMessage> { item.LoctionDesc(), " " });
                            }
                            cell.SolveMessages.Add("不能填入" + value+"\r\n");
                            cells.Add(cell);
                        }
                        #endregion
                    }
                }

            }
            return cells;
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
