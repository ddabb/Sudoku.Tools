using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku.Control
{
    public class VisibleState
    {

        public static int[] visibleState = new[] { 0, 1 };

        public static List<List<int>> allVisibleState = (from v1 in visibleState
                                                         join v2 in visibleState on 1 equals 1
                                                         join v3 in visibleState on 1 equals 1
                                                         join v4 in visibleState on 1 equals 1
                                                         join v5 in visibleState on 1 equals 1
                                                         join v6 in visibleState on 1 equals 1
                                                         join v7 in visibleState on 1 equals 1
                                                         join v8 in visibleState on 1 equals 1
                                                         join v9 in visibleState on 1 equals 1
                                                         select new List<int> { v1, v2, v3, v4, v5, v6, v7, v8, v9 }).ToList();

        public static void BatchDrawing()
        {

            try
            {
                var SmallSpace = 27;
                var bigSpace = SmallSpace * 3;
                Panel panel = new Panel();
                var smallFont = new Font("宋体", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
                var color=Color.Gray;
                panel.Size = new Size(bigSpace, bigSpace);
                using (Bitmap bmp = new Bitmap(panel.Size.Width, panel.Size.Height))
                {

                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        var rect = new Rectangle(new Point(0, 0), bmp.Size);
                        var filePath = "";
                        g.FillRectangle(new SolidBrush(Color.White), rect);
                        List<int> test=new List<int>{1,0,1,0,1,1,0,0,1};
                        for (int i = 0; i < test.Count; i++)
                        {
                            if (test[i]!=0)
                            {
                                var item1 = i + 1;
                                var stringvalue = "" + item1;
                                Size size = TextRenderer.MeasureText(stringvalue, smallFont);
                                g.DrawString(stringvalue, smallFont, new SolidBrush(color),
                                    new PointF(
                                   (SmallSpace * ((item1 - 1) % 3)) +
                                        (SmallSpace - size.Width) / 2,
                                       (SmallSpace * ((item1 - 1) / 3)) +
                                        +(SmallSpace - size.Height) / 2));
                                g.Save();
                            }
                           
                        }
    
                    }

                    bmp.Save("d:\\abc.png", System.Drawing.Imaging.ImageFormat.Jpeg);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine("BatchDrawing" + e);
                throw;
            }


        }
    }
}
