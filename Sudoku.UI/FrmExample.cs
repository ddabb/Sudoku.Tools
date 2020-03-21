using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku.UI
{
    public partial class FrmExample : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private string lblTitleText
        {
            get { return this.lblTitle.Text;}
            set { this.lblTitle.Text = value; }
        }

        public FrmExample()
        {
            InitializeComponent();
            this.CenterScreen();
            this.lblTitle.Text = lblTitleText;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {

            var AssignExample = GetHasAssignmentExampleHandler(FrmG.SolveHandlers);
            var EliminationExample = GetHasEliminationExampleHandler(FrmG.SolveHandlers);
            SetAssignPanel(AssignExample);
            SetEliminationPanel(EliminationExample);
        }

        private void SetEliminationPanel(List<ISudokuSolveHandler> eliminationExample,string title="删除示例")
        {
            this.lblTitleText = title;
        }

        private void SetAssignPanel(List<ISudokuSolveHandler> assignExample, string title = "出数示例")
        {
            this.lblTitleText = title;
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

