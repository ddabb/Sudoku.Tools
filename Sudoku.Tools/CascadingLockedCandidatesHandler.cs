using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tools
{
    [AssignmentExample(3, "R8C6", "005238090000900600000700000826005901450192086100800452000007000004000000080519200")]
    public class CascadingLockedCandidatesHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.CascadingLockedCandidates;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
