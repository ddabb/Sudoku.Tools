﻿using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample("000109030190700006300286001581472003900568002600391785700915008210007050050020000")]
    public class WXYZWingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.WXYZWing;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.AllUnSetCell.Where(c => c.GetRest().Count == 4).ToList();

            foreach (var checkcell in checkCells)
            {
                var rests = checkcell.GetRest();
                Dictionary<int, List<CellInfo>> dic = new Dictionary<int, List<CellInfo>>();
                foreach (var item in rests)
                {
                    var PositiveCellInfos = new PositiveCellInfo(checkcell.Index, item) { Sudoku = qSudoku, CellType = CellType.Negative, IsRoot = true }.NextCells;
                    foreach (var positiveCell in PositiveCellInfos)
                    {
                        if (dic.ContainsKey(item))
                        {
                            dic[item].AddRange(positiveCell.NextCells);
                        }
                        else
                        {
                            dic.Add(item, positiveCell.NextCells);
                        }
                    }
                }

                var checkCondition = rests.Where(r => dic.Values.All(c => c.Select(x => x.Value).Contains(r)));
                if (checkCondition.Count() > 0)
                {
                    var value = checkCondition.First();

                    var temp = dic.Values.SelectMany(c => c.Select(x => x)).Where(c => c.Value == value).OrderBy(c => c.Index).ToList();

                    if (temp.Count > 2)
                    {
                        var tempresult = (from w in temp
                                          join x in temp on 1 equals 1
                                          join y in temp on 1 equals 1
                                          join z in qSudoku.AllUnSetCell on 1 equals 1
                                          where
                                           new List<CellInfo> { w, x, y, z, checkcell }.Select(c => c.Index).Distinct().Count() == 5
                                           && w.Index < x.Index
                                           && x.Index < y.Index

                                          && GetIntersectCellIndexs(qSudoku.AllUnSetCell, x, checkcell).Contains(z.Index)
                                          && GetIntersectCellIndexs(qSudoku.AllUnSetCell, y, checkcell).Contains(z.Index)
                                          && GetIntersectCellIndexs(qSudoku.AllUnSetCell, w, checkcell).Contains(z.Index)
                                          select z).ToList();
                        foreach (var results in tempresult)
                        {
                            var rest1 = results.GetRest();
                            if (rest1.Contains(value) && rest1.Count == 2)
                            {
                                var postiveCell = new PositiveCellInfo(results.Index, rest1.First(c => c != value));
                                postiveCell.Sudoku = qSudoku;
                                postiveCell.CellType = CellType.Positive;
                                cells.Add(postiveCell);
                            }
                        }
                    }
                }
            }

            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
