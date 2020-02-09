using Sudoku.Core;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Sudoku.UI
{
    public partial class ctlSudoku : UserControl
    {

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool ShowCandidates { get { return this.sudokuPanel1.ShowCandidates; } set { this.sudokuPanel1.ShowCandidates = value; } }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public QSudoku Sudoku { get { return this.sudokuPanel1.Sudoku; } set { this.sudokuPanel1.Sudoku = value; } }

        public ctlSudoku()
        {
            InitializeComponent();

        }

        private void ctlSudoku_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void ReLoad()
        {
            this.sudokuPanel1.Refresh();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

    }
}
