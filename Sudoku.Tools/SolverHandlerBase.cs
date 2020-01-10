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
    public abstract class SolverHandlerBase : ISudokuSolveHandler

    {
        public SolverHandlerBase()
        { }
        public abstract SolveMethodEnum methodType { get; }

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
            return GetIntersectCells(cells, new PositiveCellInfo(index1, 0), new PositiveCellInfo(index2, 0));
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


        /// <summary>
        /// 获取两个单元格的共同影响区域
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <returns></returns>
        public List<int> GetIntersectCellIndexs(List<CellInfo> cells, CellInfo cell1, CellInfo cell2)
        {
            return cells.Where(c => (c.Row == cell1.Row && c.Block == cell2.Block
                                 || (c.Row == cell2.Row && c.Block == cell1.Block)  //row block

                                 || (c.Column == cell1.Column && c.Block == cell2.Block)
                                 || (c.Column == cell2.Column && c.Block == cell1.Block) //column block

                                 || (c.Column == cell1.Column && c.Row == cell2.Row)
                                 || (c.Column == cell2.Column && c.Row == cell1.Row)    //column row         
            ) && c.Index != cell1.Index && c.Index != cell2.Index
            ).Select(c=>c.Index).ToList();
        }





        public bool IsSameRowOrSameColumn(int index1, int index2)
        {
            return IsSameRow(index1,index2) || IsSameColumn(index1, index2);
        }

        public bool IsSameBlock(int index1, int index2)
        {
            if (new PositiveCellInfo(index1, 0).Block == new PositiveCellInfo(index2, 0).Block)
            {
                return true;
            }
            return false;
        }

        public bool IsSameRow(int index1, int index2)
        {
            if (new PositiveCellInfo(index1, 0).Row == new PositiveCellInfo(index2, 0).Row)
            {
                return true;
            }
            return false;
        }

        public bool IsSameColumn(int index1, int index2)
        {
            if (new PositiveCellInfo(index1, 0).Column == new PositiveCellInfo(index2, 0).Column)
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
        public List<PossibleIndex> GetAllPossibleIndexInRowOrColumn(QSudoku qSudoku, int times)
        {
            List<PossibleIndex> allPossibleindex = new List<PossibleIndex>();
            allPossibleindex.AddRange(GetAllPossibleIndexInRow(qSudoku,times));
            allPossibleindex.AddRange(GetAllPossibleIndexInColumn(qSudoku, times));
            return allPossibleindex;
        }


        /// <summary>
        /// 获取所有不在A位置就在B位置的候选数
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public List<PossibleIndex> GetAllPossibleIndexInBlock(QSudoku qSudoku, int times)
        {
            return GetAllPossibleIndex(qSudoku, times, c => c == Direction.Block);
        }

        /// <summary>
        /// 获取所有不在A位置就在B位置的候选数
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public List<PossibleIndex> GetAllPossibleIndexInColumn(QSudoku qSudoku, int times)
        {

            return GetAllPossibleIndex(qSudoku, times, c => c == Direction.Column);
        }

        /// <summary>
        /// 获取所有不在A位置就在B位置的候选数
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public List<PossibleIndex> GetAllPossibleIndex(QSudoku qSudoku, int times)
        {

            return GetAllPossibleIndex(qSudoku, times, c => true);
        }

        private List<PossibleIndex> GetAllPossibleIndex(QSudoku qSudoku, int times, Func<Direction, bool> predicate)
        {
            List<PossibleIndex> allPossibleindex = new List<PossibleIndex>();
            foreach (var direction in G.AllDirection.Where(predicate))
            {
                foreach (var directionIndex in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkDirectionCells = qSudoku.AllUnSetCell.Where(G.GetDirectionCells(direction, directionIndex)).ToList();

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

        /// <summary>
        /// 获取所有不在A位置就在B位置的候选数
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public List<PossibleIndex> GetAllPossibleIndexInRow(QSudoku qSudoku, int times)
        {
            return GetAllPossibleIndex(qSudoku, times, c => c == Direction.Row);
        }

        public List<CellInfo> GetNakedSingleCell(QSudoku qSudoku, int speacilValue, List<CellInfo> PositiveCellsInColumn)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var indexs = qSudoku.GetPossibleIndex(speacilValue, c => PositiveCellsInColumn.Select(x => x.Index).Contains(c.Index));
            if (indexs.Count() == 1)
            {
                cells.Add(new PositiveCellInfo(indexs.First(), speacilValue));
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
            Func<CellInfo, bool> where = c => G.GetFilter(c, direction, directionIndex) && c.Value == 0 && !exceptIndex.Contains(c.Index);
            Func<CellInfo, bool> directionwhere = c => G.GetFilter(c, direction, directionIndex) && c.Value == 0;
            var cellList = qSudoku.GetFilterCell(where);
            List<int> allrest = new List<int>();
            foreach (var cell in cellList)
            {
                allrest.AddRange(cell.GetRest());
            }
            allrest = allrest.Distinct().ToList();
            foreach (var speacialValue in allrest)
            {
                var leftIndexs = qSudoku.GetPossibleIndex( speacialValue, directionwhere);
                var leftIndexs1 = leftIndexs.Except(exceptIndex);
                if (leftIndexs.Count > 1 && leftIndexs1.Count() == 1)
                {
                    //Debug.WriteLine("speacialValue" + speacialValue + "location  " + string.Join(",", leftIndexs1));
                    cells.Add(new PositiveCellInfo(leftIndexs1.First(), speacialValue));
                }

            }


            return cells;
        }



         public List<CellInfo> GetHiddenSingleCellInfo(QSudoku qSudoku, Func<CellInfo, bool> predicate)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var rests = qSudoku.GetFilterCell(predicate);

            foreach (var spricevalue in G.AllBaseValues)
            {
                if (rests.Count(c=>c.GetRest().Contains(spricevalue))==1)
                {
                  var cell=    rests.Where(c => c.GetRest().Contains(spricevalue)).First();
                    if (cell.GetRest().Count>1)
                    {
                   
                        cells.Add(new PositiveCellInfo(cell.Index, spricevalue));
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


}
