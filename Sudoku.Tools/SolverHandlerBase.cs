using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Sudoku.Tools
{
    /// <summary>
    /// 求解数独基础类
    /// </summary>
   public abstract class SolverHandlerBase: ISudokuSolveHelper

    {
        public abstract List<CellInfo> Excute(QSudoku qSoduku);


        public  List<CellInfo> GetNakedSingleCell(QSudoku qSoduku, int speacilValue, List<CellInfo> PositiveCellsInColumn)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var columnCount = 0;
            foreach (var positiveCell in PositiveCellsInColumn)
            {
                columnCount += (qSoduku.GetRest(positiveCell.index).Contains(speacilValue) ? 1 : 0);
            }
            if (columnCount == 1)
            {
                var postiveCell = PositiveCellsInColumn.Where(c => qSoduku.GetRest(c.index).Contains(speacilValue)).First();
          
                postiveCell.Value = speacilValue;
                cells.Add(postiveCell);
            }
            return cells;
        }
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
