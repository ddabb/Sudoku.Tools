﻿using System;
using System.Collections.Generic;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample("030150209000360050700490603001273800000519000003684700100000008320040000409001060")]

    public class LockedCandidatesHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.LockedCandidates;

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
