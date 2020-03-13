using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;

namespace Sudoku.Tools
{
   public class AlignedQuadrupleExclusionHandler : SolverHandlerBase
    {
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

        public override SolveMethodEnum methodType=>SolveMethodEnum.AlignedQuadrupleExclusion;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
    }
}
