using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sudoku.Core
{
    /// <summary>
    /// 
    /// </summary>
    public enum SolveMethodEnum
    {
        [Description("强制链")]
        ForcingChain,
        [Description("隐藏数对")]
        HiddenPair
    }
}
