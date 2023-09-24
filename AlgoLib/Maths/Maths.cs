// Algorithms for basic mathematical operations
using System;

namespace AlgoLib.Maths
{
    public static class Maths
    {
        #region GCD

        /// <summary>Calculates greatest common divisor of two numbers.</summary>
        /// <param name="number1">First number.</param>
        /// <param name="number2">Second number.</param>
        /// <returns>The greatest common divisor.</returns>
        public static int GCD(int number1, int number2)
        {
            (int Min, int Max) numberPair = (Math.Abs(Math.Min(number1, number2)),
                                             Math.Abs(Math.Max(number1, number2)));

            while(numberPair.Min > 0)
                numberPair = (numberPair.Max % numberPair.Min, numberPair.Min);

            return numberPair.Max;
        }

        /// <summary>Calculates greatest common divisor of two numbers.</summary>
        /// <param name="number1">First number.</param>
        /// <param name="number2">Second number.</param>
        /// <returns>The greatest common divisor.</returns>
        public static long GCD(long number1, long number2)
        {
            (long Min, long Max) numberPair = (Math.Abs(Math.Min(number1, number2)),
                                               Math.Abs(Math.Max(number1, number2)));

            while(numberPair.Min > 0)
                numberPair = (numberPair.Max % numberPair.Min, numberPair.Min);

            return numberPair.Max;
        }

        #endregion
        #region LCM

        /// <summary>Calculates lowest common multiple of two numbers.</summary>
        /// <param name="number1">First number.</param>
        /// <param name="number2">Second number.</param>
        /// <returns>The lowest common multiple.</returns>
        public static int LCM(int number1, int number2) =>
            Math.Max(number1, number2) / GCD(number1, number2) * Math.Min(number1, number2);

        /// <summary>Calculates lowest common multiple of two numbers.</summary>
        /// <param name="number1">First number.</param>
        /// <param name="number2">Second number.</param>
        /// <returns>The lowest common multiple.</returns>
        public static long LCM(long number1, long number2) =>
            Math.Max(number1, number2) / GCD(number1, number2) * Math.Min(number1, number2);

        #endregion
        #region Multiply

        /// <summary>Performs fast multiplication of two numbers.</summary>
        /// <param name="factor1">First factor.</param>
        /// <param name="factor2">Second factor.</param>
        /// <returns>The multiplication result.</returns>
        public static int Multiply(int factor1, int factor2)
        {
            int result = 0;

            if(factor1 < 0 && factor2 < 0)
                return Multiply(-factor1, -factor2);

            if(factor1 < 0)
                return -Multiply(-factor1, factor2);

            if(factor2 < 0)
                return -Multiply(factor1, -factor2);

            while(factor2 > 0)
            {
                if(factor2 % 2 == 1)
                    result += factor1;

                factor1 += factor1;
                factor2 /= 2;
            }

            return result;
        }

        /// <summary>Performs fast multiplication of two numbers.</summary>
        /// <param name="factor1">First factor.</param>
        /// <param name="factor2">Second factor.</param>
        /// <returns>The multiplication result.</returns>
        public static long Multiply(long factor1, long factor2)
        {
            long result = 0;

            if(factor1 < 0 && factor2 < 0)
                return Multiply(-factor1, -factor2);

            if(factor1 < 0)
                return -Multiply(-factor1, factor2);

            if(factor2 < 0)
                return -Multiply(factor1, -factor2);

            while(factor2 > 0)
            {
                if(factor2 % 2 == 1)
                    result += factor1;

                factor1 += factor1;
                factor2 /= 2;
            }

            return result;
        }

        /// <summary>Performs fast multiplication of two numbers with modulo taken.</summary>
        /// <param name="factor1">First factor.</param>
        /// <param name="factor2">Second factor.</param>
        /// <param name="modulo">A modulo.</param>
        /// <returns>The multiplication result with modulo taken.</returns>
        public static int Multiply(int factor1, int factor2, int modulo)
        {
            int result = 0;

            if(modulo <= 0)
                throw new ArithmeticException("Non-positive modulo");

            if(factor1 < 0 && factor2 < 0)
                return Multiply(-factor1, -factor2, modulo);

            if(factor1 < 0)
                return modulo - Multiply(-factor1, factor2, modulo);

            if(factor2 < 0)
                return modulo - Multiply(factor1, -factor2, modulo);

            while(factor2 > 0)
            {
                if(factor2 % 2 == 1)
                    result = (factor1 + result) % modulo;

                factor1 = (factor1 + factor1) % modulo;
                factor2 /= 2;
            }

            return result;
        }

