using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Core;
using Sudoku.Tools;
using System.Diagnostics;

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
            var cells = hander.Excute(qsudu);
            foreach (var item in cells)
            {
                Debug.WriteLine(""+item);
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
            var cells = hander.Excute(qsudu);
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
            var cells = hander.Excute(qsudu);
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
            var cells = hander.Excute(qsudu);
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
            var cells = hander.Excute(qsudu);
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
            var cells = hander.Excute(qsudu);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
            qsudu = qsudu.ApplyCells(cells);
            Debug.WriteLine(qsudu.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudu.QueryString));
        }




    }
}
