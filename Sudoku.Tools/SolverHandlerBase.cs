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
        public abstract MethodClassify methodClassify { get; }

        public abstract string GetDesc();

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
        public abstract List<CellInfo> Elimination(QSudoku qSudoku);

        public bool IsSameRowOrSameColumn(int index1, int index2)
        {
            return IsSameRow(index1,index2) || IsSameColumn(index1, index2);
        }

        public bool IsSameBlock(int index1, int index2)
        {
            if (new PositiveCell(index1, 0, null).Block == new PositiveCell(index2, 0, null).Block)
            {
                return true;
            }
            return false;
        }

        public List<CellInfo> AssignmentCellByEliminationCell(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var eliminationCells = Elimination(qSudoku);
            foreach (var cellInfo in eliminationCells)
            {
                if (cellInfo.NextCells!=null)
                {
                    foreach (var postiveCell in cellInfo.NextCells)
                    {
                        if (!cells.Exists(c=>c.Value==postiveCell.Value&&c.Index==postiveCell.Index))
                        {
                            var parentMessage = cellInfo.SolveMessages.ToList();
                            parentMessage.AddRange(new List<SolveMessage> { "所以", postiveCell.Location, "是", postiveCell.Value });
                            postiveCell.SolveMessages = parentMessage;
                            var drawMessage = cellInfo.drawCells.ToList();
                            drawMessage.Add(postiveCell);
                            postiveCell.drawCells = drawMessage;
                            cells.Add(postiveCell);
                        }
                        
                    }
                }
      
            }

            return cells;
        }
        public bool IsSameRow(int index1, int index2)
        {
            if (new PositiveCell(index1, 0, null).Row == new PositiveCell(index2, 0, null).Row)
            {
                return true;
            }
            return false;
        }

        public bool IsSameColumn(int index1, int index2)
        {
            if (new PositiveCell(index1, 0,null).Column == new PositiveCell(index2, 0, null).Column)
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


        public static List<CellInfo> XRSizeCommonMethod(QSudoku qSudoku, int pairCount)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allunsetcell = qSudoku.AllUnSetCells;
            foreach (var direction in G.AllDirection.Where(c => c != Direction.Block))
            {
                Dictionary<int, int> indexCount = new Dictionary<int, int>();
                foreach (var index in G.baseIndexs)
                {
                    var checkCells = allunsetcell.Where(G.GetDirectionCells(direction, index)).ToList();
                    if (checkCells.Count >= pairCount)
                    {
                        indexCount.Add(index, checkCells.Count);
                    }
                }

                if (indexCount.Count >= 2)
                {
                    var baseCellGroup = indexCount.Where(c => c.Value == pairCount).ToList();
                    var otherCellGroup = indexCount.Where(c => c.Value != pairCount).ToList();
                    foreach (var baseCell in baseCellGroup)
                    {
                        foreach (var otherCell in otherCellGroup)
                        {
                            var baseIndex = baseCell.Key;
                            var otherIndex = otherCell.Key;
                            if (baseIndex==3&&otherIndex==5)
                            {
                                
                            }
                            var ab = (from a in allunsetcell.Where(G.GetDirectionCells(direction, baseIndex))
                                      join b in allunsetcell.Where(G.GetDirectionCells(direction, otherIndex)) on 1 equals 1
                                      where direction == Direction.Column ? a.Row == b.Row : a.Column == b.Column
                                      select new { a, b }).ToList();
                            if (ab.Count == pairCount)
                            {
                                if (ab.All(c => c.a.RestList.Intersect(c.b.RestList).Count() == c.a.RestList.Count))
                                {
                                    var checkList = ab.Where(c => c.b.RestList.Count > c.a.RestList.Count).ToList();
                                    if (checkList.Count == 1)
                                    {
                                        var checkPair = checkList.First();
                                        var cella = checkPair.a;
                                        var cellb = checkPair.b;
                                        cells.Add(new PositiveCell(cellb.Index, cellb.RestList.Except(cella.RestList).First(), qSudoku));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return cells;
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

        public List<SolveMessage> MergeCellSolveLocationMessage(params CellInfo[] cells)
        {
            var  orderList = cells.OrderBy(c => c.Index).ToList();
            return MergeCellSolveMessages(orderList);
        }

        private static List<SolveMessage> MergeCellSolveMessages(List<CellInfo> orderList)
        {
            List<SolveMessage> message = new List<SolveMessage>();
            for (int i = 0; i < orderList.Count; i++)
            {
                message.Add(orderList[i].Location);
                message.Add(",");
            }

            if (message.Count > 0)
            {
                message = message.Take(message.Count - 1).ToList();
            }

            return message;
        }

        /// <summary>
        /// 简化表达N个单元格的坐标
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        public List<SolveMessage> MergeCellSolveLocationMessage(List<CellInfo> cells)
        {
            List<SolveMessage> message = new List<SolveMessage>();
            cells= cells.OrderBy(c=>c.Index).ToList();
            return MergeCellSolveMessages(cells);
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
                    var checkDirectionCells = qSudoku.AllUnSetCells.Where(G.GetDirectionCells(direction, directionIndex)).ToList();

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
                cells.Add(new PositiveCell(indexs.First(), speacilValue, qSudoku));
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
                allrest.AddRange(cell.RestList);
            }
            allrest = allrest.Distinct().ToList();
            foreach (var speacialValue in allrest)
            {
                var leftIndexs = qSudoku.GetPossibleIndex( speacialValue, directionwhere);
                var leftIndexs1 = leftIndexs.Except(exceptIndex).ToList();
                if (leftIndexs.Count > 1 && leftIndexs1.Count() == 1)
                {
                    //Debug.WriteLine("speacialValue" + speacialValue + "location  " + string.Join(",", leftIndexs1));
                    cells.Add(new PositiveCell(leftIndexs1.First(), speacialValue, qSudoku));
                }

            }


            return cells;
        }
        
        public Func<CellInfo, int> FindDirectionCondtion(Direction direction)
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
        /// 将必定范围画绿，否则标红
        /// </summary>
        /// <param name="cellList"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public List<CellInfo> GetDrawPossibleCell(List<CellInfo> cellList, List<int> values)
        {
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var item in cellList)
            {
                foreach (var rest in item.RestList)
                {
                    if (values.Contains(rest))
                    {
                        cells.Add(new PossibleCell(item.Index, rest, item.Sudoku));
                    }
 }

            }

            return cells;
        }

        public List<CellInfo> GetDrawNakedCell(List<CellInfo> cellList, List<int> values)
        {
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var item in cellList)
            {
                foreach (var rest in item.RestList)
                {
                    if (values.Contains(rest))
                    {
                        cells.Add(new PossibleCell(item.Index, rest, item.Sudoku));
                    }
                    else
                    {
                        cells.Add(new NegativeCell(item.Index, rest, item.Sudoku));
                    }
                }

            }

            return cells;
        }

        /// <summary>
        /// 将必定范围画红
        /// </summary>
        /// <param name="cellList"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public List<CellInfo> GetDrawNegativeCell(List<CellInfo> cellList, List<int> values)
        {
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var item in cellList)
            {
                foreach (var rest in item.RestList)
                {
                    if (values.Contains(rest))
                    {
                        cells.Add(new NegativeCell(item.Index, rest, null));
                    }
                }

            }

            return cells;
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

        public SolveMessage GetDirectionMessage(Direction d, int dirIndex)
        {
            return new SolveMessage(G.GetEnumDescription(d) + (dirIndex + 1), MessageType.Location);
        }

    }


}
