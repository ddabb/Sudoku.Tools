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


        /// <summary>
        /// 获取两个单元格的共同影响区域
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <returns></returns>
        public List<CellInfo> GetIntersectCells(List<CellInfo> cells, int index1,int index2)
        {
            return GetIntersectCells(cells, new CellInfo(index1, 0), new CellInfo(index2, 0));
        }

        /// <summary>
        /// 获取两个单元格的共同影响区域
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <returns></returns>
        public List<CellInfo> GetIntersectCells(List<CellInfo> cells, CellInfo cell1, CellInfo cell2)
        {
            return cells.Where(c => (c.Row == cell1.Row && c.Block == cell2.Block
                                 || (c.Row == cell2.Row && c.Block == cell1.Block)  //row block
                           
                                 || (c.Column == cell1.Column && c.Block == cell2.Block)
                                 || (c.Column == cell2.Column && c.Block == cell1.Block) //column block

                                 || (c.Column == cell1.Column && c.Row == cell2.Row)
                                 || (c.Column == cell2.Column && c.Row == cell1.Row)    //column row         
            ) && c.Index != cell1.Index && c.Index != cell2.Index
            ).ToList();
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

        public bool IsSameRowOrSameColumn(int index1, int index2)
        {
            return IsSameRow(index1,index2) || IsSameColumn(index1, index2);
        }

        public bool IsSameBlock(int index1, int index2)
        {
            if (new CellInfo(index1, 0).Block == new CellInfo(index2, 0).Block)
            {
                return true;
            }
            return false;
        }

        public bool IsSameRow(int index1, int index2)
        {
            if (new CellInfo(index1, 0).Row == new CellInfo(index2, 0).Row)
            {
                return true;
            }
            return false;
        }

        public bool IsSameColumn(int index1, int index2)
        {
            if (new CellInfo(index1, 0).Column == new CellInfo(index2, 0).Column)
            {
                return true;
            }
            return false;
        }

        public bool IsSameRow(CellInfo index1, CellInfo index2)
        {
            if (index1.Row == index2.Row)
            {
                return true;
            }
            return false;
        }

        public bool IsSameColumn(CellInfo index1, CellInfo index2)
        {
            if (index1.Column == index2.Column)
            {
                return true;
            }
            return false;
        }

        public bool IsSameBlock(CellInfo index1, CellInfo index2)
        {
            if (index1.Block == index2.Block)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取所有不在A位置就在B位置的候选数
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public List<PossibleIndex> GetAllAorBInRowOrColumnIndex(QSudoku qSudoku, int times)
        {
            List<PossibleIndex> allPossibleindex = new List<PossibleIndex>();

            foreach (var direction in allDirection.Where(c=>c!=Direction.Block))
            {
                foreach (var directionIndex in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkDirectionCells = qSudoku.AllUnSetCell.Where(GetDirectionCells(direction, directionIndex)).ToList();

                    var temp = (from value in G.AllBaseValues
                                where qSudoku.GetPossibleIndex(value, checkDirectionCells).Count == times
                                select new { direction, directionIndex, value }
                                ).ToList();
                    foreach (var item in temp)
                    {
                        allPossibleindex.Add(new PossibleIndex(direction, directionIndex, item.value, qSudoku.GetPossibleIndex(item.value, checkDirectionCells)));

                    }

                }
            }
            return allPossibleindex;
        }


        public List<CellInfo> GetNakedSingleCell(QSudoku qSudoku, int speacilValue, List<CellInfo> PositiveCellsInColumn)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var indexs = qSudoku.GetPossibleIndex(speacilValue, c => PositiveCellsInColumn.Select(x => x.Index).Contains(c.Index));
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
        /// <param name="directionIndex"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public List<CellInfo> GetNakedSingleCell(QSudoku qSudoku, List<int> exceptIndex, int directionIndex, Direction direction)
        {
            List<CellInfo> cells = new List<CellInfo>();
            Func<CellInfo, bool> where = c => GetFilter(c, direction, directionIndex) && c.Value == 0 && !exceptIndex.Contains(c.Index);
            Func<CellInfo, bool> directionwhere = c => GetFilter(c, direction, directionIndex) && c.Value == 0;
            var cellList = qSudoku.GetFilterCell(where);
            List<int> allrest = new List<int>();
            foreach (var cell in cellList)
            {
                allrest.AddRange(qSudoku.GetRest(cell.Index));
            }
            allrest = allrest.Distinct().ToList();
            foreach (var speacialValue in allrest)
            {
                var leftIndexs = qSudoku.GetPossibleIndex( speacialValue, directionwhere);
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

            foreach (var spricevalue in G.AllBaseValues)
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



        public Func<CellInfo, int> Selector(Direction direction)
        {
            switch (direction)
            {
                case Direction.Row:
                    return c => c.Row;
                    break;
                case Direction.Column:
                    return c => c.Column;
                    break;
                case Direction.Block:
                    return c => c.Block;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
      
        }

        public Func<CellInfo, bool> GetDirectionCells(Direction direction, int index)
        {
            return c => GetFilter(c, direction, index);
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
        public int directionIndex;
        public int SpeacialValue;

        public List<int> indexs;

        public PossibleIndex (Direction direction,int directionIndex,int SpeacialValue, List<int> indexs)
        {
            this.direction = direction;
            this.directionIndex = directionIndex;
            this.SpeacialValue = SpeacialValue;
            indexs.Sort();
            this.indexs = indexs;
        
        }

              
        public override string ToString()
        {
            return "在" + directionIndex + "" + SolverHandlerBase.GetEnumDescription(direction) + "值" + SpeacialValue + "可能位置" + indexs.JoinString() + "direction value" + direction;
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
