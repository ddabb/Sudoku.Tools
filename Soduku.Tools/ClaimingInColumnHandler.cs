using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{

    [Example("030150209000360050700490603001273800000519000003684700100000008320040000409001060")]
    public class ClaimingInColumnHandler : ISudokuSolveHelper
    {

        public List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            Func<CellInfo, bool> predicate = c => c.Value == 0;
            var rests = qSoduku.GetFilterCell(predicate).OrderBy(c => c.Column).ThenBy(c => c.Block).ToList();
            foreach (var item in rests)
            {
                Debug.WriteLine(typeof(ClaimingInColumnHandler) + " 位置  " + item.index + "  行号  " + item.Row +
                                "  列号  " + item.Column + " 宫 " + item.Block + " 候选数 " +
                                string.Join(",", qSoduku.GetRest(item.index)));
            }

            var columnBlockDtos = rests.Select(c => new {c.Column, c.Block}).Distinct().ToList();
            List<ColumnBlockDto> allDtos = new List<ColumnBlockDto>();
            foreach (var dto in columnBlockDtos)
            {
                ColumnBlockDto temp = new ColumnBlockDto();
                temp.Block = dto.Block;
                temp.Column = dto.Column;
                var filter = rests.Where(c => c.Block == dto.Block && c.Column == dto.Column);
                foreach (var filterItem in filter)
                {
                    temp.rests.AddRange(qSoduku.GetRest(filterItem.index));
                }
                temp.rests = temp.rests.Distinct().ToList();
                allDtos.Add(temp);
            }

            foreach (var eachDto in allDtos)
            {
                Debug.WriteLine("eachDto  列 " + eachDto.Column + "宮" + eachDto.Block + "候选数" +
                                string.Join(",", eachDto.rests));
            }

            return cells;
        }

        public class ColumnBlockDto
        {
            public int Column;
            public int Block;
            public List<int> rests = new List<int>();
        }
    }
}
