using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    [EliminationExample(5,"R1C1","080671009000394010901285037016037090098106703030928164100802075070019080800703041")]
    public class SashimiSwordfishHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.SashimiSwordfish;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
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
    }
}
