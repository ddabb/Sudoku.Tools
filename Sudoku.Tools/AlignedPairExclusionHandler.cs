using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core;

namespace Sudoku.Tools
{

  [EliminationExampleAttribute(8,"R2C6","783060054569700020124503700070005400405007002030800075007050010300906507650070200")]    
   public class AlignedPairExclusionHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.AlignedPairExclusion;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
