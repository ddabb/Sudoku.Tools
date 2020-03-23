using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample(2, "R4C7", "521738000649010837783649152470090000190060000236087590917854326054026000062970405")]
    public class CannibalisticAICHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.CannibalisticAIC;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
