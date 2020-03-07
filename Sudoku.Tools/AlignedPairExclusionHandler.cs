using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Core;

namespace Sudoku.Tools
{

  [EliminationExampleAttribute(8,"R2C6","783060054569700020124503700070005400405007002030800075007050010300906507650070200")]    
   public class AlignedPairExclusionHandler : SolverHandlerBase
    {
        public override List<CellInfo> Elimination(QSudoku qSudoku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            //qSudoku.RemoveCells(new List<CellInfo> { new NegativeCell(59, 4), new NegativeCell(77, 4) });
            var allUnSetCells = qSudoku.AllUnSetCells;
            var checkCells = allUnSetCells.Where(c => c.RestCount == 3);
            foreach (var checkCell in checkCells)
            {
                var relatedCell = checkCell.RelatedUnsetCells;
                var filterCell = relatedCell.Where(c => c.RestCount == 2).ToList();
                var checkCellRest = checkCell.RestList;
                if (checkCell.Index==5)
                {

                }
                var abcd = (from a in filterCell
                    join b in filterCell on 1 equals 1
                    join c in filterCell on 1 equals 1
                    join d in filterCell on 1 equals 1
                    let mergeRest = G.DinstinctInt(a.RestList, b.RestList, c.RestList, d.RestList)
                    let mergeRestString=G.DinstinctCellRestString(a,b,c,d)
                    let mergeBlock=G.MergeCellBlocks(a,b,c,d)
                    where a.Index < b.Index && b.Index < c.Index && c.Index < d.Index
                          && mergeRest.Count == 4
                          //&& mergeBlock.Count == 2
                          && mergeRest.Intersect(checkCellRest).Count() == checkCellRest.Count
                          && mergeRestString.Count==4
                            select new { a, b, c, d, mergeRest }).ToList();
                foreach (var item in abcd)
                {
                    var mergeRest = item.mergeRest;
                    var removeValue = mergeRest.Except(checkCellRest).First();
                    var a = item.a;
                    var b = item.b;
                    var c = item.c;
                    var d = item.d;
                    var publicIndex = qSudoku.GetPublicUnsetAreaIndexs(a, b, c, d).Where(c1=>c1!= checkCell.Index);
                    foreach (var index in publicIndex)
                    {
                        if (qSudoku.GetRest(index).Contains(removeValue))
                        {
                            cells.Add(new NegativeCell(index, removeValue));
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
            List<CellInfo> cells=new List<CellInfo>();
    
            return cells;
        }

        public override string GetDesc()
        {
            return "";
        }
    }
}
