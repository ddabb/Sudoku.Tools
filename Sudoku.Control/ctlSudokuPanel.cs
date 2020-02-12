using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sudoku.Core;

namespace Sudoku.Control
{
    public class ctlSudokuPanel :UserControl
    {
        public int SmallSpace => 27;

        public ctlSudokuPanel()
        {
            this.Paint += hmaPanel_Paint;
            this.BackColor = Color.FromArgb(255, 0xd7, 0xd7, 0xd7);
            this.Resize += (sender, e) => { this.Refresh(); };
            this.MouseMove += SudokuPanel_MouseMove;
            this.MouseClick += SudokuPanel_MouseClick;
            this.MouseEnter += SudokuPanel_MouseEnter;
            var space = this.SmallSpace * 27;
            this.DoubleBuffered = true;
            this.Size = new System.Drawing.Size(space, space);
            RePaintAll();
        }

        private void SudokuPanel_MouseEnter(object sender, System.EventArgs e)
        {
           
        }

        private void SudokuPanel_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private Rectangle paintRectangle { get; set; }
        private void SudokuPanel_MouseMove(object sender, MouseEventArgs e)
        {

        }

        public bool ShowCandidates { get; set; } = true;

        private QSudoku msudoku;
        public QSudoku Sudoku
        {
            get { return msudoku;}
            set { msudoku = value; RePaintAll(); }
        }

        private void hmaPanel_Paint(object sender, PaintEventArgs e)
        {
            RePaintAll();
        }

