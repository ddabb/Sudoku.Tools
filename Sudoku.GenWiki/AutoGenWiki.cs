using Sudoku.Core;
using Sudoku.Core.Model;
using Sudoku.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sudoku.GenWiki
{
    public class AutoGenWiki
    {
        static void Main(string[] args)
        {
            var handlers = FrmG.SolveHandlers.OrderBy(c => (int)c.methodType);
            var folder = @"D:\Git\Sudoku.Tools.wiki";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            foreach (var handler in handlers)
            {
                var fileName = Path.Combine(folder, "数独技巧", G.GetEnumDescription(handler.methodType) + ".md");
                List<string> allString = new List<string>();
                allString.Add("**该文件由 https://github.com/ddabb/Sudoku.Tools/tree/master/Sudoku.GenWiki 自动生成**");
                var desc = handler.GetDesc();
                if (!string.IsNullOrEmpty(desc))
                {
                    allString.Add("");
                    allString.Add("## 技巧描述");
                    allString.Add(desc);

                    var type = handler.GetType();
                    object[] objs = type.GetCustomAttributes(typeof(AssignmentExampleAttribute), true);
                    if (objs.Count() == 1)
                    {
                        if ((objs[0] is AssignmentExampleAttribute a))
                        {
                            if (a.value!=0)
                            {
                                allString.Add("");
                                allString.Add("## 出数示例");
                                allString.Add(a.queryString);
                            }
                           
                        }
                    }

                    object[] objs1 = type.GetCustomAttributes(typeof(EliminationExampleAttribute), true);
                    if (objs1.Count() == 1)
                    {
                        if ((objs1[0] is EliminationExampleAttribute e))
                        {
                            if (e.value != 0)
                            {
                                allString.Add("");
                                allString.Add("## 删数示例");
                                allString.Add(e.queryString);
                            }
                        }
                    }


                }
                File.WriteAllLines(fileName, allString);
            }





            var sidebarFile = Path.Combine(folder, "_Sidebar.md");
            List<string> fileStrs = new List<string>();
            fileStrs.Add("## 基础文档");
            fileStrs.Add("");

            foreach (var handler in handlers)
            {
                var name = G.GetEnumDescription(handler.methodType);

                fileStrs.Add("* [" + name + "](" + "https://github.com/ddabb/Sudoku.Tools/wiki/" + UrlEncode(name) + ")");

            }
            File.WriteAllLines(sidebarFile, fileStrs);

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

