using Sudoku.Core.Model;

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
        CellInfo toCell { get; set; }
    }
}
