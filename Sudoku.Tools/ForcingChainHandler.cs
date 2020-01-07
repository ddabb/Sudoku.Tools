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

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var item in qSudoku.AllUnSetCell)
            {
                foreach (var testValue in item.GetRest())
                {
                    NegativeCellInfo cell = new NegativeCellInfo(item.Index, testValue) { Sudoku = qSudoku, CellType = CellType.Negative, IsRoot = true };
            List<CellInfo> traceCell = new List<CellInfo>();
            Fuc(cell, ref traceCell);
            if (traceCell.Count != 0)
            {
                var temp = new PositiveCellInfo(item.Index, testValue) { CellType = CellType.Positive };
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

        private void Fuc(CellInfo cell, ref List<CellInfo> traceCell)
        {
              if (traceCell.Count() > 0) return;
                //index  70  row  7  column  7  block  8  value  5  类型：肯定的 层级17

                if (cell.CellType == CellType.Positive)
            {
                var checkList = cell.GetAllParents().Where(c => c.Index == cell.Index && c.CellType == CellType.Positive && c.Value != cell.Value);
                if (checkList.Count() > 0)
                {
                    traceCell.Add(cell);


                }

            }
            var parents = cell.GetAllParents();
            Debug.WriteLine(cell+  " parents.Count" + parents.Count);
            if (parents.Where(c => c.Index == cell.Index && c.CellType == cell.CellType && c.Value == cell.Value).Count() == 0)
            {
                var temp = cell.NextCells;
                foreach (var item in temp)
                {
                    Fuc(item, ref traceCell);
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
