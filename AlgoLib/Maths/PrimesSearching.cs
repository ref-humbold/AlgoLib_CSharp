using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Maths;

/// <summary>Algorithms for searching for prime numbers.</summary>
public static class PrimesSearching
{
    /// <summary>Searches for prime numbers less than given number.</summary>
    /// <param name="maximum">The maximal number, exclusive.</param>
    /// <returns>The prime numbers.</returns>
    public static IEnumerable<int> FindPrimes(int maximum) => FindPrimes(0, maximum);

    /// <summary>Searches for prime numbers inside given range of numbers.</summary>
    /// <param name="minimum">The minimal number, inclusive.</param>
    /// <param name="maximum">The maximal number, exclusive.</param>
    /// <returns>The prime numbers.</returns>
    public static IEnumerable<int> FindPrimes(int minimum, int maximum)
    {
        if(maximum <= minimum || maximum <= 2)
            return Enumerable.Empty<int>();

        int segmentSize = (int)Math.Sqrt(maximum);
        int[] basePrimes = getBasePrimes(segmentSize).ToArray();
        var primes = new List<int>();

        if(minimum < segmentSize)
            primes.AddRange(Enumerable.Range(2, 1)
                  .Concat(basePrimes)
                  .Where(p => p >= minimum));

        for(int i = Math.Max(minimum, segmentSize); i < maximum; i += segmentSize)
            primes.AddRange(getSegmentPrimes(i, Math.Min(i + segmentSize, maximum), basePrimes));

        return primes;
    }

    // Extracts prime numbers between zero and given maximum value.
    private static IEnumerable<int> getBasePrimes(int baseMaximum)
    {
        bool[] isPrime = Enumerable.Repeat(true, (baseMaximum - 1) / 2).ToArray();

        for(int i = 0; i < (int)(Math.Sqrt(baseMaximum) / 2); ++i)
            if(isPrime[i])
            {
                int primeValue = 2 * i + 3;

                for(int j = primeValue * primeValue; j <= baseMaximum; j += 2 * primeValue)
                    isPrime[(j - 3) / 2] = false;
            }

        return isPrime.Select((flag, index) => (Flag: flag, Value: 2 * index + 3))
                      .Where(elem => elem.Flag)
                      .Select(elem => elem.Value);
    }

    // Extracts prime numbers from given range using given basic prime numbers.
    private static IEnumerable<int> getSegmentPrimes(
        int segmentStart, int segmentEnd, IEnumerable<int> basePrimes)
    {
        int segmentBegin = segmentStart + 1 - segmentStart % 2;
        bool[] isPrime = Enumerable.Range(segmentBegin, segmentEnd - segmentBegin)
                                   .Where(i => i % 2 == 1)
                                   .Select(i => i > 2)
                                   .ToArray();

        foreach(int p in basePrimes)
        {
            int primeMultiple = (segmentBegin + p - 1) / p * p;
            int multipleStart = primeMultiple % 2 == 0 ? primeMultiple + p : primeMultiple;

            for(int i = multipleStart; i < segmentEnd; i += 2 * p)
                isPrime[(i - segmentBegin) / 2] = false;
        }

        return isPrime.Select((flag, index) => (Flag: flag, Value: segmentBegin + 2 * index))
                      .Where(elem => elem.Flag)
                      .Select(elem => elem.Value);
    }
}
