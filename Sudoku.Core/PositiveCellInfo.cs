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

 


        public override bool IsError {
            get
            {
                return GetAllParents().Count(c => c.Index == Index && c.CellType == CellType.Positive && c.Value != Value)>0||
                    GetAllParents().Count(c => c.Index == Index && c.CellType == CellType.Negative && c.Value == Value) > 0;
            }

        }

        public override List<CellInfo> NextCells
        {
            get { return GetNextCells(); }

        }

        /// <summary>
        /// 如果指定单元格为真,则其关联的单元格都不能取该值。获取这些单元格的位置信息。
        /// </summary>
        /// <returns></returns>
        public override List<CellInfo> GetNextCells()
        {
            if (Index == 68 && Value == 9)
            {

            }
            List<CellInfo> cellsA = new List<CellInfo>();
            var cells = new List<CellInfo>();
  

            cells = this.RelatedUnsetCells.Where(c => c.RestList.Contains(this.Value)&&Sudoku.AllChainsIndex.Contains(c.Index)).ToList();
            foreach (var cellInfo in cells)
            {
                NegativeCellInfo cell = new NegativeCellInfo(cellInfo.Index, Value)
                {
                    CellType = CellType.Negative,
                    Sudoku = this.Sudoku,
                    Parent = this,
             
                    Fromto = {fromIndex = this.Index},
                };
                cell.Level = Level + 1;

                cell.Fromto.toIndex = cell.Index;
                cellsA.Add(cell);
            }
            foreach (var item in this.RestList.Where(c=>c!=Value))
            {
                NegativeCellInfo cell = new NegativeCellInfo(Index, item)
                {
                    CellType = CellType.Negative,
                    Sudoku = this.Sudoku,
                    Parent = this,
               

                };
                cell.Level = Level + 1;
                cell.Fromto.fromIndex = this.Index;
                cell.Fromto.toIndex = cell.Index;
                cellsA.Add(cell);
            }
            if (this.Parent != null)
                cellsA = cellsA.Where(c => !(c.Index == Parent.Index && c.Value == Parent.Value)).ToList();
            return cellsA.ToList();

        }
    }
}
