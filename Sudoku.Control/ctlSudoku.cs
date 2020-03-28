using System;
using Sudoku.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sudoku.Control;
using Sudoku.Core.Model;

namespace Sudoku.UI
{
    public partial class ctlSudoku : UserControl
    {

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool ShowCandidates { get; set; }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public QSudoku Sudoku { get; set; }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public CellInfo DrawingCell { get; set; }

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

        /// <summary>
        /// 没有在绘制
        /// </summary>
        public bool IsNotPainting = true;



        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            RefreshSudokuPanel();
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

        public static Dictionary<int, DisplacementIntervall> lengths = new Dictionary<int, DisplacementIntervall>()
        {
            {0,  new DisplacementIntervall{MaxValue = BigSpace*1+2  ,MinValue =Int32.MinValue}},//防止拖拽出边界
            {1,  new DisplacementIntervall{MaxValue = BigSpace*2+3  ,MinValue =BigSpace*1+2 }},
            {2,  new DisplacementIntervall{MaxValue = BigSpace*3+4  ,MinValue =BigSpace*2+3 }},
            {3,  new DisplacementIntervall{MaxValue = BigSpace*4+6  ,MinValue =BigSpace*3+4 }},
            {4,  new DisplacementIntervall{MaxValue = BigSpace*5+7  ,MinValue =BigSpace*4+6 }},
            {5,  new DisplacementIntervall{MaxValue = BigSpace*6+8  ,MinValue =BigSpace*5+7 }},
            {6,  new DisplacementIntervall{MaxValue = BigSpace*7+10 ,MinValue =BigSpace*6+8 } },
            {7,  new DisplacementIntervall{MaxValue = BigSpace*8+11 ,MinValue =BigSpace*7+10} },
            {8,  new DisplacementIntervall{MaxValue = Int32.MaxValue,           MinValue =BigSpace*8+11} },
      
        };

        public int GetIndex(int offset)
        {
            return  (from kv in lengths where kv.Value.MinValue<=offset&&kv.Value.MaxValue>offset select kv.Key).First();
        }

