using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Core;
using Sudoku.Core.Model;

namespace Sudoku.Tools
{
    [EliminationExample(7,"R8C2", "109340050850000143364510000003850001080691030600034580000080320208003015030025908",SolveMethodEnum.DirectPointing,SolveMethodEnum.NakedTriple)]//测试用例待优化
   public class AlignedQuadrupleExclusionHandler : SolverHandlerBase
    {
        public override List<CellInfo> Assignment(QSudoku qSudoku)
        {
            return new List<CellInfo>();
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
                        foreach (var cell4 in allUnSetCells)
                        {
                            var index1 = cell1.Index;
                            var index2 = cell2.Index;
                            var index3 = cell3.Index;
                            var index4 = cell4.Index;
                            if (index1 < index2 && index2 < index3 && index3 < index4)
                            {
                                var publicAreas = qSudoku.GetPublicUnsetAreaIndexs(index1, index2, index3,index4);
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
                                                foreach (var value4 in cell4.RestList)
                                                {
                                                    var mergeList = G.MergeInt(value1, value2, value3, value4);
                                                    bool allHasValueToSet = qSudoku.AllUnSetCells.Where(c => checkIndex.Contains(c.Index)).ToList().All(c => c.RestList.Except(mergeList).Any());


                                                    dtos.Add(new AlignedDTO(allHasValueToSet, new InitCell(index1, value1), new InitCell(index2, value2), new InitCell(index3, value3), new InitCell(index4, value4)));
                                                }
                                   
                                            }

                                        }

                                    }

                                    List<CellInfo> all = G.MergeCells(cell1, cell2, cell3, cell4);
                                    foreach (var item in all)
                                    {
                                        NewMethod1(NewMethod(qSudoku, item, dtos, all), cells);

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

            return "若四个候选数格X,Y,Z,W值任意组合，当X为x时，若无论W,Y,Z任意组合，该四个单元格的共同相关格存在单元格没有有效数字可以填，则X不为x。";
        }

        public override SolveMethodEnum methodType=>SolveMethodEnum.AlignedQuadrupleExclusion;
        public override MethodClassify methodClassify => MethodClassify.SudokuTechniques;
    }
}
