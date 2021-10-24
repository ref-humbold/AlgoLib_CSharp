// Algorithms for prime numbers
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Mathmat
{
    public static class Primes
    {
        private const int attempts = 17;
        private static readonly Random random = new Random();

        public static IEnumerable<int> Find(int maxNumber) => Find(0, maxNumber);

        public static IEnumerable<int> Find(int minNumber, int maxNumber)
        {
            if(maxNumber < minNumber)
                return Enumerable.Empty<int>();

            List<int> primes = new List<int>();
            List<bool> isPrime = new List<bool>();
            List<bool> basePrimes = Enumerable.Repeat(true, (int)(Math.Sqrt(maxNumber) / 2)).ToList();

            for(int i = minNumber; i < maxNumber; ++i)
                isPrime.Add(i == 2 || i > 2 && i % 2 != 0);

            for(int i = 0; i < basePrimes.Count; ++i)
                if(basePrimes[i])
                {
                    int p = 2 * i + 3;
                    int begin = minNumber < p * p ? p * p - minNumber : (p - minNumber % p) % p;

                    for(int j = (p * p - 3) / 2; j < basePrimes.Count; j += p)
                        basePrimes[j] = false;

                    for(int j = begin; j < isPrime.Count; j += p)
                        isPrime[j] = false;
                }

            for(int i = 0; i < isPrime.Count; ++i)
                if(isPrime[i])
                    primes.Add(minNumber + i);

            return primes;
        }

        /// <summary>Performs Fermat primality test.</summary>
        /// <param name="number">Number to test</param>
        /// <returns><c>true</c> if number is prime, otherwise <c>false</c></returns>
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

        /// <summary>Performs Miller–Rabin primality test.</summary>
        /// <param name="number">Number to test</param>
        /// <returns><c>true</c> if number is prime, otherwise <c>false</c></returns>
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
                    List<int> exponents = new List<int>();

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
