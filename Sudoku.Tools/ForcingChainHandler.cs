using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sudoku.Tools
{

    [AssignmentExample("716000289298176543534928761000490108080607904945812376403089612809201437020000895")]
    //200607984700480230048000710000070598017800623580000471395000847126748359874000162
    public class ForcingChainHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ForcingChain;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();

            var checkIndexLists = qSudoku.AllUnSetCell.Where(c => c.GetRest().Count == 2).Select(c => c.Index).ToList();
            var aOrBIndex = qSudoku.GetPossibleIndexByTimes(2);
            foreach (var item in aOrBIndex)
            {
                checkIndexLists.AddRange(item.indexs);
            }

            checkIndexLists = checkIndexLists.Distinct().ToList();
            Debug.WriteLine(checkIndexLists.JoinString());
            foreach (var index in checkIndexLists)
            {
                foreach (var testValue in qSudoku.GetRest(index))
                {
                    var index1 = index;
                    var testValue1 = testValue;
                    NegativeCellInfo cell = new NegativeCellInfo(index1, testValue1)
                    { Sudoku = qSudoku, CellType = CellType.Negative, IsRoot = true };
                    traceCell.Clear();
                    Fuc(cell, ref checkIndexLists);
                    if (traceCell.Count != 0)
                    {
                        var temp = new PositiveCellInfo(index1, testValue1) { CellType = CellType.Positive };
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

        public static List<CellInfo> traceCell = new List<CellInfo>();
        private void Fuc(CellInfo cell, ref List<int> checkIndexLists)
        {
            //Debug.WriteLine("Fuc in  " + cell + "cell parent" + cell.Parent);
        
            if (checkIndexLists.Contains(cell.Index))
            {
                if (traceCell.Any()) return;
                if (cell.IsError)
                {

                    Debug.WriteLine(" count  " + cell.NextCells.Count + " cellinfo   " + cell + " parent " + cell.Parent);

                    Debug.WriteLine("ForcingChainHandler  \r\n");
                    foreach (var parent in cell.GetAllParents().OrderBy(c => c.Level))
                    {
                        Debug.WriteLine("parent  " + parent);
                    }
                    Debug.WriteLine("result  " + cell);
                    traceCell.Add(cell);
                    return;
                }

                if (!cell.GetAllParents().Any(c => c.Index == cell.Index && c.CellType == cell.CellType && c.Value == cell.Value))
                {
                    var temp = cell.NextCells;
                    foreach (var sub in temp)
                    {
                        Debug.WriteLine(" count  " + sub.NextCells.Count + " cellinfo   " + sub + " parent " + sub.Parent);


                    }

                    foreach (var item in temp)
                    {

                        if (traceCell.Count != 0) //找到一个就跳出循环
                        {
                            //break;
                        }
                        else
                        {

                            Fuc(item, ref checkIndexLists);
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



        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
