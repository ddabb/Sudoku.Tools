using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Core
{
    public class NegativeValuesGroup : CellInfo
    {

        /// <summary>
        /// 指定index的单元格不包含指定的值values
        /// </summary>
        /// <param name="index"></param>
        /// <param name="values"></param>
        public NegativeValuesGroup(int index, List<int> values) : base(index, values)
        {
            this.CellType = CellType.NegativeValuesGroup;
            
        }


        public override bool IsError { get; }
        public override List<CellInfo> GetNextCells()
        {
            List < CellInfo > cells=new List<CellInfo>();
            var restList = Sudoku.GetRest(this.Index).Except(this.NegativeValues).ToList();
            if (restList.Count()==1)
            {
                cells.Add(new PositiveCell(Index, restList.First()));
            }

            return cells;
        }

        public override List<CellInfo> NextCells
        {
            get { return this.GetNextCells(); }
        }

        public override string Desc => throw new NotImplementedException();
    }
}