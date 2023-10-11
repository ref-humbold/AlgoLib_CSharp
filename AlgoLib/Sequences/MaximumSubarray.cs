// Algorithms for maximum subarray.
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Sequences;

public static class MaximumSubarray
{
    /// <summary>Dynamically computes coherent subarray with maximal sum.</summary>
    /// <param name="sequence">The sequence of numbers.</param>
    /// <returns>The maximum subarray.</returns>
    public static List<double> FindMaximumSubarray(this IEnumerable<double> sequence)
    {
        (double Sum, List<double> Subarray) actual = (0.0, new List<double>());
        (double Sum, List<double> Subarray) maximal = (0.0, new List<double>());

        foreach(double elem in sequence)
        {
            if(actual.Sum < 0.0)
                actual = (0.0, new List<double>());

            actual = (actual.Sum + elem, actual.Subarray);
            actual.Subarray.Add(elem);

            if(actual.Sum > maximal.Sum)
                maximal = (actual.Sum, new List<double>(actual.Subarray));
        }

        return maximal.Subarray;
    }

    /// <summary>Computes maximal sum from all coherent subarrays using interval tree.</summary>
    /// <param name="sequence">The sequence of numbers.</param>
    /// <returns>The sum of maximum subarray.</returns>
    public static double CountMaximalSubsum(this IEnumerable<double> sequence)
    {
        int size = 1;

        while(size < 2 * sequence.Count())
            size *= 2;

        var intervalSums = Enumerable.Repeat(0.0, size).ToList();
        var prefixSums = Enumerable.Repeat(0.0, size).ToList();
        var suffixSums = Enumerable.Repeat(0.0, size).ToList();
        var allSums = Enumerable.Repeat(0.0, size).ToList();

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
                        suffixSums[indexRight] + prefixSums[indexLeft]);
                prefixSums[index] = Math.Max(
                    prefixSums[indexRight], allSums[indexRight] + prefixSums[indexLeft]);
                suffixSums[index] = Math.Max(
                    suffixSums[indexLeft], suffixSums[indexRight] + allSums[indexLeft]);
                allSums[index] = allSums[indexRight] + allSums[indexLeft];
                index /= 2;
            }
        }

        return intervalSums[1];
    }
}
