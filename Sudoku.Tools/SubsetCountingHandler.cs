using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
   public class SubsetCountingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }
        public override SolveMethodEnum methodType => SolveMethodEnum.SubsetCounting;
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
