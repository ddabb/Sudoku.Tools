using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core.Model
{
    /// <summary>
    /// 强链 PostiveCell.NextCell的缓存信息
    /// </summary>

    
    public class PostiveCell2NegativeCell
    {
        public CellInfo negativeCell;
        public CellInfo PostiveCell;

        public PostiveCell2NegativeCell(CellInfo postiveCell,CellInfo negativeCell)
        {
            this.PostiveCell = postiveCell;
            this.negativeCell = negativeCell;
        }
    }
}
