using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    [EliminationExample(5,"R4C4","002004030481253796703008400000020903006340070030000004100007300300000500009430010")]
    public class XRSize10Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XRSize10;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var result = XRSizeCommonMethod(qSudoku, 5);
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
