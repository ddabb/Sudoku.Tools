using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Sudoku.Core;

namespace Sudoku.Core
{
    /// <summary>
    /// 数独题
    /// </summary>
    public class QSudoku
    {


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

   
        private string queryString;

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
            if (list.Count== times)
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
        public List<int> GetPossibleIndex( int speacialValue, Func<CellInfo, bool> whereCondition)
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

        public  void SaveTohtml()
        {
            string str2 = "";
            var names1 = typeof(QSudoku).Assembly.GetManifestResourceNames();
            var name = names1.Where(c => c.Contains("template.html")).First();
            Stream manifestResourceStream = typeof(QSudoku).Assembly.GetManifestResourceStream(name);
            if (manifestResourceStream != null)
            {
                StreamReader reader = new StreamReader(manifestResourceStream);

                str2 = reader.ReadToEnd();
                reader.Close();
                manifestResourceStream.Close();

            }
        
            dynamic type = typeof(QSudoku);
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);
            string saveDirectory = Path.Combine(currentDirectory, "UnSolveSudoku");
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }
            saveDirectory = Path.Combine(saveDirectory, DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }
            string filePath= Path.Combine(saveDirectory, this.QueryString + ".html");
            File.WriteAllText(filePath, str2.Replace("replaceMark", this.QueryString));
            Debug.WriteLine("文件"+filePath+" 已生成");
          

        }


        public bool IsAllSeted
        {
            get { return !this.cellInfos.Exists(c => c.Value == 0); }
        }

        public string QueryString { get => queryString; set => queryString = value; }

        public QSudoku ApplyCells(List<CellInfo> cells)
        {
            var chars = QueryString.Select(c=>""+c).ToList();
            foreach (var item in cells)
            {
                chars[item.Index] = "" + item.Value;
            }

            return new QSudoku(String.Join("", chars));


        }


        private List<PossibleIndex> GetAllPossibleIndex( int times, Func<Direction, bool> predicate)
        {
            List<PossibleIndex> allPossibleindex = new List<PossibleIndex>();
            foreach (var direction in G.AllDirection.Where(predicate))
            {
                foreach (var directionIndex in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkDirectionCells = AllUnSetCell.Where(G.GetDirectionCells(direction, directionIndex)).ToList();

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
            foreach (var location in allLocations)
            {
                cellInfos.Add(new PositiveCellInfo(location, Convert.ToInt32("" + chars[location])));
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

        public List<CellInfo> AllUnSetCell
        {
            get {
                return cellInfos.Where(c => c.Value == 0).ToList();
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

        public List<int> GetRest(CellInfo cellInfo)
        {
            return GetRest(cellInfo.Index);
        }

        public string GetRestString(CellInfo cellInfo)
        {
            return GetRestString(cellInfo.Index);
        }
        public string GetRestString(int cellIndex)
        {
            return String.Join(",", GetRest(cellIndex));
        }

        public List<int> GetRest(int cellIndex)
        {
            var cell = cellInfos.First(c => c.Index == cellIndex);
            var relatedCells = cellInfos.Where(c => c.Value != 0 && (c.Row == cell.Row || c.Column == cell.Column || c.Block == cell.Block));
            var result = G.AllBaseValues.Except(relatedCells.Select(c => c.Value)).ToList();
            result.Sort();
            return result;
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
