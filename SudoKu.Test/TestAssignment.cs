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
        [InlineData(typeof(URType2Handler))]
        [InlineData(typeof(URType4Handler))]
        [InlineData(typeof(LockedURType1Handler))]
        [InlineData(typeof(ULSize8Handler))]
        [InlineData(typeof(ULSize6Type1Handler))]
        [InlineData(typeof(ULSize6Type3Handler))]
        [InlineData(typeof(ULSize6Type4Handler))]
        [InlineData(typeof(IncompleteWXYZWingHandler))]
        [InlineData(typeof(NakedQuadrupleHandler))]
        [InlineData(typeof(DirectPointingHandler))]
        [InlineData(typeof(HiddenTripleHandler))]
        [InlineData(typeof(HiddenPairHandler))]
        [InlineData(typeof(NakedPairHandller))]
        [InlineData(typeof(WWingHandler))]
        [InlineData(typeof(SkyscraperHandler))]
        [InlineData(typeof(TwoStringsKiteHandler))]
        [InlineData(typeof(ClaimingInColumnHandler))]
        [InlineData(typeof(XYWingHandler))]
        [InlineData(typeof(ForcingChainOnHandler))]
        [InlineData(typeof(HiddenSingleBlockHandler))]
        [InlineData(typeof(ImcompletedJellyfishHandler))]
        [InlineData(typeof(DymanicForcingChainHandler))]
        [InlineData(typeof(DiscontinuousNiceLoopHandler))]
        [InlineData(typeof(CannibalisticAICHandler))]
        [InlineData(typeof(XYChainHandler))]
        [InlineData(typeof(HiddenQuadrupleHandler))]
        [InlineData(typeof(XYZWingHandler))]
        [InlineData(typeof(RemotePairHandler))]
        [InlineData(typeof(ImcompletedSwordfishHandler))]
        [InlineData(typeof(NakedTripleHandler))]
        [InlineData(typeof(EmptyRectangleHandler))]
        [InlineData(typeof(WXYZWingHandler))]
        [InlineData(typeof(TurbotFishNormalHandler))]
        public void TestAssignmentExample(Type type)
        {
            TestAssignmentExample1(type);
        }
        private static void TestAssignmentExample1(Type type)
        {
            object[] objs = type.GetCustomAttributes(typeof(AssignmentExampleAttribute), true);
            if (objs.Count() != 1) return;
            if (!(objs[0] is AssignmentExampleAttribute a)) return;
            var queryString = a.queryString;
            var value = a.value;
            var positionString = a.positionString;
            var handers = a.SolveHandlers;
            TestAssignmentExample1(type, queryString, value, positionString, handers);
        }
        private static void TestAssignmentExample1(Type type, string queryString, int value, string positionString, SolveMethodEnum[] handlerEnums = null)
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
   
        [Theory]
        [InlineData(typeof(ULSize6Type2Handler), "016705920492160750057290106273600589145879000689352417500900001900500800720406395", 3, "R3C8")]
        [InlineData(typeof(ULSize6Type2Handler), "390002804120864039048739012273000008080327145451698327032080006814976253760203081", 5, "R9C5")]
        [InlineData(typeof(ImcompletedSwordfishHandler), "900605008070240030001700400200854007489127563715963004007400300090370640300506009", 9, "R2C7")]
        [InlineData(typeof(ULSize8Handler), "361095240045002901092010635209008510486159723150200890914526300620081459508940162", 4, "R3C4")]
        [InlineData(typeof(ULSize8Handler), "361240095045901002092635010209510008486723159150890200914300526620459081508162940", 4, "R3C7")]
        [InlineData(typeof(ULSize10Handler), "003400879008093002902068534750284093294316785830975240009800007325047908087009000", 5, "R9C4")]
        [InlineData(typeof(URType4Handler), "318652007497381005600070138849010000531026009700890051900108500100200006203067014", 2, "R4C9")]
        [InlineData(typeof(XYWingHandler), "056200970749536812028079000000003129000920040290607008815792060902304080000005290", 1, "R5C6")]
        [InlineData(typeof(ForcingChainOnHandler), "104900853385140000070358000653714298741800536800635417007580300508403000430201085", 7, "R1C6")]
        [InlineData(typeof(ForcingChainOnHandler), "000400900324006000060500040000000600040160800600085090718003000000000080400070023", 4, "R6C9")]
        [InlineData(typeof(ForcingChainOnHandler), "318652007497381005600070138849010000531026009700890051900108500100200006203067014", 4, "R3C4")]
        [InlineData(typeof(ForcingChainOnHandler), "007000000800439000490071000000000906002000040008103000000002308005300000000710052", 9, "R9C3")]
        [InlineData(typeof(ForcingChainOnHandler), "060300570052041006000605100580004267429006315607002498006207000005410620290063700", 3, "R2C8")]
        [InlineData(typeof(ForcingChainOnHandler), "030050400000004000200006090007600009080000500300800041073060120020003900050001374", 6, "R8C9")]
        [InlineData(typeof(ForcingChainOnHandler), "000200581072001463000060279020006300743928156600300000237000014410702000008004000", 3, "R3C6")]
        [InlineData(typeof(ImcompletedJellyfishHandler), "204103580000020341103485600732954168005010900619832400001508200300240000026300004", 9, "R9C5")]
        [InlineData(typeof(TwoStringsKiteHandler), "256009170010000003400001500645007281000182465821564739000210000102003807500008012", 4, "R7C9")]
        [InlineData(typeof(NakedTripleHandler), "006300150045026000000000060090500700052670300700001005500000041004007000080090000", 6, "R7C5")]
        [InlineData(typeof(NakedTripleHandler), "500000300081300000000076900000060813013400009000000000200090004690000250000007000", 8, "R3C8")]
        public void TestAssignmentWithExample(Type type, string queryString, int value, string positionString, SolveMethodEnum[] handlerEnums = null)
        {
            TestAssignmentExample1(type, queryString, value, positionString, handlerEnums);
        }
    }
}
