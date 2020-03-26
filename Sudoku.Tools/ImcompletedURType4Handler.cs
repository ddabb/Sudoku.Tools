using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [EliminationExample(9,"R5C6","000521306601700528205860401720015864186200053500600012452006107060170245017452609")]
    public class ImcompletedURType4Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ImcompletedURType4;

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
