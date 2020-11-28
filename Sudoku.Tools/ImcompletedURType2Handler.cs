using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    [EliminationExample(6,"R3C2","032710000000623704000809302020530100701206803300100420203401000010900230000302541")]
    public class ImcompletedURType2Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ImcompletedURType2;
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
