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
  public  class StaticTools
    {
        static void Main(string[] args)
        {
            var runtest = true;
            runtest = false;
            if (runtest)
            {
                ForcingChainHandler hander = new ForcingChainHandler();
                QSudoku qsudoku = new QSudoku("000200581072001463000060279020006300743928156600300000237000014410702000008004000");
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
            List<QSudoku> qSudokus = new List<QSudoku>();
            var solveCount = 0;




            do
            {
                QSudoku example;
                if (qSudokus.Count == 0)
                {
                    var queryString = "000000243300080000706020000107302608200000000009040000500800004001095700000000900";
                    Debug.WriteLine("DanceLinkvalid" + new DanceLink().isValid(queryString));
                    QSudoku correct = new QSudoku(new DanceLink().do_solve(queryString));

                    example = new QSudoku(queryString);
                }
                else
                {
                    example = new MinimalPuzzleFactory().Make(new SudokuBuilder().MakeWholeSudoku());
                }
                var initString = example.QueryString;
                Debug.WriteLine("example init " + example.QueryString);

                if (SolveSudoku(example))
                {
                    solveCount += 1;
                    Debug.WriteLine("solved sudoku count " + solveCount);
                }
                else
                {
                    Debug.WriteLine("example SaveTohtml in");
                    SaveTohtml(example.QueryString);
                    SaveToTXT(example.QueryString);
                    SaveToTXT(example.QueryString, initString);
                    qSudokus.Add(example);
                    example.SaveTohtml();

                }

            } while (qSudokus.Count < count);

            Debug.WriteLine("tryFindSudoku end");

        }

        /// <summary>
        /// 求解数独
        /// </summary>
        /// <param name="solveHandlers"></param>
        /// <param name="example"></param>
        /// <returns></returns>
        public static bool SolveSudoku( QSudoku example)
        {
            var builder = new ContainerBuilder();
            Assembly[] assemblies = new Assembly[] { typeof(SolverHandlerBase).Assembly };
            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();

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
                            Debug.WriteLine("type" + helps.GetType());
                            Debug.WriteLine("cellinfo" + cellinfos.JoinString("\r\n"));
                            Debug.WriteLine("example before" + example.QueryString + "isvalid" + new DanceLink().isValid(example.QueryString));

                            example = example.ApplyCells(cellinfos);
                            Debug.WriteLine("example after " + example.QueryString + "isvalid" + new DanceLink().isValid(example.QueryString));
                            if (example.IsAllSeted)
                            {
                                return true;
                            }
                            else
                            {
                                i = -1; //又从唯余法开始算起。
                            }
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
