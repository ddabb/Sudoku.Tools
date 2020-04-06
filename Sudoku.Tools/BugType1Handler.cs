using Sudoku.Core;
using Sudoku.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(1, "R8C9", "578136294004289705002574180459321800381765942726948500067853420845602300203407658")] //正确的
    //[AssignmentExample(1, "R8C9", "540090801392008050010000020034600592620040103159003040085000007071830465463050200")] //不能推出bugtype的例子
    //[AssignmentExample(1, "R8C9", "952001736008576249476923185780092561691705420205160970020609817869217354007000692")] //不能推出bugtype的例子
    public class BugType1Handler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.BugType1;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkcells = qSudoku.AllUnSetCells;
            var setCell = qSudoku.AllSetCell;
            if (checkcells.Count(c => c.RestCount == 3) == 1)
            {
                if (checkcells.Count(c => c.RestCount == 2) == checkcells.Count - 1)
                {

                    var c1 = (from value in G.AllBaseValues
                              let setCount = setCell.Count(c => c.Value == value)
                              let restCount = checkcells.Count(c => c.RestList.Contains(value))
                              where (restCount == ((G.AllBaseValues.Count - setCount) * 2) || (restCount == (G.AllBaseValues.Count - setCount) * 2 + 1))
                              select new { value, setCount, restCount }).ToList();
                    if (c1.Count == G.AllBaseValues.Count)
                    {
                        var valueCondition =
                            c1.Where(c => (c.restCount == (G.AllBaseValues.Count - c.setCount) * 2 + 1)).ToList();
                        if (valueCondition.Count == 1)
                        {
                            var value = valueCondition.First().value;
                            var cell = new PositiveCell(checkcells.First(c => c.RestCount == 3).Index, value, qSudoku);
                            cell.SolveMessages=new List<SolveMessage>
                            {
                                "只有候选数"+value+"满足可以填的位置个数"+"(9-已经填入的"+value+"的个数)*2+1\t\t\r\n",
                                "其余候选数y满足可以填的位置个数"+"(9-已经填入的y的个数)*2\t\t\r\n",
                                "所以",cell.Location,"必须填入"+value+"\t\t\r\n"
                            };
                            cell.drawCells=new List<CellInfo>
                            {
                                cell,
                                new NegativeCell(cell.Index,cell.RestList.First(c=>c!=cell.Value),qSudoku),
                                new NegativeCell(cell.Index,cell.RestList.Last(c=>c!=cell.Value),qSudoku),
                            };
                            cells.Add(cell);
                        }
                        else
                        {
                            return cells;
                        }
                    }
                    else
                        return cells;
                }
            }
            return cells;
        }


        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            return new List<CellInfo>();
        }

        public override string GetDesc()
        {
            return "若所有候选数格中，只存在一个单元格只能填入3个候选数，其余都是两个候选数。\t\t\r\n" +
                   "所有候选数y满足可以填的位置个数=(9-已经填入的y的个数)*2\t\t\r\n" +
                   "只有一个候选数x满足可以填的位置个数=(9-已经填入的x的个数)*2+1\t\t\r\n" +
                   "则可以填入3个候选数的单元格中必须填入x。"
                ;
        }
    }
}
