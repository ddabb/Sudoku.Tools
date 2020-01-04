using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{

    
     [EliminationExample("094782130782103009100094728041920873920807514807001962279318000318000297465279381")]
     [AssignmentExample("700002600000000900090615074009100703400020859073059100040000500927500400600001097")]
    public class TurbotFishNormalHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
        //000080000506030080980500102200758000004090058058614000400070860000805000800020009
        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
