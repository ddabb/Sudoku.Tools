using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(5, "R1C6", "390000700000000650507000349049380506601054983853000400900800134002940865400000297")] //已调整
    public class NakedQuadrupleHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.NakedQuadruple;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;
            List<int> possibleCount = new List<int> { 2, 3, 4 };
            //只有2或3个候选数的单元格
            var twoOrThreeRests = allUnSetCell.Where(c => possibleCount.Contains(c.RestCount)).ToList();
            foreach (var direction in G.AllDirection)
            {
                foreach (var index in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkCells = allUnSetCell.Where(G.GetDirectionCells(direction, index)).ToList();
                    var abcd = (from a in checkCells
                                join b in checkCells on 1 equals 1
                                join c in checkCells on 1 equals 1
                                join d in checkCells on 1 equals 1
                                let abdcIndexs = G.MergeCellIndexs(a, b, c, d)
                                let values = G.MergeInt(a.RestList, b.RestList, c.RestList, d.RestList)
                                let aRestList = a.RestList
                                let bRestList = b.RestList
                                let cRestList = c.RestList
                                let dRestList = d.RestList
                                where a.Index < b.Index && b.Index < c.Index && c.Index < d.Index
                              && values.Distinct().Count() == 4
                                select new
                                {
                                    a,
                                    b,
                                    c,
                                    d,
                                    abdcIndexs,
                                    values,
                                }).ToList();
                    foreach (var item in abcd)
                    {
                        var exceptIndexs = item.abdcIndexs;
                        var values = item.values;
                        foreach (var cell in checkCells.Where(c => !exceptIndexs.Contains(c.Index)))
                        {

                            if (cell.RestList.Count > 1)
                            {
                                var leftvalue = cell.RestList.Except(values);
                                if (leftvalue.Count() == 1)
                                {
                                    cells.Add(new PositiveCell(cell.Index, leftvalue.First()));
                                }
                            }
                        }
                    }
                }

            }

            return cells;
        }


        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override string GetDesc()
        {
            return @"在某个行/列/宫内,若四个单元格只能填入a、b、c、d四个数,则该行/列/宫的其余单元格不能填入a、b、c、d。";
        }
    }
}
