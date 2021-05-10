// Algorithms for basic mathematics
using System;

namespace Algolib.Mathmat
{
    public sealed class Maths
    {
        /// <summary>Greatest common divisor of two numbers</summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>greatest common divisor</returns>
        public static long GCD(long number1, long number2)
        {
            (long Min, long Max) numberPair = (Math.Abs(Math.Min(number1, number2)),
                                       Math.Abs(Math.Max(number1, number2)));

            while(numberPair.Min > 0)
                numberPair = (numberPair.Max % numberPair.Min, numberPair.Min);

            return numberPair.Max;
        }

        /// <summary>Greatest common divisor of two numbers</summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>greatest common divisor</returns>
        public static int GCD(int number1, int number2)
        {
            (int Min, int Max) numberPair = (Math.Abs(Math.Min(number1, number2)),
                                       Math.Abs(Math.Max(number1, number2)));

            while(numberPair.Min > 0)
                numberPair = (numberPair.Max % numberPair.Min, numberPair.Min);

            return numberPair.Max;
        }

        /// <summary>Lowest common multiple of two numbers</summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>lowest common multiple</returns>
        public static long LCM(long number1, long number2) =>
            Math.Max(number1, number2) / GCD(number1, number2) * Math.Min(number1, number2);

        /// <summary>Lowest common multiple of two numbers</summary>
        /// <param name="number1">first number</param>
        /// <param name="number2">second number</param>
        /// <returns>lowest common multiple</returns>
        public static int LCM(int number1, int number2) =>
            Math.Max(number1, number2) / GCD(number1, number2) * Math.Min(number1, number2);

        /// <summary>Fast modulo multiplication</summary>
        /// <param name="factor1">multiplier</param>
        /// <param name="factor2">multiplicand</param>
        /// <param name="modulo">modulo</param>
        /// <returns>multiplication result taken modulo</returns>
        public static int MultMod(int factor1, int factor2, int modulo)
        {
            int result = 0;

            if(modulo < 0)
                throw new ArithmeticException("Negative modulo");

            if(factor1 < 0 && factor2 < 0)
                return MultMod(-factor1, -factor2, modulo);

            if(factor1 < 0)
                return modulo - MultMod(-factor1, factor2, modulo);

            if(factor2 < 0)
                return modulo - MultMod(factor1, -factor2, modulo);

            while(factor2 > 0)
            {
                if(factor2 % 2 == 1)
                    result = modulo == 0 ? factor1 + result : (factor1 + result) % modulo;

                factor1 = modulo == 0 ? factor1 + factor1 : (factor1 + factor1) % modulo;
                factor2 >>= 1;
            }

            return result;
        }

        /// <summary>Fast modulo multiplication</summary>
        /// <param name="factor1">multiplier</param>
        /// <param name="factor2">multiplicand</param>
        /// <param name="modulo">modulo</param>
        /// <returns>multiplication result taken modulo</returns>
        public static long MultMod(long factor1, long factor2, long modulo)
        {
            long result = 0;

            if(modulo < 0)
                throw new ArithmeticException("Negative modulo");

            if(factor1 < 0 && factor2 < 0)
                return MultMod(-factor1, -factor2, modulo);

            if(factor1 < 0)
                return modulo - MultMod(-factor1, factor2, modulo);

            if(factor2 < 0)
                return modulo - MultMod(factor1, -factor2, modulo);

            while(factor2 > 0)
            {
                if(factor2 % 2 == 1)
                    result = modulo == 0 ? factor1 + result : (factor1 + result) % modulo;

                factor1 = modulo == 0 ? factor1 + factor1 : (factor1 + factor1) % modulo;
                factor2 >>= 1;
            }

            return result;
        }

        /// <summary>Fast modulo exponentiation</summary>
        /// <param name="baseNum">base</param>
        /// <param name="exponent">exponent</param>
        /// <param name="modulo">modulo</param>
        /// <returns>exponentiation result taken modulo</returns>
        public static int PowerMod(int baseNum, int exponent, int modulo)
        {
            int result = 1;

            if(modulo < 0)
                throw new ArithmeticException("Negative modulo");

            if(exponent < 0)
                throw new ArithmeticException("Negative exponent");

            if(baseNum == 0 && exponent == 0)
                throw new NotFiniteNumberException("Zero to the power of zero is NaN");

            while(exponent > 0)
            {
                if(exponent % 2 == 1)
                    result = MultMod(result, baseNum, modulo);

                baseNum = MultMod(baseNum, baseNum, modulo);
                exponent >>= 1;
            }

            return result;
        }

        /// <summary>Fast modulo exponentiation</summary>
        /// <param name="baseNum">base</param>
        /// <param name="exponent">exponent</param>
        /// <param name="modulo">modulo</param>
        /// <returns>exponentiation result taken modulo</returns>
        public static long PowerMod(long baseNum, long exponent, long modulo)
        {
            long result = 1;

            if(modulo < 0)
                throw new ArithmeticException("Negative modulo");

            if(exponent < 0)
                throw new ArithmeticException("Negative exponent");

            if(baseNum == 0 && exponent == 0)
                throw new NotFiniteNumberException("Zero to the power of zero is NaN");

            while(exponent > 0)
            {
                if(exponent % 2 == 1)
                    result = MultMod(result, baseNum, modulo);

                baseNum = MultMod(baseNum, baseNum, modulo);
                exponent >>= 1;
            }

            return result;
        }
    }
}
