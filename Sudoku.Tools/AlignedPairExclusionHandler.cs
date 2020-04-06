using Sudoku.Core;
using Sudoku.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Tools
{

    [EliminationExample(8, "R2C6", "783060054569700020124503700070005400405007002030800075007050010300906507650070200",SolveMethodEnum.ClaimingInColumn)]
    public class AlignedPairExclusionHandler : SolverHandlerBase
    {
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
                    var index1 = cell1.Index;
                    var index2 = cell2.Index;

                    if (index1 < index2)
                    {
                        var publicAreas = qSudoku.GetPublicUnsetAreaIndexs(index1, index2);
                        var checkIndex = allUnsetIndex.Intersect(publicAreas).ToList();
                        if (checkIndex.Any())
                        {
                            List<AlignedDTO> dtos = new List<AlignedDTO>();
                            foreach (var value1 in cell1.RestList)
                            {
                                foreach (var value2 in cell2.RestList)
                                {
                                    var mergeList = G.MergeInt(value1, value2);
                                    bool allHasValueToSet = qSudoku.AllUnSetCells.Where(c => checkIndex.Contains(c.Index)).ToList().All(c => c.RestList.Except(mergeList).Any());


                                    dtos.Add(new AlignedDTO(allHasValueToSet, new InitCell(index1, value1), new InitCell(index2, value2)));
                                }

                            }

                            List<CellInfo> all = G.MergeCells(cell1, cell2);
                            foreach (var item in all)
                            {
                                NewMethod1(NewMethod(qSudoku, item, dtos, all), cells);

                            }
                        }
                    }

                }
            }

            return cells;
        }
    

        public override SolveMethodEnum methodType => SolveMethodEnum.AlignedPairExclusion;

        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;

        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();

            return cells;
        }

        public override string GetDesc()
        {

            return "若两个候选数格X,Y值任意组合，当X为x时，若无论Y取什么值，该两个单元格的共同相关格存在单元格没有有效数字可以填，则X不为x。";
        }
    }


}
