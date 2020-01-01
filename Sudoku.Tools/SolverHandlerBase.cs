using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;
using Sudoku.Core;

namespace Sudoku.Tools
{
    /// <summary>
    /// 求解数独基础类
    /// </summary>
    public abstract class SolverHandlerBase : ISudokuSolveHelper

    {
        public abstract List<CellInfo> Excute(QSudoku qSoduku);

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
        public List<CellInfo> GetNakedSingleCell(QSudoku qSoduku, int speacilValue, List<CellInfo> PositiveCellsInColumn)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var indexs = GetPossibleIndex(qSoduku, speacilValue, c => PositiveCellsInColumn.Select(x => x.index).Contains(c.index));
            if (indexs.Count() == 1)
            {
                cells.Add(new CellInfo(indexs.First(), speacilValue));
            }
            
            return cells;
        }

        /// <summary>
        /// 在指定方向上的指定(行,列，宫)，排除掉不能填入的位置，剩余候选数可以填写的位置。
        /// </summary>
        /// <param name="qSoduku"></param>
        /// <param name="exceptIndex"></param>
        /// <param name="direactionIndex"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public List<CellInfo> GetNakedSingleCell(QSudoku qSoduku, List<int> exceptIndex, int direactionIndex, Direction direction)
        {
            List<CellInfo> cells = new List<CellInfo>();
            Func<CellInfo, bool> where = c => GetFilter(c, direction, direactionIndex) && c.Value == 0 && !exceptIndex.Contains(c.index);
            Func<CellInfo, bool> directionwhere = c => GetFilter(c, direction, direactionIndex) && c.Value == 0;
            var cellList = qSoduku.GetFilterCell(where);
            List<int> allrest = new List<int>();
            foreach (var cell in cellList)
            {
                allrest.AddRange(qSoduku.GetRest(cell.index));
            }
            allrest = allrest.Distinct().ToList();
            foreach (var speacialValue in allrest)
            {
                var leftIndexs = GetPossibleIndex(qSoduku, speacialValue, directionwhere);
                var leftIndexs1 = leftIndexs.Except(exceptIndex);
                if (leftIndexs.Count > 1 && leftIndexs1.Count() == 1)
                {
                    //Debug.WriteLine("speacialValue" + speacialValue + "location  " + string.Join(",", leftIndexs1));
                    cells.Add(new CellInfo(leftIndexs1.First(), speacialValue));
                }

            }


            return cells;
        }


        /// <summary>
        /// 返回候选数在过滤条件下筛选出来的可能存在的坐标位置。
        /// </summary>
        /// <param name="qSoduku">数独原题</param>
        /// <param name="speacialValue">候选数</param>
        /// <param name="whereCondition">过滤条件</param>
        /// <returns></returns>
        public List<int> GetPossibleIndex(QSudoku qSoduku, int speacialValue, Func<CellInfo, bool> whereCondition)
        {
            List<int> indexs = new List<int>();

            var cell = qSoduku.GetFilterCell(whereCondition).OrderBy(c => c.index);
            foreach (var item in cell)
            {
                if (qSoduku.GetRest(item.index).Contains(speacialValue))
                {
                    indexs.Add(item.index);
                }
            }
            return indexs;
        }
        public static readonly List<Direction> allDireaction = new List<Direction> { Direction.Row, Direction.Column, Direction.Block };
        public List<CellInfo> GetHiddenSingleCellInfo(QSudoku qSoduku, Func<CellInfo, bool> predicate)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var rests = qSoduku.GetUnSetInfo(predicate);
            List<int> cues = new List<int>();
            foreach (var item in rests)
            {
                if (item.Value.Count > 1)
                {
                    cues.AddRange(item.Value);
                }


            }
            var temp = cues.GroupBy(c => c).Where(c => c.Count() == 1).ToList();
            foreach (var item in temp)
            {
                var cellValue = item.Key;
                foreach (var restCell in rests)
                {
                    if (restCell.Value.Contains(cellValue))
                    {
                        cells.Add(new CellInfo(restCell.Key, cellValue));
                    }
                }


            }
            return cells;
        }
        public bool GetFilter(CellInfo cell, Direction direction, int index)
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



        public static bool IsVaildSudoku(string queryString)
        {
            return new DanceLink().isValid(queryString);
        }

        public static bool IsPearl(string newGens)
        {

            var a = new DanceLink().isValid(newGens);
            if (!a) return false;
            foreach (var subString in GetSubString(newGens))
            {
                if (!new DanceLink().isValid(subString)) continue;
                return false;

            }
            return true;
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



        /// <summary>
        /// 将坐标的拼接字符串解析成坐标链表
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<int> ConvertToIndexs(string index)
        {
            return index.Split(',').Select(c => Convert.ToInt32(c)).ToList();
        }

    }

    public class PossibleIndex
    {
        public Direction direction;
        public int direactionIndex;
        public int SpeacialValue;
        public string IndexsString;
        public List<int> indexs;

        public void SetIndexs(List<int> indexs)
        {
            indexs.Sort();
            this.indexs = indexs;
            IndexsString = string.Join(",", indexs);
        }

              
        public override string ToString()
        {
            return "在" + direactionIndex + "" + SolverHandlerBase.GetEnumDescription(direction) + "值" + SpeacialValue + "可能位置" + IndexsString + "direction value" + direction;
        }



    }


    /// <summary>
    /// 获取枚举类子项描述信息
    /// </summary>
    /// <param name="enumSubitem">枚举类子项</param>        



    public enum Direction
    {
        [Description("行")]
        Row,
        [Description("列")]
        Column,
        [Description("宫")]
        Block
    }


    public class ColumnBlockDto
    {
        public int Column;
        public int Block;
        public List<int> AllRests = new List<int>();
    }

    public class ColumnBlockSingle
    {
        public int Column;
        public int Block;
        public int Value;
    }

    public class RowBlockDto
    {
        public int Row;
        public int Block;
        public List<int> AllRests = new List<int>();
    }

    public class RowBlockSingle
    {
        public int Row;
        public int Block;
        public int Value;
    }
    public interface ISolverHandlerBase

    {
    }
}
