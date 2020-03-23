using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Sudoku.Core.Model;

namespace SudoKu.Test
{
    [TestClass]
    public  class TestElimination
    {
        [TestMethod]
        public void TestForcingChainOffHandler()
        {
            TestEliminationExample(typeof(ForcingChainOffHandler));
        }


        [TestMethod]
        public void TestXRSize10Handler()
        {
            TestEliminationExample(typeof(XRSize10Handler));
        }

        [TestMethod]
        public void TestXRSize6Type3Handler()
        {
            TestEliminationExample(typeof(XRSize6Type3Handler));
        }
        [TestMethod]
        public void TestIncompleteVWXYZWingHandler()
        {
            TestEliminationExample(typeof(IncompleteVWXYZWingHandler));
        }

        [TestMethod]
        public void TestSplitWingHandler()
        {
            TestEliminationExample(typeof(SplitWingHandler));
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
            Assert.AreEqual(true, cellinfo.Exists(c => c.RrCc == positionString && c.Value == value));
            Debug.WriteLine("cellinfo " + cellinfo.JoinString());
            qsudoku = qsudoku.ApplyCells(cellinfo);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }
    }
}
