using Sudoku.Core;
using Sudoku.Core.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku.Control
{
    public class DrawGird
    {
        private bool ShowCandidates;
        private QSudoku Sudoku;
        private CellInfo DrawingCell;
        private int panelWidth;
        private int panel1Height; 
        public DrawGird(int smallSpace, bool ShowCandidates, CellInfo DrawingCell, QSudoku Sudoku, int panelWidth, int panel1Height)
        {
            this.Sudoku = Sudoku;
            this.ShowCandidates = ShowCandidates;
            this.DrawingCell = DrawingCell;
            this.SmallSpace = smallSpace;
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

        private int SmallSpace;
        public void DrawPanel(Graphics objGraphics)
        {
            var bigSpace = SmallSpace * 3;

            Rectangle rectangle = new Rectangle(0, 0, panelWidth, panel1Height);
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
                    if (sudoku.CurrentCell != null && item.Index == sudoku.CurrentCell.Index)
                    {
                        var currentCell = sudoku.CurrentCell;
                        var color = Color.DarkOrange;
                        PaintCurrentCell(g, color, bigSpace, currentCell.Row, currentCell.Column);
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

                if (ShowCandidates && DrawingCell != null)
                {
                    if (sudoku.AllCell[DrawingCell.Index].Value == 0)
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

            graphBuffer.Render(objGraphics);
        }

        private void PaintCurrentCell(Graphics g, Color color, int bigSpace, int rowIndex, int columnIndex)
        {
            g.FillRectangle(new SolidBrush(color),
                new Rectangle(
                    new Point(bigSpace * columnIndex + indexOffset[columnIndex] + 1,
                        bigSpace * rowIndex + indexOffset[rowIndex] + 1),
                    new Size(bigSpace, bigSpace)));
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
                    Color color = Color.White;
                    if (item.CellType == CellType.Positive)
                    {
                        color = Color.FromArgb(134, 242, 128);
                    }
                    if (item.CellType == CellType.Possible)
                    {
                        color = Color.FromArgb(255, 192, 89);
                    }
                    if (item.CellType == CellType.Negative)
                    {
                        color = Color.FromArgb(245, 165, 167);
                    }
                    g.FillRectangle(new SolidBrush(color), new Rectangle(item.Column * bigSpace + indexOffset[item.Column] + (SmallSpace * ((item1 - 1) % 3)) +
                                                                        (SmallSpace - size.Width) / 2, item.Row * bigSpace + indexOffset[item.Row] + (SmallSpace * ((item1 - 1) / 3)) + (SmallSpace - size.Height) / 2, size.Width, size.Height));
                    g.DrawString(stringvalue, smallFont, new SolidBrush(Color.Gray),
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


    }
}
