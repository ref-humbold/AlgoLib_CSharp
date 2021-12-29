// Algorithm for longest common subsequence
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Text
{
    public static class LongestCommonSubsequence
    {
        public static int CountLcsLength<T>(IList<T> sequence1, IList<T> sequence2)
        {
            (IList<T> shortSeq, IList<T> longSeq) = sequence1.Count <= sequence2.Count
                ? (sequence1, sequence2)
                : (sequence2, sequence1);

            int[] lcs = Enumerable.Repeat(0, shortSeq.Count + 1).ToArray();

            foreach(T element in longSeq)
            {
                int previousAbove = lcs[0];

                for(int i = 0; i < shortSeq.Count; ++i)
                {
                    int previousDiagonal = previousAbove;

                    previousAbove = lcs[i + 1];
                    lcs[i + 1] = Equals(element, shortSeq[i])
                        ? previousDiagonal + 1
                        : Math.Max(previousAbove, lcs[i]);
                }
            }

            return lcs[^1];
        }

        public static int CountLcsLength(string text1, string text2)
        {
            (string shortText, string longText) = text1.Length <= text2.Length
                ? (text1, text2)
                : (text2, text1);

            int[] lcs = Enumerable.Repeat(0, shortText.Length + 1).ToArray();

            foreach(char element in longText)
            {
                int previousAbove = lcs[0];

                for(int i = 0; i < shortText.Length; ++i)
                {
                    int previousDiagonal = previousAbove;

                    previousAbove = lcs[i + 1];
                    lcs[i + 1] = Equals(element, shortText[i])
                        ? previousDiagonal + 1
                        : Math.Max(previousAbove, lcs[i]);
                }
            }

            return lcs[^1];
        }
    }
}
