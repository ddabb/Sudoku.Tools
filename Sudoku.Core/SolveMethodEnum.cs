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
        [DifficultuRating(4.4)]
        [Description("首尾数对匹配法(WWing)")]
        WWing,
        [DifficultuRating(3.2)]
        XWing,
        [DifficultuRating(4.2)]
        XYWing,
        [DifficultuRating(4.4)]
        XYZWing,
        [DifficultuRating(4.6)]
        WXYZWing,
        [Description("双值格链")]        
        XYChain,
        [DifficultuRating(6.6)]
        ForcingXChain,
        URType1,
        [DifficultuRating(4.6)]
        ULSize6Type1,
        ULSize6Type2,
        [Description("双线风筝")]
        [DifficultuRating(4.1)]
        TwoStringsKite,
        [Description("摩天楼")]
        [DifficultuRating(4.0)]
        Skyscraper,
        [Description("多宝鱼(普通)")]
        [DifficultuRating(4.2)]
        TurbotFishNormal,
        [Description("隔一数对匹配法")]
        MWing,
        Jellyfish,
        HiddenAR,
        LockedSubset,
        [DifficultuRating(4.7)]
        ULSize6Type3,
        ImcompletedURType2,
        ALPExtendedType,
        SashimiSwordfish,
        XRSize6Type4,
        [DifficultuRating(4.5)]
        URType2,
        ARType3WithHiddenTriple,
        ALPBasicType,
        ALTBasicType,
        ALTExtendedType,
        ARType2,
        ARType1,
        XRSize10,
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
        VWXYZWing,
        XRSize6Type3,
        XRSize8,
        XRSize6Type1,
        XRSize6Type2,
        XRSize12,
        XRSize14,
        ARType3WithNakedPair,
        BugType1,
        [DifficultuRating(5.2)]
        FinnedJeffyfish,
        [DifficultuRating(3.8)]
        FinnedSwordfish,
        [DifficultuRating(3.8)]
        Swordfish,
        FinnedXwing,
        ImcompletedURType1,
        ImcompletedURType3,
        LockedCandidates,
        LockedURType1,
        LockedURType2,
        SiameseSwordfish,
        SiameseXwing,
        [DifficultuRating(5.0)]
        ULSize10,
        [DifficultuRating(4.7)]
        ULSize8,
        [DifficultuRating(4.6)]
        ULSize6Type4,
        ULSize14,
        RemotePair,
        SashimiJellyfish,
        SashimiXwing,
        SiameseJeffyfish,
        ULSize12,
        [Description("拐角匹配法")]
        LocalWing,
        [Description("杂合匹配法")]
        HybridWing,
        [Description("分裂匹配法")]
        SplitWing,
        [DifficultuRating(4.6)]
        IncompleteVWXYZWing,
        [Description("空矩形")]
        EmptyRectangle,
        [Description("不连续环")]
        DiscontinuousNiceLoop,
        CannibalisticAIC,
        [Description("均衡三数组")]
        AlignedTripleExclusion,
        [Description("均衡数对")]
        AlignedPairExclusion,
        [Description("均衡四数组")]
        AlignedQuadrupleExclusion,
        [Description("级联区块")]
        CascadingLockedCandidates,
        [Description("区块数组")]
        NakedSubsetWithLockedCandidates,
        [Description("强制链(开)")]
        [DifficultuRating(7.3)]
        ForcingChainOn,
        [Description("强制链(关)")]
        ForcingChainOff,
        ImcompletedURType4,
        [DifficultuRating(9.1)]
        DymanicForcingChain,
        [DifficultuRating(4.5)]
        IncompleteWXYZWing,

    }
}
