using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Sequences;

/// <summary>Algorithms for maximum subarray.</summary>
public static class MaximumSubarray
{
    /// <summary>Dynamically computes coherent subarray with maximal sum.</summary>
    /// <param name="sequence">The sequence of numbers.</param>
    /// <returns>The maximum subarray.</returns>
    public static List<double> FindMaximumSubarray(this IEnumerable<double> sequence)
    {
        (double Sum, List<double> Subarray) actual = (0.0, []);
        (double Sum, List<double> Subarray) maximal = (0.0, []);

        foreach(double elem in sequence)
        {
            if(actual.Sum < 0.0)
                actual = (0.0, []);

            actual = (actual.Sum + elem, actual.Subarray);
            actual.Subarray.Add(elem);

            if(actual.Sum > maximal.Sum)
                maximal = (actual.Sum, [.. actual.Subarray]);
        }

        return maximal.Subarray;
    }

    /// <summary>Computes maximal sum from all coherent subarrays using interval tree.</summary>
    /// <param name="sequence">The sequence of numbers.</param>
    /// <returns>The sum of maximum subarray.</returns>
    public static double CountMaximalSubsum(this IEnumerable<double> sequence)
    {
        var size = 1;
        List<double> sequenceList = sequence.ToList();

        while(size < 2 * sequenceList.Count)
            size *= 2;

        List<double> intervalSums = Enumerable.Repeat(0.0, size).ToList();
        List<double> prefixSums = Enumerable.Repeat(0.0, size).ToList();
        List<double> suffixSums = Enumerable.Repeat(0.0, size).ToList();
        List<double> allSums = Enumerable.Repeat(0.0, size).ToList();

        var i = 0;

        foreach(double elem in sequenceList)
        {
            int index = size / 2 + i;

            allSums[index] += elem;
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
