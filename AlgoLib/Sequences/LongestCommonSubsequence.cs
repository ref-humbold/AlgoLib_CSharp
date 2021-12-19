// Algorithm for longest common subsequence
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Text
{
    public static class LongestCommonSubsequence
    {
        public static int FindLcsLength<T>(IEnumerable<T> sequence1, IEnumerable<T> sequence2)
        {
            (List<T> shorterList, List<T> longerList) = sequence1.Count() <= sequence2.Count()
                ? (sequence1.ToList(), sequence2.ToList())
                : (sequence2.ToList(), sequence1.ToList());

            List<int> lcs = Enumerable.Repeat(0, shorterList.Count + 1).ToList();

            foreach(T element in longerList)
            {
                for(int j = 0; j < shorterList.Count; ++j)
                    lcs[j + 1] = Equals(element, shorterList[j])
                        ? lcs[j] + 1
                        : Math.Max(lcs[j + 1], lcs[j]);
            }

            return lcs.Last();
        }
    }
}
