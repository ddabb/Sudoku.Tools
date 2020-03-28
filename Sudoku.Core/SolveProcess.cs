using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core.Model;

namespace Sudoku.Core
{
   /// <summary>
   /// 求解过程
   /// </summary>
    public class SolveProcess
    {
        public List<CellInfo> IfCellInfos;
        public List<CellInfo> ThenCellInfos;

        public string SolveDesc;
        public CellInfo finallyCell;
    }
}
