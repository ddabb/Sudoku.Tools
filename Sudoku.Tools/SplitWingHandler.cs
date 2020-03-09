using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [EliminationExample(2, "R8C1", "000058793093074185578931462010582934005169000982347651054726019009015006060093500")]
    public   class SplitWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {

            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCells = qSudoku.AllUnSetCells;
            var pairCells = allUnsetCells.Where(c => c.RestCount == 2).ToList();
            foreach (var item in pairCells)
            {
                var rest = item.RestList;
                var value1 = rest[0];//2
                var value2 = rest[1];//7
                var filterCells = allUnsetCells.Where(c => c.RestList.Contains(value1) && c.RestList.Contains(value2) && c.Index != item.Index).ToList();
                if (filterCells.Count>3)
                {
                    var keyCells = (from a in filterCells
                                   join b in filterCells on 1 equals 1
                                   join c in filterCells on 1 equals 1
                                   join d in filterCells on 1 equals 1
                                   let indexs = G.MergeCellIndexs(a, b, c, d).ToList()
                                   let aNextCell = new PositiveCell(a.Index, value2) { Sudoku = qSudoku }.NextCells
                                   let bNextCell = new PositiveCell(b.Index, value1) { Sudoku = qSudoku }.NextCells
                                   let cNextCell = new PositiveCell(c.Index, value2) { Sudoku = qSudoku }.NextCells
                                   let dNextCell = new PositiveCell(d.Index, value1) { Sudoku = qSudoku }.NextCells
                                   where indexs.Count() == 4
                                   && aNextCell.Exists(x => x.Index == item.Index)
                                   && aNextCell.Exists(x => x.Index == c.Index)
                                   && bNextCell.Exists(x => x.Index == item.Index)
                                   && bNextCell.Exists(x => x.Index == d.Index)
                                   && cNextCell.Exists(x => x.Index == a.Index)
                                   && dNextCell.Exists(x => x.Index == b.Index)
                                   && (c.Row==d.Row||c.Block==d.Block||c.Column==d.Column)
                                   && a.Index<b.Index
                                   select new { a, b, c, d }
                      ).ToList();

                    foreach (var keyCell in keyCells)
                    {
                        var a = keyCell.a;
                        var b = keyCell.b;
                        var c = keyCell.c;
                        var d = keyCell.d;
                        var cell1 = new NegativeCell(c.Index, value1) { Sudoku = qSudoku };
                        var cell2 = new NegativeCell(d.Index, value2) { Sudoku = qSudoku };
                        cells.Add(cell1);
                        cells.Add(cell2);
                    }
                }
            }

            return cells;
        }

        public override string GetDesc()
        {
            return "";
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.SplitWing;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
    }
}
