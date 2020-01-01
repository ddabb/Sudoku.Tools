using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{

    [Example("030150209000360050700490603001273800000519000003684700100000008320040000409001060")]
    public class ClaimingInColumnHandler : SolverHandlerBase
    {

        public override List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            return cells;
            Func<CellInfo, bool> predicate = c => c.Value == 0;
            var rests = qSoduku.GetFilterCell(predicate).ToList();
            var columnBlockDtos = rests.Select(c => new { c.Column, c.Block }).Distinct().ToList();
            List<ColumnBlockDto> allDtos = new List<ColumnBlockDto>();
            foreach (var dto in columnBlockDtos)
            {
                ColumnBlockDto temp = new ColumnBlockDto { Block = dto.Block, Column = dto.Column };
                var filter = rests.Where(c => c.Block == dto.Block && c.Column == dto.Column);
                foreach (var filterItem in filter)
                {
                    temp.AllRests.AddRange(qSoduku.GetRest(filterItem.Index));
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
                            Column = kv.Key,
                            Value = groupItem.Key,
                            Block = allDtos.First(c => c.Column == kv.Key && c.AllRests.Contains(groupItem.Key)).Block
                        });

                    }
                }
            }
            //同宮不同行的未知单元格是否仅有两个候选数
            foreach (var item in list)
            {

                int speacilValue = item.Value;
                var negativeCells = rests.Where(c => c.Block == item.Block && c.Column != item.Column).ToList();
                foreach (var item1 in negativeCells)
                {
                    var cellrest = qSoduku.GetRest(item1.Index);
                    if (cellrest.Count == 2 && cellrest.Contains(speacilValue))
                    {
                        item1.Value = cellrest.First(c => c != speacilValue);
                        cells.Add(item1);
                    }
                    var PositiveCellsInRow = rests.Where(c => (c.Index != item1.Index && c.Block != item1.Block) && (c.Row == item1.Row)).ToList();
                    cells.AddRange(GetNakedSingleCell(qSoduku, speacilValue, PositiveCellsInRow));
                    var PositiveCellsInColumn = rests.Where(c => (c.Index != item1.Index && c.Block != item1.Block) && (c.Column == item1.Column)).ToList();
                    cells.AddRange(GetNakedSingleCell(qSoduku, speacilValue, PositiveCellsInColumn));
                }

            }

            cells = cells.Distinct().ToList();
            return cells;
        }




    }
}
