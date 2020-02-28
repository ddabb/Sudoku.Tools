using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample(8,"R1C9","400090700007810400080060050800130007000070000170028005068051024513249876042080501")]//已调整
    public class HiddenPairHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.HiddenPair;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            var indexscount = 2;
            var restCount = 2;
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var direaction in G.AllDirection)
            {
                List<PossibleIndex> possbleIndexs = new List<PossibleIndex>();
                foreach (var index in G.baseIndexs)
                {
                    foreach (var speacilValue in G.AllBaseValues)
                    {
                        Func<CellInfo, bool> rowCondition = c => G.GetFilter(c, direaction, index) && c.Value == 0;
                        var indexs = qSudoku.GetPossibleIndex(speacilValue, rowCondition);
                        if (indexs.Count == indexscount)
                        {
                            possbleIndexs.Add(new PossibleIndex(direaction, index, speacilValue, indexs));
                        }
                    }
                }
                var coupleIndexs = possbleIndexs.Where(c => c.direction == direaction).GroupBy(c => c.indexs.JoinString()).Where(c => c.Count() > 1).ToList();
                foreach (var item in coupleIndexs)
                {

                    var indexs = ConvertToInts(item.Key);

                    if (indexs.Exists(c => qSudoku.GetRest(c).Count() > restCount))//和显性数对区分
                    {
                        var pairIndexs = possbleIndexs.Where(c => c.indexs.JoinString() == item.Key).ToList();
                        var indexs1 = new List<int>();
                        var values = new List<int>();
                        foreach (var item1 in pairIndexs)
                        {
                            indexs1.AddRange(item1.indexs);
                            values.Add(item1.SpeacialValue);
                        }
                        indexs1= indexs1.Distinct().ToList();
                        var index1 = indexs1[0];
                        var index2 = indexs1[1];

                        foreach (var index in indexs1)
                        {
                            foreach (var value1 in qSudoku.GetRest(index).Except(values).ToList())
                            {
                                var cell = new NegativeCell(index, value1) {Sudoku = qSudoku};
                                cells.Add(cell);
                            }


                        }

                   
                    }
                }
                Debug.WriteLine("");
            }


            return cells;
        }
    }
}
