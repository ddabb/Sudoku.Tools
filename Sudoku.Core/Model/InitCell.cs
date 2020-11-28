using System.Collections.Generic;
namespace Sudoku.Core.Model
{
    public class InitCell : CellInfo
    {
        public InitCell(int index, int value,QSudoku sudoku) : base(index, value, sudoku)
        {
            this.CellType = CellType.Init;
        }
        public InitCell(int index, int value) : this(index, value, null)
        {
            this.CellType = CellType.Init;
        }
        public override bool IsError => throw new System.NotImplementedException();
        public override List<CellInfo> NextCells => throw new System.NotImplementedException();
        public override string Desc => this.Location + " 初始值是：" + Value;
        public override List<CellInfo> InitNextCells()
        {
          return new List<CellInfo>();
        }
        public override List<CellInfo> GetNextCellsFromSudokuCache()
        {
            return new List<CellInfo>();
        }
    }
}
