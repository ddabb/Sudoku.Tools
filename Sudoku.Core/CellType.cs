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
        /// 肯定的，即出题时该单元就有的值
        /// </summary>
        [Description("初始值")]
        Init,
        [Description("单元格不包含以下值")]
        NegativeValuesGroup,
        /// <summary>
        /// 肯定的，即指定Index单元格的候选数一定是Value
        /// </summary>
        [Description("肯定的")]
        Positive,
        [Description("肯定包含某候选数的单元格的集合")]
        PositiveCellGroup,
        [Description("肯定不包含某候选数的单元格的集合")]
        NegativeIndexsGroup,
        [Description("可能的")]
        Possible,
        [Description("链单元格")]
        Chain
    }
}