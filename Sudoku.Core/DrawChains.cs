using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class DrawChains
    {
        enum LineType { }

        bool DrawLine { get; set; }

       CellInfo fromCell { get; set; }
        CellInfo toCell  { get; set; }
    }
}
