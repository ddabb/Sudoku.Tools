﻿using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{

    [AssignmentExample(3,"R1C7", "670000010931746000520009700462538070197624008853971000716400003389067400245003007")]
    //200607984700480230048000710000070598017800623580000471395000847126748359874000162
    public class ForcingChainHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ForcingChain;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();

            foreach (var cell1 in qSudoku.AllUnSetCells)
            {
                foreach (var testValue in cell1.RestList)
                {
                    var index1 = cell1.Index;
                    var testValue1 = testValue;
                    NegativeCellInfo cell = new NegativeCellInfo(index1, testValue1)
                        { Sudoku = qSudoku, CellType = CellType.Negative, IsRoot = true };
                    traceCell.Clear();
                    List<CellInfo> initCellInfo = new List<CellInfo>();
                    Fuc(cell, ref initCellInfo);
                    if (traceCell.Count != 0)
                    {
                        var temp = new PositiveCellInfo(index1, testValue1) { CellType = CellType.Init };

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
            throw new NotImplementedException();
        }
    }
}
