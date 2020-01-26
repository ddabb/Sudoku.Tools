﻿using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(3,"R8C2","980006375376850140000700860569347218000000537723581496000205780000000950000008620")]
    public class NakedPairHandller :SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.NakedPair;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkCells = qSudoku.GetFilterCell(c => c.Value == 0 && c.RestCount ==2);
            foreach (var direction in G.AllDirection)
            {
                foreach (var index in G.baseIndexs)
                {
                    var subcells = qSudoku.AllUnSetCells.Where(c => G.GetFilter(c, direction, index)).ToList();
                    if (subcells.Count>2)
                    {
                        var temp = checkCells.Where(c => G.GetFilter(c, direction, index)).GroupBy(c => c.RestString).Where(c => c.Count() == 2);
                        foreach (var sub in temp)
                        {
                            var removeCells = subcells.Where(c => c.RestString != sub.Key);
                            var removeValues = ConvertToInts(sub.Key);
                            foreach (var cell in removeCells)
                            {
                                var rests = cell.RestList;
                                if (rests.Count>1&&rests.Intersect(removeValues).Any())
                                {
                                    foreach (var value in removeValues)
                                    {
                                        rests.Remove(value);
                                    }
                                    if (rests.Count==1)
                                    {
                                        cells.Add(new PositiveCellInfo(cell.Index, rests[0]));
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
            throw new NotImplementedException();
        }
    }
}
