using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{
    public class PositiveCellInfo : CellInfo
    {
        /// <summary>
        /// 前置否定单元格
        /// </summary>
        public NegativeCellInfo reductionBeforeCell;
        /// <summary>
        /// 后置否定单元格链表
        /// </summary>
        public List<NegativeCellInfo> reductionAfterCells;
        public PositiveCellInfo(int index, int value) : base(index, value)
        {


        }
    }
}
