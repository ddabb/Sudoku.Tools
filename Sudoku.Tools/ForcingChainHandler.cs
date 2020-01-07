using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
            NegativeCellInfo cell = new NegativeCellInfo(71, 6) {Sudoku = qSudoku, CellType = CellType.Negative,IsRoot = true};
            List<CellInfo> foundCells = new List<CellInfo>();
            qSudoku.cachedAnalysisCells.Clear();

            var result = qSudoku.Analysis(cell);
            foreach (var found in result)
            {

                List<string> infos = new List<string>();
                Output(found, ref infos);
                Debug.WriteLine("链信息如下：\r\n");
                foreach (var info in infos)
                {
                    Debug.WriteLine(info);
                }
            }

            var temp = qSudoku.cachedAnalysisCells;
            return cells;

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
