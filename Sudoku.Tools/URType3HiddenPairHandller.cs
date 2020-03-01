using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [EliminationExample(3, "R3C6", "000900004400000500760080000290000100071209005004710902100840000907025801002190000")]
    public class URType3HiddenPairHandller : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.URType3HiddenPair;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
    }
}
