using Sudoku.Core;
using System;
using System.Collections.Generic;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample("")]
    public class DistributedDisjointedSubsetHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.DistributedDisjointedSubset;
        public override MethodClassify methodClassify { get; }
        public override string GetDesc()
        {
            return "";
        }

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }
    }
}
