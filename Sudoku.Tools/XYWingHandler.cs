using Sudoku.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku.Tools
{
    [Example("860035900700068351530074020070810530005307100183540200020650703057400002010700495")]
    public class XYWingHandler : SolverHandlerBase
    {
        public override List<CellInfo> Excute(QSudoku qSoduku)
        {
            List<CellInfo> cells = new List<CellInfo>();
            var checkcells = qSoduku.GetFilterCell(c => c.Value == 0 && (qSoduku.GetRest(c.Index).Count == 2));

            foreach (var direction in allDireaction.Where(c => c != Direction.Block)) //从行或列的角度出发
            {
                foreach (var index in QSudoku.baseIndexs)
                {
                    var specialRowOrColumn = checkcells.Where(c => GetFilter(c, direction, index)).ToList(); //特定行或列
                    foreach (var specialValue in QSudoku.baseFillList)
                    {
                        var list = specialRowOrColumn.Where(c => qSoduku.GetRest(c.Index).Contains(specialValue))
                            .ToList();
                        if (list.Count == 2)
                        {
                            var cell1 = list[0];
                            var cell2= list[1];
                            var block1 = cell1.Block;
                            var block2 = cell2.Block;
                            if (block1 != block2 &&
                                qSoduku.GetRestString(cell1) != qSoduku.GetRestString(cell2)) //xy  xz 同行(列)不同宮
                            {
                                var x = specialValue;
                                var y = qSoduku.GetRest(cell1).First(c => c != x);
                                var z = qSoduku.GetRest(cell2).First(c => c != x);
                                var findcell = checkcells.Where(c =>
                                    qSoduku.GetRest(c).Contains(y) && qSoduku.GetRest(c).Contains(z) &&
                                    !GetFilter(c, direction, index)&&(c.Block==block1||c.Block==block2)).ToList();
                                if (findcell.Count==1)
                                {
                                    var cell3 = findcell[0];
                                    List<CellInfo> allCell=new List<CellInfo>{cell1,cell2,cell3};
                                    var removeNumber = qSoduku.GetRest(allCell.First(c => !GetFilter(c, direction, index))).First(c => c != x);
                                    
                                    Debug.WriteLine("关联单元格的待删除数是   " + removeNumber + "   在" + index + GetEnumDescription(direction) +"  找到 "+cell3+"  满足  "+ "  xy  " + (x + "" + y) + "  xz  " + (x + "" + z) + " yz " + (y + "" + z) );

                                    


                                }

                            }

                        }
                    }
                }
            }

            return cells;
        }
    }
}
