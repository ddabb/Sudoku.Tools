using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core
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

        public override List<CellInfo> GetNextCells()
        {
            throw new System.NotImplementedException();
        }
    }
}
