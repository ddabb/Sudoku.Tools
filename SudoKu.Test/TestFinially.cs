using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Console;
using Sudoku.Core;
using System.Collections.Generic;
using System.Diagnostics;

namespace SudoKu.Test
{
    [TestClass]
    public class TestFinially
    {
        /// <summary>
        /// 最终测试,所有数独都要通过现有方法能够解出来。
        /// </summary>
        [TestMethod]
        public void TestFinally()
        {
            var queryString = StaticTools.queryStrings;
            var count = 0;
            foreach (var c in queryString)
            {
                Debug.WriteLine("c"+ c);
                if (StaticTools.SolveSudoku(new QSudoku(c)))
                {
                    count += 1;
                }          
            }
            Debug.WriteLine("count" + count);
            Assert.AreEqual(true, count== queryString.Count);
        }

    }
}
