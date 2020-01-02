﻿using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Sudoku.Tools
{
    [Example("000000000000040329000651847000000500000000473008473296004000900000005180060180700")]
    public  class HiddenSingleColumnHandler:SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
          
            var direction = Direction.Column;
            foreach (var index in baseIndexs)
            {        
                cells.AddRange(GetHiddenSingleCellInfo(qSudoku, c => GetFilter(c, direction, index)&&c.Value==0));
            }
            return cells;
        }
    }
}
