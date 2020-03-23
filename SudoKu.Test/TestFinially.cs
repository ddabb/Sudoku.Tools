using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Console;
using Sudoku.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SudoKu.Test
{
    [TestClass]
    public class TestFinially
    {
        /// <summary>
        /// 最终测试,所有数独都要通过现有方法能够解出来。
        /// </summary>
        //[TestMethod]
        //public void TestFinally()
        //{
        //    var queryString = StaticTools.queryStrings;
        //    var count = 0;
        //    var unsloveList = new List<string>();
        //    foreach (var c in queryString)
        //    {
        //        Debug.WriteLine("c" + c);
        //        if (StaticTools.SolveSudoku(new QSudoku(c)))
        //        {
        //            count += 1;
        //        }
        //        else
        //        {
        //            unsloveList.Add(c);
        //        }
        //    }
        //    Debug.WriteLine("count" + count);
        //    Debug.WriteLine(string.Join(",", unsloveList.Select(c => "\"" + c + "\"\r\n")));
        //    Assert.AreEqual(true, count == queryString.Count);
        //}


        [TestMethod]
        public void TestFinally1()
        {
            var queryString = new List<string> { "000070146000006329006300875000463298603200750000000000100600087062900010800014002" };
            VaildMethod(queryString);
        }

        private static void VaildMethod(List<string> queryString)
        {
            var count = 0;
            var unsloveList = new List<string>();
            foreach (var c in queryString)
            {
                Debug.WriteLine("c" + c);
                if (StaticTools.SolveSudoku(new QSudoku(c)))
                {
                    count += 1;
                }
                else
                {
                    unsloveList.Add(c);
                }
            }
            Debug.WriteLine("count" + count);
            Debug.WriteLine(string.Join(",", unsloveList.Select(c => "\"" + c + "\"\r\n")));
            Assert.AreEqual(true, count == queryString.Count);
        }

        [TestMethod]
        public void TestFinally2()
        {
            var queryString = new List<string> { "001000035430109872000003601000030008900002306350800004100320069243000187000018203" };
            VaildMethod(queryString);
        }

        [TestMethod]
        public void TestFinally3()
        {
            var queryString = new List<string> { "000008000072600100180040006000900560000400729709006000010734000347500600000060347" };
            VaildMethod(queryString);
        }

        [TestMethod]
        public void TestFinally4()
        {
            var queryString = new List<string> { "500080603263509000480063009130090000005604310604030000346000070050300201000000030" };
            VaildMethod(queryString);
        }



        [TestMethod]
        public void TestFinally5()
        {
            var queryString = new List<string> { "063740800407000003090306427749618352300004000000200040970060034034097000015400070" };
            VaildMethod(queryString);
        }

        [TestMethod]
        public void TestFinally6()
        {
            var queryString = new List<string> { "000009400200070030040080690000148000500907080000002900005000009900800003326790050" };
            VaildMethod(queryString);
        }


        [TestMethod]
        public void TestFinally7()
        {
            var queryString = new List<string> { "004072105000400720000500384020004003600000041401080057340000512100043078008000430" };
            VaildMethod(queryString);
        }

        [TestMethod]
        public void TestFinally8()
        {
            var queryString = new List<string> { "080704021201800000003000000902000100805000692010020000050083217008070000107006438" };
            VaildMethod(queryString);
        }

        [TestMethod]
        public void TestFinally9()
        {
            var queryString = new List<string> { "100000005008020000403870200000400600765030000004000002340050076007003090000010023" };
            VaildMethod(queryString);
        }


        [TestMethod]
        public void TestFinally10()
        {
            var queryString = new List<string> { "000000243300080000706020000107302608200000000009040000500800004001095700000000900" };
            VaildMethod(queryString);
        }

        [TestMethod]
        public void TestFinally11()
        {
            var queryString = new List<string> { "000080004107000320000000070000000000064820000900300250750009002003000106000200000" };
            VaildMethod(queryString);
        }

        [TestMethod]
        public void TestFinally12()
        {
            var queryString = new List<string> { "000070340003900721007004509000000405000000890604700213029003650301005902586297134" };
            VaildMethod(queryString);
        }

        [TestMethod]
        public void TestAllHasDesc()
        {
            var descList = TestG.SolveHandlers.Where(c => string.IsNullOrEmpty(c.GetDesc())).ToList();
            if (descList.Count()!=0)
            {
                Debug.Write(descList.First().GetType());

            }

            Assert.AreEqual(true, descList.Count()==0);

        }


        [TestMethod]
        public void TestAllSolveType()
        {
            Assert.AreEqual(TestG.SolveHandlers.Count, TestG.SolveHandlers.Select(c=>c.methodType).Distinct().Count());

        }

    }
}
