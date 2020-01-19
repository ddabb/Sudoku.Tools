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
        [DifficultuRating(2.3)]
        NakedSingle,
        [DifficultuRating(1.2)]
        HiddenSingleBlock,
        [DifficultuRating(1.5)]
        HiddenSingleColumn,
        HiddenSingleRow,
        [DifficultuRating(1.7)]
        DirectPointing,
        [DifficultuRating(2.0)]
        HiddenPair,
        [DifficultuRating(4.0)]
        HiddenTriple,
        [DifficultuRating(5.4)]
        HiddenQuadruple,
        [DifficultuRating(3.0)]
        NakedPair,
        [DifficultuRating(3.6)]
        NakedTriple,
        [DifficultuRating(5.0)]
        NakedQuadruple,
        ClaimingInRow,
        ClaimingInColumn,
        [DifficultuRating(4.4)]
        WWing,
        [DifficultuRating(3.2)]
        XWing,
        [DifficultuRating(4.2)]
        XYWing,
        [DifficultuRating(4.4)]
        XYZWing,
        [DifficultuRating(4.6)]
        WXYZWing,
        XYChain,
        [DifficultuRating(6.6)]
        ForcingXChain,
        URType1,  
        ULSize6Type1,
        ULSize6Type2,
        [DifficultuRating(4.1)]
        TwoStringsKite,
        [DifficultuRating(4.0)]
        Skyscraper,
        [DifficultuRating(4.2)]
        TurbotFishNormal,
        Jellyfish,
        HiddenAR,
        LockedSubset,
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
        ULSize6Type4,
        ULSize14,
        RemotePair,
        SashimiJellyfish,
        SashimiXwing,
        SiameseJeffyfish,
        ULSize12,
        [Description("强制链")]
        [DifficultuRating(7.3)]
        ForcingChain,
        ImcompletedURType4,
        [DifficultuRating(9.1)]
        DymanicForcingChain,
        [DifficultuRating(4.5)]
        IncompleteWXYZWing,
        EmptyRectangle,
        DiscontinuousNiceLoop,
        CannibalisticAIC
    }
}
