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

        /// <summary>
        /// 1到9的候选数
        /// </summary>
        public static readonly List<int> baseFillList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        /// <summary>
        /// 0到8，坐标从0开始，到8结束，每个方向都一致。
        /// </summary>
        public static readonly List<int> baseIndexs = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

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
                chars[item.index] = "" + item.Value;
            }

            return new QSudoku(string.Join("", chars));


        }


        /// <summary>
        /// 根据查询字符串，计算出坐标以及值的初始化信息。
        /// </summary>
        public void Init()
        {

            var chars = QueryString.ToCharArray();
            foreach (var location in allLocations)
            {
                cellInfos.Add(new CellInfo(location, Convert.ToInt32("" + chars[location])));
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

        /// <summary>
        /// 用于HiddenSingleBlockHandler
        /// </summary>
        /// <param name="blockIndex"></param>
        /// <returns></returns>
        public Dictionary<int, List<int>> GetUnSetInfo(Func<CellInfo, bool> whereCondition)
        {
            var cells = GetFilterCell(whereCondition);
            List<int> setCell = cells.Where(c => c.Value != 0).Select(c => c.Value).ToList();
            List<int> unsetIndexs = cells.Where(c => c.Value == 0).Select(c => c.index).ToList();
            List<int> unsetCell = baseFillList.Except(setCell).ToList();
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



        public List<int> GetRest(int cellIndex)
        {
            var cell = cellInfos.Where(c => c.index == cellIndex).First();
            var relatedCells = cellInfos.Where(c => c.Value != 0 && (c.Row == cell.Row || c.Column == cell.Column || c.Block == cell.Block));
            return baseFillList.Except(relatedCells.Select(c => c.Value)).ToList();
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
