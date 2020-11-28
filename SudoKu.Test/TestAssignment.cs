using Sudoku.Core;
using Sudoku.Core.Model;
using Sudoku.Tools;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
namespace SudoKu.Test
{
    public class TestAssignment
    {
        [Theory]
        [InlineData(typeof(HiddenSingleRowHandler))]
        [InlineData(typeof(HiddenSingleColumnHandler))]
        [InlineData(typeof(XWingHandler))]
        [InlineData(typeof(CascadingLockedCandidatesHandler))]
        [InlineData(typeof(NakedTripleWithLockedCandidatesHandler))]
        [InlineData(typeof(SiameseXwingHandler))]
        [InlineData(typeof(SiameseJeffyfishHandler))]
        [InlineData(typeof(ClaimingInRowHandler))]
        [InlineData(typeof(URType1Handler))]
        [InlineData(typeof(MWingHandler))]
        [InlineData(typeof(URType3NakedPairHandller))]
        [InlineData(typeof(URType3NakedTripleHandler))]
        [InlineData(typeof(URType3NakedQuadrupleHandler))]
        [InlineData(typeof(URType3HiddenPairHandller))]
        [InlineData(typeof(URType3HiddenTripleHandller))]
        [InlineData(typeof(URType3HiddenQuadrupleHandler))]
        [InlineData(typeof(LocalWingHandler))]
        [InlineData(typeof(ULSize6Type2Handler))]
        [InlineData(typeof(HybridWingHandler))]
        [InlineData(typeof(XRSize6Type1Handler))]
        [InlineData(typeof(XRSize6Type2Handler))]
        [InlineData(typeof(XRSize6Type3Handler))]
        [InlineData(typeof(BugType1Handler))]
        [InlineData(typeof(XRSize6Type4Handler))]
        [InlineData(typeof(XRSize8Handler))]
        [InlineData(typeof(XRSize12Handler))]
        [InlineData(typeof(XRSize14Handler))]
        public void TestTypes(Type type)
        {
            TestAssignmentExample(type);
        }
        [Fact]
        public void TestXRSize14Handler()
        {
            TestAssignmentExample(typeof(XRSize14Handler));
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
            var handler = ((ISudokuSolveHandler) Activator.CreateInstance(type, true));
            var cellinfo = handler.Assignment(
                    qsudoku);
            Assert.True( cellinfo.Exists(c => c.RrCc == positionString && c.Value == value));
            Debug.WriteLine("cellinfo " + cellinfo.JoinString());
            qsudoku = qsudoku.ApplyCells(cellinfo);
            Assert.True( new DanceLink().isValid(qsudoku.QueryString));
        }
        
