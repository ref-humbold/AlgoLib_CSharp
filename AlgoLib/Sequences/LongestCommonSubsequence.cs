﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Sequences;

/// <summary>Algorithm for longest common subsequence.</summary>
public static class LongestCommonSubsequence
{
    /// <summary>Computes length of the longest common subsequence of given sequences.</summary>
    /// <typeparam name="T">The type of sequence elements.</typeparam>
    /// <param name="sequence1">The first sequence of elements.</param>
    /// <param name="sequence2">The second sequence of elements.</param>
    /// <returns>The length of the longest common subsequence.</returns>
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

    /// <summary>Computes length of the longest common subsequence of given texts.</summary>
    /// <param name="text1">The first text.</param>
    /// <param name="text2">The second text.</param>
    /// <returns>The length of the longest common subsequence.</returns>
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
