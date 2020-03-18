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
           
                //ConsoleAssignmentExample(typeof(NakedSubsetWithLockedCandidatesHandler));
                ConsoleEliminationExample(typeof(SplitWingHandler));




                return;

            }
            else
            {
                tryFindSudoku(5);
                return;
            }



        }

        private static void ConsoleAssignmentExample(Type type)
        {
            object[] objs = type.GetCustomAttributes(typeof(AssignmentExampleAttribute), true);
            if (objs.Count() != 1) return;
            if (!(objs[0] is AssignmentExampleAttribute a)) return;
            var queryString = a.queryString;
            var value = a.value;
            var positionString = a.positionString;
            var handers = a.SolveHandlers;
            ConsoleAssignmentExample(type, queryString, value, positionString,handers);
        }

        private static void ConsoleAssignmentExample(Type type, string queryString, int value, string positionString, SolveMethodEnum[] handlerEnums = null)
        {

            var qsudoku = new QSudoku(queryString);
            if (handlerEnums != null)
            {
                foreach (var handerEnum in handlerEnums)
                {
                    var eliminationHanders = SolveHandlers.First(c => handerEnum == (c.methodType));
                    var removeCells = eliminationHanders.Elimination(qsudoku);
                    qsudoku.RemoveCells(removeCells);
                }
            }
            var cellinfo =
                ((ISudokuSolveHandler)Activator.CreateInstance(type, true)).Assignment(
                    qsudoku);

            Debug.WriteLine("cellinfo " + cellinfo.JoinString());
            qsudoku = qsudoku.ApplyCells(cellinfo);
       
        }

        public static List<ISudokuSolveHandler> SolveHandlers
        {
            get
            {
                Assembly[] assemblies = new Assembly[] { typeof(SolverHandlerBase).Assembly };
                var builder = new ContainerBuilder();
                builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();

                IContainer container = builder.Build();
                List<ISudokuSolveHandler> solveHandlers = container.Resolve<IEnumerable<ISudokuSolveHandler>>().ToList();
                return solveHandlers;
            }
        }

        private static void ConsoleEliminationExample(Type type)
        {
            object[] objs = type.GetCustomAttributes(typeof(EliminationExampleAttribute), true);
            if (objs.Count() != 1) return;
            if (!(objs[0] is EliminationExampleAttribute a)) return;
            var queryString = a.queryString;
            var value = a.value;
            var positionString = a.positionString;
            ConsoleEliminationExample(type, queryString, value, positionString);
        }

        private static void ConsoleEliminationExample(Type type, string queryString, int value, string positionString)
        {
            var qsudoku = new QSudoku(queryString);
            var cellinfo =
                ((ISudokuSolveHandler)Activator.CreateInstance(type, true)).Elimination(
                    qsudoku);

            Debug.WriteLine("cellinfo " + cellinfo.JoinString());
            qsudoku = qsudoku.ApplyCells(cellinfo);
            Debug.WriteLine("isValid " + new DanceLink().isValid(qsudoku.QueryString));
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

        public static List<string> queryStrings = new List<string>
            {

                "000070146000006329006300875000463298603200750000000000100600087062900010800014002"
                ,"001000035430109872000003601000030008900002306350800004100320069243000187000018203"
                ,"000008000072600100180040006000900560000400729709006000010734000347500600000060347"
                ,"500080603263509000480063009130090000005604310604030000346000070050300201000000030"
                ,"063740800407000003090306427749618352300004000000200040970060034034097000015400070"
                ,"000009400200070030040080690000148000500907080000002900005000009900800003326790050"
                ,"004072105000400720000500384020004003600000041401080057340000512100043078008000430"
                ,"080704021201800000003000000902000100805000692010020000050083217008070000107006438"
                ,"100000005008020000403870200000400600765030000004000002340050076007003090000010023"
                ,"000000243300080000706020000107302608200000000009040000500800004001095700000000900"
                ,"000080004107000320000000070000000000064820000900300250750009002003000106000200000"
                ,"000070340003900721007004509000000405000000890604700213029003650301005902586297134" //wxyz liyou
            };
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
                var handler = solveHandlers[i];

                try
                {
                    if (!types1.Contains(handler.GetType()))
                    {
                        var cellinfos = new List<CellInfo>();

                        cellinfos = handler.Assignment(example);
                        if (cellinfos.Count != 0)
                        {
                            Debug.WriteLine("handler  " + handler.GetType().ToString());
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
                        types1.Add(handler.GetType());
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
