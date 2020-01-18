using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample(4, "R2C5", "000070146000006329006300875000463298603200750000000000100600087062900010800014002")]
    public class DymanicForcingChainHandler : SolverHandlerBase
    {
        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.DymanicForcingChain;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            //R5C6 为引子~
            throw new NotImplementedException();
        }
    }
}
