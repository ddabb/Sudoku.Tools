﻿using System.ComponentModel;
namespace Sudoku.Core
{
    /// <summary>
    /// 
    /// </summary>
    public enum SolveMethodEnum
    {
        [Description("宮排除")]
        [DifficultuRating(1.2)]
        HiddenSingleBlock,
        [Description("唯一余数")]
        [DifficultuRating(2.3)]
        NakedSingle,
        [Description("列排除")]
        [DifficultuRating(1.5)]
        HiddenSingleColumn,
        [Description("行排除")]
        HiddenSingleRow,
        [Description("宫区块")]
        [DifficultuRating(1.7)]
        DirectPointing,
        [Description("行区块")]
        [DifficultuRating(2.8)]
        ClaimingInRow,
        [Description("列区块")]
        [DifficultuRating(2.8)]
        ClaimingInColumn,
        [Description("级联区块")]
        CascadingLockedCandidates,
        [Description("隐性数对")]
        [DifficultuRating(2.0)]
        HiddenPair,
        [Description("隐性三数组")]
        [DifficultuRating(4.0)]
        HiddenTriple,
        [Description("隐性四数组")]
        [DifficultuRating(5.4)]
        HiddenQuadruple,
        [Description("显性数对")]
        [DifficultuRating(3.0)]
        NakedPair,
        [Description("显性三数组")]
        [DifficultuRating(3.6)]
        NakedTriple,
        [Description("显性四数组")]
        [DifficultuRating(5.0)]
        NakedQuadruple,
        [Description("区块三数组")]
        NakedTripleWithLockedCandidates,
        [Description("区块四数组")]
        NakedQuadrupleWithLockedCandidates,
        [Description("跨区数组")]
        DistributedDisjointedSubset,
        [Description("假数组_伪数组")]
        SubsetCounting,
        [Description("M-Wing")]
        MWing,
        [DifficultuRating(4.4)]
        [Description("W-Wing")]
        WWing,
        FinnedXwing,
        SashimiXwing,
        SiameseXwing,
        [Description("X-Wing")]
        [DifficultuRating(3.2)]
        XWing,
        [Description("XY-Wing")]
        [DifficultuRating(4.2)]
        XYWing,
        [Description("XYZ-Wing")]
        [DifficultuRating(4.4)]
        XYZWing,
        [Description("WXYZ-Wing")]
        [DifficultuRating(4.6)]
        WXYZWing,
        [Description("VWXYZ-Wing")]
        VWXYZWing,
        [Description("Local-Wing")]
        LocalWing,
        [Description("Hybrid-Wing")]
        HybridWing,
        [Description("Split-Wing")]
        SplitWing,
        [DifficultuRating(4.6)]
        [Description("Incomplete VWXYZ-Wing")]
        IncompleteVWXYZWing,
        [DifficultuRating(4.5)]
        [Description("Incomplete WXYZ-Wing")]
        IncompleteWXYZWing,
        [Description("双值格链")]
        XYChain,
        [DifficultuRating(6.6)]
        ForcingXChain,
        URType1,
        [DifficultuRating(4.5)]
        URType2,
        [Description("唯一矩形+显性数对")]
        [DifficultuRating(4.5)]
        URType3NakedPair,
        [Description("唯一矩形+显性三数组")]
        [DifficultuRating(4.5)]
        URType3NakedTriple,
        [Description("唯一矩形+显性四数组")]
        [DifficultuRating(4.5)]
        URType3NakedQuadruple,
        [Description("唯一矩形+隐性数对")]
        URType3HiddenPair,
        [Description("唯一矩形+隐性三数组")]
        URType3HiddenTriple,
        [Description("唯一矩形+隐性四数组")]
        URType3HiddenQuadruple,
        [DifficultuRating(4.5)]
        URType4,
        ImcompletedURType1,
        ImcompletedURType2,
        ImcompletedURType3,
        ImcompletedURType4,
        LockedURType1,
        LockedURType2,
        [DifficultuRating(4.6)]
        ULSize6Type1,
        ULSize6Type2,
        [DifficultuRating(4.7)]
        ULSize6Type3,
        [DifficultuRating(4.6)]
        ULSize6Type4,
        [DifficultuRating(4.7)]
        ULSize8,
        [DifficultuRating(5.0)]
        ULSize10,
        ULSize12,
        ULSize14,
        [Description("双线风筝")]
        [DifficultuRating(4.1)]
        TwoStringsKite,
        [Description("摩天楼")]
        [DifficultuRating(4.0)]
        Skyscraper,
        [Description("多宝鱼(普通)")]
        [DifficultuRating(4.2)]
        TurbotFishNormal,
        [Description("远程数对")]
        RemotePair,
        [DifficultuRating(3.8)]
        FinnedSwordfish,
        SashimiSwordfish,
        SiameseSwordfish,
        [DifficultuRating(3.8)]
        Swordfish,
        SashimiJellyfish,
        SiameseJeffyfish,
        [DifficultuRating(5.2)]
        FinnedJeffyfish,
        ImcompletedSwordfish,
        Jellyfish,
        HiddenAR,
        LockedSubset,
        ALPBasicType,
        ALPExtendedType,
        ALTBasicType,
        ALTExtendedType,
        ARType2,
        ARType1,
        ARType3WithNakedPair,
        ARType3WithHiddenTriple,
        XRSize6Type1,
        XRSize6Type2,
        XRSize6Type3,
        XRSize6Type4,
        XRSize8,
        XRSize10,
        XRSize12,
        XRSize14,
        BugType1,
        LockedCandidates,
        [Description("空矩形")]
        EmptyRectangle,
        [Description("不连续环")]
        DiscontinuousNiceLoop,
        CannibalisticAIC,
        [Description("均衡数对")]
        AlignedPairExclusion,
        [Description("均衡三数组")]
        AlignedTripleExclusion,
        [Description("均衡四数组")]
        AlignedQuadrupleExclusion,
        [Description("强制链(开)")]
        [DifficultuRating(7.3)]
        ForcingChainOn,
        [Description("强制链(关)")]
        ForcingChainOff,
        [DifficultuRating(9.1)]
        DymanicForcingChain,
        NishioForcingChains,
        ImcompletedJellyfish,
    }
}
