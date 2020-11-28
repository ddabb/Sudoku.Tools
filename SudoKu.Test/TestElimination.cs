using Sudoku.Core;
using Sudoku.Core.Model;
using Sudoku.Tools;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
namespace SudoKu.Test
{

    public  class TestElimination
    {
        [Theory]
        [InlineData(typeof(ForcingChainOffHandler))]
        [InlineData(typeof(XRSize10Handler))]
        [InlineData(typeof(LockedURType2Handler))]
        [InlineData(typeof(XRSize6Type3Handler))]
        [InlineData(typeof(IncompleteVWXYZWingHandler))]
        [InlineData(typeof(VWXYZWingHandler))]
        [InlineData(typeof(XWingHandler))]
        [InlineData(typeof(AlignedPairExclusionHandler))]
        [InlineData(typeof(AlignedTripleExclusionHandler))]
        [InlineData(typeof(AlignedQuadrupleExclusionHandler))]
        [InlineData(typeof(FinnedXwingHandler))]
        [InlineData(typeof(ImcompletedURType1Handler))]
        [InlineData(typeof(SplitWingHandler))]
        [InlineData(typeof(SashimiJellyfishHandler))]
        [InlineData(typeof(SashimiSwordfishHandler))]
        [InlineData(typeof(SashimiXwingHandler))]
        [InlineData(typeof(SiameseSwordfishHandler))]
        public void TestElimination1(Type type)
        {
            TestEliminationExample(type);
        }



        [Theory]
        [InlineData(typeof(AlignedTripleExclusionHandler), "205000000080000007060010902007039500010000078002000009070390000509060000000001300", 4, "R3C1")]
        public void TestAlignedTripleExclusionHandler1(Type type, string queryString, int value, string positionString, SolveMethodEnum[] handlerEnums = null)
        {
            TestEliminationExample(type, queryString, value, positionString, handlerEnums  );
        } 

        
        private static void TestEliminationExample(Type type)
        {
            object[] objs = type.GetCustomAttributes(typeof(EliminationExampleAttribute), true);
            if (objs.Count() != 1) return;
            if (!(objs[0] is EliminationExampleAttribute a)) return;
            var queryString = a.queryString;
            var value = a.value;
            var positionString = a.positionString;
            var handers = a.SolveHandlers;
            TestEliminationExample(type, queryString, value, positionString, handers);
        }
        private static void TestEliminationExample(Type type, string queryString, int value, string positionString,SolveMethodEnum[] handlerEnums = null)
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
                ((ISudokuSolveHandler)Activator.CreateInstance(type, true)).Elimination(
                    qsudoku);
            Assert.True(cellinfo.Exists(c => c.RrCc == positionString && c.Value == value));
            Debug.WriteLine("cellinfo " + cellinfo.JoinString());
            qsudoku = qsudoku.ApplyCells(cellinfo);
            Assert.True((new DanceLink().isValid(qsudoku.QueryString)));
        }
        
        
    }
}
