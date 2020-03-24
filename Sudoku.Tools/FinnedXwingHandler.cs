using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample(7,"R6C4","000800400203060050070050060092480010718526943000009280020090070537248691901005000")]
  public  class FinnedXwingHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.FinnedXwing;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return new List<CellInfo>();
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
