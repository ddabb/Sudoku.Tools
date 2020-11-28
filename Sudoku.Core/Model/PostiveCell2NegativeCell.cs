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
    
    public class PostiveCell2NegativeCell:IChainCell
    {
  
        public PostiveCell2NegativeCell(CellInfo postiveCell,CellInfo negativeCell)
        {
            this.PostiveCell = postiveCell;
            this.negativeCell = negativeCell;
            this.thentype = ThenType.ThenNo;
        }
        public ThenType thentype { get; set; }
        public CellInfo negativeCell { get; set; }
        public CellInfo PostiveCell { get; set; }
        public List<IChainCell> Children { get; set; }
        public IChainCell Parent { get; set; }
    }
}
