using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Core.Model
{
    public class NegativeValuesGroup : CellInfo
    {

        /// <summary>
        /// 指定index的单元格不包含指定的值values
        /// </summary>
        /// <param name="index"></param>
        /// <param name="values"></param>
        public NegativeValuesGroup(int index, List<int> values, QSudoku sudoku) : base(index, values, sudoku)
        {
            this.CellType = CellType.NegativeValuesGroup;
            
        }


        public override bool IsError { get; }
        public override List<CellInfo> InitNextCells()
        {
            List < CellInfo > cells=new List<CellInfo>();
            var restListPre = Sudoku.GetRest(this.Index);
            if (this.NegativeValues.Intersect(restListPre).Any())
            {
                var restList = restListPre.Except(this.NegativeValues).ToList();
                if (restList.Count() == 1)
                {
                    cells.Add(new PositiveCell(Index, restList.First(), Sudoku));
                }         
            }
            return cells;
        }

        public override List<CellInfo> GetNextCellsFromSudokuCache()
        {
            return new List<CellInfo>();
        }

        public override List<CellInfo> NextCells
        {
            get { return this.InitNextCells(); }
        }

        public override string Desc => throw new NotImplementedException();
    }
}