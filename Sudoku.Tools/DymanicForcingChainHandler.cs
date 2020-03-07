using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample(4, "R2C5", "000070146000006329006300875000463298603200750000000000100600087062900010800014002")]
    public class DymanicForcingChainHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.DymanicForcingChain;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            //R5C6 为引子~
            return new List<CellInfo>();
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
