using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core.Model
{
    /// <summary>
    /// 强链 NegativeCell.NextCell的缓存信息
    /// </summary>

    public class NegativeCell2PostiveCell
    {
        public CellInfo negativeCell;
        public CellInfo PostiveCell;

        public NegativeCell2PostiveCell(CellInfo negativeCell, CellInfo postiveCell)
        {
            this.PostiveCell = postiveCell;
            this.negativeCell = negativeCell;
        }
    }
}
