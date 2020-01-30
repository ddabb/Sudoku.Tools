using Sudoku.Core;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku.UI
{
    public class SudokuPanel : Panel
    {
        public SudokuPanel()
        {
            this.Paint += hmaPanel_Paint;
            this.BackColor = Color.FromArgb(255, 0xd7, 0xd7, 0xd7);
            this.Resize += (sender, e) => { this.Refresh(); };



        }
        private void hmaPanel_Paint(object sender, PaintEventArgs e)
        {

            var objGraphics = this.CreateGraphics();
            var smallspace = 30;
            var bigSpace = smallspace * 3;
            Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
            var smallFont = new Font("宋体", 14f, FontStyle.Bold, GraphicsUnit.Point, 0);
            var bigFont = new Font("宋体", smallFont.Size * 3, FontStyle.Bold, GraphicsUnit.Point, 0);
            objGraphics.FillRectangle(new SolidBrush(Color.White), rectangle);
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 0), new Point(this.Width, bigSpace * 0));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 1), new Point(this.Width, bigSpace * 1));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 2), new Point(this.Width, bigSpace * 2));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 3), new Point(this.Width, bigSpace * 3));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 4), new Point(this.Width, bigSpace * 4));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 5), new Point(this.Width, bigSpace * 5));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 6), new Point(this.Width, bigSpace * 6));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 7), new Point(this.Width, bigSpace * 7));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 8), new Point(this.Width, bigSpace * 8));
            objGraphics.DrawLine(Pens.Black, new Point(0, bigSpace * 9), new Point(this.Width, bigSpace * 9));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 0, 0), new Point(bigSpace * 0, this.Height));
            //objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 0+1, 0), new Point(bigSpace * 0+1, this.Height));
            //objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 0 + 2, 0), new Point(bigSpace * 0 + 2, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 1, 0), new Point(bigSpace * 1, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 2, 0), new Point(bigSpace * 2, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 3, 0), new Point(bigSpace * 3, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 4, 0), new Point(bigSpace * 4, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 5, 0), new Point(bigSpace * 5, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 6, 0), new Point(bigSpace * 6, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 7, 0), new Point(bigSpace * 7, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 8, 0), new Point(bigSpace * 8, this.Height));
            objGraphics.DrawLine(Pens.Black, new Point(bigSpace * 9, 0), new Point(bigSpace * 9, this.Height));

            if (this.Tag is QSudoku sudoku)
            {
                foreach (var item in sudoku.AllSetCell)
                {

                    var stringvalue = "" + item.Value;
                    Size size = TextRenderer.MeasureText(stringvalue, bigFont);
                    var color= item.CellType==CellType.Init? Color.Black:Color.Blue;
                    objGraphics.DrawString(stringvalue, bigFont, new SolidBrush(color), item.Column * bigSpace +(bigSpace - size.Width)/2, item.Row * bigSpace + (bigSpace - size.Height) / 2);

                }

                foreach (var item in sudoku.AllUnSetCells)
                {
                    foreach (var item1 in item.RestList)
                    {
                        var stringvalue = "" + item1;
                        Size size = TextRenderer.MeasureText(stringvalue, smallFont);
                        objGraphics.DrawString(stringvalue, smallFont, new SolidBrush(Color.Gray), new PointF(item.Column * bigSpace + (smallspace * ((item1-1) % 3)) + (smallspace - size.Width) / 2, item.Row * bigSpace + (smallspace * ((item1 - 1) / 3)) + +(smallspace - size.Height) / 2));
                    }

                }
            }


            ButtonBorderStyle style = ButtonBorderStyle.Solid;

            ControlPaint.DrawBorder(this.CreateGraphics(), rectangle,
               Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
               Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
               Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style,
               Color.FromArgb(255, 0xd7, 0xd7, 0xd7), 1, style);
        }


    }
}
