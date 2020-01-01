﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Sudoku.Core;

namespace Sudoku.Tools
{
    /// <summary>
    /// 
    /// 候选数X若在R行仅存在一个宫B中，则该B宫中的非R行排除候选数X。
    /// </summary>
    [Example("200007450537420008419050723000040075170000046640070000004060537700084092000700004")]
    public class ClaimingInRowHandler :SolverHandlerBase
    {

        public override List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            return cells;
            Func<CellInfo, bool> predicate = c => c.Value == 0;
            var rests = qSoduku.GetFilterCell(predicate).ToList();
            var columnBlockDtos = rests.Select(c => new { c.Row, c.Block }).Distinct().ToList();
            List<RowBlockDto> allDtos = new List<RowBlockDto>();
            foreach (var dto in columnBlockDtos)
            {
                RowBlockDto temp = new RowBlockDto { Block = dto.Block, Row = dto.Row };
                var filter = rests.Where(c => c.Block == dto.Block && c.Row == dto.Row);
                foreach (var filterItem in filter)
                {
                    temp.AllRests.AddRange(qSoduku.GetRest(filterItem.index));
                }

                temp.AllRests = temp.AllRests.Distinct().ToList();
                allDtos.Add(temp);
            }



            var rows = allDtos.Select(c => c.Row).Distinct().ToList();
            Dictionary<int, List<int>> rowMap = new Dictionary<int, List<int>>();
            foreach (var row in rows)
            {
                List<int> allRestInt = new List<int>();
                var eachRests = allDtos.Where(c => c.Row == row).ToList();
                foreach (var eachRest in eachRests)
                {
                    allRestInt.AddRange(eachRest.AllRests);
                }

                rowMap.Add(row, allRestInt);

            }

            List<RowBlockSingle> list = new List<RowBlockSingle>();

            foreach (var kv in rowMap)
            {
                foreach (var groupItem in kv.Value.GroupBy(c => c).ToList())
                {
                    if (groupItem.Count() == 1)
                    {
                        list.Add(new RowBlockSingle
                        {
                            Row = kv.Key,
                            Value = groupItem.Key,
                            Block = allDtos.First(c => c.Row == kv.Key && c.AllRests.Contains(groupItem.Key)).Block
                        });

                    }
                }
            }
            //同宮不同行的未知单元格是否仅有两个候选数
            foreach (var item in list)
            {
                var speacilValue = item.Value;
                var negativeCells = rests.Where(c => c.Block == item.Block && c.Row != item.Row).ToList();
                foreach (var item1 in negativeCells)
                {
                    var cellrest = qSoduku.GetRest(item1.index);
                    if (cellrest.Count == 2 && cellrest.Contains(item.Value))
                    {
                        item1.Value = cellrest.First(c => c != item.Value);
                        cells.Add(item1);
                    }
                    var PositiveCellsInRow = rests.Where(c => (c.index != item1.index && c.Block != item1.Block) && (c.Row == item1.Row)).ToList();
                    cells.AddRange(GetNakedSingleCell(qSoduku, speacilValue, PositiveCellsInRow));
                    var PositiveCellsInColumn = rests.Where(c => (c.index != item1.index && c.Block != item1.Block) && (c.Column == item1.Column)).ToList();
                    cells.AddRange(GetNakedSingleCell(qSoduku, speacilValue, PositiveCellsInColumn));
                }

            }
            return cells;
        }



    }
}
