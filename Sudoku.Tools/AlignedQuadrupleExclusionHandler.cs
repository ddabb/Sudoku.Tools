using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [EliminationExample(7,"R8C2", "109340050850000143364510000003850001080691030600034580000080320208003015030025908")]//测试用例待优化
   public class AlignedQuadrupleExclusionHandler : SolverHandlerBase
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

        public override SolveMethodEnum methodType=>SolveMethodEnum.AlignedQuadrupleExclusion;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
    }
}
