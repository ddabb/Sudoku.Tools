using Autofac;
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Sudoku.Console
{
    class StaticTools
    {
        static void Main(string[] args)
        {
            var runtest = true;
            runtest = false;
            if (runtest)
            {
                ForcingChainHandler hander = new ForcingChainHandler();
                QSudoku qsudoku = new QSudoku("104900853385140000070358000653714298741800536800635417007580300508403000430201085");
                Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
                var cells = hander.Assignment(qsudoku);               

                return;
             
           
            }
            else
            {
                tryFindSudoku(5);
                return;

                var assembly = typeof(SolverHandlerBase).Assembly;
                var types = assembly.GetTypes().Where(t => typeof(ISudokuSolveHelper).IsAssignableFrom(t) && t.IsAbstract == false);
                var notimplemented = 0;
                foreach (var type in types)
                {
                    object[] objs = type.GetCustomAttributes(typeof(AssignmentExampleAttribute), true);
                    if (objs.Count() == 1)
                    {
                        if (objs[0] is AssignmentExampleAttribute a)
                        {
                            try
                            {
                                var cellinfo =
                                    ((ISudokuSolveHelper)Activator.CreateInstance(type, true)).Assignment(
                                        new QSudoku(a.queryString));
                                Debug.WriteLine("解题方法：  " + type.ToString());
                                Debug.WriteLine("测试用例  " + a.queryString);
                                foreach (var item in cellinfo)
                                {
                                    Debug.WriteLine("   " + item);
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("is not implemented"))
                                {
                                    notimplemented += 1;
                                }

                                Debug.WriteLine(type + "   " + ex.Message);


                            }

                        }
                    }


                }

                Debug.WriteLine(" 未实现方法个数为：  " + notimplemented);

                return;
            }



        }

        public static void tryFindSudoku(int count=50)
        {
            List<QSudoku> qSudokus = new List<QSudoku>();
            var solveCount = 0;
            List<Type> types1 = new List<Type>();
            do
            {
                QSudoku example;
                if (qSudokus.Count==0)
                {
                    var queryString = "000020080040009003000005700000000000805070020037004000070080056090000300100040000";              
                    Debug.WriteLine("DanceLinkvalid" + new DanceLink().isValid(queryString));
                    QSudoku correct = new QSudoku(new DanceLink().do_solve(queryString));
                  
                    example = new QSudoku(queryString);
                }
                else
                {
                    example = new MinimalPuzzleFactory().Make(new SudokuBuilder().MakeWholeSudoku());
                }
                var initString = example.QueryString;
                Debug.WriteLine("example init "+ example.QueryString);
                var assembly = typeof(SolverHandlerBase).Assembly;
                var types = assembly.GetTypes().Where(t => typeof(ISudokuSolveHelper).IsAssignableFrom(t) && t.IsAbstract == false);
                var tryagain = false;

                do
                {
                    tryagain = false;
                    foreach (var type in types)
                    {
                        object[] objs = type.GetCustomAttributes(typeof(AssignmentExampleAttribute), true);
                        try
                        {
                            if (!types1.Contains(type))
                            {
                                var cellinfos =
                                    ((ISudokuSolveHelper)Activator.CreateInstance(type, true)).Assignment(
                                      example);

                                if (cellinfos.Count != 0)
                                {
                                    Debug.WriteLine("type" + type);
                                    Debug.WriteLine("cellinfo" + cellinfos.JoinString("\r\n"));
                                    Debug.WriteLine("example before" + example.QueryString + "isvalid"+new DanceLink().isValid(example.QueryString));

                                    example = example.ApplyCells(cellinfos);
                                    Debug.WriteLine("example after " + example.QueryString + "isvalid" + new DanceLink().isValid(example.QueryString));
                                    if (!example.IsAllSeted)
                                    {
                                        tryagain = true;
                                    }
                                    else
                                    {
                                        solveCount += 1;
                                    }
                                    Debug.WriteLine("\r\n");

                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("is not implemented"))
                            {
                                types1.Add(type);
                            }




                        }


                    }
                } while (tryagain);
                if (!example.IsAllSeted)
                {
                    Debug.WriteLine("example SaveTohtml in");
                    SaveTohtml(example.QueryString);
                    SaveToTXT(example.QueryString);
                    SaveToTXT(example.QueryString,initString);
                    qSudokus.Add(example);
                    example.SaveTohtml();
                }
                Debug.WriteLine("solveCount " + solveCount);


            } while (qSudokus.Count<count);
        
            Debug.WriteLine("tryFindSudoku end");

        }

        public static void SaveToTXT(string queryString,string initstring=null,string end=".txt")
        {
            string str2 = "";

            dynamic type = typeof(QSudoku);
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);
            string saveDirectory = Path.Combine(currentDirectory, "UnSolveSudoku");
            saveDirectory = Path.Combine(saveDirectory, DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }
        
            if (string.IsNullOrEmpty(initstring))
            {
                initstring = queryString;

            }
            else
            {
                queryString += "init";
            }
            string filePath2 = Path.Combine(saveDirectory, queryString  + end);
            File.WriteAllText(filePath2, initstring);
            Debug.WriteLine("文件" + filePath2 + " 已生成");


        }

        public static void SaveTohtml(string queryString)
        {
            string str2 = "";
            var names1 = typeof(QSudoku).Assembly.GetManifestResourceNames();
            var name = names1.Where(c => c.Contains("template.html")).First();
            Stream manifestResourceStream = typeof(QSudoku).Assembly.GetManifestResourceStream(name);
            if (manifestResourceStream != null)
            {
                StreamReader reader = new StreamReader(manifestResourceStream);

                str2 = reader.ReadToEnd();
                reader.Close();
                manifestResourceStream.Close();

            }

            dynamic type = typeof(QSudoku);
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);
            string saveDirectory = Path.Combine(currentDirectory, "UnSolveSudoku");
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }
            saveDirectory = Path.Combine(saveDirectory, DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }
            string filePath = Path.Combine(saveDirectory, queryString + ".html");
            File.WriteAllText(filePath, str2.Replace("replaceMark", queryString));
             Debug.WriteLine("文件" + filePath + " 已生成");


        }



    }
}
