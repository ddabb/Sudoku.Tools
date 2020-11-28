using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core.Model;
namespace Sudoku.Core
{
    public class LocationGroup
    {
        public string LocationDesc { get; set; }
        public List<CellInfo> cells;
        public LocationGroup(List<CellInfo> cells,string locationDesc)
        {
            this.cells = cells;
            this.LocationDesc = locationDesc;
        }
        public List<int> CellIndexs
        {
            get { return cells.Select(c => c.Index).ToList(); }
        }
    }
}
