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
            var rows = qSoduku.GetFilterCell(c => true).ToList().Select(c => c.Row).Distinct();
            var columns = qSoduku.GetFilterCell(c => true).ToList().Select(c => c.Column).Distinct();
            var blocks = qSoduku.GetFilterCell(c => true).ToList().Select(c => c.Block).Distinct();
            List<PossibleIndex> possbleIndexs = new List<PossibleIndex>();
            foreach (var direaction in allDireaction)
            {
                foreach (var index in QSudoku.baseIndexs)
                {
                    foreach (var speacilValue in QSudoku.baseFillList)
                    {
                        Func<CellInfo, bool> rowCondition = c => GetFilter(c, direaction, index) && c.Value == 0;
                        var indexs = GetPossibleIndex(qSoduku, speacilValue, rowCondition);
                        if (indexs.Count == 2)
                        {
                            Debug.WriteLine("候选数" + speacilValue + "在"+ GetEnumDescription(direaction)+""+  + index + "方向的可能位置是" + string.Join(",", indexs));
                            possbleIndexs.Add(new PossibleIndex { IndexsString = string.Join(",", indexs), direactionIndex = index, SpeacialValue = speacilValue });
                        }
                    }
                }
                Debug.WriteLine("");
            }


            return cells;
        }
    }
}
