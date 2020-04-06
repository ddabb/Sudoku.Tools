using Sudoku.Core;
using Sudoku.Core.Model;
using Sudoku.Tools;
using Sudoku.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sudoku.GenWiki
{
    public class AutoGenWiki
    {
        static void Main(string[] args)
        {
            var handlers = FrmG.SolveHandlers.OrderBy(c => (int)c.methodType).ToList();
            var folder = @"D:\Git\Sudoku.Tools.wiki";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            foreach (var handler in handlers)
            {
                var fileName = Path.Combine(folder, "数独技巧", G.GetEnumDescription(handler.methodType) + ".md");
                List<string> allString = new List<string>();

                var desc = handler.GetDesc();

                allString.Add("");
                allString.Add("## 技巧描述");
                allString.Add(string.IsNullOrEmpty(desc) ? "请联系作者,要求其补充描述。" : desc);

                var type = handler.GetType();
                object[] objs = type.GetCustomAttributes(typeof(AssignmentExampleAttribute), true);
                if (objs.Count() == 1)
                {
                    if ((objs[0] is AssignmentExampleAttribute a))
                    {
                        if (a.value != 0)
                        {
                            allString.Add("");
                            allString.Add("## 出数示例");
                         
                            var positionString = a.positionString;
                            var qsudoku = new QSudoku(a.queryString);
                            var handers = a.SolveHandlers;
                            if (handers!=null)
                            {
                                foreach (var handerEnum in handers)
                                {
                                    var eliminationHanders = handlers.First(c => handerEnum == (c.methodType));
                                    var removeCells = eliminationHanders.Elimination(qsudoku);
                                    qsudoku.RemoveCells(removeCells);
                                }
                            }
                            var cellinfo = handler.Assignment(
                                qsudoku);
                            if (cellinfo.Exists(c => c.RrCc == positionString && c.Value == a.value))
                            {
                                var drawingCell =
                                    cellinfo.First(c => c.RrCc == positionString && c.Value == a.value);
                                Draw(drawingCell, qsudoku, @"D:\Git\Sudoku.Tools\Images\AssignmentExample",
                                    G.GetEnumDescription(handler.methodType));
                                allString.Add("");
                                allString.Add("<p align= \"center\" >");
                                allString.Add(" <img src = \"/ddabb/Sudoku.Tools/blob/master/Images/AssignmentExample/" + UrlEncode(G.GetEnumDescription(handler.methodType) + ".jpg") + "\" />");
                                allString.Add(" </p>");
                                var solveMessage = drawingCell.SolveMessages;
                                if (solveMessage.Count != 0)
                                {
                                    allString.Add("");
                                    allString.Add("## 出数描述");
                                    allString.Add(solveMessage.JoinStringWithEmpty() + "\t\t\r\n");
                                }

                            }

                            allString.Add("**数独特征码：**" + a.queryString);




                        }

                    }
                }



                object[] objs1 = type.GetCustomAttributes(typeof(EliminationExampleAttribute), true);
                if (handler is SplitWingHandler t)
                {

                }
                if (objs1.Count() == 1)
                {
                    if ((objs1[0] is EliminationExampleAttribute e))
                    {
                        if (e.value != 0)
                        {
                            allString.Add("");
                            allString.Add("## 删数示例");
                           

                            var positionString = e.positionString;
                            var qsudoku = new QSudoku(e.queryString);
                            var handers = e.SolveHandlers;
                            if (handers != null)
                            {
                                foreach (var handerEnum in handers)
                                {
                                    var eliminationHanders = handlers.First(c => handerEnum == (c.methodType));
                                    var removeCells = eliminationHanders.Elimination(qsudoku);
                                    qsudoku.RemoveCells(removeCells);
                                }
                            }
                            var cellinfo = handler.Elimination(
                                qsudoku);
                            if (cellinfo.Exists(c => (c.CellType == CellType.Negative && c.RrCc == positionString && c.Value == e.value)
                                                     || (c.CellType == CellType.NegativeIndexsGroup && c.Indexs.Exists(x => x.LoctionDesc().ToString() == positionString) && c.Value == e.value)
                                                     || (c.CellType == CellType.NegativeValuesGroup && c.RrCc == positionString && c.NegativeValues.Contains(e.value))
                                                     ))
                            {
                                var drawingCell =
                                    cellinfo.First(c => c.RrCc == positionString && c.Value == e.value);
                                Draw(drawingCell, qsudoku, @"D:\Git\Sudoku.Tools\Images\EliminationExample",
                                    G.GetEnumDescription(handler.methodType));

                                allString.Add("");
                                allString.Add("<p align= \"center\" >");
                                allString.Add(" <img src = \"/ddabb/Sudoku.Tools/blob/master/Images/EliminationExample/" + UrlEncode(G.GetEnumDescription(handler.methodType) + ".jpg") + "\" />");
                                allString.Add(" </p >");
                               
                                var solveMessage = drawingCell.SolveMessages;
                                if (solveMessage.Count != 0)
                                {
                                    allString.Add("");
                                    allString.Add("## 删数描述");
                                    allString.Add(solveMessage.JoinStringWithEmpty() + "\t\t\r\n");
                                }
                            }
                            allString.Add("**数独特征码：**"+e.queryString);

                        }
                    }
                }
                allString.Add("该文件由[AutoGenWiki.cs](https://github.com/ddabb/Sudoku.Tools/blob/master/Sudoku.GenWiki/AutoGenWiki.cs)自动生成");
                File.WriteAllLines(fileName, allString);
            }





            var sidebarFile = Path.Combine(folder, "_Sidebar.md");
            List<string> fileStrs = new List<string>();
            fileStrs.Add("## 软件现状");
            fileStrs.Add("");
            fileStrs.Add("* [" + "软件现状" + "](https://github.com/ddabb/Sudoku.Tools/wiki/task_list)");

            fileStrs.Add("## 基础文档");
            fileStrs.Add("");

            foreach (var handler in handlers)
            {
                var name = G.GetEnumDescription(handler.methodType);

                fileStrs.Add("* [" + name + "](" + "https://github.com/ddabb/Sudoku.Tools/wiki/" + UrlEncode(name) + ")");

            }
            File.WriteAllLines(sidebarFile, fileStrs);

        }


        public static void Draw(CellInfo DrawingCell, QSudoku sudoku, string subDirectory, string fileName)
        {
            var panelWidth = 745;
            var panel1Height = 745;
            var SmallSpace = 27;
            var ShowCandidates = true;
            using (Bitmap bmp = new Bitmap(745, 745))
            {

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    var rect = new Rectangle(new Point(0, 0), bmp.Size);

                    g.FillRectangle(new SolidBrush(Color.White), rect);


                    var bigSpace = SmallSpace * 3;

                    Rectangle rectangle = new Rectangle(0, 0, panelWidth, panel1Height);

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
                                DrawHint(DrawingCell, bigSpace, g, smallFont, sudoku, SmallSpace);
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


                    var filePath = Path.Combine(subDirectory, fileName + ".jpg");
                    bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }


            }




        }


        private static Dictionary<int, int> indexOffset = new Dictionary<int, int>
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



        /// <summary>
        /// 绘制提示内容
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="bigSpace"></param>
        /// <param name="g"></param>
        /// <param name="smallFont"></param>
        public static void DrawHint(CellInfo cell, int bigSpace, Graphics g, Font smallFont, QSudoku Sudoku, int SmallSpace)
        {
            #region 绘制单元格
            foreach (var item in cell.drawCells)
            {
                if (Sudoku.AllCell[item.Index].Value == 0)
                {
                    var item1 = item.Value;
                    var stringvalue = "" + item1;
                    Size size = TextRenderer.MeasureText(stringvalue, smallFont);
                    var color = G.GetCellColor(item);
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


        /// <summary>
        /// panel的size=bigSpace*9+15;
        /// </summary>
        /// <param name="bigSpace"></param>
        /// <returns></returns>
        private static Dictionary<int, Pen> GetOffSet(int bigSpace)
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

        public static void PaintCurrentCell(Graphics g, Color color, int bigSpace, int rowIndex, int columnIndex)
        {
            g.FillRectangle(new SolidBrush(color),
                new Rectangle(
                    new Point(bigSpace * columnIndex + indexOffset[columnIndex] + 1,
                        bigSpace * rowIndex + indexOffset[rowIndex] + 1),
                    new Size(bigSpace, bigSpace)));
        }

        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }


    }

}

