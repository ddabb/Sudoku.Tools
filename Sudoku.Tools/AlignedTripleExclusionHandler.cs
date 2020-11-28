using Sudoku.Core;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Core.Model;
namespace Sudoku.Tools
{
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
                                List<CellInfo> all = G.MergeCells(cell1, cell2, cell3);
                                foreach (var item in all)
                                {
                                    NewMethod1(NewMethod(qSudoku, item, dtos, all), cells);
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
            return "若三个候选数格X,Y,Z值任意组合，当X为x时，若无论Y,Z任意组合，该三个单元格的共同相关格都没有有效数字可以填，则X不为x。";
        }
    }
}
