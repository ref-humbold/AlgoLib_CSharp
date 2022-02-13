// Structure of linear equation
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Maths
{
    public class Equation
    {
        public double[] Coefficients;
        public double Free;

        /// <summary>Number of coefficients.</summary>
        public int Count => Coefficients.Length;

        public Equation(double[] coefficients, double free)
        {
            Coefficients = coefficients;
            Free = free;
        }

        /// <param name="i">Index of a variable</param>
        /// <returns>Coefficient by i-th variable</returns>
        public double this[Index i] => Coefficients[i];

        /// <returns>String representation of this equation.</returns>
        public override string ToString()
        {
            IEnumerable<string> terms = Coefficients.Select((c, i) => c != 0 ? $"{c} x_{i}" : "")
                                                    .Where(s => !string.IsNullOrEmpty(s));

            return $"{string.Join(" + ", terms)} = {Free}";
        }

        /// <summary>Multiplies this equation by given constant.</summary>
        /// <param name="constant">The constant.</param>
        /// <exception cref="ArithmeticException">If constant is equal to zero.</exception>
        public void Multiply(double constant)
        {
            if(constant == 0)
                throw new ArithmeticException("Constant cannot be zero");

            for(int i = 0; i < Coefficients.Length; ++i)
                Coefficients[i] *= constant;

            Free *= constant;
        }

        /// <summary>Transforms this equation through linear combination with another equation.</summary>
        /// <param name="equation">The equation.</param>
        /// <param name="constant">The linear combination constant.</param>
        /// <exception cref="ArgumentException">If equations sizes are different.</exception>
        /// <exception cref="ArithmeticException">If constant is equal to zero.</exception>
        public void Combine(Equation equation, double constant = 1)
        {
            if(equation.Count != Count)
                throw new ArgumentException("Equation has different number of variables");

            if(constant == 0)
                throw new ArithmeticException("Constant cannot be zero");

            for(int i = 0; i < Coefficients.Length; ++i)
                Coefficients[i] += equation.Coefficients[i] * constant;

            Free += equation.Free * constant;
        }

        /// <summary>Checks whether given values solve this equation.</summary>
        /// <param name="solution">Values to check.</param>
        /// <returns><c>true</c> if solution is correct, otherwise <c>false</c>.</returns>
        public bool IsSolution(double[] solution) =>
            solution.Length == Coefficients.Length
                && Coefficients.Zip(solution).Select(p => p.First * p.Second).Sum() == Free;
    }
}