        public int GetCellIndex(int rowIndex, int columnIndex)
        {
            return rowIndex * 9 + columnIndex;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsNotPainting)
            {
                var x = e.X;
                var y = e.Y;

                var columnIndex = GetIndex(x);
                var rowIndex = GetIndex(y);
                var cellIndex = GetCellIndex(rowIndex, columnIndex);
                if (Sudoku.CurrentCell == null || Sudoku.CurrentCell.Index != cellIndex)
                {
                    Sudoku.CurrentCell = Sudoku.AllCell[GetCellIndex(rowIndex, columnIndex)];
                    RefreshSudokuPanel();
                }
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

  
        public void RefreshSudokuPanel()
        {
            if (IsNotPainting)
            {
                IsNotPainting = false;
                var objGraphics = sudokuPanel.CreateGraphics();
                var bigSpace = SmallSpace * 3;
                var panelWidth = sudokuPanel.Width;
                var panel1Height = sudokuPanel.Height;
                Rectangle rectangle = new Rectangle(0, 0, sudokuPanel.Width, sudokuPanel.Height);
                BufferedGraphics graphBuffer = (new BufferedGraphicsContext()).Allocate(objGraphics, rectangle);
                Graphics g = graphBuffer.Graphics;
                var smallFont = new Font("宋体", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
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
                        if (sudoku.CurrentCell!=null&&item.Index == sudoku.CurrentCell.Index)
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
                                    Color color = Color.Gray;
                                    g.DrawString(stringvalue, smallFont, new SolidBrush(color),
                                    new PointF(
                                        item.Column * bigSpace + indexOffset[item.Column] + (SmallSpace * ((item1 - 1) % 3)) +
                                        (SmallSpace - size.Width) / 2,
                                        item.Row * bigSpace + indexOffset[item.Row] + (SmallSpace * ((item1 - 1) / 3)) +
                                        +(SmallSpace - size.Height) / 2));
                                }
                            }
                        }
                    }
                    #region 绘制提示数信息
                    if (ShowCandidates&& DrawingCell != null)
                    {
                        if (sudoku.AllCell[DrawingCell.Index].Value==0)
                        {
                            DrawHint(DrawingCell, bigSpace, g, smallFont);
                        }                

                    }
                    #endregion
                }
                ButtonBorderStyle style = ButtonBorderStyle.Solid;
                ControlPaint.DrawBorder(g, rectangle,
                    Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
                    Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
                    Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
                    Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style);
                IsNotPainting = true;
                graphBuffer.Render(objGraphics);
            }
        }

        /// <summary>
        /// 绘制提示内容
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="bigSpace"></param>
        /// <param name="g"></param>
        /// <param name="smallFont"></param>
        private void DrawHint(CellInfo cell, int bigSpace, Graphics g, Font smallFont)
        {
            #region 绘制单元格
            foreach (var item in cell.drawCells)
            {
                if (Sudoku.AllCell[item.Index].Value == 0)
                {
                    var item1 = item.Value;
                    var stringvalue = "" + item1;
                    Size size = TextRenderer.MeasureText(stringvalue, smallFont);
                    Color color = Color.Gray;
                    if (item.CellType == CellType.Positive)
                    {
                        color = Color.Lime;
                    }
                    if (item.CellType == CellType.Possible)
                    {
                        color = Color.Orange;
                    }
                    if (item.CellType == CellType.Negative)
                    {
                        color = Color.Red;
                    }
                    g.DrawString(stringvalue, smallFont, new SolidBrush(color),
                    new PointF(
                        item.Column * bigSpace + indexOffset[item.Column] + (SmallSpace * ((item1 - 1) % 3)) +
                        (SmallSpace - size.Width) / 2,
                        item.Row * bigSpace + indexOffset[item.Row] + (SmallSpace * ((item1 - 1) / 3)) +
                        +(SmallSpace - size.Height) / 2));
                }

            }
            #endregion

            #region 绘制线条
            foreach (var item in cell.drawChains)
            {

            }
            #endregion
        }

        private void PaintCurrentCell(Graphics g,Color color, int bigSpace,int rowIndex,int columnIndex)
        {
            g.FillRectangle(new SolidBrush(color),
                    new Rectangle(
                        new Point(bigSpace * columnIndex + indexOffset[columnIndex] + 1,
                            bigSpace * rowIndex + indexOffset[rowIndex] + 1),
                        new Size(bigSpace, bigSpace)));
        }



        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void panel1_MouseLeave(object sender, System.EventArgs e)
        {
            Sudoku.CurrentCell = null;
            RefreshSudokuPanel();
        }

        private void RefreshRowPanel()
        {
            var objGraphics = rowPanel.CreateGraphics();
            var width = 0;
            var heigth = BigSpace / 2-5;
            var smallFont = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point,
                ((byte) (134)));
            Rectangle rectangle = new Rectangle(0, 0, rowPanel.Width, rowPanel.Height);
            BufferedGraphics graphBuffer = (new BufferedGraphicsContext()).Allocate(objGraphics, rectangle);
            Graphics g = graphBuffer.Graphics;
            g.FillRectangle(new SolidBrush(Color.White), rectangle);
            var color = Color.Black;
            for (int i = 0; i < G.alpha.Count; i++)
            {
                g.DrawString(G.LocationType==LocationType.R1C1?G.RowListString[i]:G.alpha[i], smallFont, new SolidBrush(color),
                    width,
                    BigSpace * i + heigth+ indexOffset[i]);
            }

            graphBuffer.Render(objGraphics);
        }

   

        private void RefreshColumnPanel()
        {
            var objGraphics = columnPanel.CreateGraphics();
            var width = 0;
            var heigth = BigSpace / 2-5;
            var smallFont = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point,
                ((byte) (134)));
            Rectangle rectangle = new Rectangle(0, 0, columnPanel.Width, columnPanel.Height);
            BufferedGraphics graphBuffer = (new BufferedGraphicsContext()).Allocate(objGraphics, rectangle);
            Graphics g = graphBuffer.Graphics;
            g.FillRectangle(new SolidBrush(Color.White), rectangle);
            var color = Color.Black;
            for (int i = 0; i < G.AllBaseValues.Count; i++)
            {
                g.DrawString(G.LocationType == LocationType.R1C1 ? G.ColumnListString[i] :G.AllBaseValues[i] + "", smallFont, new SolidBrush(color),
                    BigSpace * i + heigth + indexOffset[i],
                    0);
            }

            graphBuffer.Render(objGraphics);
        }

        private void rowPanel_Paint(object sender, PaintEventArgs e)
        {

            RefreshRowPanel();
        }

        private void columnPanel_Paint(object sender, PaintEventArgs e)
        {
            RefreshColumnPanel();
        }

        public void RetSetRowAndColumnFormat()
        {
            RefreshRowPanel();
            RefreshColumnPanel();
        }


        //keycode 37 = Left ←
        //keycode 38 = Up ↑
        //keycode 39 = Right →
        //keycode 40 = Down ↓







    }
}
