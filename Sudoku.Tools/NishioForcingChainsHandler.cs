using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample(2,"R1C2", "706014593390000100100039000032000600018900300047300800409100030871063900203090010")]
   public class NishioForcingChainsHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.NishioForcingChains;
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
