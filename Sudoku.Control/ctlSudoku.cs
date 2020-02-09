using Sudoku.Core;
using System;
using System.Collections.Generic;
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

        private Dictionary<int, int> keyCodeNumMap = new Dictionary<int, int>
        { { 49,1},
          { 50,2},
          { 51,3},
          { 52,4},
          { 53,5},
          { 54,6},
          { 55,7},
          { 56,8},
          { 57,9},
          { 97,1},
          { 98,2},
          { 99,3},
          { 100,4},
          { 101,5},
          { 102,6},
          { 103,7},
          { 104,8},
          { 105,9},
        };


        



//keycode 37 = Left ←
//keycode 38 = Up ↑
//keycode 39 = Right →
//keycode 40 = Down ↓






        private void ctlSudoku_KeyUp(object sender, KeyEventArgs e)
        {
            var intKey= (int)e.KeyCode;
            if (keyCodeNumMap.ContainsKey(intKey))
            {
                if (this.Sudoku.CurrentCell.CellType!=CellType.Init)
                {
                    this.Sudoku.ApplyCell(new PositiveCell(this.Sudoku.CurrentCell.Index, keyCodeNumMap[intKey]));
                }
               
            }
            else if(e.KeyCode==Keys.Left)
            {
                this.Sudoku.MoveCurrentCellToLeft();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.Sudoku.MoveCurrentCellToUp();
            }
            else if (e.KeyCode == Keys.Right)
            {
                this.Sudoku.MoveCurrentCellToRight();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.Sudoku.MoveCurrentCellToDown();
            }
        }
    }
}
