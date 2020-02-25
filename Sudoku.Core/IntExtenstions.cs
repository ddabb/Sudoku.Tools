using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core
{
   public static class IntExtenstions
    {
        public static SolveMessage LoctionDesc(this int input)
        {
            if (G.LocationType == LocationType.R1C1)
            {
                return Enum.GetValues(typeof(AllR1C1)).Cast<AllR1C1>().ToList()[input];
            }
            else
            {
                return Enum.GetValues(typeof(AllA1I9)).Cast<AllA1I9>().ToList()[input];


            }
        
            
        }
    }
}
