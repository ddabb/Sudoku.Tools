using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{



    [AssignmentExample(5, "R6C1", "006020100238617549100080062003750400871346295004890000012460803480230601365178924", SolveMethodEnum.NakedPair)]
    public class HybridWingHandler : SolverHandlerBase
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
            var keyCells = (from a in pairCells
                            join b in pairCells on 1 equals 1
                            let intersects = a.RestList.Intersect(b.RestList).ToList()
                            let sameRow = a.Row == b.Row
                            where intersects.Count == 1
                            && (a.Row == b.Row || a.Column == b.Column)
                            select new { a, b, intersects, sameRow }).ToList();
            foreach (var item in keyCells)
            {
                var a = item.a;
                var b = item.b;
                var intersects = item.intersects;
                var commonValue = intersects.First();
                var sameRow = item.sameRow;
                var unionValues = a.RestList.Except(intersects).Union(b.RestList.Except(intersects)).ToList();
                var list = allUnsetCells.Where(c => unionValues.All(x => c.RestList.Contains(x)) && (sameRow ? c.Column == a.Column : c.Row == a.Row)).ToList();
                foreach (var keyCell in list)
                {
                    var keyValue = b.RestList.Except(intersects).First();
                    if (new NegativeCell(keyCell.Index, keyValue) { Sudoku = qSudoku }.NextCells.Exists(c => c.Value == keyValue && sameRow ? c.Column == b.Column : c.Row == b.Row))
                    {
                        cells.Add(new NegativeCell(keyCell.Index, unionValues.Except(b.RestList).First()) { Sudoku = qSudoku });
                    }
                }

            }
            return cells;

        }

        public override string GetDesc()
        {
            return "";
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.HybridWing;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
    }
}
