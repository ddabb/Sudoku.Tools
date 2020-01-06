using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{
    public class NegativeCellInfo : CellInfo
    {
        /// <summary>
        /// 前置肯定单元格
        /// </summary>
        public PositiveCellInfo reductionBeforeCell;


        /// <summary>
        /// 后置肯定单元格链表
        /// </summary>
        public List<PositiveCellInfo> reductionAfterCells;
        public NegativeCellInfo(int index, int value) : base(index, value)
        {

        }

        public override CellInfo parent { get; set; }
        public override List<CellInfo> NextCells { get; set; }


        public override List<CellInfo> GetNextCells(QSudoku sudoku)
        {
            throw new NotImplementedException();
        }
    }
}
