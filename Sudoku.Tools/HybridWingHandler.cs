using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;

namespace Sudoku.Tools
{


    
    [AssignmentExample(5, "R6C1", "006020100238617549100080062003750400871346295004890000012460803480230601365178924")]
    public class HybridWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType { get; }
        public override MethodClassify methodClassify { get; }
    }
}
