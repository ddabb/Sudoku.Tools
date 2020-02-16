﻿using Sudoku.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku.UI
{
    public partial class ctlSudoku : UserControl
    {

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool ShowCandidates { get; set; }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public QSudoku Sudoku { get; set; }

        public static int SmallSpace { get; private set; } = 27;

        public static int BigSpace
        {
            get { return SmallSpace * 3; }
        }

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

        public static Dictionary<int, int> lengths = new Dictionary<int, int>()
        {
            {0, BigSpace*0 + 0},
            {1, BigSpace*1 + 3},
            {2, BigSpace*2 +4},
            {3, BigSpace*3 +6 },
            {4, BigSpace*4 +7 },
            {5, BigSpace*5 +8 },
            {6, BigSpace*6 +10 },
            {7, BigSpace*7 +11 },
            {8, BigSpace*8 +12 },
            {9, 742 },
        };

        public int GetIndex(int offset)
        {
            return (from a in lengths
                join b in lengths on 1 equals 1
                where a.Key == b.Key + 1
                      && a.Value > offset
                      && b.Value <= offset
                select b.Key).First();
        }

        public int GetCellIndex(int rowIndex, int columnIndex)
        {
            return rowIndex * 9 + columnIndex;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            var x = e.X;
            var y = e.Y;

            var columnIndex = GetIndex(x);
            var rowIndex = GetIndex(y);
            var cellIndex = GetCellIndex(rowIndex, columnIndex);
            if (Sudoku.CurrentCell == null|| Sudoku.CurrentCell.Index!=cellIndex)
            {
                Sudoku.CurrentCell= Sudoku.AllCell[GetCellIndex(rowIndex, columnIndex)];
                RefreshPanel();
            }
        }


        private Dictionary<int, int> indexOffset = new Dictionary<int, int>
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

        private CellInfo LastCurrentCell=new InitCell(0,0);

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
                    g.DrawLine(kv.Value, new Point(kv.Key, 0), new Point(kv.Key, panel1Height));
                }

                #endregion

                QSudoku sudoku = Sudoku;
                if (sudoku != null)
                {
                    foreach (var item in sudoku.AllCell)
                    {
                        if (item.Index == sudoku.CurrentCell.Index)
                        {
                            var currentCell = sudoku.CurrentCell;
                            var color = Color.DarkOrange;
                            PaintCurrentCell( g,color, bigSpace, currentCell.Row,currentCell.Column);
                        }

                        if (item.Value != 0)
                        {
                            var stringvalue = "" + item.Value;
                            Size size = TextRenderer.MeasureText(stringvalue, bigFont);
                            var color = item.CellType == CellType.Init ? Color.Black : Color.Blue;
                            g.DrawString(stringvalue, bigFont, new SolidBrush(color),
                            item.Column * bigSpace + indexOffset[item.Column] + (bigSpace - size.Width) / 2,
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

        private void PaintCurrentCell(Graphics g,Color color, int bigSpace,int rowIndex,int columnIndex)
        {
            g.FillRectangle(new SolidBrush(color),
                    new Rectangle(
                        new Point(bigSpace * columnIndex + indexOffset[columnIndex] + 1,
                            bigSpace * rowIndex + indexOffset[columnIndex] + 1),
                        new Size(bigSpace, bigSpace)));
        }



        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {

        }



        //keycode 37 = Left ←
        //keycode 38 = Up ↑
        //keycode 39 = Right →
        //keycode 40 = Down ↓







    }
}
