using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tools
{
    [EliminationExampleAttribute(4, "R1C4", "000010900001209870029306451690023100482061000130000600000002610056134700010697000",SolveMethodEnum.ClaimingInColumn)]
    public class AlignedTripleExclusionHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.AlignedTripleExclusion;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
