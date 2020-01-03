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
        /// <summary>
        /// 出数
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <returns></returns>
        public abstract List<CellInfo> Assignment(QSudoku qSudoku);

        /// <summary>
        /// 删数
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <returns></returns>
        public abstract List<NegativeCellInfo> Elimination(QSudoku qSudoku);

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

        /// <summary>
        /// 0到8，坐标从0开始，到8结束，每个方向都一致。
        /// </summary>
        public static readonly List<int> baseIndexs = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        public List<CellInfo> GetNakedSingleCell(QSudoku qSudoku, int speacilValue, List<CellInfo> PositiveCellsInColumn)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var indexs = GetPossibleIndex(qSudoku, speacilValue, c => PositiveCellsInColumn.Select(x => x.Index).Contains(c.Index));
            if (indexs.Count() == 1)
            {
                cells.Add(new CellInfo(indexs.First(), speacilValue));
            }
            
            return cells;
        }

        /// <summary>
        /// 在指定方向上的指定(行,列，宫)，排除掉不能填入的位置，剩余候选数可以填写的位置。
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <param name="exceptIndex"></param>
        /// <param name="direactionIndex"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public List<CellInfo> GetNakedSingleCell(QSudoku qSudoku, List<int> exceptIndex, int direactionIndex, Direction direction)
        {
            List<CellInfo> cells = new List<CellInfo>();
            Func<CellInfo, bool> where = c => GetFilter(c, direction, direactionIndex) && c.Value == 0 && !exceptIndex.Contains(c.Index);
            Func<CellInfo, bool> directionwhere = c => GetFilter(c, direction, direactionIndex) && c.Value == 0;
            var cellList = qSudoku.GetFilterCell(where);
            List<int> allrest = new List<int>();
            foreach (var cell in cellList)
            {
                allrest.AddRange(qSudoku.GetRest(cell.Index));
            }
            allrest = allrest.Distinct().ToList();
            foreach (var speacialValue in allrest)
            {
                var leftIndexs = GetPossibleIndex(qSudoku, speacialValue, directionwhere);
                var leftIndexs1 = leftIndexs.Except(exceptIndex);
                if (leftIndexs.Count > 1 && leftIndexs1.Count() == 1)
                {
                    //Debug.WriteLine("speacialValue" + speacialValue + "location  " + string.Join(",", leftIndexs1));
                    cells.Add(new CellInfo(leftIndexs1.First(), speacialValue));
                }

            }


            return cells;
        }



        public static readonly List<Direction> allDirection = new List<Direction> { Direction.Row, Direction.Column, Direction.Block };
        public List<CellInfo> GetHiddenSingleCellInfo(QSudoku qSudoku, Func<CellInfo, bool> predicate)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var rests = qSudoku.GetFilterCell(predicate);

            foreach (var spricevalue in QSudoku.baseFillList)
            {
                if (rests.Count(c=>qSudoku.GetRest(c.Index).Contains(spricevalue))==1)
                {
                  var cell=    rests.Where(c => qSudoku.GetRest(c.Index).Contains(spricevalue)).First();
                    if (qSudoku.GetRest(cell.Index).Count>1)
                    {
                   
                        cells.Add(new CellInfo(cell.Index, spricevalue));
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
        /// 将','拼接的整数组成的字符串，还原成List<int>类型
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<int> ConvertToInts(string index)
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
