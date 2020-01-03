using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("534618729297534861600297400760009080009800076853761942976000018000976204000180697")] //已调整
    public class URType1Handler :SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
