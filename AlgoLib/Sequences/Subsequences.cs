using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Sequences
{
    public static class Subsequences
    {
        /// <summary>Constructs longest increasing subsequence according to a comparer.</summary>
        /// <param name="sequence">Sequence of elements</param>
        /// <param name="comparer">Comparer of elements in subsequence</param>
        /// <returns>Least lexicographically longest increasing subsequence</returns>
        public static IEnumerable<T> LongestIncreasing<T>(this IList<T> sequence, IComparer<T> comparer) =>
            LongestIncreasing(sequence, comparer.Compare);

        /// <summary>Constructs longest increasing subsequence according to a comparison function.</summary>
        /// <param name="sequence">Sequence of elements</param>
        /// <param name="comparison">Comparison function of elements in subsequence</param>
        /// <returns>Least lexicographically longest increasing subsequence</returns>
        public static IEnumerable<T> LongestIncreasing<T>(this IList<T> sequence, Comparison<T> comparison)
        {
            List<int?> previousElem = Enumerable.Repeat<int?>(null, sequence.Count).ToList();
            List<int> subseqLast = new List<int> { 0 };

            for(int i = 1; i < sequence.Count; ++i)
            {
                T elem = sequence[i];

                if(comparison.Invoke(elem, sequence[subseqLast[^1]]) > 0)
                {
                    previousElem[i] = subseqLast[^1];
                    subseqLast.Add(i);
                }
                else
                {
                    int index = searchIndex(sequence, comparison, subseqLast, i, 0, subseqLast.Count - 1);

                    subseqLast[index] = i;
                    previousElem[i] = index > 0 ? (int?)subseqLast[index - 1] : null;
                }
            }

            List<T> longestSubseq = new List<T>();
            int? j = subseqLast[^1];

            while(j.HasValue)
            {
                longestSubseq.Add(sequence[j.Value]);
                j = previousElem[j.Value];
            }

            return longestSubseq.Reverse<T>();
        }

        /// <summary>Dynamically constructs coherent subarray with maximal sum.</summary>
        /// <param name="sequence">Sequence of numbers</param>
        /// <returns>Maximum subarray</returns>
        public static List<double> MaximumSubarray(this IEnumerable<double> sequence)
        {
            (double sum, List<double> subarray) actual = (0.0, new List<double>());
            (double sum, List<double> subarray) maximal = (0.0, new List<double>());

            foreach(double elem in sequence)
            {
                if(actual.sum < 0.0)
                    actual = (0.0, new List<double>());

                actual = (actual.sum + elem, actual.subarray);
                actual.subarray.Add(elem);

                if(actual.sum > maximal.sum)
                    maximal = (actual.sum, new List<double>(actual.subarray));
            }

            return maximal.subarray;
        }

        /// <summary>Counts maximal sum from all coherent subarrays using interval tree.</summary>
        /// <param name="sequence">Sequence of numbers</param>
        /// <returns>The sum of maximum subarray</returns>
        public static double MaximalSubsum(this IEnumerable<double> sequence)
        {
            int size = 1;

            while(size < 2 * sequence.Count())
                size *= 2;

            List<double> intervalSums = Enumerable.Repeat(0.0, size).ToList();
            List<double> prefixSums = Enumerable.Repeat(0.0, size).ToList();
            List<double> suffixSums = Enumerable.Repeat(0.0, size).ToList();
            List<double> allSums = Enumerable.Repeat(0.0, size).ToList();

            int i = 0;

            foreach(double elem in sequence)
            {
                int index = size / 2 + i;

                allSums[index] = allSums[index] + elem;
                intervalSums[index] = Math.Max(allSums[index], 0.0);
                prefixSums[index] = Math.Max(allSums[index], 0.0);
                suffixSums[index] = Math.Max(allSums[index], 0.0);
                index /= 2;
                ++i;

                while(index > 0)
                {
                    int indexLeft = index + index;
                    int indexRight = index + index + 1;

                    intervalSums[index] = Math.Max(
                        Math.Max(intervalSums[indexRight], intervalSums[indexLeft]),
                        suffixSums[indexRight] + prefixSums[indexLeft]
                    );
                    prefixSums[index] = Math.Max(prefixSums[indexRight],
                                                 allSums[indexRight] + prefixSums[indexLeft]);
                    suffixSums[index] = Math.Max(suffixSums[indexLeft],
                                                 suffixSums[indexRight] + allSums[indexLeft]);
                    allSums[index] = allSums[indexRight] + allSums[indexLeft];
                    index /= 2;
                }
            }

            return intervalSums[1];
        }

        // Searches for place of element in list of subsequences.
        private static int searchIndex<T>(in IList<T> sequence, in Comparison<T> comparison,
                                          in List<int> subseqLast, in int indexElem, int indexBegin,
                                          int indexEnd)
        {
            if(indexBegin == indexEnd)
                return indexBegin;

            int indexMiddle = (indexBegin + indexEnd) / 2;

            return comparison.Invoke(sequence[indexElem], sequence[subseqLast[indexMiddle]]) > 0
                ? searchIndex(sequence, comparison, subseqLast, indexElem, indexMiddle + 1, indexEnd)
                : searchIndex(sequence, comparison, subseqLast, indexElem, indexBegin, indexMiddle);
        }
    }
}
