using Sudoku.Core;
using System;
using System.Collections.Generic;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    public class DistributedDisjointedSubsetHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.DistributedDisjointedSubset;
        public override MethodClassify methodClassify { get; }
        public override string GetDesc()
        {
            throw new NotImplementedException();
        }

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
