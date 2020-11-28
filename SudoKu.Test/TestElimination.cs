
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Sudoku.Core.Model;
using Xunit;

namespace SudoKu.Test
{
    
    public  class TestElimination
    {
        [Fact]
        public void TestForcingChainOffHandler()
        {
            TestEliminationExample(typeof(ForcingChainOffHandler));
        }


        [Fact]
        public void TestXRSize10Handler()
        {
            TestEliminationExample(typeof(XRSize10Handler));
        }

        [Fact]
        public void TestLockedURType2Handler()
        {
            TestEliminationExample(typeof(LockedURType2Handler));
        }

        

        [Fact]
        public void TestAlignedTripleExclusionHandler1()
        {
            TestEliminationExample(typeof(AlignedTripleExclusionHandler), "205000000080000007060010902007039500010000078002000009070390000509060000000001300", 4, "R3C1");
        }


        [Fact]
        public void TestXRSize6Type3Handler()
        {
            TestEliminationExample(typeof(XRSize6Type3Handler));
        }
        [Fact]
        public void TestIncompleteVWXYZWingHandler()
        {
            TestEliminationExample(typeof(IncompleteVWXYZWingHandler));
        }
        [Fact]
        public void TestVWXYZWingHandler()
        {
            TestEliminationExample(typeof(VWXYZWingHandler));
        }

        
        [Fact]
        public void TestXWingHandler()
        {
            TestEliminationExample(typeof(XWingHandler));
        }

        [Fact]
        public void TestAlignedPairExclusionHandler()
        {
            TestEliminationExample(typeof(AlignedPairExclusionHandler));
        }

        [Fact]
        public void TestAlignedTripleExclusionHandler()
        {
            TestEliminationExample(typeof(AlignedTripleExclusionHandler));
        }

        [Fact]
        public void TestAlignedQuadrupleExclusionHandler()
        {
            TestEliminationExample(typeof(AlignedQuadrupleExclusionHandler));
        }

        
        [Fact]
        public void TestFinnedXwingHandler()
        {
            TestEliminationExample(typeof(FinnedXwingHandler));
        }

        [Fact]
        public void TestImcompletedURType1Handler()
        {
            TestEliminationExample(typeof(ImcompletedURType1Handler));
        }

        [Fact]
        public void TestSplitWingHandler()
        {
            TestEliminationExample(typeof(SplitWingHandler));
        }


        [Fact]
        public void TestSashimiJellyfishHandler()
        {
            TestEliminationExample(typeof(SashimiJellyfishHandler));
        }

        [Fact]
        public void TestSashimiSwordfishHandler()
        {
            TestEliminationExample(typeof(SashimiSwordfishHandler));
        }

        [Fact]
        public void TestSashimiXwingHandler()
        {
            TestEliminationExample(typeof(SashimiXwingHandler));
        }


        [Fact]
        public void TestSiameseSwordfishHandler()
        {
            TestEliminationExample(typeof(SiameseSwordfishHandler));
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
