﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Sudoku.Core
{
    public class ComfirmedPostion
    {
        /// <summary>
        /// 将数独指定位置的数进行交换构成标准数独。 
        /// </summary>
        /// <example>
        ///new ComfirmedPostion().GenSoduku("003000400068943250102000700050000020004000800000706000000020000000605000001000900", "艾");
        /// new ComfirmedPostion().GenSoduku("040000500060204170092580400300609782000350010000402356005700030000805090000900240", "特");
        ///new ComfirmedPostion().GenSoduku("003000080060204000291587460000709000000308005000600310000420500035801090000900006", "我");
        /// </example>
        /// <param name="sodukuString"></param>
        public string GenSoduku(string sodukuString, string fileName = "")
        {
            var matrix = G.StringToList(sodukuString);
            Console.WriteLine(sodukuString);
            var list = G.GetLocations(matrix);
            Console.WriteLine(JsonConvert.SerializeObject(list));
            var switchList = PermutationAndCombination<int>.GetCombination(list.ToArray(), 2);
            //switchList.Reverse();
            Console.WriteLine("list" + list.Count);
            Console.WriteLine("switchList" + switchList.Count);
            Dictionary<string, int> expressCount = new Dictionary<string, int>();
            List<string> tryedList = new List<string> { sodukuString };
            var min = GetMinCount(sodukuString, expressCount, switchList);
            while (min != 1)
            {
                var result = (from item1 in expressCount
                              where
                                  !(tryedList.Any(item2 => item2 == item1.Key))
                              select item1).Where(c => c.Value != 0).ToList();
                if (result.Count == 0)
                {
                    //所有该尝试的组合都已经尝试过了
                    //表明已知提示数在固定位置的确无法构成唯一解。
                    return null;
                }
                var newSeed = result.OrderBy(c => c.Value).Last();
                var newMin = result.OrderBy(c => c.Value).First();
                if (true)
                {
                    Console.WriteLine(fileName + "最少的终盘个数: " + newMin.Value + "表达式为   " + newMin.Key + "   最多的终盘个数: " + newSeed.Value + "表达式为   " + newSeed.Key);
                }
                min = GetMinCount(newSeed.Key, expressCount, switchList);
                tryedList.Add(newSeed.Key);
            }
            string Value = expressCount.Where(c => c.Value == 1).Select(c => c.Key).First();
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            var noticeCount = G.GetLocations(Value).Count;
            string configName = Path.Combine(dir, fileName + "生成于" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
            File.WriteAllText(configName, Value);
            return Value;
        }
        private int GetMinCount(string sodukuString, Dictionary<string, int> expressCount, List<int[]> switchList)
        {
            int min = 0;
            if (!expressCount.ContainsKey(sodukuString))
            {
                min = new DanceLink().solution_count(sodukuString);
                expressCount.Add(sodukuString, min);
            }
            else
            {
                min = expressCount[sodukuString];
            }
            int start = 0;
            int end = 0;
            do
            {
                start = min;
                foreach (var switchListCouple in switchList)
                {
                    var newStr = G.SwitchLocation(sodukuString, switchListCouple[0], switchListCouple[1]);
                    if (!expressCount.ContainsKey(newStr))
                    {
                        var count = new DanceLink().solution_count(newStr);
                        //Console.WriteLine("newStr  " + newStr + "  " + count);
                        expressCount.Add(newStr, count);
                        if (count != 0 && count < min)
                        {
                            sodukuString = newStr;
                            min = count;
                            break;
                        }
                    }
                }
                end = min;
            } while (start != end);
            return min;
        }
    }
}
