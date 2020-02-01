﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sudoku.Core
{
    /// <summary>
    /// 全局类
    /// </summary>
    public static  class G
    {
        /// <summary>
        /// 1到9的候选数
        /// </summary>
        public static readonly List<int> AllBaseValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        /// <summary>
        /// 0到8，坐标从0开始，到8结束，每个方向都一致。
        /// </summary>
        public static readonly List<int> baseIndexs = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        public static readonly List<int> allLocations = new List<int>
        {
            0,1,2,3,4,5,6,7,8,
            9,10,11,12,13,14,15,16,17,
            18,19,20,21,22,23,24,25,26,
            27,28,29,30,31,32,33,34,35,
            36,37,38,39,40,41,42,43,44,
            45,46,47,48,49,50,51,52,53,
            54,55,56,57,58,59,60,61,62,
            63,64,65,66,67,68,69,70,71,
            72,73,74,75,76,77,78,79,80

        };

        public static List<int> GetLocations(List<List<int>> initValues)
        {
            var tempList = new List<int>();

            int location = 0;
            foreach (var lists in initValues)
            {
                foreach (var value in lists)
                {
                    if (value != 0)
                    {
                        tempList.Add(location);
                    }

                    location += 1;
                }
            }

            return tempList;
        }

        public static string SwitchLocation(string str, int a, int b)
        {
            char[] newStr = str.ToCharArray();
            var c1 = newStr[a];
            var c2 = newStr[b];
            newStr[a] = c2;
            newStr[b] = c1;
            return new string(newStr);
        }
        /// <summary>
        /// 获取字符串的所有非0的位置
        /// </summary>
        /// <param name="initValues"></param>
        /// <returns></returns>
        public static List<int> GetLocations(string initValues)
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
        public static List<List<int>> StringToList(string str)
        {
            str = str.Replace("*", "0").Replace(".", "").Replace("\r\n", "").Trim();
            var arr = str.ToCharArray();
            List<List<int>> result = new List<List<int>>()
            {
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0}
            };
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    result[i][j] = Convert.ToInt32("" + arr[i * 9 + j]);
                }
            }


            return result;

        }

        public static string GetEnumDescription(Enum enumSubitem)
        {
            string strValue = enumSubitem.ToString();

            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);
            Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                return strValue;
            }
            else
            {
                DescriptionAttribute da = (DescriptionAttribute)objs[0];
                return da.Description;
            }

        }

        public static Func<CellInfo, bool> GetDirectionCells(Direction direction, int index)
        {
            return c => G.GetFilter(c, direction, index);
        }

        public static readonly List<Direction> AllDirection = new List<Direction> { Direction.Row, Direction.Column, Direction.Block };

        public static List<int> DistinctRow(List<CellInfo> cells)
        {
            return cells.Select(c => c.Row).Distinct().ToList();
        }

        public static List<int> DistinctColumn(List<CellInfo> cells)
        {
            return cells.Select(c => c.Column).Distinct().ToList();
        }


        public static List<int> DinstinctInt(params List<int>[] n)
        {
            List<int> result=new List<int>();
            foreach (var item in n)
            {
                result.AddRange(item);
            }

            result = result.Distinct().ToList();
            return result;
        }

        public static List<int> MergeCellIndexs(params CellInfo[] n)
        {
            List<int> result = new List<int>();
            foreach (var item in n)
            {
                result.Add(item.Index);
            }

            result = result.Distinct().ToList();
            return result;
        }

        public static List<int> MergeInt(params List<int>[] n)
        {
            List<int> result = new List<int>();
            foreach (var item in n)
            {
                result.AddRange(item);
            }
            return result;
        }


        public static List<int> DistinctBlock(List<CellInfo> cells)
        {
            return cells.Select(c => c.Block).Distinct().ToList();
        }
        public static bool GetFilter(CellInfo cell, Direction direction, int index)
        {

            bool r = false;
            switch (direction)
            {
                case Direction.Row:
                    r = cell.Row == index;
                    break;
                case Direction.Column:
                    r = cell.Column == index;
                    break;
                case Direction.Block:
                    r = cell.Block == index;
                    break;
            }
            return r;

        }
    }
}
