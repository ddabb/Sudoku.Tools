using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Core
{
    public class PositiveCellInfo : CellInfo
    {


        public PositiveCellInfo(int index, int value) : base(index, value)
        {
            this.CellType = CellType.Positive;

            if (Index==70&&Value==5)
            {

            }

        }

        private List<CellInfo> temp;


        public override bool IsError {
            get
            {
                return GetAllParents().Count(c => c.Index == Index && c.CellType == CellType.Positive && c.Value != Value)>0||
                    GetAllParents().Count(c => c.Index == Index && c.CellType == CellType.Negative && c.Value == Value) > 0;
            }

        }

        public override List<CellInfo> NextCells
        {
            get { return temp ?? (temp = GetNextCells()); }

        }

        /// <summary>
        /// 如果指定单元格为真,则其关联的单元格都不能取该值。获取这些单元格的位置信息。
        /// </summary>
        /// <returns></returns>
        public override List<CellInfo> GetNextCells()
        {
            List<CellInfo> cellsA = new List<CellInfo>();
            var cells = new List<CellInfo>();
            if (Parent != null)
            {
                cells=   this.RelatedUnsetCells.Where(c => c.Index != Parent.Index && c.GetRest().Contains(this.Value)).ToList();
            }
            else
            {
                cells = this.RelatedUnsetCells.Where(c => c.GetRest().Contains(this.Value)).ToList();
            }

            foreach (var cellInfo in cells)
            {
                NegativeCellInfo cell = new NegativeCellInfo(cellInfo.Index, Value)
                {
                    CellType = CellType.Negative,
                    Sudoku = this.Sudoku,
                    Parent = this,
                    Level = this.Level+1
                    
                };
                cell.Fromto.fromIndex = this.Index;
                cell.Fromto.toIndex = cell.Index;
                cellsA.Add(cell);
            }
            return cellsA;

        }
    }
}
