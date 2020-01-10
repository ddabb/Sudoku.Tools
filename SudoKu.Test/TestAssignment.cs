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
    public class TestAssignment
    {
        [TestMethod]
        public void TestHiddenSingleColumnHandler()
        {
            HiddenSingleColumnHandler hander = new HiddenSingleColumnHandler();
            QSudoku qsudoku = new QSudoku("000000000000040329000651847000000500000000473008473296004000900000005180060180700");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestClaimingInRowHandler()
        {
            ClaimingInRowHandler hander = new ClaimingInRowHandler();
            QSudoku qsudoku = new QSudoku("000000360000000427000563810000000040000000936005630081006300000080406193031872054");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestXYWingHandler1()
        {
            XYWingHandler hander = new XYWingHandler();
            QSudoku qsudoku = new QSudoku("056200970749536812028079000000003129000920040290607008815792060902304080000005290");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            Assert.AreEqual(true, cells.Count > 0);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 1));
            Assert.AreEqual(true, cells.Exists(c => c.Value == 8));
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }


        

        [TestMethod]
        public void TestClaimingInColumnHandler()
        {
            ClaimingInColumnHandler hander = new ClaimingInColumnHandler();
            QSudoku qsudoku = new QSudoku("000020080040009003000005700000000030805070020037004000070080056090000300100040000");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestXYWingHandler()
        {
            XYWingHandler hander = new XYWingHandler();
            QSudoku qsudoku = new QSudoku("300417826000359741010008935002000000100090000573184260800900504030045082040801093");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestXYWingHandler2()
        {
            XYWingHandler hander = new XYWingHandler();
            QSudoku qsudoku = new QSudoku("860035900700068351530074020070810530005307100183540200020650703057400002010700495");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestXYWingHandler3()
        {
            XYWingHandler hander = new XYWingHandler();
            QSudoku qsudoku = new QSudoku("409016020072000006000203094700458060854621030006007548000102479000000685947865010");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestForcingChainHandler11()
        {
            ForcingChainHandler hander = new ForcingChainHandler();
            QSudoku qsudoku = new QSudoku("104900853385140000070358000653714298741800536800635417007580300508403000430201085");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Assert.AreEqual(true, cells.Exists(c=>c.Value==7));
            Assert.AreEqual(true, cells.Exists(c => c.Index == 5));
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestForcingChainHandler55()
        {
            ForcingChainHandler hander = new ForcingChainHandler();
            QSudoku qsudoku = new QSudoku("000400900324006000060500040000000600040160800600085090718003000000000080400070023");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 4));
            Assert.AreEqual(true, cells.Exists(c => c.Index == 53));
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }
        [TestMethod]
        public void TestForcingChainHandler33()
        {
            ForcingChainHandler hander = new ForcingChainHandler();
            QSudoku qsudoku = new QSudoku("007000000800439000490071000000000906002000040008103000000002308005300000000710052");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 9));
            Assert.AreEqual(true, cells.Exists(c => c.Index == 74));
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }
        

        [TestMethod]
        public void TestForcingChainHandler1()
        {
            ForcingChainHandler hander = new ForcingChainHandler();
            QSudoku qsudoku = new QSudoku("670000010931746000520009700462538070197624008853971000716400003389067400245003007");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 3));
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }


        [TestMethod]
        public void TestForcingChainHandler2()
        {
            ForcingChainHandler hander = new ForcingChainHandler();
            QSudoku qsudoku = new QSudoku("060300570052041006000605100580004267429006315607002498006207000005410620290063700");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 7));
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestForcingChainHandler4()
        {
            ForcingChainHandler hander = new ForcingChainHandler();
            QSudoku qsudoku = new QSudoku("030050400000004000200006090007600009080000500300800041073060120020003900050001374");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 6));
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        

        [TestMethod]
        public void TestXYChainHandler()
        {
            XYChainHandler hander = new XYChainHandler();
            QSudoku qsudoku = new QSudoku("060300570052041006000605100580004267429006315607002498006207000005410620290063700");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 7));
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestXYZWingHandler()
        {
            XYZWingHandler hander = new XYZWingHandler();
            QSudoku qsudoku = new QSudoku("869453721000921568215800439621534987407610352000200146000102803932785614100340205");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 8));
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        
        [TestMethod]
        public void TestRemotePairHandler()
        {
            RemotePairHandler hander = new RemotePairHandler();
            QSudoku qsudoku = new QSudoku("450106237073000165261573489316000578724865913005731642542317896130650724607002351");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 4));
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        


        [TestMethod]
        public void TestSwordfishHandler()
        {
            SwordfishHandler hander = new SwordfishHandler();
            QSudoku qsudoku = new QSudoku("000074200200906740004520009100457928547892000002600574400705092005209403629040057");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestNakedPairHandller()
        {
            NakedPairHandller hander = new NakedPairHandller();
            QSudoku qsudoku = new QSudoku("980006375376850140000700860569347218000000537723581496000205780000000950000008620");
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestNakedTripleHandler()
        {
            NakedTripleHandler hander = new NakedTripleHandler();
            QSudoku qsudoku = new QSudoku("390000700000000650507000349049380506601054983853000400900800134002940865400000297");
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            Assert.AreEqual(23, cells[0].Index);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestNakedTripleHandler2()
        {
            NakedTripleHandler hander = new NakedTripleHandler();
            QSudoku qsudoku = new QSudoku("006300150045026000000000060090500700052670300700001005500000041004007000080090000");
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            Assert.AreEqual(true, cells.Count > 0);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestDirectPointingHandler()
        {
            DirectPointingHandler hander = new DirectPointingHandler();
            QSudoku qsudoku = new QSudoku("000436517000280000006170000000061070001000000050804100000043761003610000000000394");
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            Assert.AreEqual(2, cells.Count);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 9));
            Assert.AreEqual(true, cells.Exists(c => c.Value == 8));
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestHiddenTripleHandler()
        {
            HiddenTripleHandler hander = new HiddenTripleHandler();
            QSudoku qsudoku = new QSudoku("015070000900040105834651000050097018108065000000180526403518000060030851581026043");
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 4));
            Assert.AreEqual(true, cells.Exists(c => c.Index == 8));
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestWWingHandler()
        {
            WWingHandler hander = new WWingHandler();
            QSudoku qsudoku = new QSudoku("012009600600012094749635821428397100397156482156020009904573210070261940201900007");
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 8));
            Assert.AreEqual(true, cells.Exists(c => c.Index == 0));
            Assert.AreEqual(true, cells.Count==1);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestSkyscraperHandler()
        {
            SkyscraperHandler hander = new SkyscraperHandler();
            QSudoku qsudoku = new QSudoku("805630070003705800027081305236894157481257030579163284702300000304000700198076023");
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 9));
            Assert.AreEqual(true, cells.Exists(c => c.Index == 6));
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestTwoStringsKiteHandler()
        {
            TwoStringsKiteHandler hander = new TwoStringsKiteHandler();
            QSudoku qsudoku = new QSudoku("081020600042060089056800240693142758428357916175689324510036892230008460860200000");
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 7));

            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }
        [TestMethod]
        public void TestTwoStringsKiteHandler1()
        {
            TwoStringsKiteHandler hander = new TwoStringsKiteHandler();
            QSudoku qsudoku = new QSudoku("256009170010000003400001500645007281000182465821564739000210000102003807500008012");
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 4));

            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        

        [TestMethod]
        public void TestWXYZWingHandler()
        {
            WXYZWingHandler hander = new WXYZWingHandler();
            QSudoku qsudoku = new QSudoku("000109030190700006300286001581472003900568002600391785700915008210007050050020000");
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 7));

            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }



        [TestMethod]
        public void TestTurbotFishNormalHandler1()
        {
            TurbotFishNormalHandler hander = new TurbotFishNormalHandler();
            QSudoku qsudoku = new QSudoku("700002600000000900090615074009100703400020859073059100040000500927500400600001097");
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Assignment(qsudoku);
            Assert.AreEqual(true, cells.Exists(c => c.Value == 2));

            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudoku = qsudoku.ApplyCells(cells);
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }







    }
}
