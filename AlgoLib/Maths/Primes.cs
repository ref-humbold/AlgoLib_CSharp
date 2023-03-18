// Algorithms for prime numbers
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Maths
{
    public static class Primes
    {
        private static readonly int attempts = 17;
        private static readonly Random random = new Random();

        /// <summary>Finds prime numbers less than given number.</summary>
        /// <param name="maximum">Maximal number, exclusive.</param>
        /// <returns>Enumerable of prime numbers.</returns>
        public static IEnumerable<int> FindPrimes(int maximum) => FindPrimes(0, maximum);

        /// <summary>Finds prime numbers inside given range of numbers.</summary>
        /// <param name="minimum">Minimal number, inclusive.</param>
        /// <param name="maximum">Maximal number, exclusive.</param>
        /// <returns>Enumerable of prime numbers.</returns>
        public static IEnumerable<int> FindPrimes(int minimum, int maximum)
        {
            if(maximum <= minimum)
                return Enumerable.Empty<int>();

            bool[] isPrime = Enumerable.Range(minimum, maximum - minimum)
                                       .Select(i => i == 2 || i > 2 && i % 2 != 0)
                                       .ToArray();
            bool[] basePrimes = Enumerable.Repeat(true, (int)(Math.Sqrt(maximum) / 2))
                                          .ToArray();

            for(int i = 0; i < basePrimes.Length; ++i)
                if(basePrimes[i])
                {
                    int basePrime = 2 * i + 3;
                    int square = basePrime * basePrime;
                    int begin = minimum < square
                        ? square - minimum
                        : (basePrime - minimum % basePrime) % basePrime;

                    for(int j = (square - 3) / 2; j < basePrimes.Length; j += basePrime)
                        basePrimes[j] = false;

                    for(int j = begin; j < isPrime.Length; j += basePrime)
                        isPrime[j] = false;
                }

            return isPrime.Select((flag, index) => (flag, index))
                          .Where(elem => elem.flag)
                          .Select(elem => minimum + elem.index);
        }

        /// <summary>Checks whether given number is prime using Fermat's prime test.</summary>
        /// <param name="number">Number to check.</param>
        /// <returns><c>true</c> if number is prime, otherwise <c>false</c>.</returns>
        public static bool TestFermat(int number)
        {
            number = Math.Abs(number);

            if(number == 2 || number == 3)
                return true;

            if(number < 2 || number % 2 == 0 || number % 3 == 0)
                return false;

            for(int i = 0; i < attempts; ++i)
            {
                int witness = random.Next(1, number - 1);

                if(Maths.GCD(witness, number) > 1 || Maths.Power(witness, number - 1, number) != 1)
                    return false;
            }

            return true;
        }

        /// <summary>Checks whether given number is prime using Miller-Rabin's prime test.</summary>
        /// <param name="number">Number to check.</param>
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

            for(int i = 0; i < attempts; ++i)
            {
                int witness = random.Next(1, number - 1);

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
    }
}
