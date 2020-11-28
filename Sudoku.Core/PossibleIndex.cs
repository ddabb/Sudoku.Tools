using System;
using System.Collections.Generic;
using System.Text;
namespace Sudoku.Core
{
    public class PossibleIndex
    {
        public Direction direction;
        public int directionIndex;
        public int SpeacialValue;
        public List<int> indexs;
        public PossibleIndex(Direction direction, int directionIndex, int speacialValue, List<int> indexs)
        {
            this.direction = direction;
            this.directionIndex = directionIndex;
            this.SpeacialValue = speacialValue;
            indexs.Sort();
            this.indexs = indexs;
        }
        public override string ToString()
        {
            return "在" + directionIndex + "" + G.GetEnumDescription(direction) + "值" + SpeacialValue + "可能位置" + indexs.JoinString() + "direction value" + direction;
        }
    }
}
