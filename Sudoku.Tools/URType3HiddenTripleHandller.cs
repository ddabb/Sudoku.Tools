﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
    [AssignmentExample(9, "R5C6", "000040900080905600097160000009001200300000007008600100800794352035216089902583000")]
    public class URType3HiddenTripleHandller : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCells = qSudoku.AllUnSetCells;
            var pairCells = (from a in allUnsetCells
                             join b in allUnsetCells on a.RestString equals b.RestString
                             let sameRow = a.Row == b.Row
                             let indexs = G.MergeCellIndexs(a, b)
                             where a.Index < b.Index
                             && a.RestCount == 2 && (a.Row == b.Row || a.Column == b.Column)
                             select new { a, b, sameRow, indexs }).ToList();
            foreach (var item in pairCells)
            {
                var a = item.a;
                var b = item.b;
                var rest = a.RestList;
                var indexs = item.indexs;
                var sameRow = item.sameRow;
                var filterCell = allUnsetCells.Where(c => !indexs.Contains(c.Index)).ToList();
                var filter2 = (from c in filterCell
                               join d in filterCell on 1 equals 1
                               let interc = c.RestList.Intersect(rest).ToList()
                               let interd = d.RestList.Intersect(rest).ToList()
                               let exceptC = c.RestList.Except(rest).ToList()
                               let exceptD = d.RestList.Except(rest).ToList()
                               let indexCD = G.MergeCellIndexs(c, d)
                               where c.Index < d.Index
                               && (sameRow ?
                                 c.Column == a.Column && d.Column == b.Column && c.Row == d.Row
                               : c.Row == a.Row && d.Row == b.Row && c.Column == d.Column)
                                && interc.Count == 2
                               && interd.Count == 2
                               select new { c, d, exceptC, exceptD, indexCD }).ToList();
                foreach (var keyCells in filter2)
                {
                    var c = keyCells.c;
                    var d = keyCells.d;
                    var indexCD = keyCells.indexCD;
                    var exceptC = keyCells.exceptC;
                    var exceptD = keyCells.exceptD;
                    var filterCells = allUnsetCells.Where(cell => sameRow ? cell.Row == c.Row : cell.Column == c.Column).ToList();
                    var otherValues = G.AllBaseValues.Except(rest).ToList();
                    var keyValue1 = rest[0];
                    var keyValue2 = rest[1];
                    var findvalues = (from value1 in otherValues
                                  
                                      select new { value1 }
                                                                      ).ToList();
                    foreach (var values in findvalues)
                    {
                        var keyValue3 = values.value1;
                        var HiddenValues = new List<int> { keyValue1, keyValue2, keyValue3 };
                        var findCell = (filterCells.Where(c => c.RestList.Contains(keyValue1) || c.RestList.Contains(keyValue2) || c.RestList.Contains(keyValue3) )).ToList();
                        if (findCell.Count == 4 && HiddenValues.All(value => filterCells.Count(cell1 => cell1.RestList.Contains(value)) > 0))
                        {
                     
                            var allRemoveValues = new List<int>();
                            var removeCells = findCell.Where(c => !indexCD.Contains(c.Index));
                            var locations = removeCells.Select(c => c.Location).ToList();
                            foreach (var keyCell in removeCells)
                            {
                                var removeValues = keyCell.RestList.Except(HiddenValues).ToList();
                               
                                allRemoveValues.AddRange(removeValues);
                                if (removeValues.Any())
                                {
                                    foreach (var removeValue in removeValues)
                                    {
                                        var nagetiveCell1 = new NegativeCell(keyCell.Index, removeValue, qSudoku);
                                        nagetiveCell1.SolveMessages = new List<SolveMessage>
                                        {"a  ",a.Location,
                                        "b  ",b.Location,
                                        "c  ",c.Location,
                                          "d  ",d.Location,
                                           "隐藏三数组"+HiddenValues.JoinString()+"位置\t\t\r\n"
                                        };
                                        nagetiveCell1.SolveMessages.AddRange(locations);
                                        nagetiveCell1.SolveMessages.Add("\t\t\r\n");
                                        nagetiveCell1.SolveMessages.AddRange(new List<SolveMessage>
                                        {
                                           keyCell.Location,"不能为"+removeValue+"\t\t\r\n"
                                        });
                                        cells.Add(nagetiveCell1);
                                    }
                                    {
                                        var nagetiveCell = new NegativeValuesGroup(keyCell.Index, removeValues, qSudoku) ;
                                        nagetiveCell.SolveMessages = new List<SolveMessage>
                                        {"a  ",a.Location,
                                        "b  ",b.Location,
                                        "c  ",c.Location,
                                          "d  ",d.Location,
                                           "隐藏三数组"+HiddenValues.JoinString()+"位置\t\t\r\n"
                                        };
                                        nagetiveCell.SolveMessages.AddRange(locations);
                                        nagetiveCell.SolveMessages.Add("\t\t\r\n");
                                        nagetiveCell.SolveMessages.AddRange(new List<SolveMessage>
                                        {
                                           keyCell.Location,"不能为"+removeValues.JoinString()+"\t\t\r\n"
                                        });
                                        cells.Add(nagetiveCell);
                                    }
                                }
                            }
                            foreach (var value in G.AllBaseValues)
                            {
                                if (allRemoveValues.Count(c => c == value) > 1)
                                {
                                    var indexs1 = removeCells.Where(c => c.RestList.Contains(value)).Select(c => c.Index).ToList();
                                    var nagetiveCell = new NegativeIndexsGroup(indexs1, value, qSudoku);
                                    nagetiveCell.SolveMessages = new List<SolveMessage>
                                        {"a  ",a.Location,
                                        "b  ",b.Location,
                                        "c  ",c.Location,
                                          "d  ",d.Location,
                                           "隐藏三数组"+HiddenValues.JoinString()+"位置\t\t\r\n"
                                        };
                                    nagetiveCell.SolveMessages.AddRange(locations);
                                    nagetiveCell.SolveMessages.Add("\t\t\r\n");
                                    nagetiveCell.SolveMessages.AddRange(new List<SolveMessage>
                                        {
                                            "位置" ,indexs1.Select(c=>c.LoctionDesc()).JoinString(),"不能为"+value+"\t\t\r\n"
                                        });
                                    cells.Add(nagetiveCell);
                                }
                            }
                        }
                    }
                }
            }
            return cells;
        }
        public override string GetDesc()
        {
            return "若a,b在一个矩形的四个顶点中都存在，且其中存在一行(列)S1满足a,b构成显性数对，另外一行(列)S2上，a,b,c只在四个单元格中存在，则第3,4个单元格不包含a,b,c之外的其余候选数。";
        }
        public override SolveMethodEnum methodType => SolveMethodEnum.URType3HiddenTriple;
        public override MethodClassify methodClassify { get; }
    }
}
