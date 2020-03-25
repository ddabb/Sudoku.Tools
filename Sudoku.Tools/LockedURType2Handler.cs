using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [EliminationExample(4,"R5C8","000060000007009501954371800300007258000000000785000003000082607216700980070090004")]
    public class LockedURType2Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.LockedURType2;

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
