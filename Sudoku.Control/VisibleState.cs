using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Sudoku.Core;
namespace Sudoku.Control
{
    public class VisibleState
    {
        public static int[] visibleState = new[] { 0, 1 };
        public static List<Dictionary<int,int>> allVisibleState = (from v1 in visibleState
                                                         join v2 in visibleState on 1 equals 1
                                                         join v3 in visibleState on 1 equals 1
                                                         join v4 in visibleState on 1 equals 1
                                                         join v5 in visibleState on 1 equals 1
                                                         join v6 in visibleState on 1 equals 1
                                                         join v7 in visibleState on 1 equals 1
                                                         join v8 in visibleState on 1 equals 1
                                                         join v9 in visibleState on 1 equals 1
                                                         select new Dictionary<int,int>() { {1, v1 }, 
                                                             
                                                             { 2, v2 },  { 3, v3 },  { 4, v4 },  { 5, v5 },  { 6, v6 }, { 7, v7 }, { 8, v8 }, { 9, v9 } }).ToList();
        public static void BatchDrawing()
        {
            try
            {
                var SmallSpace = 27;
                var bigSpace = SmallSpace * 3;
                var smallFont = new Font("宋体", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
                foreach (var test in allVisibleState)
                {
      
                    Panel panel = new Panel();
                
                    var color = Color.Gray;
                    panel.Size = new Size(bigSpace, bigSpace);
                    using (Bitmap bmp = new Bitmap(panel.Size.Width, panel.Size.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            var rect = new Rectangle(new Point(0, 0), bmp.Size);
                            var folder = @"D:\Images";
                            var subFolder = "";
                            if (!Directory.Exists(folder))
                            {
                                Directory.CreateDirectory(folder);
                            };
                      
                            g.FillRectangle(new SolidBrush(Color.White), rect);
                            if (test.Any(c => c.Value != 0))
                            {
                                subFolder = "Hint" + test.Where(c=>c.Value!=0).Select(c=>c.Key).JoinString();
                                var subDirectory=  Path.Combine(folder, subFolder);
                                if (!Directory.Exists(subDirectory))
                                {
                                    Directory.CreateDirectory(subDirectory);
                                };
                                for (int i = 0; i < test.Count; i++)
                                {
                                    if (test[i+1] != 0)
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
                                   
                                    }
                                }
                                g.Save();
                                var filePath = Path.Combine(subDirectory, subFolder + ".jpg") ;
                                bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                            else
                            {
                                subFolder ="emptyHint";
                                var subDirectory = Path.Combine(folder, subFolder);
                                if (!Directory.Exists(subDirectory))
                                {
                                    Directory.CreateDirectory(subDirectory);
                                };
                                var filePath = Path.Combine(subDirectory, subFolder + ".jpg");
                                bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                        
                        }
              
                    }
                }
                var bigFont = new Font("宋体", smallFont.Size * 3, FontStyle.Bold, GraphicsUnit.Point, 0);
                foreach (var item in G.AllBaseValues)
                {
                    Panel panel = new Panel();
                    panel.Size = new Size(bigSpace, bigSpace);
                    using (Bitmap bmp = new Bitmap(panel.Size.Width, panel.Size.Height))
                    {
                       
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            var rect = new Rectangle(new Point(0, 0), bmp.Size);
                            var folder = @"D:\Images";
                            var subFolder = "";
                            if (!Directory.Exists(folder))
                            {
                                Directory.CreateDirectory(folder);
                            };
                            subFolder = "Value" + item;
                            var subDirectory = Path.Combine(folder, subFolder);
                            if (!Directory.Exists(subDirectory))
                            {
                                Directory.CreateDirectory(subDirectory);
                            };
                            g.FillRectangle(new SolidBrush(Color.White), rect);
                            var stringvalue = "" + item;
                            Size size = TextRenderer.MeasureText(stringvalue, bigFont);
                            var color = Color.Black;
                            g.DrawString(stringvalue, bigFont, new SolidBrush(color),
                                (bigSpace - size.Width) / 2,
                                (bigSpace - size.Height) / 2);
                            var filePath = Path.Combine(subDirectory, subFolder + ".jpg");
                            bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
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
