using Sudoku.Core;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku.UI
{
    public partial class ctlSudoku : UserControl
    {

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool ShowCandidates { get; set; }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public QSudoku Sudoku { get; set; }

        public int SmallSpace { get; private set; } = 27;

        public ctlSudoku()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

        }


        public bool Paintflag = true;



        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            RefreshPanel();
        }

        public void RefreshPanel()
        {
            if (Paintflag)
            {

                Paintflag = false;
                var objGraphics = panel1.CreateGraphics();
                var bigSpace = SmallSpace * 3;
                var panelWidth = panel1.Width;
                var panel1Height = panel1.Height;
                Rectangle rectangle = new Rectangle(0, 0, panel1.Width, panel1.Height);
                BufferedGraphics graphBuffer = (new BufferedGraphicsContext()).Allocate(objGraphics, rectangle);
                Graphics g = graphBuffer.Graphics;
                var smallFont = new Font("宋体", 14f, FontStyle.Bold, GraphicsUnit.Point, 0);
                var bigFont = new Font("宋体", smallFont.Size * 3, FontStyle.Bold, GraphicsUnit.Point, 0);
                var lineweith = 1;
                g.FillRectangle(new SolidBrush(Color.White), rectangle);
                g.DrawLine(Pens.Black, new Point(0, bigSpace * 0), new Point(panelWidth, bigSpace * 0));
                g.DrawLine(Pens.Black, new Point(0, bigSpace * 0 + lineweith * 1),
                       new Point(panelWidth, bigSpace * 0 + lineweith * 1));
                g.DrawLine(Pens.Black, new Point(0, bigSpace * 0 + lineweith * 2),
                       new Point(panelWidth, bigSpace * 0 + lineweith * 2));
                g.DrawLine(Pens.Gray, new Point(0, bigSpace * 1), new Point(panelWidth, bigSpace * 1));
                g.DrawLine(Pens.Gray, new Point(0, bigSpace * 2), new Point(panelWidth, bigSpace * 2));
                g.DrawLine(Pens.Black, new Point(0, bigSpace * 3), new Point(panelWidth, bigSpace * 3));
                g.DrawLine(Pens.Black, new Point(0, bigSpace * 3 + lineweith * 1),
                       new Point(panelWidth, bigSpace * 3 + lineweith * 1));
                g.DrawLine(Pens.Gray, new Point(0, bigSpace * 4), new Point(panelWidth, bigSpace * 4));
                g.DrawLine(Pens.Gray, new Point(0, bigSpace * 5), new Point(panelWidth, bigSpace * 5));

                g.DrawLine(Pens.Black, new Point(0, bigSpace * 6), new Point(panelWidth, bigSpace * 6));
                g.DrawLine(Pens.Black, new Point(0, bigSpace * 6 + lineweith * 1),
                       new Point(panelWidth, bigSpace * 6 + lineweith * 1));
                g.DrawLine(Pens.Gray, new Point(0, bigSpace * 7), new Point(panelWidth, bigSpace * 7));
                g.DrawLine(Pens.Gray, new Point(0, bigSpace * 8), new Point(panelWidth, bigSpace * 8));
                g.DrawLine(Pens.Black, new Point(0, bigSpace * 9 - lineweith * 3),
                       new Point(panelWidth, bigSpace * 9 - lineweith * 3));
                g.DrawLine(Pens.Black, new Point(0, bigSpace * 9 - lineweith * 2),
                       new Point(panelWidth, bigSpace * 9 - lineweith * 2));
                g.DrawLine(Pens.Black, new Point(0, bigSpace * 9 - lineweith * 1),
                       new Point(panelWidth, bigSpace * 9 - lineweith * 1));
                g.DrawLine(Pens.Black, new Point(0, bigSpace * 9), new Point(panelWidth, bigSpace * 9));

                g.DrawLine(Pens.Black, new Point(bigSpace * 0, 0), new Point(bigSpace * 0, panel1Height));
                g.DrawLine(Pens.Black, new Point(bigSpace * 0 + lineweith * 1, 0),
                       new Point(bigSpace * 0 + lineweith * 1, panel1Height));
                g.DrawLine(Pens.Black, new Point(bigSpace * 0 + lineweith * 2, 0),
                       new Point(bigSpace * 0 + lineweith * 2, panel1Height));
                g.DrawLine(Pens.Gray, new Point(bigSpace * 1, 0), new Point(bigSpace * 1, panel1Height));
                g.DrawLine(Pens.Gray, new Point(bigSpace * 2, 0), new Point(bigSpace * 2, panel1Height));
                g.DrawLine(Pens.Black, new Point(bigSpace * 3, 0), new Point(bigSpace * 3, panel1Height));
                g.DrawLine(Pens.Black, new Point(bigSpace * 3 + lineweith * 1, 0),
                       new Point(bigSpace * 3 + lineweith * 1, panel1Height));
                g.DrawLine(Pens.Gray, new Point(bigSpace * 4, 0), new Point(bigSpace * 4, panel1Height));
                g.DrawLine(Pens.Gray, new Point(bigSpace * 5, 0), new Point(bigSpace * 5, panel1Height));
                g.DrawLine(Pens.Black, new Point(bigSpace * 6, 0), new Point(bigSpace * 6, panel1Height));
                g.DrawLine(Pens.Black, new Point(bigSpace * 6 + lineweith * 1, 0),
                       new Point(bigSpace * 6 + lineweith * 1, panel1Height));
                g.DrawLine(Pens.Gray, new Point(bigSpace * 7, 0), new Point(bigSpace * 7, panel1Height));
                g.DrawLine(Pens.Gray, new Point(bigSpace * 8, 0), new Point(bigSpace * 8, panel1Height));
                g.DrawLine(Pens.Black, new Point(bigSpace * 9 - lineweith * 3, 0),
                       new Point(bigSpace * 9 - lineweith * 3, panel1Height));
                g.DrawLine(Pens.Black, new Point(bigSpace * 9 - lineweith * 2, 0),
                       new Point(bigSpace * 9 - lineweith * 2, panel1Height));
                g.DrawLine(Pens.Black, new Point(bigSpace * 9 - lineweith * 1, 0),
                       new Point(bigSpace * 9 - lineweith * 1, panel1Height));
                g.DrawLine(Pens.Black, new Point(bigSpace * 9, 0), new Point(bigSpace * 9, panel1Height));
                QSudoku sudoku = Sudoku;
                if (sudoku != null)
                {
                    foreach (var item in sudoku.AllCell)
                    {
                        if (item.Index == sudoku.CurrentCell.Index)
                        {
                            g.FillRectangle(new SolidBrush(Color.Brown),
                                new Rectangle(new Point(bigSpace * item.Column, bigSpace * item.Row),
                                    new Size(bigSpace, bigSpace)));
                        }

                        if (item.Value != 0)
                        {
                            var stringvalue = "" + item.Value;
                            Size size = TextRenderer.MeasureText(stringvalue, bigFont);
                            var color = item.CellType == CellType.Init ? Color.Black : Color.Blue;
                            g.DrawString(stringvalue, bigFont, new SolidBrush(color),
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
                                    g.DrawString(stringvalue, smallFont, new SolidBrush(Color.Gray),
                                    new PointF(
                                        item.Column * bigSpace + (SmallSpace * ((item1 - 1) % 3)) +
                                        (SmallSpace - size.Width) / 2,
                                        item.Row * bigSpace + (SmallSpace * ((item1 - 1) / 3)) +
                                        +(SmallSpace - size.Height) / 2));
                                }
                            }
                        }
                    }
                }
                ButtonBorderStyle style = ButtonBorderStyle.Solid;
                ControlPaint.DrawBorder(g, rectangle,
                    Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
                    Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
                    Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
                    Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style);
                Paintflag = true;
                graphBuffer.Render(objGraphics);
            }
        }


        //keycode 37 = Left ←
        //keycode 38 = Up ↑
        //keycode 39 = Right →
        //keycode 40 = Down ↓







    }
}
