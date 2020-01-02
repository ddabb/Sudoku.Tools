using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SudoKu.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestHiddenSingleColumnHandler()
        {
            HiddenSingleColumnHandler hander = new HiddenSingleColumnHandler();
            QSudoku qsudu = new QSudoku("000000000000040329000651847000000500000000473008473296004000900000005180060180700");
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        [TestMethod]
        public void TestClaimingInRowHandler()
        {
            ClaimingInRowHandler hander = new ClaimingInRowHandler();
            QSudoku qsudu = new QSudoku("000000360000000427000563810000000040000000936005630081006300000080406193031872054");
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        [TestMethod]
        public void TestClaimingInColumnHandler()
        {
            ClaimingInColumnHandler hander = new ClaimingInColumnHandler();
            QSudoku qsudu = new QSudoku("000020080040009003000005700000000030805070020037004000070080056090000300100040000");
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        [TestMethod]
        public void TestXYWingHandler()
        {
            XYWingHandler hander = new XYWingHandler();
            QSudoku qsudu = new QSudoku("300417826000359741010008935002000000100090000573184260800900504030045082040801093");
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        [TestMethod]
        public void TestXYWingHandler2()
        {
            XYWingHandler hander = new XYWingHandler();
            QSudoku qsudu = new QSudoku("860035900700068351530074020070810530005307100183540200020650703057400002010700495");
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        [TestMethod]
        public void TestNakedPairHandller()
        {
            NakedPairHandller hander = new NakedPairHandller();
            QSudoku qsudu = new QSudoku("980006375376850140000700860569347218000000537723581496000205780000000950000008620");
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        [TestMethod]
        public void TestNakedTripleHandler()
        {
            NakedTripleHandler hander = new NakedTripleHandler();
            QSudoku qsudu = new QSudoku("390000700000000650507000349049380506601054983853000400900800134002940865400000297");
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            Assert.AreEqual(23, cells[0].Index);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        [TestMethod]
        public void TestNakedTripleHandler2()
        {
            NakedTripleHandler hander = new NakedTripleHandler();
            QSudoku qsudu = new QSudoku("006300150045026000000000060090500700052670300700001005500000041004007000080090000");
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            Assert.AreEqual(true, cells.Count>0);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        [TestMethod]
        public void TestDirectPointingHandler()
        {
            DirectPointingHandler hander = new DirectPointingHandler();
            QSudoku qsudu = new QSudoku("000436517000280000006170000000061070001000000050804100000043761003610000000000394");
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 9));
            Assert.AreEqual(true, cells.Exists(c => c.Value == 8));
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        [TestMethod]
        public void TestHiddenTripleHandler()
        {
            HiddenTripleHandler hander = new HiddenTripleHandler();
            QSudoku qsudu = new QSudoku("015070000900040105834651000050097018108065000000180526403518000060030851581026043");
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 4));
            Assert.AreEqual(true, cells.Exists(c => c.Index == 8));
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        [TestMethod]
        public void TestWWingHandler()
        {
            WWingHandler hander = new WWingHandler();
            QSudoku qsudu = new QSudoku("015070000900040105834651000050097018108065000000180526403518000060030851581026043");
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 4));
            Assert.AreEqual(true, cells.Exists(c => c.Index == 8));
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        [TestMethod]
        public void TestSkyscraperHandler()
        {
            SkyscraperHandler hander = new SkyscraperHandler();
            QSudoku qsudu = new QSudoku("072000498040702365600408712260900184093000627010020953020000839706000241080240576");
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudu.QueryString));
            var cells = hander.Assignment(qsudu);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 4));
            Assert.AreEqual(true, cells.Exists(c => c.Index == 8));
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }

        


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
                "072000498040702365600408712260900184093000627010020953020000839706000241080240576"
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
                QSudoku example=new QSudoku(item);
                List<Type> types1 = new List<Type>();
                do
                {

                    tryagain = false;
                    foreach (var type in types)
                    {
                        object[] objs = type.GetCustomAttributes(typeof(ExampleAttribute), true);
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
