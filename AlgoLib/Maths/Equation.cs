// Structure of linear equation
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AlgoLib.Maths
{
    public class Equation
    {
        public double[] Coefficients { get; set; }

        public double Free { get; set; }

        /// <summary>Gets number of coefficients.</summary>
        public int Count => Coefficients.Length;

        public Equation(double[] coefficients, double free)
        {
            Coefficients = coefficients;
            Free = free;
        }

        /// <summary>Gets coefficient by given variable.</summary>
        /// <param name="i">Index of a variable.</param>
        /// <returns>Coefficient by i-th variable.</returns>
        public double this[Index i] => Coefficients[i];

        public static Equation operator +(Equation eq) =>
            new(eq.Coefficients.Select(c => +c).ToArray(), +eq.Free);

        public static Equation operator -(Equation eq) =>
            new(eq.Coefficients.Select(c => -c).ToArray(), -eq.Free);

        /// <summary>Adds two equations.</summary>
        /// <param name="eq1">First equation.</param>
        /// <param name="eq2">Second equation.</param>
        /// <returns>Equation with coefficients and free factor added.</returns>
        /// <exception cref="ArgumentException">If equations have different number of variables.</exception>
        public static Equation operator +(Equation eq1, Equation eq2) =>
            eq1.Count != eq2.Count
                ? throw new ArgumentException("Equations have different number of variables")
                : new Equation(
                eq1.Coefficients.Zip(eq2.Coefficients, (c1, c2) => c1 + c2).ToArray(),
                eq1.Free + eq2.Free);

        /// <summary>Subtracts two equations.</summary>
        /// <param name="eq1">First equation.</param>
        /// <param name="eq2">Second equation.</param>
        /// <returns>Equation with coefficients and free factor subtracted.</returns>
        /// <exception cref="ArgumentException">If equations have different number of variables.</exception>
        public static Equation operator -(Equation eq1, Equation eq2) =>
            eq1.Count != eq2.Count
                ? throw new ArgumentException("Equations have different number of variables")
                : new Equation(
                eq1.Coefficients.Zip(eq2.Coefficients, (c1, c2) => c1 - c2).ToArray(),
                eq1.Free - eq2.Free);

        /// <summary>Multiplies equation by given constant.</summary>
        /// <param name="eq">The equation.</param>
        /// <param name="constant">The constant.</param>
        /// <returns>Equation with all coefficients and free factor multiplied.</returns>
        /// <exception cref="ArithmeticException">If constant is equal to zero.</exception>
        public static Equation operator *(Equation eq, double constant) =>
            constant == 0
                ? throw new ArithmeticException("Constant cannot be zero")
                : new Equation(eq.Coefficients.Select(c => c * constant).ToArray(),
                               eq.Free * constant);

        /// <summary>Multiplies equation by given constant.</summary>
        /// <param name="eq">The equation.</param>
        /// <param name="constant">The constant.</param>
        /// <returns>Equation with all coefficients and free factor multiplied.</returns>
        /// <exception cref="ArithmeticException">If constant is equal to zero.</exception>
        public static Equation operator *(double constant, Equation eq) => eq * constant;

        /// <summary>Divides equation by given constant.</summary>
        /// <param name="eq">The equation.</param>
        /// <param name="constant">The constant.</param>
        /// <returns>Equation with all coefficients and free factor divided.</returns>
        /// <exception cref="ArithmeticException">If constant is equal to zero.</exception>
        public static Equation operator /(Equation eq, double constant) =>
            constant == 0
                ? throw new ArithmeticException("Constant cannot be zero")
                : new Equation(eq.Coefficients.Select(c => c / constant).ToArray(),
                               eq.Free / constant);

        public override string ToString()
        {
            IEnumerable<string> terms = Coefficients.Select((c, i) =>
                c != 0 ? $"{c.ToString(CultureInfo.InvariantCulture)} x_{i}" : string.Empty)
                                                    .Where(s => !string.IsNullOrEmpty(s));

            return $"{string.Join(" + ", terms)} = {Free}";
        }

        /// <summary>Transforms this equation through linear combination with another equation.</summary>
        /// <param name="equation">The equation.</param>
        /// <param name="constant">The linear combination constant.</param>
        /// <exception cref="ArgumentException">If equations sizes are different.</exception>
        /// <exception cref="ArithmeticException">If constant is equal to zero.</exception>
        public void Combine(Equation equation, double constant)
        {
            if(equation.Count != Count)
                throw new ArgumentException("Equations have different number of variables");

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
