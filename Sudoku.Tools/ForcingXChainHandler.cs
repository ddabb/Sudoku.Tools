﻿using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample("007000000800439000490071000000000906002000040008103000000002308005300000000710052")]
    public class ForcingXChainHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();

            List<PossibleIndex> allPossibleindex1 = GetAllPossibleIndex(qSudoku, 2);
            foreach (var item in allPossibleindex1)
            {
                var negativeCell = new NegativeCellInfo(item.indexs[0], item.SpeacialValue);
                if (IsContradiction(qSudoku, negativeCell, true))
                {
                    cells.Add(negativeCell);
                }
                 negativeCell = new NegativeCellInfo(item.indexs[1], item.SpeacialValue);
                if (IsContradiction(qSudoku, negativeCell, true))
                {
                    cells.Add(negativeCell);
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