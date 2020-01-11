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
        DirectPointing,
        [Description("隐藏数对")]
        HiddenPair,
        [Description("显性数对")]
        NakedPair,
        ClaimingInRow,
        ClaimingInColumn,
        WWing,
        XWing,
        XYWing,
        XYZWing,
        WXYZWing,
        XYChain,  
        NakedTriple,
        NakedQuadruple,
        ForcingXChain,
        URType1,  
        ULSize6Type1,
        ULSize6Type2,
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
        URType3,
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
        HiddenQuadruple,      
        HiddenTriple,
        ImcompletedURType1,
        ImcompletedURType3,
        LockedCandidates,
        LockedURType1,
        LockedURType2,  
        SiameseSwordfish,
        SiameseXwing,
        ULSize10,
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
        ForcingChain,
        ImcompletedURType4
    }
}
