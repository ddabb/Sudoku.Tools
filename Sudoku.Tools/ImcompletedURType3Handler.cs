using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample(6,"R1C6","005000018371908000800100000004581372518070600723400185400800901180600400009014800")]
    public class ImcompletedURType3Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ImcompletedURType3;

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
