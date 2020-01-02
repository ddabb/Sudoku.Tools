﻿using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [Example("000900100039014500002000003900400008050078030080090005470809300090000457320047000")] //已调整
    public class BugType1Handler :SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List < CellInfo > cells=new List<CellInfo>();
            var checkcells= qSudoku.GetFilterCell(c => c.Value == 0 && (qSudoku.GetRest(c.Index).Count == 2));
     
            return cells;
         
        }
    }
}
