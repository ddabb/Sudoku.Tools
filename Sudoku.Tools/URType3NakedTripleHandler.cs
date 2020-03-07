using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{


    [AssignmentExample(4,"R8C4","738591624691800357254367891382000010049030780067008032423685179975003268816009543")]
    public class URType3NakedTripleHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.URType3NakedTriple;

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
