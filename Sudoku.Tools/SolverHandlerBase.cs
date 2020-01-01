using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

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
            var columnCount = 0;
            foreach (var positiveCell in PositiveCellsInColumn)
            {
                columnCount += (qSoduku.GetRest(positiveCell.index).Contains(speacilValue) ? 1 : 0);
            }
            if (columnCount == 1)
            {
                var postiveCell = PositiveCellsInColumn.Where(c => qSoduku.GetRest(c.index).Contains(speacilValue)).First();

                postiveCell.Value = speacilValue;
                cells.Add(postiveCell);
            }
            return cells;
        }

        public List<CellInfo> GetNakedSingleCell(QSudoku qSoduku, List<int> exceptIndex, int direactionIndex, Direction direction)
        {
            List<CellInfo> cells = new List<CellInfo>();
            Func<CellInfo, bool> where = c => GetFilter(c, direction, direactionIndex) && c.Value == 0 && !exceptIndex.Contains(c.index);
            Func<CellInfo, bool> directionwhere = c => GetFilter(c, direction, direactionIndex) && c.Value == 0;

            var cellList = qSoduku.GetFilterCell(where);
            //Debug.WriteLine("exceptIndex" + string.Join(",", exceptIndex));
            //Debug.WriteLine("exceptValues" + string.Join(",", exceptValues));
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
        /// 获取可能的坐标。
        /// </summary>
        /// <param name="qSoduku"></param>
        /// <param name="speacialValue"></param>
        /// <param name="rowIndex"></param>
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