        public void RePaintAll()
        {
            var objGraphics = this.CreateGraphics();

            var bigSpace = SmallSpace * 3;
            Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
            var smallFont = new Font("宋体", 14f, FontStyle.Bold, GraphicsUnit.Point, 0);
            var bigFont = new Font("宋体", smallFont.Size * 3, FontStyle.Bold, GraphicsUnit.Point, 0);
            var lineweith = 1;
            objGraphics.FillRectangle(new SolidBrush(Color.White), rectangle);
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 0), new Point(this.Width, bigSpace * 0));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 0 + lineweith * 1),
                new Point(this.Width, bigSpace * 0 + lineweith * 1));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 0 + lineweith * 2),
                new Point(this.Width, bigSpace * 0 + lineweith * 2));
            objGraphics.DrawLine(Pens.Gray, new Point(0, bigSpace * 1), new Point(this.Width, bigSpace * 1));
            objGraphics.DrawLine(Pens.Gray, new Point(0, bigSpace * 2), new Point(this.Width, bigSpace * 2));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 3), new Point(this.Width, bigSpace * 3));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 3 + lineweith * 1),
                new Point(this.Width, bigSpace * 3 + lineweith * 1));
            objGraphics.DrawLine(Pens.Gray, new Point(0, bigSpace * 4), new Point(this.Width, bigSpace * 4));
            objGraphics.DrawLine(Pens.Gray, new Point(0, bigSpace * 5), new Point(this.Width, bigSpace * 5));

            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 6), new Point(this.Width, bigSpace * 6));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 6 + lineweith * 1),
                new Point(this.Width, bigSpace * 6 + lineweith * 1));
            objGraphics.DrawLine(Pens.Gray, new Point(0, bigSpace * 7), new Point(this.Width, bigSpace * 7));
            objGraphics.DrawLine(Pens.Gray, new Point(0, bigSpace * 8), new Point(this.Width, bigSpace * 8));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 9 - lineweith * 3),
                new Point(this.Width, bigSpace * 9 - lineweith * 3));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 9 - lineweith * 2),
                new Point(this.Width, bigSpace * 9 - lineweith * 2));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 9 - lineweith * 1),
                new Point(this.Width, bigSpace * 9 - lineweith * 1));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 9), new Point(this.Width, bigSpace * 9));

            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 0, 0), new Point(bigSpace * 0, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 0 + lineweith * 1, 0),
                new Point(bigSpace * 0 + lineweith * 1, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 0 + lineweith * 2, 0),
                new Point(bigSpace * 0 + lineweith * 2, this.Height));
            objGraphics.DrawLine(Pens.Gray, new Point(bigSpace * 1, 0), new Point(bigSpace * 1, this.Height));
            objGraphics.DrawLine(Pens.Gray, new Point(bigSpace * 2, 0), new Point(bigSpace * 2, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 3, 0), new Point(bigSpace * 3, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 3 + lineweith * 1, 0),
                new Point(bigSpace * 3 + lineweith * 1, this.Height));
            objGraphics.DrawLine(Pens.Gray, new Point(bigSpace * 4, 0), new Point(bigSpace * 4, this.Height));
            objGraphics.DrawLine(Pens.Gray, new Point(bigSpace * 5, 0), new Point(bigSpace * 5, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 6, 0), new Point(bigSpace * 6, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 6 + lineweith * 1, 0),
                new Point(bigSpace * 6 + lineweith * 1, this.Height));
            objGraphics.DrawLine(Pens.Gray, new Point(bigSpace * 7, 0), new Point(bigSpace * 7, this.Height));
            objGraphics.DrawLine(Pens.Gray, new Point(bigSpace * 8, 0), new Point(bigSpace * 8, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 9 - lineweith * 3, 0),
                new Point(bigSpace * 9 - lineweith * 3, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 9 - lineweith * 2, 0),
                new Point(bigSpace * 9 - lineweith * 2, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 9 - lineweith * 1, 0),
                new Point(bigSpace * 9 - lineweith * 1, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 9, 0), new Point(bigSpace * 9, this.Height));
            QSudoku sudoku = this.Sudoku;
            if (sudoku != null)
            {
                foreach (var item in sudoku.AllCell)
                {
                    if (item.Index==sudoku.CurrentCell.Index)
                    {
                        objGraphics.FillRectangle(new SolidBrush(Color.Brown),new Rectangle(new Point(bigSpace*item.Column,bigSpace * item.Row),new Size(bigSpace,bigSpace) ));


                    }
                    if (item.Value!=0)
                    {
                        var stringvalue = "" + item.Value;
                        Size size = TextRenderer.MeasureText(stringvalue, bigFont);
                        var color = item.CellType == CellType.Init ? Color.Black : Color.Blue;
                        objGraphics.DrawString(stringvalue, bigFont, new SolidBrush(color),
                            item.Column * bigSpace + (bigSpace - size.Width) / 2,
                            item.Row * bigSpace + (bigSpace - size.Height) / 2);
                    }
                    else
                    {
                        if (ShowCandidates)
                        {
                            foreach (var item1 in item.RestList)
                            {
                                var stringvalue = "" + item1;
                                Size size = TextRenderer.MeasureText(stringvalue, smallFont);
                                objGraphics.DrawString(stringvalue, smallFont, new SolidBrush(Color.Gray),
                                    new PointF(
                                        item.Column * bigSpace + (SmallSpace * ((item1 - 1) % 3)) + (SmallSpace - size.Width) / 2,
                                        item.Row * bigSpace + (SmallSpace * ((item1 - 1) / 3)) + +(SmallSpace - size.Height) / 2));
                            }
                        }
                    }
                }
            }


            ButtonBorderStyle style = ButtonBorderStyle.Solid;

            ControlPaint.DrawBorder(this.CreateGraphics(), rectangle,
                Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
                Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
                Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
                Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ctlSudokuPanel
            // 
            this.Name = "ctlSudokuPanel";
            this.Load += new System.EventHandler(this.SudokuPanel_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ctlSudokuPanel_KeyUp);
            this.ResumeLayout(false);

        }

        private void SudokuPanel_Load(object sender, System.EventArgs e)
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


        private void ctlSudokuPanel_KeyUp(object sender, KeyEventArgs e)
        {
            var intKey = (int)e.KeyCode;
            if (keyCodeNumMap.ContainsKey(intKey))
            {
                if (this.Sudoku.CurrentCell.CellType != CellType.Init)
                {
                    this.Sudoku.ApplyCell(new PositiveCell(this.Sudoku.CurrentCell.Index, keyCodeNumMap[intKey]));
                }

            }
            else if (e.KeyCode == Keys.Left)
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

            RePaintAll();
        }
    }
}
