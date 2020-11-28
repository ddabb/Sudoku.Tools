using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    [EliminationExample(9, "R4C4", "600070590000010008002600004006021000470806021000340600200003400300060000069080005")]
    public class ForcingChainOffHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ForcingChainOff;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }
        public static List<CellInfo> traceCell = new List<CellInfo>();
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var cell1 in qSudoku.AllUnSetCells)
            {
                foreach (var testValue in cell1.RestList)
                {
                    var index1 = cell1.Index;
                    var testValue1 = testValue;
                    PositiveCell cell = new PositiveCell(index1, testValue1, qSudoku)
                        { IsRoot = true };
                    traceCell.Clear();
                    List<CellInfo> initCellInfo = new List<CellInfo>();
                    Fuc(cell, ref initCellInfo);
                    if (traceCell.Count == 1)
                    {
                        var temp = new NegativeCell(index1, testValue1, qSudoku);
                        var list = traceCell.First().GetAllParents().OrderBy(c => c.Level).ToList();
                        var temp1 = new List<SolveMessage>();
                        for (int i = 0; i < list.Count - 1; i++)
                        {
                            temp1.AddRange(new List<SolveMessage>
                            {
                                new SolveMessage("若"+list[i].Desc, MessageType.Reason),
                                new SolveMessage(" 则"+list[i+1].Desc, MessageType.Result),
                                new SolveMessage("\t\t\r\n")
                            });
                        }
                        temp1.Add(new SolveMessage("与"+ cell.Desc+"矛盾\t\t\r\n"));
                        temp1.Add(new SolveMessage("所以"+ temp.Desc +"\t\t\r\n"));
                        temp.SolveMessages = temp1;
                        cells.Add(temp);
                    }
                    else
                    {
                        //Debug.WriteLine("我还是可以进来");
                    }
                }
            }
            return cells;
        }
        private void Fuc(CellInfo cell, ref List<CellInfo> initCellInfo)
        {
            //Debug.WriteLine("Fuc in  " + cell + "cell parent" + cell.Parent);
            if (!traceCell.Any())
            {
                if (cell.IsError)
                {
                    traceCell.Add(cell);
                    return;
                }
                if (cell.GetAllParents()
                    .Any(c => c.Index == cell.Index && c.CellType == cell.CellType && c.Value == cell.Value)) return;
                var temp = cell.NextCells;
                foreach (var item in temp)
                {
                    if (traceCell.Count != 0) //找到一个就跳出循环
                    {
                        //break;
                    }
                    else
                    {
                        if (!initCellInfo.Exists(c =>
                            c.CellType == item.CellType && c.Index == item.Index && item.Value == c.Value))
                        {
                            initCellInfo.Add(item);
                            Fuc(item, ref initCellInfo);
                        }
                    }
                }
            }
        }
        private void Output(CellInfo found, ref List<string> infoList)
        {
            infoList.Insert(0, found.ToString());
            if (found.Parent != null)
            {
                Output(found.Parent, ref infoList);
            }
        }
        public override string GetDesc()
        {
            return "";
        }
    }
}
