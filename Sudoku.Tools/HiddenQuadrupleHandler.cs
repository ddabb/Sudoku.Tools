using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(8, "R9C4","900164080070983215813200964080020000500001070001000042040716000000000000007092000")] //已调整
    public class HiddenQuadrupleHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.HiddenQuadruple;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var direction in G.AllDirection)
            {
                foreach (var index in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkCells = qSudoku.AllUnSetCells.Where(G.GetDirectionCells(direction, index)).ToList();

                    if (checkCells.Count > 4)
                    {
                        var allRests = new List<int>();
                        foreach (var cell in checkCells)
                        {
                            allRests.AddRange(cell.RestList);

                        }

                        var restValues = allRests.Distinct().ToList();
                        if (restValues.Count > 4)
                        {
                            var values = (from a in restValues
                                join b in restValues on 1 equals 1
                                join c in restValues on 1 equals 1
                                join d in restValues on 1 equals 1
                                where new List<int> {a, b, c, d}.Select(c => c).Distinct().Count() == 4 && a < b &&
                                      b < c && c < d
                                select new List<int> {a, b, c, d}).ToList();
                            foreach (var eachQuadruple in values)
                            {
                                Dictionary<int, List<int>> a = new Dictionary<int, List<int>>();
                                foreach (var value in eachQuadruple)
                                {
                                    a.Add(value, qSudoku.GetPossibleIndex(value, checkCells));
                                }

                                var allindexs = new List<int>();
                                foreach (var kv in a)
                                {
                                    allindexs.AddRange(kv.Value);
                                }

                                var exceptIndexs = allindexs.Distinct().ToList();
                                if (exceptIndexs.Count() == 4)
                                {

                                    var rows = exceptIndexs.Select(c => new PositiveCell(c, 0)).Select(c => c.Row)
                                        .Distinct().ToList();
                                    var blocks = exceptIndexs.Select(c => new PositiveCell(c, 0))
                                        .Select(c => c.Block).Distinct().ToList();
                                    var columns = exceptIndexs.Select(c => new PositiveCell(c, 0))
                                        .Select(c => c.Column).Distinct().ToList();

                                    var checkValues = G.AllBaseValues.Except(eachQuadruple).ToList();
                                    foreach (var checkValue in checkValues)
                                    {
                                        if (checkValue==8)
                                        {
                                            
                                        }
                                        cells.AddRange((from row in rows
                                            select qSudoku.GetPossibleIndex(checkValue,
                                                c => c.Value == 0 && c.Row == row && !exceptIndexs.Contains(c.Index))
                                            into count
                                            where count.Count == 1
                                            select new PositiveCell(count.First(), checkValue)).Cast<CellInfo>());
                                        cells.AddRange((from column in columns
                                            select qSudoku.GetPossibleIndex(checkValue,
                                                c => c.Value == 0 && c.Column == column &&
                                                     !exceptIndexs.Contains(c.Index))
                                            into count
                                            where count.Count == 1
                                            select new PositiveCell(count.First(), checkValue)).Cast<CellInfo>());
                                        cells.AddRange((from block in blocks
                                            select qSudoku.GetPossibleIndex(checkValue,
                                                c => c.Value == 0 && c.Block == block &&
                                                     !exceptIndexs.Contains(c.Index))
                                            into count
                                            where count.Count == 1
                                            select new PositiveCell(count.First(), checkValue)).Cast<CellInfo>());
                                    }
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

            return @"若在某行/列/宫中，候选者a,b,c,d只在单元格A,B,C,D中出现,则A,B,C,D只能填入a,b,c,d;可以删除A,B,C,D单元格中a,b,c,d以外的候选数。";
        }
    }
}
