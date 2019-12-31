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
            var rests = qSoduku.GetFilterCell(predicate).ToList();
            var columnBlockDtos = rests.Select(c => new {c.Column, c.Block}).Distinct().ToList();
            List<ColumnBlockDto> allDtos = new List<ColumnBlockDto>();
            foreach (var dto in columnBlockDtos)
            {
                ColumnBlockDto temp = new ColumnBlockDto {Block = dto.Block, Column = dto.Column};
                var filter = rests.Where(c => c.Block == dto.Block && c.Column == dto.Column);
                foreach (var filterItem in filter)
                {
                    temp.AllRests.AddRange(qSoduku.GetRest(filterItem.index));
                }

                temp.AllRests = temp.AllRests.Distinct().ToList();
                allDtos.Add(temp);
            }



            var columns = allDtos.Select(c => c.Column).Distinct().ToList();
            Dictionary<int, List<int>> columnMap = new Dictionary<int, List<int>>();
            foreach (var columnIndex in columns)
            {
                List<int> allRestInt = new List<int>();
                var eachRests = allDtos.Where(c => c.Column == columnIndex).ToList();
                foreach (var eachRest in eachRests)
                {
                    allRestInt.AddRange(eachRest.AllRests);
                }

                columnMap.Add(columnIndex, allRestInt);

            }

            List<ColumnBlockSingle> list = new List<ColumnBlockSingle>();

            foreach (var kv in columnMap)
            {
                foreach (var groupItem in kv.Value.GroupBy(c => c).ToList())
                {
                    if (groupItem.Count() == 1)
                    {
                        list.Add(new ColumnBlockSingle
                        {
                            Column = kv.Key, Value = groupItem.Key,
                            Block = allDtos.First(c => c.Column == kv.Key && c.AllRests.Contains(groupItem.Key)).Block
                        });

                    }
                }
            }
            //同宮不同列的未知单元格是否仅有两个候选数
            foreach (var item in list)
            {
                foreach (var item1 in rests.Where(c => c.Block == item.Block && c.Column != item.Column))
                {
                    var cellrest = qSoduku.GetRest(item1.index);
                    if (cellrest.Count == 2 && cellrest.Contains(item.Value))
                    {
                        item1.Value = cellrest.First(c => c != item.Value);
                        cells.Add(item1);
                    }
                }
            }
            return cells;
        }

        public class ColumnBlockDto
        {
            public int Column;
            public int Block;
            public List<int> AllRests = new List<int>();
        }

        public class ColumnBlockSingle
        {
            public int Column;
            public int Block;
            public int Value;
        }
    }
}
