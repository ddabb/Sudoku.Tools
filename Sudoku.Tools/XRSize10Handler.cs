﻿using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [EliminationExample(5,"R4C4","002004030481253796703008400000020903006340070030000004100007300300000500009430010")]
    public class XRSize10Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize10;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
