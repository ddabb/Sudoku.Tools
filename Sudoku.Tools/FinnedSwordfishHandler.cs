using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
 public   class FinnedSwordfishHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.FinnedSwordfish;
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
