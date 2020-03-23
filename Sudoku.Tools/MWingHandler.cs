using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [AssignmentExample(1, "R5C2", "703001006000700030090600207000475801007382000548196372001064720070000000600807409")]
    public class MWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnsetCells = qSudoku.AllUnSetCells;
            var pairCells = allUnsetCells.Where(c => c.RestCount == 2).ToList();
            foreach (var cell1 in pairCells)
            {
                var keyRest = cell1.RestList;
           
                foreach (var cell2 in allUnsetCells.Where(c=>c.Row!=cell1.Row&&c.Column!=cell1.Column&& keyRest.All(rest=> c.RestList.Contains(rest))))
                {
                    var interCells = qSudoku.GetPublicUnsetAreas(cell1, cell2);
                    foreach (var cell3 in interCells)
                    {
                        foreach (var one in keyRest)
                        {
                            if (cell3.RestList.Contains(one))
                            {
                                var other = keyRest.First(c => c != one);
                                if (new NegativeCell(cell3.Index, one, qSudoku).NextCells.Exists(c => c.Index == cell2.Index&&c.Value==one))
                                {
                                    var filter = qSudoku.GetPublicUnsetAreas(cell2, cell3).Where(c => c.RestList.Contains(other));
                                    foreach (var cell4 in filter)
                                    {
                                        if (new NegativeCell(cell4.Index, other, qSudoku) .NextCells.Exists(c => c.Index == cell2.Index && c.Value == other))
                                        {
                                            foreach (var cell5 in qSudoku.GetPublicUnsetAreas(cell1, cell4))
                                            {
                                                if (cell5.RestList.Contains(other))
                                                {
                                                    cells.Add(new NegativeCell(cell5.Index, other, qSudoku) );
                                                }
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

        public  int GetOne(List<int> list,int index)
        {
            return list[index];
        }

        public override string GetDesc()
        {
            return "";
        }

        public override SolveMethodEnum methodType => SolveMethodEnum.MWing;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
    }
}
