using System.Collections.Generic;
using Sudoku.Core;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Schema;

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
        private Dictionary<int, Pen> GetOffSet(int bigSpace)
        {
    
            return new Dictionary<int, Pen>()
            {

                {1 + bigSpace * 0, Pens.Black},
                {2 + bigSpace * 0, Pens.Black},
                {3 + bigSpace * 1, Pens.Gray},
                {4 + bigSpace * 2, Pens.Gray},
                {5 + bigSpace * 3, Pens.Black},
                {6 + bigSpace * 3, Pens.Black},
                {7 + bigSpace * 4, Pens.Gray},
                {8 + bigSpace * 5, Pens.Gray},
                {9 + bigSpace * 6, Pens.Black},
                {10 + bigSpace * 6, Pens.Black},
                {11 + bigSpace * 7, Pens.Gray},
                {12 + bigSpace * 8, Pens.Gray},
                {13 + bigSpace * 9, Pens.Black},
                {14 + bigSpace * 9, Pens.Black},
            };
        }

        private Dictionary<int,int> indexOffset=new Dictionary<int, int>
        {
               {0 , 2 },
               {1 , 3 },
               {2 , 4 },
               {3 , 6 },
               {4 , 7 },
               {5 , 8 },
               {6 , 10},
               {7 , 11},
               {8 , 12},

        };

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

                foreach (var kv in offSets)
                {
                    g.DrawLine(kv.Value, new Point(0, kv.Key), new Point(panelWidth, kv.Key));
                    g.DrawLine(kv.Value, new Point(kv.Key,0), new Point(kv.Key, panel1Height));
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
                                new Rectangle(new Point(bigSpace * item.Column + indexOffset[item.Column]+1, bigSpace * item.Row + indexOffset[item.Row]+1),
                                    new Size(bigSpace, bigSpace)));
                        }

                        if (item.Value != 0)
                        {
                            var stringvalue = "" + item.Value;
                            Size size = TextRenderer.MeasureText(stringvalue, bigFont);
                            var color = item.CellType == CellType.Init ? Color.Black : Color.Blue;
                            g.DrawString(stringvalue, bigFont, new SolidBrush(color),
                            item.Column * bigSpace+ indexOffset[item.Column] + (bigSpace - size.Width) / 2,
                            item.Row * bigSpace + indexOffset[item.Row] + (bigSpace - size.Height) / 2);
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
                                        item.Column * bigSpace + indexOffset[item.Column] + (SmallSpace * ((item1 - 1) % 3)) +
                                        (SmallSpace - size.Width) / 2,
                                        item.Row * bigSpace + indexOffset[item.Row] + (SmallSpace * ((item1 - 1) / 3)) +
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
