using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Core
{
    public class NegativeIndexsGroup : CellInfo
    {


        public NegativeIndexsGroup(List<int> index, int value) : base(index, value)
        {
            this.CellType = CellType.NegativeIndexsGroup;


        }


        public override bool IsError { get; }
        public override List<CellInfo> GetNextCells()
        {
            List<CellInfo> cells = new List<CellInfo>();
            var find = Sudoku.AllUnSetCells.Where(c => !Indexs.Contains(c.Index) && c.RestList.Contains(Value))
                .Select(c => new { c.Row, c.Column, c.Block }).ToList();
            var removeCells = Sudoku.AllUnSetCells.Where(c => Indexs.Contains(c.Index)).ToList();
            var rows = removeCells.Select(c => c.Row).Distinct().ToList();
           
            foreach (var row in rows)
            {
                var filter = Sudoku.AllUnSetCells.Where(c =>
                    c.Row == row && !Indexs.Contains(c.Index) && c.RestList.Contains(Value)).ToList();
                if (filter.Count == 1)
                {
                    PositiveCell positive = new PositiveCell(filter.First().Index, Value)
                    {
                        CellType = CellType.Init,
                        Sudoku = this.Sudoku,
                        Parent = this,
                        Level = this.Level + 1,
                        Fromto = { fromIndex = this.Index },
                    };
                    positive.Fromto.toIndex = positive.Index;
                    cells.Add(positive);

                }
                
            }
            var columns = removeCells.Select(c => c.Column).Distinct().ToList();
            foreach (var column in columns)
            {
                var filter = Sudoku.AllUnSetCells.Where(c =>
                    c.Column == column && !Indexs.Contains(c.Index) && c.RestList.Contains(Value)).ToList();
                if (filter.Count == 1)
                {
                    PositiveCell positive = new PositiveCell(filter.First().Index, Value)
                    {
                        CellType = CellType.Init,
                        Sudoku = this.Sudoku,
                        Parent = this,
                        Level = this.Level + 1,
                        Fromto = { fromIndex = this.Index },
                    };
                    positive.Fromto.toIndex = positive.Index;
                    cells.Add(positive);

                }

            }
            var blocks = removeCells.Select(c => c.Block).Distinct().ToList();
            foreach (var block in blocks)
            {
                var filter = Sudoku.AllUnSetCells.Where(c =>
                    c.Block == block && !Indexs.Contains(c.Index) && c.RestList.Contains(Value)).ToList();
                if (filter.Count == 1)
                {
                    PositiveCell positive = new PositiveCell(filter.First().Index, Value)
                    {
                        CellType = CellType.Init,
                        Sudoku = this.Sudoku,
                        Parent = this,
                        Level = this.Level + 1,
                        Fromto = { fromIndex = this.Index },
                    };
                    positive.Fromto.toIndex = positive.Index;
                    cells.Add(positive);

                }

            }

            return cells;
        }

        public override List<CellInfo> NextCells
        {
            get
            {
                //var list = temp;
                //if (list != null)
                //{
                //    return list;
                //}

                return GetNextCells();
            }
        }

        public override string Desc =>"";
    }
}