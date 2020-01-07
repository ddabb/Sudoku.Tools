using System;
using System.Collections.Generic;
using System.Linq;
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

        public override CellInfo Parent { get; set; }
        public override List<CellInfo> NextCells { get; set; }


        public override List<CellInfo> GetNextCells()
        {
            throw new NotImplementedException();
        }


        public List<PositiveCellInfo> GetPositiveCellInfos(List<PossibleIndex> indexs, NegativeCellInfo cell)
        {
            foreach (var unsetCell in this.RelatedUnsetCells)
            {
             
            }
            List<PositiveCellInfo> cells = new List<PositiveCellInfo>();
            var temp = indexs.Where(c => c.indexs.Count == 2 && c.indexs.Contains(cell.Index) && c.SpeacialValue == cell.Value).ToList();
            foreach (var item in temp)
            {
                var positive = new PositiveCellInfo(item.indexs.First(c => c != cell.Index), cell.Value);
                positive.Parent = cell;
                cells.Add(positive);
            }
            return cells;
        }
    }
}
