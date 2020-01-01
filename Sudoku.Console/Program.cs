using Autofac;
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sudoku.Console
{
    class StaticTools
    {
        static void Main(string[] args)
        {

            tryFindSudoku(50);
            return ;
            var assembly = typeof(SolverHandlerBase).Assembly;
            var types = assembly.GetTypes().Where(t => typeof(ISudokuSolveHelper).IsAssignableFrom(t) && t.IsAbstract == false);
            var notimplemented = 0;
            foreach (var type in types)
            {
                object[] objs = type.GetCustomAttributes(typeof(ExampleAttribute), true);
                if (objs.Count() == 1)
                {
                    if (objs[0] is ExampleAttribute a)
                    {
                        try
                        {
                            var cellinfo =
                                ((ISudokuSolveHelper)Activator.CreateInstance(type, true)).Excute(
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

        public static void tryFindSudoku(int count=50)
        {
            var queryString = "000020080040009003000005700000000000805070020037004000070080056090000300100040000";
            Debug.WriteLine("DanceLinkvalid" + new DanceLink().isValid(queryString));
            QSudoku correct = new QSudoku(new DanceLink().do_solve(queryString));
            correct.SaveTohtml();
            QSudoku example = new QSudoku(queryString);

            var assembly = typeof(SolverHandlerBase).Assembly;
            var types = assembly.GetTypes().Where(t => typeof(ISudokuSolveHelper).IsAssignableFrom(t) && t.IsAbstract == false);
            var tryagain = false;
            List<Type> types1 = new List<Type>();
            do
            {
                tryagain = false;
                foreach (var type in types)
                {
                    object[] objs = type.GetCustomAttributes(typeof(ExampleAttribute), true);
                    try
                    {
                        if (!types1.Contains(type))
                        {
                            var cellinfos =
                                ((ISudokuSolveHelper)Activator.CreateInstance(type, true)).Excute(
                                  example);

                            if (cellinfos.Count != 0)
                            {
                                Debug.WriteLine("type" + type+"\r\n");
                                Debug.WriteLine("cellinfo" + string.Join("\r\n", cellinfos));
                                Debug.WriteLine("example before" + example.QueryString);
                           
                                example = example.ApplyCells(cellinfos);
                                Debug.WriteLine("example after " + example.QueryString);
                                if (!example.IsAllSeted)
                                {
                                    tryagain = true;
                                }

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
                Debug.WriteLine("example SaveTohtml in" );
                example.SaveTohtml();
            }

        }



    }
}
