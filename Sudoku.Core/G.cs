using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{
    /// <summary>
    /// 全局类
    /// </summary>
    public   class G
    {
        /// <summary>
        /// 1到9的候选数
        /// </summary>
        public static readonly List<int> AllBaseValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        /// <summary>
        /// 0到8，坐标从0开始，到8结束，每个方向都一致。
        /// </summary>
        public static readonly List<int> baseIndexs = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    }
}
