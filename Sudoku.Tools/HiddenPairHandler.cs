using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using Sudoku.Core;

namespace Sudoku.Tools
{
    [AssignmentExample("400090700007810400080060050800130007000070000170028005068051024513249876042080501")]//已调整
    public class HiddenPairHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.HiddenPair;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
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
                        var indexs = qSudoku.GetPossibleIndex( speacilValue, rowCondition);
                        if (indexs.Count == indexscount)
                        {
                            possbleIndexs.Add(new PossibleIndex(direaction,index, speacilValue, indexs) );
                        }
                    }
                }
                var coupleIndexs = possbleIndexs.Where(c => c.direction == direaction).GroupBy(c => c.indexs.JoinString()).Where(c => c.Count() > 1).ToList();
                foreach (var item in coupleIndexs)
                {

                    var indexs = ConvertToInts(item.Key);
                    
                    if (indexs.Exists(c => qSudoku.GetRest(c).Count() > restCount))//和显性数对区分
                    {

                        //Debug.WriteLine("关联数组区块坐标：" + item.Key);
                        var pairIndexs = possbleIndexs.Where(c => c.indexs.JoinString() == item.Key).ToList();
                        //Debug.WriteLine("values  " + string.Join(",", pairIndexs.Select(c => c.SpeacialValue)) + "  " + GetEnumDescription(direaction) + "   locations " + item.Key + "方向索引号" + pairIndexs.First().directionIndex);
                        cells.AddRange(GetNakedSingleCell(qSudoku, indexs, pairIndexs.First().directionIndex, direaction));
                    }
                }
                Debug.WriteLine("");
            }


            return cells;
        }

        public override List<NegativeCellInfo> Elimination(QSudoku qSudoku)
        {
            throw new NotImplementedException();
        }
    }
}
