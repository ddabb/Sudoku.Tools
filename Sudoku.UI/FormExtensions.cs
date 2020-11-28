using System.Drawing;
using System.Windows.Forms;
namespace Sudoku.UI
{
 public static   class FormExtensions
    {
        public static void CenterScreen(this Form @this)
        {
            //指定窗口初始化时的位置（计算机屏幕中间）
            @this.StartPosition = FormStartPosition.CenterScreen;
            //指定窗口初始化时的位置,如果为Manual，位置由Location决定(计算机屏幕中间，如果不/2，则计算机右下角)
            @this.StartPosition = FormStartPosition.Manual;
            @this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - @this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - @this.Height) / 2);
        }
    }
}
