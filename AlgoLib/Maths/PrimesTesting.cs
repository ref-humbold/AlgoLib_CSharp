using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Maths;

/// <summary>Algorithms for testing prime numbers.</summary>
public static class PrimesTesting
{
    private const int Attempts = 17;
    private static readonly Random Random = new();

    #region TestPrimeFermat

    /// <summary>Checks whether given number is prime using Fermat prime test.</summary>
    /// <param name="number">The number.</param>
    /// <returns><c>true</c> if the number is probably prime, otherwise <c>false</c>.</returns>
    public static bool TestPrimeFermat(this int number)
    {
        number = Math.Abs(number);

        if(number == 2 || number == 3)
            return true;

        if(number < 2 || number % 2 == 0 || number % 3 == 0)
            return false;

        for(int i = 0; i < Attempts; ++i)
        {
            int witness = Random.Next(1, number - 1);

            if(Maths.Gcd(witness, number) > 1 || Maths.Power(witness, number - 1, number) != 1)
                return false;
        }

        return true;
    }

    /// <summary>Checks whether given number is prime using Fermat prime test.</summary>
    /// <param name="number">The number.</param>
    /// <returns><c>true</c> if the number is probably prime, otherwise <c>false</c>.</returns>
    public static bool TestPrimeFermat(this long number)
    {
        number = Math.Abs(number);

        if(number == 2 || number == 3)
            return true;

        if(number < 2 || number % 2 == 0 || number % 3 == 0)
            return false;

        for(int i = 0; i < Attempts; ++i)
        {
            long witness = Random.NextInt64(1, number - 1);

            if(Maths.Gcd(witness, number) > 1 || Maths.Power(witness, number - 1, number) != 1)
                return false;
        }

        return true;
    }

    #endregion
    #region TestPrimeMiller

    /// <summary>Checks whether given number is prime using Miller-Rabin prime test.</summary>
    /// <param name="number">The number.</param>
    /// <returns><c>true</c> if the number is probably prime, otherwise <c>false</c>.</returns>
    public static bool TestPrimeMiller(this int number)
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

    /// <summary>Checks whether given number is prime using Miller-Rabin prime test.</summary>
    /// <param name="number">The number.</param>
    /// <returns><c>true</c> if the number is probably prime, otherwise <c>false</c>.</returns>
    public static bool TestPrimeMiller(this long number)
    {
        number = Math.Abs(number);

        if(number == 2 || number == 3)
            return true;

        if(number < 2 || number % 2 == 0 || number % 3 == 0)
            return false;

        long multiplicand = number - 1;

        while(multiplicand % 2 == 0)
            multiplicand /= 2;

        for(int i = 0; i < Attempts; ++i)
        {
            long witness = Random.NextInt64(1, number - 1);

            if(Maths.Power(witness, multiplicand, number) != 1)
            {
                var exponents = new List<long>();

                for(long d = multiplicand; d <= number / 2; d *= 2)
                    exponents.Add(d);

                if(exponents.All(d => Maths.Power(witness, d, number) != number - 1))
                    return false;
            }
        }

        return true;
    }

    #endregion
}
