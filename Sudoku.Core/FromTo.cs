﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{
    public class FromTo
    {
        public int fromIndex { get; set; }
        public int toIndex { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is FromTo b)
            {
                return fromIndex==b.fromIndex&&toIndex==b.fromIndex;
            }

            return false;

        }
    }
}
