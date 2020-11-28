using System;
using System.Collections.Generic;
using System.Text;
namespace Sudoku.Core
{
    public class fangxiang
    {
        public int fromIndex { get; set; }
        public int toIndex { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is fangxiang b)
            {
                return fromIndex==b.fromIndex&&toIndex==b.fromIndex;
            }
            return false;
        }
    }
}
