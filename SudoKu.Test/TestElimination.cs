using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var cells = hander.Assignment(qsudoku);
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
