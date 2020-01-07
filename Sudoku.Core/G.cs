using System;
using System.Collections.Generic;
using System.ComponentModel;
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
