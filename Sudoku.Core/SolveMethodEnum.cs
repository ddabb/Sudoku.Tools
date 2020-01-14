﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sudoku.Core
{
    /// <summary>
    /// 
    /// </summary>
    public enum SolveMethodEnum
    {
        NakedSingle,
        HiddenSingleBlock,
        HiddenSingleColumn,
        HiddenSingleRow,
        [DifficultuRating(2.6)]
        DirectPointing,
        [Description("隐藏数对")]
        HiddenPair,
        HiddenTriple,
        HiddenQuadruple,
        [Description("显性数对")]
        NakedPair,
        [DifficultuRating(3.6)]
        NakedTriple,
        [DifficultuRating(5.0)]
        NakedQuadruple,
        ClaimingInRow,
        ClaimingInColumn,
        WWing,
        XWing,
        XYWing,
        XYZWing,
        WXYZWing,
        XYChain, 
        ForcingXChain,
        URType1,  
        ULSize6Type1,
        ULSize6Type2,
        [DifficultuRating(4.1)]
        TwoStringsKite,
        Skyscraper,
        TurbotFishNormal,
        Jellyfish,
        HidderAR,
        LockedSubset,
        ULSize6Type3,
        ImcompletedURType2,    
        ALPExtendedType,   
        SashimiSwordfish,
        XRSize6Type4,
        URType2Handler,
        ARType3WithHiddenTriple,
        ALPBasicType,
        ALTBasicType,
        ALTExtendedType,
        ARType2,
        ARType1,
        XRSize10,
        [DifficultuRating(4.5)]
        URType3NakedPair,
        [DifficultuRating(4.5)]
        URType3NakedQuadruple,
        [DifficultuRating(4.5)]
        URType3NakedTriple,
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
        FinnedJeffyfish,
        FinnedSwordfish,
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
        ULSize6Type4,
        ULSize14,
        RemotePair,
        SashimiJellyfish,
        SashimiXwing,
        SiameseJeffyfish, 
        Swordfish,
        ULSize12,
        [Description("强制链")]
        [DifficultuRating(7.3)]
        ForcingChain,
        ImcompletedURType4,

    }
}
