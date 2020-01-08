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

        private List<CellInfo> temp;


        public override bool IsError
        {
            get { return false; }
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
            if (Index== 32 && Value==2)
            {

            }
            List <CellInfo> cells=new List<CellInfo>();

            if (this.GetRest().Count==2)
            {
                PositiveCellInfo positive = new PositiveCellInfo(this.Index, GetRest().First(c => c != Value))
                {
                    CellType = CellType.Positive, Sudoku = this.Sudoku, Parent = this,
                    Level = this.Level + 1
                };
                positive.Fromto.fromIndex = this.Index;
                positive.Fromto.toIndex = this.Index;
                cells.Add(positive);
            }
            
            cells.AddRange(GetPostiveCellInDirtion(UnSetCellInSameRow()));
            cells.AddRange(GetPostiveCellInDirtion(UnSetCellInSameColumn()));
            cells.AddRange(GetPostiveCellInDirtion(UnSetCellInSameBlock()));
            return cells;
        }

        private List<CellInfo> GetPostiveCellInDirtion(Func<CellInfo, bool> temp)
        {
            List<CellInfo> cells =new List<CellInfo>();
            var row = Sudoku.GetPossibleIndex(Value, temp);
            if (row.Count == 1)
            {
                if (Parent == null || Parent.Index != Row)
                {
                    PositiveCellInfo positive = new PositiveCellInfo(row.First(), Value)
                    {
                        CellType = CellType.Positive,
                        Sudoku = this.Sudoku,
                        Parent = this,
                        Level = this.Level + 1,
                   
                    };
                    positive.Fromto.fromIndex = this.Index;
                    positive.Fromto.toIndex = positive.Index;
                    cells.Add(positive);
                }
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
                    new PositiveCellInfo(item.indexs.First(c => c != cell.Index), cell.Value) {Parent = cell};
                cells.Add(positive);
            }
            return cells;
        }
    }
}
