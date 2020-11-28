using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    [AssignmentExample(8,"R4C6","016000482450260713203014659327400061060132074140076328534000096000045037000000845")]
    public class SiameseXwingHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.SiameseXwing;
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
