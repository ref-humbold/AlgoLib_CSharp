// Algorithms for prime numbers
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Maths
{
    public static class Primes
    {
        private const int Attempts = 17;
        private static readonly Random Random = new();

        /// <summary>Finds prime numbers less than given number.</summary>
        /// <param name="maximum">Maximal number, exclusive.</param>
        /// <returns>The enumerable of prime numbers.</returns>
        public static IEnumerable<int> FindPrimes(int maximum) => FindPrimes(0, maximum);

        /// <summary>Finds prime numbers inside given range of numbers.</summary>
        /// <param name="minimum">Minimal number, inclusive.</param>
        /// <param name="maximum">Maximal number, exclusive.</param>
        /// <returns>The enumerable of prime numbers.</returns>
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

        /// <summary>Checks whether given number is prime using Fermat's prime test.</summary>
        /// <param name="number">A number.</param>
        /// <returns><c>true</c> if number is prime, otherwise <c>false</c>.</returns>
        public static bool TestFermat(int number)
        {
            number = Math.Abs(number);

            if(number == 2 || number == 3)
                return true;

            if(number < 2 || number % 2 == 0 || number % 3 == 0)
                return false;

            for(int i = 0; i < Attempts; ++i)
            {
                int witness = Random.Next(1, number - 1);

                if(Maths.GCD(witness, number) > 1 || Maths.Power(witness, number - 1, number) != 1)
                    return false;
            }

            return true;
        }

        /// <summary>Checks whether given number is prime using Miller-Rabin's prime test.</summary>
        /// <param name="number">A number.</param>
        /// <returns><c>true</c> if number is prime, otherwise <c>false</c>.</returns>
        public static bool TestMiller(int number)
        {
            number = Math.Abs(number);

            if(number == 2 || number == 3)
                return true;

            if(number < 2 || number % 2 == 0 || number % 3 == 0)
                return false;

            int multiplicand = number - 1;

            while(multiplicand % 2 == 0)
                multiplicand /= 2;

            for(int i = 0; i < Attempts; ++i)
            {
                int witness = Random.Next(1, number - 1);

                if(Maths.Power(witness, multiplicand, number) != 1)
                {
                    var exponents = new List<int>();

                    for(int d = multiplicand; d <= number / 2; d *= 2)
                        exponents.Add(d);

                    if(exponents.All(d => Maths.Power(witness, d, number) != number - 1))
                        return false;
                }
            }

            return true;
        }

        // Extracts prime numbers between 0 and given maximum value
        private static IEnumerable<int> getBasePrimes(int baseMaximum)
        {
            bool[] isPrime = Enumerable.Repeat(true, (baseMaximum - 1) / 2).ToArray();

            for(int i = 0; i < (int)(Math.Sqrt(baseMaximum) / 2); ++i)
                if(isPrime[i])
                {
                    int primeValue = 2 * i + 3;

                    for(int j = primeValue * primeValue; j < baseMaximum; j += 2 * primeValue)
                        isPrime[(j - 3) / 2] = false;
                }

            return isPrime.Select((flag, index) => (Flag: flag, Value: 2 * index + 3))
                          .Where(elem => elem.Flag)
                          .Select(elem => elem.Value);
        }

        // Extracts prime numbers from given range using given basic prime numbers
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
}
