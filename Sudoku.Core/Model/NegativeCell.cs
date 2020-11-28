using System;
using System.Collections.Generic;
using System.Linq;
namespace Sudoku.Core.Model
{
    public class NegativeCell : CellInfo
    {
        /// <summary>
        /// 前置肯定单元格
        /// </summary>
        public PositiveCell reductionBeforeCell;
        /// <summary>
        /// 后置肯定单元格链表
        /// </summary>
        public List<PositiveCell> reductionAfterCells;
        public NegativeCell(int index, int value, QSudoku sudoku) : base(index, value,sudoku)
        {
            this.CellType =CellType.Negative;
        }
        private List<CellInfo> temp;
        public override bool IsError
        {
            get
            {
                return GetAllParents().Count(c => c.Index == Index && c.CellType == CellType.Init && c.Value != Value) > 0 ||
                       GetAllParents().Count(c => c.Index == Index && c.CellType == CellType.Positive && c.Value == Value) > 0;
            }
        }
        public override List<CellInfo> NextCells
        {
            get
            {
                return InitNextCells();
            }
        }
        public override string Desc => this.Location + "  不是：" + Value;
        /// <summary>
        /// 如果单元格X的值不为a,若单元格只能取a或b，则X的取值为b。
        /// 如果单元格X的值不为a，且同行，同宫，同列，只有另外一个位置X1可以取a,则X1的值为a。
        /// </summary>
        /// <returns></returns>
        public override List<CellInfo> InitNextCells()
        {
            List<CellInfo> cells = new List<CellInfo>();
            if (this.RestCount == 2)
            {
                PositiveCell positive = new PositiveCell(this.Index, RestList.First(c => c != Value), Sudoku)
                {
                    CellType = CellType.Positive,
          
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
        public override List<CellInfo> GetNextCellsFromSudokuCache()
        {
            throw new NotImplementedException();
        }
        private List<CellInfo> GetPostiveCellInDirtion(Func<CellInfo, bool> temp)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var row = Sudoku.GetPossibleIndex(Value, temp);
            if (row.Count == 1)
            {
                PositiveCell positive = new PositiveCell(row.First(), Value, Sudoku)
                {
                    CellType = CellType.Positive,
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
        public List<PositiveCell> GetPositiveCellInfos(List<PossibleIndex> indexs, NegativeCell cell)
        {
            foreach (var unsetCell in this.RelatedUnsetCells)
            {
            }
            List<PositiveCell> cells = new List<PositiveCell>();
            var temp = indexs.Where(c => c.indexs.Count == 2 && c.indexs.Contains(cell.Index) && c.SpeacialValue == cell.Value).ToList();
            foreach (var item in temp)
            {
                var positive =
                    new PositiveCell(item.indexs.First(c => c != cell.Index), cell.Value, this.Sudoku) { Parent = cell };
                cells.Add(positive);
            }
            return cells;
        }
    }
}
