using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sudoku.Core
{
    public enum Direction
    {
        [Description("行")]
        Row,
        [Description("列")]
        Column,
        [Description("宫")]
        Block
    }
}
