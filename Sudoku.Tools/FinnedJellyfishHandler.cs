using Sudoku.Core;
using Sudoku.Core.Model;
using System.Collections.Generic;
namespace Sudoku.Tools
{
    public class FinnedJellyfishHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }
        public override SolveMethodEnum methodType => SolveMethodEnum.FinnedJeffyfish;
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
