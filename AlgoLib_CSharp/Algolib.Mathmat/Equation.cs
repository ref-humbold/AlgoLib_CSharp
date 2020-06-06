// Structure of linear equation
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Mathmat
{
    public class Equation
    {
        public double[] Coefficients;
        public double Free;

        public int Count => Coefficients.Length;

        public Equation(double[] coefficients, double free)
        {
            Coefficients = coefficients;
            Free = free;
        }

        /// <param name="i">index of a variable</param>
        /// <returns>coefficient by i-th variable</returns>
        public double this[int i] => Coefficients[i];

        /// <returns>string representation of this equation</returns>
        public override string ToString()
        {
            IEnumerable<string> terms = Coefficients.Select((c, i) => c != 0 ? $"{c} x_{i}" : "")
                                                    .Where(s => !string.IsNullOrEmpty(s));

            return $"{string.Join(" + ", terms)} = {Free}";
        }

        /// <summary>Multiplies equation by a constant.</summary>
        /// <param name="constant">constant</param>
        /// <exception cref="ArithmeticException">if the constant is equal to zero</exception>
        public void Multiply(double constant)
        {
            if(constant == 0)
                throw new ArithmeticException("Constant cannot be zero");

            for(int i = 0; i < Coefficients.Length; ++i)
                Coefficients[i] *= constant;

            Free *= constant;
        }

        /// <summary>Transforms equation through a linear combination with another equation.</summary>
        /// <param name="equation">equation</param>
        /// <param name="constant">linear combination constant</param>
        /// <exception cref="ArgumentException">if equations sizes differ</exception>
        /// <exception cref="ArithmeticException">if the constant is equal to zero</exception>
        public void Combine(Equation equation, double constant = 1)
        {
            if(equation.Count != this.Count)
                throw new ArgumentException("Equation has different number of variables");

            if(constant == 0)
                throw new ArithmeticException("Constant cannot be zero");

            for(int i = 0; i < Coefficients.Length; ++i)
                Coefficients[i] += equation.Coefficients[i] * constant;

            Free += equation.Free * constant;
        }

        /// <summary>Checks whether specified values solve this equation.</summary>
        /// <param name="solution">values to check</param>
        /// <returns><c>true</c> if solution is correct, otherwise <c>false</c></returns>
        public bool IsSolution(double[] solution)
        {
            return solution.Length == Coefficients.Length
                && Coefficients.Zip(solution).Select(p => p.First * p.Second).Sum() == Free;
        }
    }
}
