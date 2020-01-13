using System;

namespace Sudoku.Core
{
    public class DifficultuRatingAttribute : Attribute
    {
        public DifficultuRatingAttribute(double difficultValue)
        {
            this.difficultValue = difficultValue;
        }

        public double difficultValue { get; private set; }
    }
}