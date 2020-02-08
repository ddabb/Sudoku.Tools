using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core
{
   public static class IntExtenstions
    {
        public static string LoctionDesc(this int input)
        {
            var Row = input / 9;
            var Column = input % 9;
            var Block = Row / 3 * 3 + Column / 3;
            if (G.LocationType==LocationType.R1C1)
            {
                return "R" + (Row + 1) + "C" + (Column + 1);
            }
            else
            {
                return G.alpha[Row] + (Column + 1);
            }


        }
    }
}
