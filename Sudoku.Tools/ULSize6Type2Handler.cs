using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample("390002804120864039048739012273000008080327145451698327032080006814976253760203081")] //出数
                       //060000725257000000409572806045007000726308504000050670592700000004925367673000259

    public class ULSize6Type2Handler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.ULSize6Type2;

        /// <summary>
        /// 满足 xyz,除去z 有xy 与该xyz 同行 或同列
        /// 满足 xyz,除去z 有xy 与该xyz 同行 或同列
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <returns></returns>
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
