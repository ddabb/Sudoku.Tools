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
            throw new NotImplementedException();
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType=>SolveMethodEnum.AlignedQuadrupleExclusionHandler;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
    }
}
