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
            for (int i = 0; i < 10; i++)
            {
                new SudokuBuilder().MakeSudoku();
            }

            var assembly = typeof(SolverHandlerBase).Assembly;
            var types = assembly.GetTypes().Where(t => typeof(ISudokuSolveHelper).IsAssignableFrom(t)&&t.IsAbstract==false);
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
                                ((ISudokuSolveHelper) Activator.CreateInstance(type, true)).Excute(
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
}
