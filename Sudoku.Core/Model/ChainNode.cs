using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core.Model
{
    /// <summary>
    /// 链节点
    /// </summary>
    public class ChainNode
    {
        public int Index;
        public int Value;
        public List<ChainNode> childrens;
        public ChainNode parent;
        public CellType cellType;
        public List<ChainNode> AllParent;
        public List<IChainCell> allChainCells;

        public ChainNode(CellInfo startCellInfo,CellInfo endCellInfo, List<IChainCell> allChainCells)
        {
            this.Index = startCellInfo.Index;
            this.Value = startCellInfo.Value;
            this.cellType = startCellInfo.CellType;
       
        }

        public void Bulid()
        {
            if (this.cellType== CellType.Negative)
            {
                
            }
            else if (this.cellType == CellType.Negative)
            {
                    
            }
        }

        /// <summary>
        /// 能找到一条这样的链；
        /// </summary>
        public bool CanFindChain
        {
          get{ return false; }  
        }
    }
}
