using System.Collections.Generic;
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

        /// <summary>
        /// panel的size=bigSpace*9+15;
        /// </summary>
        /// <param name="bigSpace"></param>
        /// <returns></returns>
        private List<int> GetOffSet(int bigSpace)
        {
            return new List<int>
                {

                    1+ bigSpace*0 ,
                    2+ bigSpace*0 ,
                    3+ bigSpace*1 ,
                    4+ bigSpace*2 ,
                    5+ bigSpace*3 ,
                    6+ bigSpace*3 ,
                    7+ bigSpace*4 ,
                    8+ bigSpace*5 ,
                    9+ bigSpace*6 ,
                    10+bigSpace*6,
                    11+bigSpace*7,
                    12+bigSpace*8,
                    13+bigSpace*9,
                    14+bigSpace*9,
                };
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

                var offSets = GetOffSet(bigSpace);

                #region 画横线
                
                foreach (var offSet in offSets)
                {
                    g.DrawLine(Pens.Black, new Point(0, offSet), new Point(panelWidth, offSet));
                }
                #endregion

                #region 画竖线

                foreach (var offSet in offSets)
                {

                    g.DrawLine(Pens.Black, new Point(offSet, 0), new Point(offSet, panel1Height));
                }
                #endregion




                QSudoku sudoku = Sudoku;
                if (sudoku != null)
                {
                    foreach (var item in sudoku.AllCell)
                    {
                        if (item.Index == sudoku.CurrentCell.Index)
                        {
                            g.FillRectangle(new SolidBrush(Color.DarkOrange),
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
