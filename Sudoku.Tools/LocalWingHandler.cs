using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{
    [AssignmentExample(4, "R7C8", "900050030400100908006090402000970020004216800090030000803000200700009080049080006",SolveMethodEnum.ClaimingInColumn)] //todo 待移除8宫4列的4
    public class LocalWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCells = qSudoku.AllUnSetCells;
            foreach (var value1 in G.AllBaseValues)
            {
                foreach (var value2 in G.AllBaseValues)
                {
                    if (value1 < value2)
                    {
                        var restList = new List<int> { value1, value2 };
                        var pairCells = allUnsetCells.Where(c => c.RestList.Contains(value1) && c.RestList.Contains(value2)).ToList();
                        var list = (from cell1 in pairCells
                                    join cell2 in pairCells on 1 equals 1
                                    where cell1.Index != cell2.Index
                                    select new { cell1, cell2 }).ToList();

                        foreach (var item in list)
                        {
                            var cell1 = item.cell1;
                            var cell2 = item.cell2;
            
                            var intercells = qSudoku.GetPublicUnsetAreas(cell1, cell2);
                            foreach (var cell3 in intercells)
                            {

                                foreach (var one in restList)
                                {
                                    var other = restList.First(c => c != one);
                                    if (cell3.RestList.Contains(one) && new NegativeCell(cell3.Index, one, qSudoku).NextCells.Exists(c => c.Value == one && c.Index == cell1.Index))
                                    {
                                        var cell1otherNextCell = new NegativeCell(cell1.Index, other, qSudoku) .NextCells;
                                        var cell2otherNextCell = new NegativeCell(cell2.Index, other, qSudoku) .NextCells;

                                        var cellkey = (from cell4 in cell1otherNextCell
                                                       join cell5 in cell2otherNextCell on cell4.Value equals cell5.Value
                                                       where cell4.Index != cell5.Index
                                                       && cell5.Block == cell4.Block
                                                       select new { cell4, cell5 }).ToList();

                                        foreach (var item1 in cellkey)
                                        {
                                            var cell4 = item1.cell4;
                                            var cell5 = item1.cell5;
                                            if (G.MergeCellIndexs(cell1, cell2, cell3, cell4, cell5).Distinct().Count() == 5)
                                            {
                                                var cell = new NegativeCell(cell2.Index, one, qSudoku) ;
                                                cell.SolveMessages = new List<SolveMessage>
                                            {
                                                cell1.Location,
                                                cell2.Location,
                                                cell3.Location,
                                                cell4.Location,
                                                cell5.Location
                                            };
                                                cells.Add(cell);
                                            }

                                        }



                                    }

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

        public override SolveMethodEnum methodType => SolveMethodEnum.LocalWing;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
    }
}
