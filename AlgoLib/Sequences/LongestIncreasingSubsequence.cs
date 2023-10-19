using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Sequences;

/// <summary>Algorithm for longest increasing subsequence.</summary>
public static class LongestIncreasingSubsequence
{
    /// <summary>Computes longest increasing subsequence according to given comparer.</summary>
    /// <typeparam name="T">The type of sequence elements.</typeparam>
    /// <param name="sequence">The sequence of elements.</param>
    /// <param name="comparer">The comparer of elements in sequence.</param>
    /// <returns>The least lexicographically longest increasing subsequence.</returns>
    public static IEnumerable<T> FindLis<T>(this IList<T> sequence, IComparer<T> comparer) =>
        FindLis(sequence, comparer.Compare);

    /// <summary>Computes longest increasing subsequence according to given comparison function.</summary>
    /// <typeparam name="T">The type of sequence elements.</typeparam>
    /// <param name="sequence">The sequence of elements.</param>
    /// <param name="comparison">The comparison function of elements in sequence.</param>
    /// <returns>The least lexicographically longest increasing subsequence.</returns>
    public static IEnumerable<T> FindLis<T>(this IList<T> sequence, Comparison<T> comparison)
    {
        var previousElem = Enumerable.Repeat<int?>(null, sequence.Count).ToList();
        var subsequence = new List<int> { 0 };

        for(int i = 1; i < sequence.Count; ++i)
        {
            T elem = sequence[i];

            if(comparison(elem, sequence[subsequence[^1]]) > 0)
            {
                previousElem[i] = subsequence[^1];
                subsequence.Add(i);
            }
            else
            {
                int index = searchIndex(sequence, comparison, subsequence, i, 0,
                                        subsequence.Count);

                subsequence[index] = i;
                previousElem[i] = index > 0 ? (int?)subsequence[index - 1] : null;
            }
        }

        var longestSubsequence = new List<T>();
        int? subsequenceIndex = subsequence[^1];

        while(subsequenceIndex.HasValue)
        {
            longestSubsequence.Add(sequence[subsequenceIndex.Value]);
            subsequenceIndex = previousElem[subsequenceIndex.Value];
        }

        return longestSubsequence.Reverse<T>();
    }

    // Searches for place of element in list of subsequences.
    private static int searchIndex<T>(
        IList<T> sequence,
        Comparison<T> comparison,
        IList<int> subsequence,
        int indexElem,
        int indexBegin,
        int indexEnd)
    {
        if(indexEnd - indexBegin <= 1)
            return indexBegin;

        int indexMiddle = (indexBegin + indexEnd - 1) / 2;

        return comparison(sequence[indexElem], sequence[subsequence[indexMiddle]]) > 0
            ? searchIndex(sequence, comparison, subsequence, indexElem, indexMiddle + 1, indexEnd)
            : searchIndex(sequence, comparison, subsequence, indexElem, indexBegin, indexMiddle + 1);
    }
}
