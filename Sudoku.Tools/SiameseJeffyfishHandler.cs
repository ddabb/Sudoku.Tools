using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample(2,"R1C9", "000160870010875003807300651050621730001700504730500100070000000080256917062007000")]
    public class SiameseJeffyfishHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.SiameseJeffyfish;
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
