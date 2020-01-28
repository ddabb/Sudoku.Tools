using System;
using System.Collections.Generic;
using System.Linq;

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

        private List<CellInfo> temp;


        public override bool IsError
        {
            get
            {
                //var sameValueList = this.NextCells.Where(c => c.Value == Value).ToList();
                //var sameValueCount = sameValueList.Count();
                //var distinctBlock = sameValueList.Select(c => c.Block).Distinct().Count();
                //var distinctRow = sameValueList.Select(c => c.Row).Distinct().Count();
                //var distinctColumn = sameValueList.Select(c => c.Column).Distinct().Count();
                //if (distinctBlock< sameValueCount|| distinctRow < sameValueCount|| distinctColumn < sameValueCount)
                //{
                //    return true;
                //}

                return false;
            }
        }


        public override List<CellInfo> NextCells
        {
            get
            {
                //var list = temp;
                //if (list != null)
                //{
                //    return list;
                //}

                return GetNextCells();
            }

        }

        /// <summary>
        /// 如果单元格X的值不为a,若单元格只能取a或b，则X的取值为b。
        /// 如果单元格X的值不为a，且同行，同宫，同列，只有另外一个位置X1可以取a,则X1的值为a。
        /// </summary>
        /// <returns></returns>
        public override List<CellInfo> GetNextCells()
        {

            List<CellInfo> cells = new List<CellInfo>();

            if (this.RestCount == 2)
            {
                PositiveCellInfo positive = new PositiveCellInfo(this.Index, RestList.First(c => c != Value))
                {
                    CellType = CellType.Init,
                    Sudoku = this.Sudoku,
                    Parent = this,
                    Level = this.Level + 1,
                    Fromto = { fromIndex = this.Index, toIndex = this.Index }
                };
                cells.Add(positive);
            }

            foreach (var item in GetPostiveCellInDirtion(UnSetCellInSameRow()).Where(item => !cells.Exists(c => c.Value == item.Value && c.Index == item.Index)))
            {
                if (Sudoku.AllChainsIndex.Contains(item.Index))
                {
                    cells.Add(item);
                }
                
            }
            foreach (var item in GetPostiveCellInDirtion(UnSetCellInSameColumn()).Where(item => !cells.Exists(c => c.Value == item.Value && c.Index == item.Index)))
            {
                if (Sudoku.AllChainsIndex.Contains(item.Index))
                {
                    cells.Add(item);
                }

            }
            foreach (var item in GetPostiveCellInDirtion(UnSetCellInSameBlock()).Where(item => !cells.Exists(c => c.Value == item.Value && c.Index == item.Index)))
            {
                if (Sudoku.AllChainsIndex.Contains(item.Index))
                {
                    cells.Add(item);
                }
            }

            if (this.Parent != null)
                cells = cells.Where(c => !(c.Index == Parent.Index && c.Value == Parent.Value)).ToList();
            return cells.ToList();
        }

        private List<CellInfo> GetPostiveCellInDirtion(Func<CellInfo, bool> temp)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var row = Sudoku.GetPossibleIndex(Value, temp);
            if (row.Count == 1)
            {
                PositiveCellInfo positive = new PositiveCellInfo(row.First(), Value)
                {
                    CellType = CellType.Init,
                    Sudoku = this.Sudoku,
                    Parent = this,
                    Level = this.Level + 1,
                    Fromto = {fromIndex = this.Index},
                };
                positive.Fromto.toIndex = positive.Index;
                cells.Add(positive);
            }
            return cells;
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
                var positive =
                    new PositiveCellInfo(item.indexs.First(c => c != cell.Index), cell.Value) { Parent = cell };
                cells.Add(positive);
            }
            return cells;
        }
    }
}
