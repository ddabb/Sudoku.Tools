using System.ComponentModel;

namespace Sudoku.Core
{
    /// <summary>
    /// 单元格类型
    /// </summary>
    public enum CellType
    {
        /// <summary>
        /// 否定的，即指定Index单元格的候选数一定不是Value
        /// </summary>
        [Description("否定的")]
        Negative,
        /// <summary>
        /// 肯定的，即指定Index单元格的候选数一定是Value
        /// </summary>
        [Description("肯定的")]
        Init,
        [Description("肯定包含某候选数的单元格的集合")]
        PositiveCellGroup,
        [Description("肯定不包含某候选数的单元格的集合")]
        NegativeCellGroup
    }
}