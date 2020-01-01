using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sudoku.Core
{
    /// <summary>
    /// 精简题工厂类
    /// </summary>
    public class MinimalPuzzleFactory
    {
        public QSudoku Make(RSudoku rsudoku)
        {
            var queryString = rsudoku.answerString;
            var flag = false;
            do
            {

                flag = IsMinimalPuzzle(queryString);
                if (!flag)
                {
                    queryString= GetSubString(queryString).First(c => new DanceLink().isValid(c));
                }

            } while (!flag);

            return new QSudoku(queryString);
        }


        /// <summary>
        /// step1：对于输入的字符串,需要能生成一个有效数独。
        /// step2：所有子字符串都没有办法构成有效数据
        /// </summary>
        /// <param name="newGens"></param>
        /// <returns></returns>
        public bool IsMinimalPuzzle(string newGens)
        {
            if (new DanceLink().isValid(newGens))
            {
                if(GetSubString(newGens).Any(c=>new DanceLink().isValid(c)))
                {
                    return false;
                }
                return true;
            }
            return false;     
        }

        public static List<string> GetSubString(string str)
        {
            var result = new List<string>();
            var locations = GetCuesLocations(str);
            foreach (var location in locations)
            {
                result.Add(SetZero(str, location));
            }

            return result;
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
    }
}
