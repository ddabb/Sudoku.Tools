using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(8, "R1C9", "400090700007810400080060050800130007000070000170028005068051024513249876042080501")]//已调整
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
                        indexs1 = indexs1.Distinct().ToList();
                        var index1 = indexs1[0];
                        var index2 = indexs1[1];
                        var value1 = values[0];
                        var value2 = values[1];
                        var d = pairIndexs.First().direction;
                        var dirIndex = pairIndexs.First().directionIndex;

                        foreach (var index in indexs1)
                        {
                            foreach (var value in qSudoku.GetRest(index).Except(values).ToList())
                            {
                                var cell = new NegativeCell(index, value) { Sudoku = qSudoku };
                                cell.SolveMessages = new List<SolveMessage>
                                {
                                    "在", GetDirectionMessage(d, dirIndex),"中",
                                    value1+"和"+value2+"只能填写在",
                                    index1.LoctionDesc(),
                                    "和",index2.LoctionDesc(),
                                    "两个单元格内","\r\n",
                                    index.LoctionDesc(),
                                    "不能填入"+value+"\r\n"

                                };
                                cell.drawCells = new List<CellInfo>
                                {
                                    new PositiveCell(index1,value1),
                                    new PositiveCell(index2,value1),
                                    new PositiveCell(index1,value2),
                                    new PositiveCell(index2,value2),
                                    new NegativeCell(index,value),
                                };
                                cells.Add(cell);
                            }


                        }


                    }
                }
                Debug.WriteLine("");
            }


            return cells;
        }



        public override string GetDesc()
        {
            return @"若在某行/列/宫中，候选者a,b只在单元格A,B中出现,则A,B只能填入a,b,可以删除A,B单元格中a,b以外的候选数。";
        }
    }
}
