using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [EliminationExample(2, "R8C1", "000058793093074185578931462010582934005169000982347651054726019009015006060093500")]
    public   class SplitWingHandler : SolverHandlerBase
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

        public override SolveMethodEnum methodType { get; }
        public override MethodClassify methodClassify { get; }
    }
}
