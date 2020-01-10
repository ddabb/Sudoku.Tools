﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Console;
using Sudoku.Core;
using System.Collections.Generic;

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
            List<string> queryString = new List<string>
            {

                "000070146000006329006300875000463298603200750000000000100600087062900010800014002",
                "072000498040702365600408712260900184093000627010020953020000839706000241080240576",
                "000037501152000600000500000070102000400750100218000750000305000829476315000090000",
                "001000035430109872000003601000030008900002306350800004100320069243000187000018203",
                "500080603263509000480063009130090000005604310604030000346000070050300201000000030",
                "063740800407000003090306427749618352300004000000200040970060034034097000015400070",
                "534618729297534861600297400760009080009800076853761942976000018000976204000180697",
                "000009400200070030040080690000148000500907080000002900005000009900800003326790050",
                "004072105000400720000500384020004003600000041401080057340000512100043078008000430",
                "080704021201800000003000000902000100805000692010020000050083217008070000107006438",
                "100000005008020000403870200000400600765030000004000002340050076007003090000010023",
                "000000243300080000706020000107302608200000000009040000500800004001095700000000900"
            };

            foreach (var c in queryString)
            {

                Assert.AreEqual(true, StaticTools.SolveSudoku(new QSudoku(c)) == true);
            }


        }

    }
}
