// Algorithm for longest common subsequence
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Text
{
    public static class LongestCommonSubsequence
    {
        public static int CountLcsLength<T>(IEnumerable<T> sequence1, IEnumerable<T> sequence2)
        {
            (List<T> shortSeq, List<T> longSeq) = sequence1.Count() <= sequence2.Count()
                ? (sequence1.ToList(), sequence2.ToList())
                : (sequence2.ToList(), sequence1.ToList());

            var prevLcs = Enumerable.Repeat(0, shortSeq.Count + 1).ToList();

            foreach(T element in longSeq)
            {
                var nextLcs = new List<int> { 0 };

                for(int j = 0; j < shortSeq.Count; ++j)
                    nextLcs.Add(Equals(element, shortSeq[j])
                        ? prevLcs[j] + 1
                        : Math.Max(prevLcs[j + 1], nextLcs[^1]));

                prevLcs = nextLcs;
            }

            return prevLcs[^1];
        }
    }
}
