using Sudoku.Console;
using Sudoku.Core.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
namespace SudoKu.Test
{
    public class TestFinially
    {
        /// <summary>
        /// 最终测试,所有数独都要通过现有方法能够解出来。
        /// </summary>
        //[Fact]
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
        //    Debug.WriteLine(string.Join(",", unsloveList.Select(c => "\"" + c + "\"\t\t\r\n")));
        //    Assert.AreEqual(true, count == queryString.Count);
        //}
        [Theory]
        [InlineData(
            "000070146000006329006300875000463298603200750000000000100600087062900010800014002",
            "598643002003759648674128593457200830906307425032405060005904380341872956009530204",
            "001000035430109872000003601000030008900002306350800004100320069243000187000018203",
            "000008000072600100180040006000900560000400729709006000010734000347500600000060347",
            "500080603263509000480063009130090000005604310604030000346000070050300201000000030",
            "000070340003900721007004509000000405000000890604700213029003650301005902586297134",
            "000080004107000320000000070000000000064820000900300250750009002003000106000200000",
            "000000243300080000706020000107302608200000000009040000500800004001095700000000900",
            "100000005008020000403870200000400600765030000004000002340050076007003090000010023",
            "080704021201800000003000000902000100805000692010020000050083217008070000107006438",
            "004072105000400720000500384020004003600000041401080057340000512100043078008000430",
            "000009400200070030040080690000148000500907080000002900005000009900800003326790050",
            "063740800407000003090306427749618352300004000000200040970060034034097000015400070"
            )]
        public void TestFinally1(params string[] list)
        {
            VaildMethod(list);
        }
        [Fact]
        public void TestFinallyEmptyRectangle()
        {
            var queryString = new List<string> {  };
            VaildMethod(queryString);
        }
        
        private static void VaildMethod(ICollection<string> queryString)
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
            Debug.WriteLine(string.Join(",", unsloveList.Select(c => "\"" + c + "\"\t\t\r\n")));
            Assert.True( count == queryString.Count);
        }
        [Fact]
        public void TestAllHasDesc()
        {
            var descList = TestG.SolveHandlers.Where(c => string.IsNullOrEmpty(c.GetDesc())).ToList();
            if (descList.Count()!=0)
            {
                Debug.Write(descList.First().GetType());
            }
            Assert.True( descList.Count()==0);
        }
        [Fact]
        public void TestAllSolveType()
        {
            Assert.Equal(TestG.SolveHandlers.Count, TestG.SolveHandlers.Select(c=>c.methodType).Distinct().Count());
        }
    }
}