        /// <summary>Performs fast multiplication of two numbers with modulo taken.</summary>
        /// <param name="factor1">First factor.</param>
        /// <param name="factor2">Second factor.</param>
        /// <param name="modulo">A modulo.</param>
        /// <returns>The multiplication result with modulo taken.</returns>
        public static long Multiply(long factor1, long factor2, long modulo)
        {
            long result = 0;

            if(modulo <= 0)
                throw new ArithmeticException("Non-positive modulo");

            if(factor1 < 0 && factor2 < 0)
                return Multiply(-factor1, -factor2, modulo);

            if(factor1 < 0)
                return modulo - Multiply(-factor1, factor2, modulo);

            if(factor2 < 0)
                return modulo - Multiply(factor1, -factor2, modulo);

            while(factor2 > 0)
            {
                if(factor2 % 2 == 1)
                    result = (factor1 + result) % modulo;

                factor1 = (factor1 + factor1) % modulo;
                factor2 /= 2;
            }

            return result;
        }

        #endregion
        #region Power

        /// <summary>Performs fast exponentiation of two numbers.</summary>
        /// <param name="baseNum">A base.</param>
        /// <param name="exponent">An exponent.</param>
        /// <returns>The exponentiation result.</returns>
        public static int Power(int baseNum, int exponent)
        {
            int result = 1;

            if(exponent < 0)
                throw new ArithmeticException("Negative exponent");

            if(baseNum == 0 && exponent == 0)
                throw new NotFiniteNumberException("Zero to the power of zero is NaN");

            while(exponent > 0)
            {
                if(exponent % 2 == 1)
                    result = Multiply(result, baseNum);

                baseNum = Multiply(baseNum, baseNum);
                exponent /= 2;
            }

            return result;
        }

        /// <summary>Performs fast exponentiation of two numbers.</summary>
        /// <param name="baseNum">A base.</param>
        /// <param name="exponent">An exponent.</param>
        /// <returns>The exponentiation result.</returns>
        public static long Power(long baseNum, long exponent)
        {
            long result = 1;

            if(exponent < 0)
                throw new ArithmeticException("Negative exponent");

            if(baseNum == 0 && exponent == 0)
                throw new NotFiniteNumberException("Zero to the power of zero is NaN");

            while(exponent > 0)
            {
                if(exponent % 2 == 1)
                    result = Multiply(result, baseNum);

                baseNum = Multiply(baseNum, baseNum);
                exponent /= 2;
            }

            return result;
        }

        /// <summary>Performs fast exponentiation of two numbers with modulo taken.</summary>
        /// <param name="baseNum">A base.</param>
        /// <param name="exponent">An exponent.</param>
        /// <param name="modulo">A modulo.</param>
        /// <returns>The exponentiation result with modulo taken.</returns>
        public static int Power(int baseNum, int exponent, int modulo)
        {
            int result = 1;

            if(modulo <= 0)
                throw new ArithmeticException("Non-positive modulo");

            if(exponent < 0)
                throw new ArithmeticException("Negative exponent");

            if(baseNum == 0 && exponent == 0)
                throw new NotFiniteNumberException("Zero to the power of zero is NaN");

            while(exponent > 0)
            {
                if(exponent % 2 == 1)
                    result = Multiply(result, baseNum, modulo);

                baseNum = Multiply(baseNum, baseNum, modulo);
                exponent /= 2;
            }

            return result;
        }

        /// <summary>Performs fast exponentiation of two numbers with modulo taken.</summary>
        /// <param name="baseNum">A base.</param>
        /// <param name="exponent">An exponent.</param>
        /// <param name="modulo">A modulo.</param>
        /// <returns>The exponentiation result with modulo taken.</returns>
        public static long Power(long baseNum, long exponent, long modulo)
        {
            long result = 1;

            if(modulo <= 0)
                throw new ArithmeticException("Non-positive modulo");

            if(exponent < 0)
                throw new ArithmeticException("Negative exponent");

            if(baseNum == 0 && exponent == 0)
                throw new NotFiniteNumberException("Zero to the power of zero is NaN");

            while(exponent > 0)
            {
                if(exponent % 2 == 1)
                    result = Multiply(result, baseNum, modulo);

                baseNum = Multiply(baseNum, baseNum, modulo);
                exponent /= 2;
            }

            return result;
        }

        #endregion
    }
}
