using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Graphics g;
            Bitmap bmp = new Bitmap("ss.bmp");
            var filePath = "";
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(pictureBox1.Image);
            //下面你可以使用Graphics自由涂鸦，下面代码是画一个X
            g.DrawLine(new Pen(Color.Red, 1f), 0, 0, 300, 300);
            g.DrawLine(new Pen(Color.Red, 1f), 0, 300, 300, 0);
            //保存涂鸦后的画
            pictureBox1.Image.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);
        }
    }
}
