using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Mathmat
{
    public class Primes
    {
        public static IEnumerable<int> Find(int maxNumber)
        {
            return Primes.Find(0, maxNumber);
        }

        public static IEnumerable<int> Find(int minNumber, int maxNumber)
        {
            if(maxNumber < minNumber)
                throw new ArgumentException(
                    "Second argument must be grater or equal to the first argument.");

            List<int> primes = new List<int>();
            List<bool> is_prime = new List<bool>();
            List<bool> base_primes = Enumerable.Repeat(true, (int)(Math.Sqrt(maxNumber) / 2))
                                               .ToList();

            for(int i = minNumber; i <= maxNumber; ++i)
                is_prime.Add(i == 2 || (i > 2 && i % 2 != 0));

            for(int i = 0; i < base_primes.Count; ++i)
                if(base_primes[i])
                {
                    int p = 2 * i + 3;
                    int begin = minNumber < p * p ? p * p - minNumber : (p - minNumber % p) % p;

                    for(int j = (p * p - 3) / 2; j < base_primes.Count; j += p)
                        base_primes[j] = false;

                    for(int j = begin; j < is_prime.Count; j += p)
                        is_prime[j] = false;
                }

            for(int i = 0; i < is_prime.Count; ++i)
                if(is_prime[i])
                    primes.Add(minNumber + i);

            return primes;
        }

        /// <summary>Fermat primality test</summary>
        /// <param name="number">number to test</param>
        /// <returns><code>true</code> if number is prime, otherwise <code>false</code></returns>
        public static bool TestFermat(int number)
        {
            number = Math.Abs(number);

            if(number == 2 || number == 3)
                return true;

            if(number < 2 || number % 2 == 0 || number % 3 == 0)
                return false;

            Random rd = new Random();

            for(int i = 0; i < 12; ++i)
            {
                long rdv = rd.Next(1, number - 1);

                if(Maths.GCD(rdv, number) > 1 || Maths.PowerMod(rdv, number - 1, number) != 1)
                    return false;
            }

            return true;
        }

        /// <summary>Miller–Rabin primality test</summary>
        /// <param name="number">number to test</param>
        /// <returns><code>true</code> if number is prime, otherwise <code>false</code></returns>
        public static bool TestMiller(int number)
        {
            number = Math.Abs(number);

            if(number == 2 || number == 3)
                return true;

            if(number < 2 || number % 2 == 0 || number % 3 == 0)
                return false;

            int multip = number - 1;

            while(multip % 2 == 0)
                multip >>= 1;

            Random rd = new Random();

            for(int i = 0; i < 12; ++i)
            {
                long rdv = rd.Next(1, number - 1);

                if(Maths.PowerMod(rdv, multip, number) != 1)
                {
                    bool isComposite = true;

                    for(int d = multip; d <= number / 2; d <<= 1)
                    {
                        long pwm = Maths.PowerMod(rdv, d, number);

                        isComposite = isComposite && pwm != number - 1;
                    }

                    if(isComposite)
                        return false;
                }
            }

            return true;
        }
    }
}
