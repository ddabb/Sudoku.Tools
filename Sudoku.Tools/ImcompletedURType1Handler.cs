using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [EliminationExample(8,"R3C4","706014593390000100100039000032000600018900300047300800409100030871063900203090010")]
   public class ImcompletedURType1Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ImcompletedURType1;

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
