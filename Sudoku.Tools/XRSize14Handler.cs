﻿using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    [AssignmentExample(1,"R5C2","000400025009005706125700094200000000007042560004310972003004000002000680001080009")]
    public class XRSize14Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize14;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var result = XRSizeCommonMethod(qSudoku, 7);
            if (result.Count > 0)
            {
                cells.AddRange(result);
            }
            return cells;
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
