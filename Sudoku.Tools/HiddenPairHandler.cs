using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace Sudoku.Tools
{
    [Example("400090700007810400080060050800130007000070000170028005068051024513249876042080501")]//已调整
    public class HiddenPairHandler : SolverHandlerBase
    {
        public override List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            foreach (var direaction in allDireaction)
            {
                List<PossibleIndex> possbleIndexs = new List<PossibleIndex>();
                foreach (var index in QSudoku.baseIndexs)
                {
                    foreach (var speacilValue in QSudoku.baseFillList)
                    {
                        Func<CellInfo, bool> rowCondition = c => GetFilter(c, direaction, index) && c.Value == 0;
                        var indexs = GetPossibleIndex(qSoduku, speacilValue, rowCondition);
                        if (indexs.Count == 2)
                        {
                            possbleIndexs.Add(new PossibleIndex { IndexsString = string.Join(",", indexs), direactionIndex = index, SpeacialValue = speacilValue });
                        }
                    }
                }
                var coupleIndexs = possbleIndexs.Where(c => c.direction == direaction).GroupBy(c => c.IndexsString).Where(c => c.Count() > 1).ToList();
                foreach (var item in coupleIndexs)
                {

                    var indexs = ConvertToIndexs(item.Key);
                    if (indexs.Exists(c => qSoduku.GetRest(c).Count() > 2))//和显性数对区分
                    {

                        //Debug.WriteLine("关联数组区块坐标：" + item.Key);
                        var pairIndexs = possbleIndexs.Where(c => c.IndexsString == item.Key).ToList();
                        //Debug.WriteLine("values  " + string.Join(",", pairIndexs.Select(c => c.SpeacialValue)) + "  " + GetEnumDescription(direaction) + "   locations " + item.Key + "方向索引号" + pairIndexs.First().direactionIndex);
                        cells.AddRange(GetNakedSingleCell(qSoduku, indexs, pairIndexs.First().direactionIndex, direaction));
                    }
                }
                Debug.WriteLine("");
            }


            return cells;
        }
    }
}
