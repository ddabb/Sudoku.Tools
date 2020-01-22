using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Core
{
    /// <summary>
    /// 数独题
    /// </summary>
    public class QSudoku
    {





        private string queryString;
        public List<CellInfo> cachedAnalysisCells = new List<CellInfo>();

        /// <summary>
        /// 
        /// </summary>
        private List<CellInfo> cellInfos { get; set; } = new List<CellInfo>();



        public QSudoku(string queryString)
        {
            this.QueryString = queryString;
            Init();

        }
        /// <summary>
        /// 获取出现了重复times的坐标。
        /// </summary>
        /// <param name="indexCondition">位置的过滤提哦啊金</param>
        /// <param name="times"></param>
        /// <returns></returns>
        public List<int> PossibleIndex(Func<CellInfo, bool> indexCondition, int speacilValue, int times)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var list = GetPossibleIndex(speacilValue, indexCondition);
            if (list.Count == times)
            {
                return cells.Select(c => c.Index).ToList();
            }
            return new List<int>();

        }

        /// <summary>
        /// 返回候选数在过滤条件下筛选出来的可能存在的坐标位置。
        /// </summary>
        /// <param name="qSudoku">数独原题</param>
        /// <param name="speacialValue">候选数</param>
        /// <param name="whereCondition">过滤条件</param>
        /// <returns></returns>
        public List<int> GetPossibleIndex(int speacialValue, Func<CellInfo, bool> whereCondition)
        {
            List<int> indexs = new List<int>();

            var cell = this.GetFilterCell(whereCondition).OrderBy(c => c.Index);
            foreach (var item in cell)
            {
                if (this.GetRest(item.Index).Contains(speacialValue))
                {
                    indexs.Add(item.Index);
                }
            }
            return indexs;
        }

        public List<PossibleIndex> GetPossibleIndexByTimes(int time)
        {
            List<PossibleIndex> possbleIndexs = new List<PossibleIndex>();
            foreach (var direaction in G.AllDirection)
            {

                foreach (var index in G.baseIndexs)
                {
                    foreach (var speacilValue in G.AllBaseValues)
                    {
                        Func<CellInfo, bool> rowCondition = c => G.GetFilter(c, direaction, index) && c.Value == 0;
                        var indexs = GetPossibleIndex(speacilValue, rowCondition);
                        if (indexs.Count == time)
                        {
                            possbleIndexs.Add(new PossibleIndex(direaction, index, speacilValue, indexs));
                        }
                    }
                }
            }

            return possbleIndexs;

        }

        public List<CellInfo> GetPublicUnsetAreas(CellInfo cell1, CellInfo cell2)
        {
            return AllUnSetCells.Where(c => GetPublicUnsetAreaIndexs(cell1, cell2).Contains(c.Index)).ToList();
        }

        public List<CellInfo> GetPublicUnsetAreas(int index1, int index2)
        {
            return AllUnSetCells.Where(c => GetPublicUnsetAreaIndexs(cellInfos.First(x=>x.Index==index1), cellInfos.First(x => x.Index == index2)).Contains(c.Index)).ToList();
        }

        public List<int> GetPublicUnsetAreaIndexs(CellInfo cell1, CellInfo cell2)
        {
            return AllUnSetCells.Where(c => cell1.RelatedUnsetIndexs.Intersect(cell2.RelatedUnsetIndexs).Contains(c.Index))
                   .Select(c => c.Index).ToList();
        }

        public List<int> GetPossibleIndex(int speacialValue, List<CellInfo> cellInfos)
        {
            List<int> indexs = new List<int>();

            var cell = cellInfos.OrderBy(c => c.Index);
            foreach (var item in cell)
            {
                if (this.GetRest(item.Index).Contains(speacialValue))
                {
                    indexs.Add(item.Index);
                }
            }
            return indexs;
        }

        public List<int> GetRest(int Index)
        {
            return this.cellInfos.First(c => c.Index == Index).GetRest();
        }








        /// <summary>
        /// 获取出现了重复times的坐标。
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public List<int> PossibleIndex(Func<CellInfo, bool> predicate, int times)
        {
            List<CellInfo> cells = new List<CellInfo>();

            return cells.Select(c => c.Index).ToList();

        }
        /// <summary>
        /// 记录ID，如果ID相同，表示最终解一致。
        /// </summary>
        public Guid RecordId { get; set; }



        public bool IsAllSeted
        {
            get { return !this.cellInfos.Exists(c => c.Value == 0); }
        }

        public string QueryString { get => queryString; set => queryString = value; }

        public QSudoku ApplyCells(List<CellInfo> cells)
        {
            var chars = QueryString.Select(c => "" + c).ToList();
            foreach (var item in cells)
            {
                chars[item.Index] = "" + item.Value;
            }

            return new QSudoku(String.Join("", chars));
        }


        private List<PossibleIndex> GetAllPossibleIndex(int times, Func<Direction, bool> predicate)
        {
            List<PossibleIndex> allPossibleindex = new List<PossibleIndex>();
            foreach (var direction in G.AllDirection.Where(predicate))
            {
                foreach (var directionIndex in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkDirectionCells = AllUnSetCells.Where(G.GetDirectionCells(direction, directionIndex)).ToList();

                    var temp = (from value in G.AllBaseValues
                                where GetPossibleIndex(value, checkDirectionCells).Count == times
                                select new { direction, directionIndex, value }
                        ).ToList();
                    foreach (var item in temp)
                    {
                        allPossibleindex.Add(new PossibleIndex(direction, directionIndex, item.value, GetPossibleIndex(item.value, checkDirectionCells)));

                    }

                }
            }
            return allPossibleindex;
        }

        /// <summary>
        /// 根据查询字符串，计算出坐标以及值的初始化信息。
        /// </summary>
        public void Init()
        {

            var chars = QueryString.ToCharArray();
            foreach (var location in G.allLocations)
            {
                var cellInit = new PositiveCellInfo(location, Convert.ToInt32("" + chars[location])) { Sudoku = this };
                cellInfos.Add(cellInit);
            }

        }
        public Dictionary<int, List<int>> GetRowSetInfo(int rowIndex)
        {

            return new Dictionary<int, List<int>>();
        }

        public Dictionary<int, List<int>> GetBlockSetInfo(int blockIndex)
        {

            return new Dictionary<int, List<int>>();
        }

        public Dictionary<int, List<int>> GetColumnSetInfo(int columnIndex)
        {

            return new Dictionary<int, List<int>>();
        }

        public Dictionary<int, List<int>> GetRowUnSetInfo(int rowIndex)
        {

            return new Dictionary<int, List<int>>();
        }

        private List<CellInfo> mAllUnSetCell = null;
        public List<CellInfo> AllUnSetCells
        {
            get { return mAllUnSetCell ?? (mAllUnSetCell = cellInfos.Where(c => c.Value == 0).ToList()); }
        }

        public List<CellInfo> AllCell
        {
            get
            {
                return cellInfos.ToList();
            }
        }

        private List<int> mAllChainsIndex;

        public List<int> AllChainsIndex
        {
            get
            {
                if (mAllChainsIndex == null)
                {
                    var checkIndexLists = AllUnSetCells.Where(c => c.GetRest().Count == 2).Select(c => c.Index).ToList();
                    var aOrBIndex = GetPossibleIndexByTimes(2);
                    foreach (var item in aOrBIndex)
                    {
                        checkIndexLists.AddRange(item.indexs);
                    }

                    mAllChainsIndex = checkIndexLists.Distinct().ToList();
                }

                return mAllChainsIndex;
            }
        }

        public List<CellInfo> AllSetCell
        {
            get
            {
                return cellInfos.Where(c => c.Value != 0).ToList();
            }
        }
        /// <summary>
        /// 用于HiddenSingleBlockHandler
        /// </summary>
        /// <param name="blockIndex"></param>
        /// <returns></returns>
        public Dictionary<int, List<int>> GetUnSetInfo(Func<CellInfo, bool> whereCondition)
        {
            var cells = GetFilterCell(whereCondition);
            List<int> setCell = cells.Where(c => c.Value != 0).Select(c => c.Value).ToList();
            List<int> unsetIndexs = cells.Where(c => c.Value == 0).Select(c => c.Index).ToList();
            List<int> unsetCell = G.AllBaseValues.Except(setCell).ToList();
            Dictionary<int, List<int>> rests = new Dictionary<int, List<int>>();
            foreach (var item in unsetIndexs)
            {
                rests.Add(item, GetRest(item));
            }
            return rests;
        }

        public List<CellInfo> GetFilterCell(Func<CellInfo, bool> whereCondition)
        {
            return cellInfos.Where(whereCondition).ToList();
        }




        public Dictionary<int, List<int>> GetColumnUnSetInfo(int columnIndex)
        {

            return new Dictionary<int, List<int>>();
        }

        public override string ToString()
        {
            var allstring = "";
            foreach (var item in cellInfos)
            {
                allstring += item.ToString() + "\r\n";
            }

            return allstring;
        }
    }
}
