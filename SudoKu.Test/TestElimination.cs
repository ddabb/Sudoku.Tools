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
    public  class TestElimination
    {
        [TestMethod]
        public void TestForcingChainHandler()
        {
            ForcingChainHandler hander = new ForcingChainHandler();
            QSudoku qsudoku = new QSudoku("716000289298176543534928761000490108080607904945812376403089612809201437020000895");
            Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
            var cells = hander.Elimination(qsudoku);
            foreach (var item in cells)
            {
                Debug.WriteLine("" + item);
            }
        
            Debug.WriteLine(qsudoku.QueryString);
            Assert.AreEqual(true, new DanceLink().isValid(qsudoku.QueryString));
        }


        [TestMethod]
        public void TestXRSize10Handler()
        {
            TestEliminationExample(typeof(XRSize10Handler));
        }

        [TestMethod]
        public void TestIncompleteVWXYZWingHandler()
        {
            TestEliminationExample(typeof(IncompleteVWXYZWingHandler));
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

                var eliminationHanders = TestG.SolveHandlers.Where(c => handlerEnums.Contains(c.methodType)).ToList();

                foreach (var eliminationHander in eliminationHanders)
                {
                    var removeCells = eliminationHander.Elimination(qsudoku);
                    qsudoku = qsudoku.RemoveCells(removeCells);

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
