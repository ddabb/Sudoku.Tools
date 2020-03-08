using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tools
{
    [EliminationExample(5, "R4C4", "002004030481253796703008400000020903006340070030000004100007300300000500009430010")]
    public class ForcingChainOffHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ForcingChainOff;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
