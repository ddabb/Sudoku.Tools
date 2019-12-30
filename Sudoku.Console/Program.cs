using Autofac;
using Soduku.Tools;
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

            var assembly = typeof(ISudokuSolveHelper).Assembly;
            var types = assembly.GetTypes().Where(t => typeof(ISudokuSolveHelper).IsAssignableFrom(t));
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
                            var cellinfo = ((ISudokuSolveHelper)Activator.CreateInstance(type, true)).
                                Excute(new QSudoku(a.queryString));
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
                       
                            Debug.WriteLine(type+"   " +ex.Message);

                        }

                    }
                }


            }
            Debug.WriteLine(" 未实现方法个数为：  " + notimplemented);


            return;
        ｝




        public static bool IsVaildSudoku(string queryString)
        {
            return new DanceLink().isValid(queryString);
        }

        public static bool IsPearl(string newGens)
        {

            var a = new DanceLink().isValid(newGens);
            if (!a) return false;
            foreach (var subString in StaticTools.GetSubString(newGens))
            {
                if (!new DanceLink().isValid(subString)) continue;
                return false;

            }
            return true;
        }
        public static List<string> GetSubString(string str)
        {
            var result = new List<string>();
            var locations = StaticTools.GetCuesLocations(str);
            foreach (var location in locations)
            {
                result.Add(StaticTools.SetZero(str, location));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string SetZero(string str, int location)
        {
            var chars = str.ToCharArray();
            chars[location] = '0';
            return new string(chars);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string SetZero(string str, IEnumerable<int> location)
        {
            var chars = str.ToCharArray();
            foreach (var zeroloction in location)
            {
                chars[zeroloction] = '0';
            }

            return new string(chars);

        }

        /// <summary>
        /// 获取数独字符串的所有非0的位置
        /// </summary>
        /// <param name="initValues"></param>
        /// <returns></returns>
        public static List<int> GetCuesLocations(string initValues)
        {
            var tempList = new List<int>();
            var chars = initValues.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] != '0')
                {
                    tempList.Add(i);
                }

            }
            return tempList;
        }


    }
}
