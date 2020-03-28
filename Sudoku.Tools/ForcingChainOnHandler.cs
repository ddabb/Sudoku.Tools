using Sudoku.Core;
using Sudoku.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{

    [AssignmentExample(3, "R1C7", "670000010931746000520009700462538070197624008853971000716400003389067400245003007")]
    //200607984700480230048000710000070598017800623580000471395000847126748359874000162
    public class ForcingChainOnHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ForcingChainOn;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();

            //var c = (from start in qSudoku.NegativeCell2PostiveCell
            //    join end in qSudoku.NegativeCell2PostiveCell on start.thentype equals end.thentype

            //    where start.negativeCell.Value == end.PostiveCell.Value
            //          && start.negativeCell.Index == end.PostiveCell.Index
            //          && start.PostiveCell.Index != end.negativeCell.Index
            //          && start.thentype == ThenType.ThenYes
            //         select new { start, end }).ToList();
            //foreach (var item in c)
            //{

            //    var start = item.start;
            //    var end = item.end;
            //    if (start.negativeCell.Index == 6 && end.PostiveCell.Index == 6)
            //    {
            //        if (  start.negativeCell.Value == 3 && end.PostiveCell.Value == 3)
            //        {
            //            var temp = qSudoku.AllChainCells.Where(c =>c.thentype==ThenType.ThenNo&& c.PostiveCell.Equals(start.PostiveCell)).ToList();
            //            List<IChainCell> temp2Cells=new List<IChainCell>();
            //            foreach (var temp1 in temp)
            //            {
            //                temp2Cells.AddRange(qSudoku.AllChainCells.Where(c => c.thentype == ThenType.ThenYes && c.negativeCell.Equals(temp1.negativeCell)).ToList()); 
            //            }

            //          var node=  new ChainNode(start.negativeCell, end.PostiveCell, qSudoku.AllChainCells);
            //          if (node.CanFindChain)
            //          {
                          
            //          }

            //        }
            //    }
            //}






            foreach (var cell1 in qSudoku.AllUnSetCells)
            {
                foreach (var testValue in cell1.RestList)
                {
                    var index1 = cell1.Index;
                    var testValue1 = testValue;
                    NegativeCell cell = new NegativeCell(index1, testValue1, qSudoku)
                    { Sudoku = qSudoku, CellType = CellType.Negative, IsRoot = true };
                    traceCell.Clear();
                    List<CellInfo> initCellInfo = new List<CellInfo>();
                    Fuc(cell, ref initCellInfo);
                    if (traceCell.Count == 1)
                    {
                        var temp = new PositiveCell(index1, testValue1, qSudoku) { CellType = CellType.Positive };

                        var list = traceCell.First().GetAllParents().OrderBy(c => c.Level).ToList();
                        var temp1 = new List<SolveMessage>();
                        for (int i = 0; i < list.Count - 1; i++)
                        {
                            temp1.AddRange(new List<SolveMessage>
                                    {
                                        new SolveMessage("若"+list[i].Desc, MessageType.Reason),
                                        new SolveMessage(" 则"+list[i+1].Desc, MessageType.Result),
                                        new SolveMessage("\r\n")
                                    });
                        }
                        temp1.Add(new SolveMessage("所以 " + temp.Desc, MessageType.Postive));
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


        public object BulidTree(CellInfo cell, List<IChainCell> allChainCells)
        {

            return new object();
        }

        public static List<CellInfo> traceCell = new List<CellInfo>();
        private void Fuc(CellInfo cell, ref List<CellInfo> initCellInfo)
        {
            //Debug.WriteLine("Fuc in  " + cell + "cell parent" + cell.Parent);

            if (traceCell.Any()) return;
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

        private void Output(CellInfo found, ref List<string> infoList)
        {
            infoList.Insert(0, found.ToString());
            if (found.Parent != null)
            {
                Output(found.Parent, ref infoList);
            }
        }



        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