        [Fact]
        public void TestULSize6Type2Handler1()
        {
            TestAssignmentExample(typeof(ULSize6Type2Handler), "016705920492160750057290106273600589145879000689352417500900001900500800720406395", 3, "R3C8");
        }
        [Fact]
        public void TestULSize6Type2Handler4()
        {
            TestAssignmentExample(typeof(ULSize6Type2Handler), "390002804120864039048739012273000008080327145451698327032080006814976253760203081", 5, "R9C5");
        }
        [Fact]
        public void TestImcompletedSwordfishHandler1()
        {
            TestAssignmentExample(typeof(ImcompletedSwordfishHandler), "900605008070240030001700400200854007489127563715963004007400300090370640300506009", 9, "R2C7");
        }
        [Fact]
        public void TestURType2Handler()
        {
            TestAssignmentExample(typeof(URType2Handler));
        }
        [Fact]
        public void TestURType4Handler()
        {
            TestAssignmentExample(typeof(URType4Handler));
        }
        [Fact]
        public void TestLockedURType1Handler()
        {
            TestAssignmentExample(typeof(LockedURType1Handler));
        }
        [Fact]
        public void TestULSize8Handler()
        {
            TestAssignmentExample(typeof(ULSize8Handler));
        }
        [Fact]
        public void TestULSize6Type1Handler()
        {
            TestAssignmentExample(typeof(ULSize6Type1Handler));
        }
        [Fact]
        public void TestULSize6Type3Handler()
        {
            TestAssignmentExample(typeof(ULSize6Type3Handler));
        }
        [Fact]
        public void TestULSize6Type4Handler()
        {
            TestAssignmentExample(typeof(ULSize6Type4Handler));
        }
        [Fact]
        public void TestULSize8Handler1()
        {
            TestAssignmentExample(typeof(ULSize8Handler), "361095240045002901092010635209008510486159723150200890914526300620081459508940162", 4, "R3C4");
        }
        [Fact]
        public void TestULSize8Handler3()
        {
            TestAssignmentExample(typeof(ULSize8Handler), "361240095045901002092635010209510008486723159150890200914300526620459081508162940", 4, "R3C7");
        }
        [Fact]
        public void TestULSize10Handler()
        {
            TestAssignmentExample(typeof(ULSize10Handler));
        }
        [Fact]
        public void TestULSize10Handler1()
        {
            TestAssignmentExample(typeof(ULSize10Handler), "003400879008093002902068534750284093294316785830975240009800007325047908087009000", 5, "R9C4");
        }
        [Fact]
        public void TestIncompleteWXYZWingHandler()
        {
            TestAssignmentExample(typeof(IncompleteWXYZWingHandler));
        }
        [Fact]
        public void TestURType4Handler1()
        {
            TestAssignmentExample(typeof(URType4Handler), "318652007497381005600070138849010000531026009700890051900108500100200006203067014", 2, "R4C9");
        }
        [Fact]
        public void TestXYWingHandler1()
        {
            TestAssignmentExample(typeof(XYWingHandler), "056200970749536812028079000000003129000920040290607008815792060902304080000005290", 1, "R5C6");
        }
        [Fact]
        public void TestClaimingInColumnHandler()
        {
            TestAssignmentExample(typeof(ClaimingInColumnHandler));
        }
        [Fact]
        public void TestXYWingHandler2()
        {
            TestAssignmentExample(typeof(XYWingHandler));
        }
        [Fact]
        public void TestForcingChainHandler3()
        {
            TestAssignmentExample(typeof(ForcingChainOnHandler), "104900853385140000070358000653714298741800536800635417007580300508403000430201085", 7, "R1C6");
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void TestForcingChainHandler7()
        {
            TestAssignmentExample(typeof(ForcingChainOnHandler), "000400900324006000060500040000000600040160800600085090718003000000000080400070023", 4, "R6C9");
        }
        [Fact]
        public void TestForcingChainHandler8()
        {
            TestAssignmentExample(typeof(ForcingChainOnHandler), "318652007497381005600070138849010000531026009700890051900108500100200006203067014", 4, "R3C4");
        }
        [Fact]
        public void TestForcingChainHandler5()
        {
            TestAssignmentExample(typeof(ForcingChainOnHandler), "007000000800439000490071000000000906002000040008103000000002308005300000000710052", 9, "R9C3");
        }
        [Fact]
        public void TestForcingChainHandler1()
        {
            TestAssignmentExample(typeof(ForcingChainOnHandler));
        }
        [Fact]
        public void TestForcingChainHandler2()
        {
            TestAssignmentExample(typeof(ForcingChainOnHandler), "060300570052041006000605100580004267429006315607002498006207000005410620290063700", 3, "R2C8");
        }
        [Fact]
        public void TestForcingChainHandler4()
        {
            TestAssignmentExample(typeof(ForcingChainOnHandler), "030050400000004000200006090007600009080000500300800041073060120020003900050001374", 6, "R8C9");
        }
        [Fact]
        public void TestForcingChainHandler6()
        {
            TestAssignmentExample(typeof(ForcingChainOnHandler), "000200581072001463000060279020006300743928156600300000237000014410702000008004000", 3, "R3C6");
        }
        [Fact]
        public void TestHiddenSingleBlockHandler()
        {
            TestAssignmentExample(typeof(HiddenSingleBlockHandler));
        }
        [Fact]
        public void TestImcompletedJellyfishHandler()
        {
            TestAssignmentExample(typeof(ImcompletedJellyfishHandler));
        }
        [Fact]
        public void TestDymanicForcingChainHandler()
        {
            TestAssignmentExample(typeof(DymanicForcingChainHandler));
        }
        [Fact]
        public void TestDiscontinuousNiceLoopHandler()
        {
            TestAssignmentExample(typeof(DiscontinuousNiceLoopHandler));
        }
        [Fact]
        public void TestCannibalisticAICHandler()
        {
            TestAssignmentExample(typeof(CannibalisticAICHandler));
        }
        [Fact]
        public void TestImcompletedJellyfishHandler1()
        {
            TestAssignmentExample(typeof(ImcompletedJellyfishHandler), "204103580000020341103485600732954168005010900619832400001508200300240000026300004", 9, "R9C5");
        }
        [Fact]
        public void TestXYChainHandler()
        {
            TestAssignmentExample(typeof(XYChainHandler));
        }
        [Fact]
        public void TestHiddenQuadrupleHandler()
        {
            TestAssignmentExample(typeof(HiddenQuadrupleHandler));
        }
        [Fact]
        public void TestXYZWingHandler()
        {
            TestAssignmentExample(typeof(XYZWingHandler));
        }
        [Fact]
        public void TestRemotePairHandler()
        {
            TestAssignmentExample(typeof(RemotePairHandler));
        }
        [Fact]
        public void TestImcompletedSwordfishHandler()
        {
            TestAssignmentExample(typeof(ImcompletedSwordfishHandler));
        }
        [Fact]
        public void TestNakedTripleHandler()
        {
            TestAssignmentExample(typeof(NakedTripleHandler));
        }
        [Fact]
        public void TestEmptyRectangleHandler()
        {
            TestAssignmentExample(typeof(EmptyRectangleHandler));
        }
        [Fact]
        public void TestNakedTripleHandler2()
        {
            TestAssignmentExample(typeof(NakedTripleHandler), "006300150045026000000000060090500700052670300700001005500000041004007000080090000", 6, "R7C5");
        }
        [Fact]
        public void TestNakedTripleHandler3()
        {
            TestAssignmentExample(typeof(NakedTripleHandler), "500000300081300000000076900000060813013400009000000000200090004690000250000007000", 8, "R3C8");
        }
        [Fact]
        public void TestNakedQuadrupleHandler()
        {
            TestAssignmentExample(typeof(NakedQuadrupleHandler));
        }
        [Fact]
        public void TestDirectPointingHandler()
        {
            TestAssignmentExample(typeof(DirectPointingHandler));
        }
        [Fact]
        public void TestHiddenSingleRowHandler()
        {
            TestAssignmentExample(typeof(HiddenSingleRowHandler));
        }
        [Fact]
        public void TestHiddenTripleHandler()
        {
            TestAssignmentExample(typeof(HiddenTripleHandler));
        }
        [Fact]
        public void TestHiddenPairHandler()
        {
            TestAssignmentExample(typeof(HiddenPairHandler));
        }
        [Fact]
        public void TestNakedPairHandller()
        {
            TestAssignmentExample(typeof(NakedPairHandller));
        }
        [Fact]
        public void TestWWingHandler()
        {
            TestAssignmentExample(typeof(WWingHandler));
        }
        [Fact]
        public void TestSkyscraperHandler()
        {
            TestAssignmentExample(typeof(SkyscraperHandler));
        }
        [Fact]
        public void TestTwoStringsKiteHandler()
        {
            TestAssignmentExample(typeof(TwoStringsKiteHandler));
        }
        [Fact]
        public void TestTwoStringsKiteHandler1()
        {
            TestAssignmentExample(typeof(TwoStringsKiteHandler), "256009170010000003400001500645007281000182465821564739000210000102003807500008012", 4, "R7C9");
        }
        [Fact]
        public void TestWXYZWingHandler()
        {
            TestAssignmentExample(typeof(WXYZWingHandler));
        }
        [Fact]
        public void TestTurbotFishNormalHandler1()
        {
            TestAssignmentExample(typeof(TurbotFishNormalHandler));
        }
    }
}
