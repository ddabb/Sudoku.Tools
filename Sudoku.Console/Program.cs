using Autofac;
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sudoku.Console
{
    public class StaticTools
    {
        static void Main(string[] args)
        {
            var runtest = true;
            //runtest = false;
            if (runtest)
            {
                ULSize8Handler hander = new ULSize8Handler();
                QSudoku qsudoku = new QSudoku("914526300620081459508940162209008510486159723150200890361095240045002901092010635");
                Debug.WriteLine(new DanceLink().do_solve(qsudoku.QueryString));
                var cells = hander.Assignment(qsudoku);
                return;

            }
            else
            {
                tryFindSudoku(5);
                return;
            }



        }

        public static List<Type> types1 = new List<Type>();

        public static void tryFindSudoku(int count = 50)
        {

            var solveCount = 0;
            var unsolveCount = 0;



            do
            {
                QSudoku example;
                if (unsolveCount == 0)
                {
                    var queryString = "002019008140005300000000000010050000507020080000008006800003002003041060000980000";
                    Debug.WriteLine("DanceLinkvalid" + new DanceLink().isValid(queryString));
                    example = new QSudoku(queryString);
                }
                else
                {
                    example = new MinimalPuzzleFactory().Make(new SudokuBuilder().MakeWholeSudoku());
                }

                Debug.WriteLine("example init " + example.QueryString);

                if (SolveSudoku(example))
                {
                    solveCount += 1;
                    Debug.WriteLine("solved sudoku count " + solveCount);
                }
                else
                {
                    unsolveCount += 1;



                }

            } while (unsolveCount < count);

            Debug.WriteLine("tryFindSudoku end");

        }

        /// <summary>
        /// 求解数独
        /// </summary>
        /// <param name="solveHandlers"></param>
        /// <param name="example"></param>
        /// <returns></returns>
        public static bool SolveSudoku(QSudoku example)
        {
            var builder = new ContainerBuilder();
            Assembly[] assemblies = new Assembly[] { typeof(SolverHandlerBase).Assembly };
            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
            var initString = example.QueryString;
            IContainer container = builder.Build();
            var solveHandlers = container.Resolve<IEnumerable<ISudokuSolveHandler>>().OrderBy(c => (int)c.methodType).ToList();

            for (int i = 0; i < solveHandlers.Count; i++)
            {
                var helps = solveHandlers[i];

                try
                {
                    if (!types1.Contains(helps.GetType()))
                    {
                        var cellinfos = new List<CellInfo>();

                        cellinfos = helps.Assignment(example);
                        if (cellinfos.Count != 0)
                        {

                            Debug.WriteLine("cellinfo" + cellinfos.JoinString("\r\n"));
                            Debug.WriteLine("example before" + example.QueryString + "isvalid" + new DanceLink().isValid(example.QueryString));

                            example = example.ApplyCells(cellinfos);
                            Debug.WriteLine("example after " + example.QueryString + "isvalid" + new DanceLink().isValid(example.QueryString));
                            if (example.IsAllSeted)
                            {
                                return true;
                            }
                            i = -1; //又从唯余法开始算起。
                        }
                    }
                    else
                    {
                        continue;
                    }


                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("is not implemented"))
                    {
                        types1.Add(helps.GetType());
                    }
                }

            }
            if (!example.IsAllSeted)
            {
                Debug.WriteLine("example SaveTohtml in");
                SaveTohtml(example.QueryString);
                SaveToTXT(example.QueryString);
                SaveToTXT(example.QueryString, initString);


            }
            return example.IsAllSeted;

        }

        public static void SaveToTXT(string queryString, string initstring = null, string end = ".txt")
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
            string filePath2 = Path.Combine(saveDirectory, queryString + end);
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
