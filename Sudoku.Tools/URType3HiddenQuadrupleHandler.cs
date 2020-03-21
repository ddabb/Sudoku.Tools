using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(9, "R8C3", "193524800850376900007000000000051230001043600035960000000000500500607048006485092")]
    public class URType3HiddenQuadrupleHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            var temp = AssignmentCellByEliminationCell(qSudoku);
            return temp;
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
                select new {a, b, sameRow, indexs}).ToList();
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

                          && (sameRow
                              ? c.Column == a.Column && d.Column == b.Column && c.Row == d.Row
                              : c.Row == a.Row && d.Row == b.Row && c.Column == d.Column)

                          && interc.Count == 2
                          && interd.Count == 2
                    select new {c, d, exceptC, exceptD, indexCD}).ToList();
                foreach (var keyCells in filter2)
                {
                    var c = keyCells.c;
                    var d = keyCells.d;
                    var indexCD = keyCells.indexCD;
                    var exceptC = keyCells.exceptC;
                    var exceptD = keyCells.exceptD;
                    var filterCells = allUnsetCells.Where(cell => sameRow ? cell.Row == c.Row : cell.Column == c.Column)
                        .ToList();
                    var otherValues = G.AllBaseValues.Except(rest).ToList();
                    var keyValue1 = rest[0];
                    var keyValue2 = rest[1];
                    var findvalues = (from value1 in otherValues
                            join value2 in otherValues on 1 equals 1
                            where value1 < value2
                            select new {value1, value2}
                        ).ToList();
                    foreach (var values in findvalues)
                    {
                        var keyValue3 = values.value1;
                        var keyValue4 = values.value2;
                        var HiddenValues = new List<int> {keyValue1, keyValue2, keyValue3, keyValue4};
                        HiddenValues.Sort();
                        var findCell = (filterCells.Where(c =>
                            c.RestList.Contains(keyValue1) || c.RestList.Contains(keyValue2) ||
                            c.RestList.Contains(keyValue3) || c.RestList.Contains(keyValue4))).ToList();
                        if (findCell.Count == 5 && HiddenValues.All(value =>
                                filterCells.Count(cell1 => cell1.RestList.Contains(value)) > 0))
                        {

                            var allRemoveValues = new List<int>();
                            var removeCells = findCell.Where(c => !indexCD.Contains(c.Index)).ToList();
                            var locations = removeCells.Select(c => c.Location).ToList();
                            foreach (var keyCell in removeCells)
                            {
                                var removeValues = keyCell.RestList.Except(HiddenValues).ToList();
                                allRemoveValues.AddRange(removeValues);
                                if (removeValues.Any())
                                {
                                    foreach (var removeValue in removeValues)
                                    {
                                        var nagetiveCell1 = new NegativeCell(keyCell.Index, removeValue)
                                            {Sudoku = qSudoku};
                                        nagetiveCell1.SolveMessages = new List<SolveMessage>
                                        {
                                            G.MergeLocationDesc(a, b, c, d),
                                            "隐性四数组" + HiddenValues.JoinString() + "位置\r\n"
                                        };
                                        nagetiveCell1.SolveMessages.AddRange(locations);
                                        nagetiveCell1.SolveMessages.Add("\r\n");
                                        nagetiveCell1.SolveMessages.AddRange(new List<SolveMessage>
                                        {
                                            keyCell.Location, "不能为" + removeValue + "\r\n"
                                        });
                                        cells.Add(nagetiveCell1);
                                    }

                                    var nagetiveCell = new NegativeValuesGroup(keyCell.Index, removeValues)
                                        {Sudoku = qSudoku};
                                    nagetiveCell.SolveMessages = new List<SolveMessage>
                                    {
                                        G.MergeLocationDesc(a, b, c, d),
                                        "隐性四数组" + HiddenValues.JoinString() + "位置\r\n"
                                    };
                                    nagetiveCell.SolveMessages.AddRange(locations);
                                    nagetiveCell.SolveMessages.Add("\r\n");
                                    nagetiveCell.SolveMessages.AddRange(new List<SolveMessage>
                                    {
                                        keyCell.Location, "不能为" + removeValues.JoinString() + "\r\n"
                                    });
                                    cells.Add(nagetiveCell);

                                }

                            }

                            foreach (var value in G.AllBaseValues)
                            {
                                if (allRemoveValues.Count(c => c == value) > 1)
                                {
                                    var indexs1 = removeCells.Where(c => c.RestList.Contains(value))
                                        .Select(c => c.Index).ToList();
                                    var nagetiveCell = new NegativeIndexsGroup(indexs1, value) {Sudoku = qSudoku};
                                    nagetiveCell.SolveMessages = new List<SolveMessage>
                                    {
                                        G.MergeLocationDesc(a, b, c, d),
                                        "隐性四数组" + HiddenValues.JoinString() + "位置\r\n"
                                    };
                                    nagetiveCell.SolveMessages.AddRange(locations);
                                    nagetiveCell.SolveMessages.Add("\r\n");
                                    nagetiveCell.SolveMessages.AddRange(new List<SolveMessage>
                                    {
                                        "位置", indexs1.Select(c => c.LoctionDesc()).JoinString(), "不能为" + value + "\r\n"
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
            return "";
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.URType3HiddenQuadruple;
        public override MethodClassify methodClassify { get; }
    }
}
