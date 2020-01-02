using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [Example("000300100500401090001028600090800001008017002010040800004085000300102040002634000")]

    public class XWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var direction in allDirection.Where(c => c != Direction.Block))
            {
                List<PossibleIndex> possibleIndexs = new List<PossibleIndex>();
                foreach (var DireactionIndex in baseIndexs)
                {
                    foreach (var speacilValue in QSudoku.baseFillList)
                    {
                        var indexs = GetPossibleIndex(qSudoku, speacilValue, c => GetFilter(c, direction, DireactionIndex) && c.Value == 0);
                        if (indexs.Count() == 2)
                        {
                            PossibleIndex index = new PossibleIndex();
                            index.direactionIndex = DireactionIndex;
                            index.direction = direction;
                            index.SpeacialValue = speacilValue;
                            index.SetIndexs(indexs);
                            possibleIndexs.Add(index);
                        }

                    }
                }
                foreach (var item in QSudoku.baseFillList)
                {
                    var signleRowOrColumnIndexs = possibleIndexs.Where(c => c.SpeacialValue == item);

                    foreach (var possibleIndex1 in signleRowOrColumnIndexs)
                    {
                        foreach (var possibleIndex2 in signleRowOrColumnIndexs)
                        {
                            if (possibleIndex1.indexs[0] < possibleIndex2.indexs[0])//只计算一遍
                            {
                                List<CellInfo> subCells = new List<CellInfo> {
                                    new CellInfo(possibleIndex1.indexs[0], 0),
                                    new CellInfo(possibleIndex1.indexs[1], 0),
                                    new CellInfo(possibleIndex2.indexs[0], 0),
                                    new CellInfo(possibleIndex2.indexs[1], 0) };
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
                }

            }
            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
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
                        var rests = qSudoku.GetRest(item.Index);
                        if (rests.Contains(speacilValue) && rests.Where(x => x != speacilValue).Count() == 1)
                        {
                            cells.Add(new CellInfo(item.Index, rests.Where(x => x != speacilValue).First()));
                        }
                    }
                }
            }

            return cells;
        }
    }
}
