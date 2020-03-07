using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku.UI
{
    public partial class FrmExample : Form
    {
        public FrmExample()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {

            var AssignExample = GetHasAssignmentExampleHandler(FrmG.SolveHandlers);
            var EliminationExample = GetHasEliminationExampleHandler(FrmG.SolveHandlers);
            SetAssignPanel(AssignExample);
            SetEliminationPanel(EliminationExample);
        }

        private void SetEliminationPanel(List<ISudokuSolveHandler> eliminationExample)
        {

        }

        private void SetAssignPanel(List<ISudokuSolveHandler> assignExample)
        {

        }

        public List<ISudokuSolveHandler> GetHasAssignmentExampleHandler(List<ISudokuSolveHandler> list)
        {
            var result = new List<ISudokuSolveHandler>();
            foreach (var item in list)
            {
                var type = item.GetType();

                object[] objs = type.GetCustomAttributes(typeof(AssignmentExampleAttribute), true);
                if (objs.Count() == 1)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public List<ISudokuSolveHandler> GetHasEliminationExampleHandler(List<ISudokuSolveHandler> list)
        {
            var result = new List<ISudokuSolveHandler>();
            foreach (var item in list)
            {
                var type = item.GetType();

                object[] objs = type.GetCustomAttributes(typeof(AssignmentExampleAttribute), true);
                if (objs.Count() == 1)
                {
                    result.Add(item);
                }
            }
            return result;
        }

    }
}

