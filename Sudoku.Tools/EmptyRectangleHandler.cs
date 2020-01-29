﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample(9,"R4C9","598643002003759648674128593457200830906307425032405060005904380341872956009530204")]
    public  class EmptyRectangleHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.EmptyRectangle;
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCell = qSudoku.AllUnSetCells;
    
            foreach (var value in G.AllBaseValues)
            {
                var cellInfos = allUnsetCell.Where(c => c.RestList.Contains(value)).ToList();
                foreach (var index in G.baseIndexs)
                {
                    var blockCells = cellInfos.Where(c => c.Block == index).ToList();
                    if (blockCells.Count <= 2) continue;
                    var result = blockCells.Where(c =>
                        blockCells.All(c1 => c1.Column == c.Column || c1.Row == c.Row)).ToList();
                    foreach (var interectCell in result)
                    {
                        var otherBlockCell = cellInfos.Where(c => c.Block != index).ToList();

                        var abc = (from a in otherBlockCell
                            join b in otherBlockCell on 1 equals 1
                            join c in otherBlockCell on 1 equals 1
                            where a.Row == interectCell.Row
                                  && b.Column == interectCell.Column
                                  && c.Row == b.Row
                                  && c.Column == a.Column
                                  && a.Block != b.Block
                            select new {a, b, c}).ToList();
                        foreach (var item in abc)
                        {
                            var aCell = item.a;
                            var bCell = item.b;
                            var cCell = item.c;
                            if (qSudoku.GetPossibleIndex(value, c => c.Value == 0 && c.Row == cCell.Row).Count == 2)
                            {
                                if (aCell.RestCount == 2)
                                {
                                    cells.Add(new PositiveCell(aCell.Index,
                                        aCell.RestList.First(c => c != value)));
                                }
                            }
                            else if (qSudoku.GetPossibleIndex(value, c => c.Value == 0 && c.Column == cCell.Column)
                                         .Count == 2 && bCell.RestCount == 2)
                            {
                                cells.Add(new PositiveCell(bCell.Index, bCell.RestList.First(c => c != value)));
                            }
                        }
                    }
                }
            }
            return cells;
        }
    }
}
