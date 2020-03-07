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

        public static SolveMessage RowDesc(this int input)
        {
            return new SolveMessage("R" + (input + 1), MessageType.Location);
        }

        public static SolveMessage ColumnDesc(this int input)
        {
            return new SolveMessage("C" + (input + 1), MessageType.Location);
        }

        public static SolveMessage BlockDesc(this int input)
        {
            return new SolveMessage("B" + (input + 1), MessageType.Location);
        }

    }


}

