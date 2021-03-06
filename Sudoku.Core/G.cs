﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Sudoku.Core.Model;
namespace Sudoku.Core
{
    /// <summary>
    /// 全局类
    /// </summary>
    public static class G
    {
        /// <summary>
        /// 1到9的候选数
        /// </summary>
        public static readonly List<int> AllBaseValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        /// <summary>
        /// 0到8，坐标从0开始，到8结束，每个方向都一致。
        /// </summary>
        public static readonly List<int> baseIndexs = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        public static List<string> alpha = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
        public static List<string> RowListString = new List<string> { "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9" };
        public static List<string> ColumnListString = new List<string> { "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9" };
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
        public static Dictionary<int, List<int>> blockMaps = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 0, 1, 2 } },
            { 2, new List<int> { 3, 4, 5 } },
            { 3, new List<int> { 6, 7, 8 } },
            { 4, new List<int> { 0, 3, 6 } },
            { 5, new List<int> { 1, 4, 7 } },
            { 6, new List<int> { 2, 5, 8 } }
        };
        public static Dictionary<int, List<int>> RowblockMaps = new Dictionary<int, List<int>>
        {
            { 4, new List<int> { 0, 1, 2 } },
            { 5, new List<int> { 3, 4, 5 } },
            { 6, new List<int> { 6, 7, 8 } }
        };
        public static Dictionary<int, List<int>> ColumnblockMaps = new Dictionary<int, List<int>>
        {
            { 4, new List<int> { 0, 3, 6 } },
            { 5, new List<int> { 1, 4, 7 } },
            { 6, new List<int> { 2, 5, 8 } }
        };
        public static LocationType LocationType { get; set; } = LocationType.R1C1;
        /// <summary>
        /// 简化坐标
        /// </summary>
        /// <param name="group1"></param>
        /// <param name="group2"></param>
        /// <returns></returns>
        public static List<LocationGroup> MergeAllloctions(LocationGroup group1, LocationGroup group2)
        {
            List<LocationGroup> groups = new List<LocationGroup>();
            if (CanMerge(group1, group2))
            {
                groups.Add(MergeGroup(group1, group2));
            }
            else
            {
                groups.Add(group1);
                groups.Add(group2);
            }
            return groups;
        }
        /// <summary>
        /// 简化坐标
        /// </summary>
        /// <param name="group1"></param>
        /// <param name="group2"></param>
        /// <returns></returns>
        public static List<LocationGroup> MergeLocationDesc(List<CellInfo> cells)
        {
            if (CanMerge(cells))
            {
                return new List<LocationGroup> { mergeGroups(cells) };
            }
            else
            {
                var groups = cells.Select(c => new LocationGroup(new List<CellInfo> { c }, c.RrCc)).ToList();
                groups = RecursionSimplify(groups);
                return groups;
            }
        }
        public static List<LocationGroup> MergeLocationDesc(params CellInfo[] n)
        {
            var cells = n.ToList();
            return MergeLocationDesc(cells);
        }
        public static Color GetCellColor(CellInfo item)
        {
            Color color = Color.White;
            if (item.CellType == CellType.Positive)
            {
                color = Color.FromArgb(134, 242, 128);
            }
            if (item.CellType == CellType.Possible)
            {
                color = Color.FromArgb(255, 192, 89);
            }
            if (item.CellType == CellType.Chain)
            {
                color = Color.FromArgb(177, 165, 243);
            }
            if (item.CellType == CellType.Negative)
            {
                color = Color.FromArgb(245, 165, 167);
            }
            return color;
        }
        public static List<LocationGroup> MergeLocationDesc(params int[] n)
        {
            var cells = n.Select(c => (CellInfo)new InitCell(c, 0, null)).ToList();
            return MergeLocationDesc(cells);
        }
        private static List<LocationGroup> RecursionSimplify(List<LocationGroup> groups)
        {
            var newGroups = groups.ToList();
            var filter = (from a in newGroups
                          join b in newGroups on 1 equals 1
                          where a.LocationDesc != b.LocationDesc
                                && CanMerge(a.cells, b.cells)
                          select new { a, b, }).ToList();
            if (filter.Count > 0)
            {
                var temp = filter.First();
                var a = temp.a;
                var b = temp.b;
                var left = newGroups.Where(c => c.LocationDesc != a.LocationDesc && c.LocationDesc != b.LocationDesc).ToList();
                var newLocationGroup = MergeGroup(a, b);
                left.Add(newLocationGroup);
                return RecursionSimplify(left);
            }
            else
            {
                return newGroups;
            }
        }
        /// <summary>
        /// 单元格是否合并成一个集合
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        public static bool CanMerge(List<CellInfo> cells1, List<CellInfo> cells2)
        {
            var mergeCells = new List<CellInfo>();
            mergeCells.AddRange(cells1.ToList());
            mergeCells.AddRange(cells2.ToList());
            return CanMerge(mergeCells);
        }
        /// <summary>
        /// 单元格是否合并成一个集合
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        public static bool CanMerge(List<CellInfo> cells)
        {
            var distintRow = cells.Select(c => c.Row).Distinct().ToList();
            var distintColumn = cells.Select(c => c.Column).Distinct().ToList();
            var cellindexs = cells.Select(c => c.Index).ToList();
            var mulIndexs = (from row in distintRow
                             join column in distintColumn on 1 equals 1
                             select row * 9 + column).ToList();
            if (mulIndexs.All(c => cellindexs.Contains(c)) && cellindexs.All(c => mulIndexs.Contains(c)))
            {
                return true;
            }
            return false;
        }
        private static LocationGroup mergeGroups(List<CellInfo> cells)
        {
            var distintRow = cells.Select(c => c.Row).Distinct().ToList();
            var distintColumn = cells.Select(c => c.Column).Distinct().ToList();
            distintRow.Sort();
            distintColumn.Sort();
            var cellindexs = cells.Select(c => c.Index).ToList();
            var mulIndexs = (from row in distintRow
                             join column in distintColumn on 1 equals 1
                             select row * 9 + column).ToList();
            LocationGroup group = null;
            if (mulIndexs.All(c => cellindexs.Contains(c)) && cellindexs.All(c => mulIndexs.Contains(c)))
            {
                var desc = "R" + distintRow.Select(c => c + 1).JoinStringWithEmpty() + "C" + distintColumn.Select(c => c + 1).JoinStringWithEmpty();
                group = new LocationGroup(cells, desc);
            }
            return group;
        }
        private static LocationGroup MergeGroup(LocationGroup group1, LocationGroup group2)
        {
            var cells1 = group1.cells;
            var cells2 = group2.cells;
            var mergeCells = new List<CellInfo>();
            mergeCells.AddRange(cells1.ToList());
            mergeCells.AddRange(cells2.ToList());
            LocationGroup mergedGroup = new LocationGroup(mergeCells, "");
            return mergeGroups(mergeCells);
        }
        /// <summary>
        /// 分析是否可以合并成同一个 坐标集合~
        /// </summary>
        /// <param name="group1"></param>
        /// <param name="group2"></param>
        /// <returns></returns>
        private static bool CanMerge(LocationGroup group1, LocationGroup group2)
        {
            var cells = new List<CellInfo>();
            cells.AddRange(group1.cells);
            cells.AddRange(group2.cells);
            return CanMerge(cells);
        }
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
            str = str.Replace("*", "0").Replace(".", "").Replace("\t\t\r\n", "").Trim();
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
            List<int> result = new List<int>();
            foreach (var item in n)
            {
                result.AddRange(item);
            }
            result = result.Distinct().ToList();
            return result;
        }
        public static List<string> DinstinctCellRestString(params CellInfo[] n)
        {
            List<string> result = new List<string>();
            foreach (var item in n)
            {
                result.Add(item.RestString);
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
        public static List<CellInfo> MergeCells(params CellInfo[] n)
        {
            List<CellInfo> result = new List<CellInfo>();
            foreach (var item in n)
            {
                result.Add(item);
            }
            return result;
        }
        public static List<int> MergeCellRest(params CellInfo[] n)
        {
            List<int> result = new List<int>();
            foreach (var item in n)
            {
                result.AddRange(item.RestList);
            }
            result = result.Distinct().ToList();
            return result;
        }
        public static List<int> MergeCellBlocks(params CellInfo[] n)
        {
            List<int> result = new List<int>();
            foreach (var item in n)
            {
                result.Add(item.Block);
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
        public static List<int> MergeInt(params int[] n)
        {
            List<int> result = new List<int>();
            foreach (var item in n)
            {
                result.Add(item);
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
