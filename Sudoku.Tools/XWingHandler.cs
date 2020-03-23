using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using Sudoku.Core;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample("000300100500401090001028600090800001008017002010040800004085000300102040002634000")]

    public class XWingHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.XWing;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var direction in G.AllDirection.Where(c => c != Direction.Block))
            {
                List<PossibleIndex> possibleIndexs = new List<PossibleIndex>();
                foreach (var directionIndex in G.baseIndexs)
                {
                    foreach (var speacilValue in G.AllBaseValues)
                    {
                        var indexs = qSudoku.GetPossibleIndex(speacilValue, c => G.GetFilter(c, direction, directionIndex) && c.Value == 0);
                        if (indexs.Count() == 2)
                        {
                            PossibleIndex index = new PossibleIndex(direction, directionIndex, speacilValue, indexs);
        
                            possibleIndexs.Add(index);
                        }

                    }
                }
                foreach (var item in G.AllBaseValues)
                {
                    var signleRowOrColumnIndexs = possibleIndexs.Where(c => c.SpeacialValue == item);

                    var listAll = (from a in signleRowOrColumnIndexs
                                   join b in signleRowOrColumnIndexs on 1 equals 1
                                   where a.indexs[0] < b.indexs[0]
                                   select new List<CellInfo> {
                                    new PositiveCell(a.indexs[0], 0, qSudoku),
                                    new PositiveCell(a.indexs[1], 0, qSudoku),
                                    new PositiveCell(b.indexs[0], 0, qSudoku),
                                    new PositiveCell(b.indexs[1], 0, qSudoku) }).ToList();
                    foreach (var subCells in listAll)
                    {
                        if (subCells.Select(c => c.Block).Distinct().Count() == 4
                   && subCells.Select(c => c.Row).Distinct().Count() == 2
                   && subCells.Select(c => c.Column).Distinct().Count() == 2
                   ) //4个格子位于4个宫，两行 两列
                        {
                            cells.AddRange(XwingWithNakeSingle(qSudoku, subCells, item));
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
            return "";
        }

        internal List<CellInfo> XwingWithNakeSingle(QSudoku qSudoku, List<CellInfo> subCells, int speacilValue)
        {
            List<CellInfo> cells = new List<CellInfo>();
            if (true)
            {
                var distinctrow = subCells.Select(c => c.Row).Distinct().ToList();
                var distinctcolumn = subCells.Select(c => c.Column).Distinct().ToList();
                var FilterCells = qSudoku.GetFilterCell(c => c.Value == 0 && (distinctcolumn.Contains(c.Column) || distinctrow.Contains(c.Row)));

                foreach (var item in FilterCells)
                {
                    if (!subCells.Exists(c => c.Row == item.Row && c.Column == item.Column))
                    {
                        var rests = item.RestList;
                        if (rests.Contains(speacilValue) && rests.Count(x => x != speacilValue) == 1)
                        {
                            cells.Add(new PositiveCell(item.Index, rests.First(x => x != speacilValue), qSudoku));
                        }
                    }
                }
            }

            return cells;
        }
    }
}
