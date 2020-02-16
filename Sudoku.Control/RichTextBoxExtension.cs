using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku.Control
{
    public static class RichTextBoxExtension
    {
        public static void AppendTextColorful(this RichTextBox rtBox, string text, Color color, bool addNewLine)
        {
            if (addNewLine)
            {
                text += Environment.NewLine;
            }
            rtBox.SelectionStart = rtBox.TextLength;
            rtBox.SelectionLength = 0;
            rtBox.SelectionColor = color;
            rtBox.AppendText(text);
            rtBox.SelectionColor = rtBox.ForeColor;
        }

        /// <summary>
        /// 改变richTextBox中指定字符串的颜色
        /// 调用即可
        /// </summary>
        /// <param name="str" value="为指定的字符串"></param>
        public static void changeColor(string str)
        {
            //ArrayList list = getIndexArray(richTextBox1.Text, str);
            //for (int i = 0; i < list.Count; i++)
            //{
            //    int index = (int)list[i];
            //    richTextBox1.Select(index, str.Length);
            //    richTextBox1.SelectionColor = Color.Green;
            //}

            //改变字体大小         呃呃呃呃呃呃呃呃呃安沃驰lllllllllllllllllllllfgglk
            //Font font = new Font(FontFamily.GenericMonospace, 14, FontStyle.Regular);
            //this.richTextBox1.SelectionFont = font;
        }

 
        //public ArrayList getIndexArray(String inputStr, String findStr)
        //{
        //    ArrayList list = new ArrayList();
        //    int start = 0;
        //    while (start < inputStr.Length)
        //    {
        //        int index = inputStr.IndexOf(findStr, start);
        //        if (index >= 0)
        //        {
        //            list.Add(index);
        //            start = index + findStr.Length;
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    return list;
        //}

}

}
