using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sudoku.Core
{
    public enum Direction
    {
        [Description("R")]
        Row,
        [Description("C")]
        Column,
        [Description("B")]
        Block
    }
}
