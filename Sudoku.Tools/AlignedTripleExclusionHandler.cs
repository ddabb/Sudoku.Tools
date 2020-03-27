using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    //205000000080000007060010902007039500010000078002000009070390000509060000000001300 假装是个出数用例
    [EliminationExample(4, "R1C4", "000010900001209870029306451697023100482061000135000260000002610056134700010697000")]
    public class AlignedTripleExclusionHandler : SolverHandlerBase
    {
        public override SolveMethodEnum methodType => SolveMethodEnum.AlignedTripleExclusion;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return AssignmentCellByEliminationCell(qSudoku);
        }

        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var allUnSetCells = qSudoku.AllUnSetCells;
            var chars = qSudoku.CurrentString.ToCharArray();
            var strs = chars.Select(c => "" + c).ToList();
            var allUnsetIndex = allUnSetCells.Select(c => c.Index).ToList();
            foreach (var cell1 in allUnSetCells)
            {
                foreach (var cell2 in allUnSetCells)
                {
                    foreach (var cell3 in allUnSetCells)
                    {
                        var index1 = cell1.Index;
                        var index2 = cell2.Index;
                        var index3 = cell3.Index;
                        if (index1 < index2&& index2<index3)
                        {
                            var publicAreas = qSudoku.GetPublicUnsetAreaIndexs(index1, index2, index3);
                            var checkIndex = allUnsetIndex.Intersect(publicAreas).ToList();
                            if (checkIndex.Any())
                            {
                                List<AlignedDTO> dtos = new List<AlignedDTO>();
                                foreach (var value1 in cell1.RestList)
                                {
                                    foreach (var value2 in cell2.RestList)
                                    {
                                        foreach (var value3 in cell3.RestList)
                                        {
                                            var mergeList = G.MergeInt(value1, value2,value3);
                                            bool allHasValueToSet = qSudoku.AllUnSetCells.Where(c => checkIndex.Contains(c.Index)).ToList().All(c => c.RestList.Except(mergeList).Any());


                                            dtos.Add(new AlignedDTO(allHasValueToSet, new InitCell(index1, value1), new InitCell(index2, value2), new InitCell(index3, value3)));
                                        }
       
                                    }

                                }

                                NewMethod1(NewMethod(qSudoku, cell1, dtos), cells);
                                NewMethod1(NewMethod(qSudoku, cell2, dtos), cells);
                                NewMethod1(NewMethod(qSudoku, cell3, dtos), cells);

                            }
                        }
                    }
   

                }
            }

            return cells;

        }
        private static void NewMethod1(List<CellInfo> temp, List<CellInfo> cells)
        {
            foreach (var cellx in temp)
            {
                if (!cells.Exists(c => c.Value == cellx.Value && c.Index == cellx.Index))
                {
                    cells.Add(cellx);
                }
            }
        }
        public override string GetDesc()
        {
            return "";
        }
    }

}
