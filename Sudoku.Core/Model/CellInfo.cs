using System;
using System.Collections.Generic;
using System.Linq;
namespace Sudoku.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CellInfo : ICellInfo
    {
        public int Row;
        public int Column;
        public int Block;
        private int mIndex;
        public CellType CellType { get; set; }
        public List<DrawChains> drawChains { get; set; } = new List<DrawChains>();
        /// <summary>
        /// 绘制信息
        /// </summary>
        public DrawType drawType { get; set; }
        /// <summary>
        /// 需要绘制的单元格
        /// </summary>
        public List<CellInfo> drawCells = new List<CellInfo>();
        public QSudoku Sudoku;
        public bool IsRoot = false;
        public List<int> mRest = null;
        public List<int> NegativeValues = new List<int>();
        public void ReSetRest()
        {
            this.mRest = null;
        }    
        public abstract string Desc { get; }
        public int RestCount => GetRest().Count;
        public string RestString => GetRest().JoinString();
        public List<int> RestList => GetRest();
        private List<int> GetRest()
        {
            if (mRest == null)
            {
                var relatedCells = Sudoku.AllSetCell.Where(c => c.Value != 0 && (c.Row == Row || c.Column == Column || c.Block == Block));
                var result = G.AllBaseValues.Except(relatedCells.Select(c => c.Value)).ToList();
                result = result.Except(NegativeValues).ToList();
                result.Sort();
                mRest = result;
            }
            return mRest;
        }
        /// <summary>
        /// 推导层级
        /// </summary>
        public int Level { get; set; } = 0;
        public abstract bool IsError { get; }
        /// <summary>
        /// 关联的未设置值的单元格
        /// </summary>
        public List<CellInfo> RelatedUnsetCells
        {
            get
            {
                return this.Sudoku.AllUnSetCells.Where(c => c.Index != this.Index
                                                          && (c.Block == this.Block || c.Row == this.Row || c.Column == this.Column)
                                                          ).ToList();
            }
        }
        /// <summary>
        /// 关联的未设置值的单元格
        /// </summary>
        public List<int> RelatedUnsetIndexs
        {
            get { return RelatedUnsetCells.Select(c => c.Index).ToList(); }
        }
        public CellInfo(int index, int value, QSudoku qSudoku)
        {
            this.Index = index;
            this.Value = value;
            this.Sudoku = qSudoku;
        }
        public CellInfo(List<int> indexs, int value, QSudoku qSudoku)
        {
            this.Indexs = indexs;
            Value = value;
            this.Sudoku = qSudoku;
        }
        protected CellInfo(int index, List<int> values, QSudoku qSudoku)
        {
            this.Index = index;
            this.NegativeValues = values;
            this.Sudoku = qSudoku;
        }
        public void SetSudoku(QSudoku qSudoku)
        {
            this.Sudoku = qSudoku;
        }
        public SolveMessage Location => G.LocationType == LocationType.R1C1? (SolveMessage)Enum.GetValues(typeof(AllR1C1)).Cast<AllR1C1>().ToList()[Index]: Enum.GetValues(typeof(AllA1I9)).Cast<AllA1I9>().ToList()[Index];
        public string RrCc
        {
            get { return "R" + (this.Row + 1) + "C" + (this.Column + 1); }
        }
        public string A1I9
        {
            get { return G.alpha[this.Row]+  (this.Column + 1); }
        }
        public int Index
        {
            get { return mIndex; }
            set
            {
                mIndex = value;
                Row = value / 9;
                Column = value % 9;
                Block = Row / 3 * 3 + Column / 3;
            }
        }
        private List<int> mValues;
        private List<int> mIndexs;
        public List<int> Indexs
        {
            get
            {
                return mIndexs;
            }
            set
            {
                mIndexs = value;
            }
        }
        public CellInfo AnalysisRoot;
        public CellInfo Parent { get; set; }
        public fangxiang Fromto = new fangxiang();
        private List<CellInfo> parentCache;
        public List<CellInfo> GetAllParents()
        {
            if (parentCache != null)
            {
                return parentCache;
            }
            else
            {
                List<CellInfo> refCellInfos = new List<CellInfo>();
                if (Parent == null)
                {
                    parentCache = refCellInfos;
                    return refCellInfos;
                }
                else
                {
                    refCellInfos.AddRange(Parent.GetAllParents());
                    refCellInfos.Insert(0, Parent);
                    parentCache = refCellInfos;
                    return refCellInfos;
                }
            }
        }
        public List<SolveMessage> SolveMessages=new List<SolveMessage>();
        private string mSolveDesc;
        public string SolveDesc
        {
            get
            {
                if (string.IsNullOrEmpty(mSolveDesc))
                {
                    return this.ToString();
                }
                else return mSolveDesc;
            }
            set { mSolveDesc = value; }
        }
        public abstract List<CellInfo> NextCells { get; }
        public override string ToString()
        {
            return "index  " + Index + " " + RrCc + "  value  " + Value + "  类型：" + G.GetEnumDescription(CellType) + "  层级" + Level;
        }
        public Func<CellInfo, bool> UnSetCellInSameColumn()
        {
            return c => c.Value == 0 && c.Index != this.Index && c.Column == this.Column;
        }
        public Func<CellInfo, bool> UnSetCellInSameRow()
        {
            return c => c.Value == 0 && c.Index != this.Index && c.Row == this.Row;
        }
        public Func<CellInfo, bool> UnSetCellInSameBlock()
        {
            return c => c.Value == 0 && c.Index != this.Index && c.Block == this.Block;
        }
        public int Value;
        public override bool Equals(object obj)
        {
            if (obj is CellInfo cell)
            {
                if (cell.Index == Index && cell.Value == Value && cell.CellType == CellType)
                {
                    return true;
                }
            }
            return false; ;
        }
        public abstract List<CellInfo> InitNextCells();
        /// <summary>
        /// 从qsudoku中获取缓存数据
        /// </summary>
        /// <returns></returns>
        public abstract List<CellInfo> GetNextCellsFromSudokuCache();
    }
}