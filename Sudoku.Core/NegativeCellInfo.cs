using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{
    public class NegativeCellInfo : CellInfo
    {
        public NegativeCellInfo(int index, int value) : base(index, value)
        {

        }
    }
}
