﻿using System.Collections.Generic;
namespace Sudoku.Core.Model
{
    public class PositiveCellGroup : CellInfo
    {
        public PositiveCellGroup(List<int> index, int value,QSudoku sudoku) : base(index, value, sudoku)
        {
            this.CellType = CellType.PositiveCellGroup;
            if (Index == 70 && Value == 5)
            {
            }
        }
        public override bool IsError { get; }
        public override List<CellInfo> InitNextCells()
        {
            throw new System.NotImplementedException();
        }
        public override List<CellInfo> GetNextCellsFromSudokuCache()
        {
            throw new System.NotImplementedException();
        }
        public override List<CellInfo> NextCells { get; }
        public override string Desc => "";
    }
}