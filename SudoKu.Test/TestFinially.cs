using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
                "100000005008020000403870200000400600765030000004000002340050076007003090000010023",
                "000070146000006329006300875000463298603200750000000000100600087062900010800014002",
                "072000498040702365600408712260900184093000627010020953020000839706000241080240576",
                "000037501152000600000500000070102000400750100218000750000305000829476315000090000",
                "001000035430109872000003601000030008900002306350800004100320069243000187000018203",
                "500080603263509000480063009130090000005604310604030000346000070050300201000000030",
                "063740800407000003090306427749618352300004000000200040970060034034097000015400070",
                "534618729297534861600297400760009080009800076853761942976000018000976204000180697",
                "000009400200070030040080690000148000500907080000002900005000009900800003326790050",
                "004072105000400720000500384020004003600000041401080057340000512100043078008000430",
                "080704021201800000003000000902000100805000692010020000050083217008070000107006438"
            };
            foreach (var item in queryString)
            {
                Assert.AreEqual(true, new DanceLink().isValid(item));
            }
            foreach (var item in queryString)
            {
                var assembly = typeof(SolverHandlerBase).Assembly;
                var types = assembly.GetTypes().Where(t => typeof(ISudokuSolveHelper).IsAssignableFrom(t) && t.IsAbstract == false);
                var tryagain = false;
                QSudoku example = new QSudoku(item);
                List<Type> types1 = new List<Type>();
                do
                {

                    tryagain = false;
                    foreach (var type in types)
                    {
                        object[] objs = type.GetCustomAttributes(typeof(AssignmentExampleAttribute), true);
                        try
                        {
                            if (!types1.Contains(type))
                            {
                                var cellinfos =
                                    ((ISudokuSolveHelper)Activator.CreateInstance(type, true)).Assignment(
                                      example);

                                if (cellinfos.Count != 0)
                                {
                                    Debug.WriteLine("type" + type);
                                    Debug.WriteLine("cellinfo" + string.Join("\r\n", cellinfos));
                                    Debug.WriteLine("example before" + example.QueryString + "isvalid" + new DanceLink().isValid(example.QueryString));

                                    example = example.ApplyCells(cellinfos);
                                    Debug.WriteLine("example after " + example.QueryString + "isvalid" + new DanceLink().isValid(example.QueryString));
                                    if (!example.IsAllSeted)
                                    {
                                        tryagain = true;
                                    }
                                    Debug.WriteLine("\r\n");

                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("is not implemented"))
                            {
                                types1.Add(type);
                            }




                        }


                    }
                } while (tryagain);

                Assert.AreEqual(true, example.IsAllSeted);
                Assert.AreEqual(true, new DanceLink().isValid(example.QueryString));
            }


        }

    }
}
