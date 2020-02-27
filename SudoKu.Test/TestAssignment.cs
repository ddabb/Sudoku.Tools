using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SudoKu.Test
{
    [TestClass]
    public class TestAssignment
    {
        [TestMethod]
        public void TestHiddenSingleColumnHandler()
        {
            TestAssignmentExample(typeof(HiddenSingleColumnHandler));
        }

        private static void TestAssignmentExample(Type type)
        {
            object[] objs = type.GetCustomAttributes(typeof(AssignmentExampleAttribute), true);
            if (objs.Count() != 1) return;
            if (!(objs[0] is AssignmentExampleAttribute a)) return;
            var queryString = a.queryString;
            var value = a.value;
            var positionString = a.positionString;
            var handers = a.SolveHandlers;
            TestAssignmentExample(type, queryString, value, positionString, handers);
        }


        private static void TestAssignmentExample(Type type, string queryString, int value, string positionString, SolveMethodEnum[] handlerEnums = null)
        {
            var qsudoku = new QSudoku(queryString);
            if (handlerEnums != null)
            {
                foreach (var handerEnum in handlerEnums)
                {
                    var eliminationHanders = TestG.SolveHandlers.First(c => handerEnum == (c.methodType));
                    var removeCells = eliminationHanders.Elimination(qsudoku);
                    qsudoku.RemoveCells(removeCells);
                }
            }
            var cellinfo =
                ((ISudokuSolveHandler)Activator.CreateInstance(type, true)).Assignment(
                    qsudoku);
            Assert.AreEqual(true, cellinfo.Exists(c => c.RrCc == positionString && c.Value == value));
            Debug.WriteLine("cellinfo " + cellinfo.JoinString());
            qsudoku = qsudoku.ApplyCells(cellinfo);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }

        [TestMethod]
        public void TestClaimingInRowHandler()
        {
            TestAssignmentExample(typeof(ClaimingInRowHandler));
        }

        [TestMethod]
        public void TestULSize6Type2Handler()
        {
            TestAssignmentExample(typeof(ULSize6Type2Handler));
        }

        [TestMethod]
        public void TestULSize6Type2Handler1()
        {
            TestAssignmentExample(typeof(ULSize6Type2Handler), "016705920492160750057290106273600589145879000689352417500900001900500800720406395", 3, "R3C8");
        }



        [TestMethod]
        public void TestXRSize6Type1Handler()
        {
            TestAssignmentExample(typeof(XRSize6Type1Handler));
        }

        [TestMethod]
        public void TestXRSize6Type2Handler()
        {
            TestAssignmentExample(typeof(XRSize6Type2Handler));
        }

        [TestMethod]
        public void TestXRSize6Type3Handler()
        {
            TestAssignmentExample(typeof(XRSize6Type3Handler));
        }

        [TestMethod]
        public void TestBugType1Handler()
        {
            TestAssignmentExample(typeof(BugType1Handler));
        }

        [TestMethod]
        public void TestXRSize6Type4Handler()
        {
            TestAssignmentExample(typeof(XRSize6Type4Handler));
        }

        [TestMethod]
        public void TestXRSize8Handler()
        {
            TestAssignmentExample(typeof(XRSize8Handler));
        }




        [TestMethod]
        public void TestXRSize12Handler()
        {
            TestAssignmentExample(typeof(XRSize12Handler));
        }


        [TestMethod]
        public void TestXRSize14Handler()
        {
            TestAssignmentExample(typeof(XRSize14Handler));
        }


        [TestMethod]
        public void TestULSize6Type2Handler4()
        {
            TestAssignmentExample(typeof(ULSize6Type2Handler), "390002804120864039048739012273000008080327145451698327032080006814976253760203081", 5, "R9C5");
        }


        [TestMethod]
        public void TestFinnedSwordfishHandler1()
        {
            TestAssignmentExample(typeof(FinnedSwordfishHandler), "900605008070240030001700400200854007489127563715963004007400300090370640300506009", 9, "R2C7");
        }


        [TestMethod]
        public void TestURType2Handler()
        {
            TestAssignmentExample(typeof(URType2Handler));
        }


        [TestMethod]
        public void TestURType4Handler()
        {
            TestAssignmentExample(typeof(URType4Handler));
        }

        [TestMethod]
        public void TestLockedURType1Handler()
        {
            TestAssignmentExample(typeof(LockedURType1Handler));
        }

        [TestMethod]
        public void TestULSize8Handler()
        {
            TestAssignmentExample(typeof(ULSize8Handler));
        }

        [TestMethod]
        public void TestULSize6Type1Handler()
        {
            TestAssignmentExample(typeof(ULSize6Type1Handler));
        }

        [TestMethod]
        public void TestULSize6Type3Handler()
        {
            TestAssignmentExample(typeof(ULSize6Type3Handler));
        }



        [TestMethod]
        public void TestULSize6Type4Handler()
        {
            TestAssignmentExample(typeof(ULSize6Type4Handler));
        }

        [TestMethod]
        public void TestULSize8Handler1()
        {
            TestAssignmentExample(typeof(ULSize8Handler), "361095240045002901092010635209008510486159723150200890914526300620081459508940162", 4, "R3C4");
        }


        [TestMethod]
        public void TestULSize8Handler3()
        {
            TestAssignmentExample(typeof(ULSize8Handler), "361240095045901002092635010209510008486723159150890200914300526620459081508162940", 4, "R3C7");
        }


        [TestMethod]
        public void TestULSize10Handler()
        {

            TestAssignmentExample(typeof(ULSize10Handler));
        }

        [TestMethod]
        public void TestULSize10Handler1()
        {
            TestAssignmentExample(typeof(ULSize10Handler), "003400879008093002902068534750284093294316785830975240009800007325047908087009000", 5, "R9C4");
        }

        [TestMethod]
        public void TestIncompleteWXYZWingHandler()
        {
            TestAssignmentExample(typeof(IncompleteWXYZWingHandler));
        }

        [TestMethod]
        public void TestURType4Handler1()
        {
            TestAssignmentExample(typeof(URType4Handler), "318652007497381005600070138849010000531026009700890051900108500100200006203067014", 2, "R4C9");
        }



        [TestMethod]
        public void TestXYWingHandler1()
        {
            TestAssignmentExample(typeof(XYWingHandler), "056200970749536812028079000000003129000920040290607008815792060902304080000005290", 1, "R5C6");

        }




        [TestMethod]
        public void TestClaimingInColumnHandler()
        {
            TestAssignmentExample(typeof(ClaimingInColumnHandler));
        }

        [TestMethod]
        public void TestXYWingHandler2()
        {

            TestAssignmentExample(typeof(XYWingHandler));
        }

        [TestMethod]
        public void TestForcingChainHandler3()
        {
            TestAssignmentExample(typeof(ForcingChainHandler), "104900853385140000070358000653714298741800536800635417007580300508403000430201085", 7, "R1C6");
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestForcingChainHandler7()
        {
            TestAssignmentExample(typeof(ForcingChainHandler), "000400900324006000060500040000000600040160800600085090718003000000000080400070023", 4, "R6C9");
        }

        [TestMethod]
        public void TestForcingChainHandler8()
        {
            TestAssignmentExample(typeof(ForcingChainHandler), "318652007497381005600070138849010000531026009700890051900108500100200006203067014", 4, "R3C4");
        }


        [TestMethod]
        public void TestForcingChainHandler5()
        {
            TestAssignmentExample(typeof(ForcingChainHandler), "007000000800439000490071000000000906002000040008103000000002308005300000000710052", 9, "R9C3");
        }


        [TestMethod]
        public void TestForcingChainHandler1()
        {
            TestAssignmentExample(typeof(ForcingChainHandler));

        }


        [TestMethod]
        public void TestForcingChainHandler2()
        {
            TestAssignmentExample(typeof(ForcingChainHandler), "060300570052041006000605100580004267429006315607002498006207000005410620290063700", 3, "R2C8");
        }

        [TestMethod]
        public void TestForcingChainHandler4()
        {
            TestAssignmentExample(typeof(ForcingChainHandler), "030050400000004000200006090007600009080000500300800041073060120020003900050001374", 6, "R8C9");
        }

        [TestMethod]
        public void TestForcingChainHandler6()
        {
            TestAssignmentExample(typeof(ForcingChainHandler), "000200581072001463000060279020006300743928156600300000237000014410702000008004000", 3, "R3C6");
        }

        [TestMethod]
        public void TestHiddenSingleBlockHandler()
        {
            TestAssignmentExample(typeof(HiddenSingleBlockHandler));
        }
        [TestMethod]
        public void TestFinnedJeffyfishHandler()
        {
            TestAssignmentExample(typeof(FinnedJellyfishHandler));
        }

        [TestMethod]
        public void TestDymanicForcingChainHandler()
        {
            TestAssignmentExample(typeof(DymanicForcingChainHandler));
        }
        [TestMethod]
        public void TestDiscontinuousNiceLoopHandler()
        {
            TestAssignmentExample(typeof(DiscontinuousNiceLoopHandler));
        }

        [TestMethod]
        public void TestCannibalisticAICHandler()
        {
            TestAssignmentExample(typeof(CannibalisticAICHandler));
        }




        [TestMethod]
        public void TestFinnedJeffyfishHandler1()
        {
            TestAssignmentExample(typeof(FinnedJellyfishHandler), "204103580000020341103485600732954168005010900619832400001508200300240000026300004", 9, "R9C5");
        }


        [TestMethod]
        public void TestXYChainHandler()
        {
            TestAssignmentExample(typeof(XYChainHandler));
        }


        [TestMethod]
        public void TestHiddenQuadrupleHandler()
        {
            TestAssignmentExample(typeof(HiddenQuadrupleHandler));
        }


        [TestMethod]
        public void TestXYZWingHandler()
        {
            TestAssignmentExample(typeof(XYZWingHandler));
        }


        [TestMethod]
        public void TestRemotePairHandler()
        {
            TestAssignmentExample(typeof(RemotePairHandler));
        }




        [TestMethod]
        public void TestFinnedSwordfishHandler()
        {
            TestAssignmentExample(typeof(FinnedSwordfishHandler));
        }

        [TestMethod]
        public void TestNakedTripleHandler()
        {
            TestAssignmentExample(typeof(NakedTripleHandler));
        }

        [TestMethod]
        public void TestEmptyRectangleHandler()
        {
            TestAssignmentExample(typeof(EmptyRectangleHandler));
        }


        [TestMethod]
        public void TestNakedTripleHandler2()
        {
            TestAssignmentExample(typeof(NakedTripleHandler), "006300150045026000000000060090500700052670300700001005500000041004007000080090000", 6, "R7C5");
        }

        [TestMethod]
        public void TestNakedTripleHandler3()
        {
            TestAssignmentExample(typeof(NakedTripleHandler), "500000300081300000000076900000060813013400009000000000200090004690000250000007000", 8, "R3C8");
        }


        [TestMethod]
        public void TestNakedQuadrupleHandler()
        {
            TestAssignmentExample(typeof(NakedQuadrupleHandler));
        }


        [TestMethod]
        public void TestDirectPointingHandler()
        {
            TestAssignmentExample(typeof(DirectPointingHandler));
        }


        [TestMethod]
        public void TestHiddenSingleRowHandler()
        {
            TestAssignmentExample(typeof(HiddenSingleRowHandler));
        }
        [TestMethod]
        public void TestHiddenTripleHandler()
        {
            TestAssignmentExample(typeof(HiddenTripleHandler));
        }

        [TestMethod]
        public void TestHiddenPairHandler()
        {
            TestAssignmentExample(typeof(HiddenPairHandler));
        }

        [TestMethod]
        public void TestNakedPairHandller()
        {
            TestAssignmentExample(typeof(NakedPairHandller));
        }

        [TestMethod]
        public void TestWWingHandler()
        {
            TestAssignmentExample(typeof(WWingHandler));
        }

        [TestMethod]
        public void TestSkyscraperHandler()
        {
            TestAssignmentExample(typeof(SkyscraperHandler));
        }

        [TestMethod]
        public void TestTwoStringsKiteHandler()
        {
            TestAssignmentExample(typeof(TwoStringsKiteHandler));
        }
        [TestMethod]
        public void TestTwoStringsKiteHandler1()
        {
            TestAssignmentExample(typeof(TwoStringsKiteHandler), "256009170010000003400001500645007281000182465821564739000210000102003807500008012", 4, "R7C9");
        }



        [TestMethod]
        public void TestWXYZWingHandler()
        {
            TestAssignmentExample(typeof(WXYZWingHandler));
        }



        [TestMethod]
        public void TestTurbotFishNormalHandler1()
        {
            TestAssignmentExample(typeof(TurbotFishNormalHandler));
        }







    }
}
