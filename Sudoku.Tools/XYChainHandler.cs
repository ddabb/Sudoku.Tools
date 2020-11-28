using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    [AssignmentExample(7,"R8C2","060300570052041006000605100580004267429006315607002498006207000005410620290063700")]
    public class XYChainHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XYChain;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return new List<CellInfo>();
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
