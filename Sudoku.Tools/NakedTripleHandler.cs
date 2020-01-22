﻿using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [AssignmentExample(8,"R3C6","390000700000000650507000349049380506601054983853000400900800134002940865400000297")]
    public class NakedTripleHandler :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.NakedTriple;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCell = qSudoku.AllUnSetCells;
            List<int> possibleCount=new List<int>{2,3};
            //只有2或3个候选数的单元格
            var twoOrThreeRests = allUnSetCell.Where(c=>possibleCount.Contains(c.GetRest().Count)).ToList();
            foreach (var direction in G.AllDirection)
            {
                foreach (var index in G.baseIndexs)
                {
                    //待检查的单元格
                    var checkCells = twoOrThreeRests.Where(G.GetDirectionCells(direction, index)).ToList();
                    if (checkCells.Count() > 2)
                    {
                        var list = (from a in checkCells
                                    join b in checkCells on 1 equals 1
                                    join c in checkCells on 1 equals 1
                                    where new List<CellInfo> { a, b, c }.Select(c => c.Index).Distinct().Count() == 3
                                    select new List<CellInfo> { a, b, c }).ToList();
                        foreach (var item in list)
                        {
                            cells.AddRange(GetCells(qSudoku, item,
                                direction, index));
                        }

                    }
                }

            }
            return cells;
        }


        /// <summary>
        /// 有三种情况
        /// <para>ab ac abc</para> 
        /// <para>abc abc ac</para>
        /// <para>abc abc abc</para>
        /// <para>ab ab abc 其实是隐性数对</para>
        /// </summary>
        /// <param name="qSudoku"></param>
        /// <param name="checkCellInfos"></param>
        /// <param name="direction"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private List<CellInfo> GetCells(QSudoku qSudoku, List<CellInfo> checkCellInfos, Direction direction, int index
            )
        {
            var allUnSetCell = qSudoku.AllUnSetCells;
            List<CellInfo> cells =new List<CellInfo>();
            var exceptIndexs = checkCellInfos.Select(c => c.Index).ToList();
            var allRest = new List<int>();
            foreach (var checkCellInfo in checkCellInfos)
            {
                allRest.AddRange(checkCellInfo.GetRest());
            }


            if (allRest.Distinct().Count() == 3 && allRest.Count >= 7)
            {
                var subCheckCells = allUnSetCell
                    .Where(c => G.GetFilter(c, direction, index) && !exceptIndexs.Contains(c.Index))
                    .ToList();

                foreach (var subCheckCell in subCheckCells)
                {
                    var rests = subCheckCell.GetRest();
                    if (rests.Intersect(allRest).Count() <= 1) continue;
                    foreach (var value in allRest)
                    {
                        rests.Remove(value);
                    }

                    if (rests.Count == 1)
                    {
                        cells.Add(new PositiveCellInfo(subCheckCell.Index, rests[0]));
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
