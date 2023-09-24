// Structure of linear equation
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AlgoLib.Maths
{
    public class Equation
    {
        private readonly double[] coefficients;

        public double FreeTerm { get; }

        /// <summary>Gets the number of coefficients.</summary>
        /// <value>The number of coefficients.</value>
        public int Count => coefficients.Length;

        public Equation(double[] coefficients, double freeTerm)
        {
            this.coefficients = coefficients;
            FreeTerm = freeTerm;
        }

        /// <summary>Gets the coefficient by given variable.</summary>
        /// <param name="i">The index of a variable.</param>
        /// <returns>The coefficient specified by given index.</returns>
        public double this[Index i] => coefficients[i];

        /// <summary>Copies equation.</summary>
        /// <param name="eq">The equation.</param>
        /// <returns>The copy of the equation.</returns>
        public static Equation operator +(Equation eq) =>
            new(eq.coefficients.Select(c => +c).ToArray(), +eq.FreeTerm);

        /// <summary>Negates equation.</summary>
        /// <param name="eq">The equation.</param>
        /// <returns>The equation with all coefficients negated.</returns>
        public static Equation operator -(Equation eq) =>
            new(eq.coefficients.Select(c => -c).ToArray(), -eq.FreeTerm);

        /// <summary>Adds two equations.</summary>
        /// <param name="eq1">First equation.</param>
        /// <param name="eq2">Second equation.</param>
        /// <returns>The equation with coefficients added.</returns>
        /// <exception cref="ArgumentException">If equations have different number of variables.</exception>
        public static Equation operator +(Equation eq1, Equation eq2) =>
            eq1.Count != eq2.Count
                ? throw new ArgumentException("Equations have different number of variables")
                : new Equation(
                    eq1.coefficients.Zip(eq2.coefficients, (c1, c2) => c1 + c2).ToArray(),
                    eq1.FreeTerm + eq2.FreeTerm);

        /// <summary>Subtracts two equations.</summary>
        /// <param name="eq1">First equation.</param>
        /// <param name="eq2">Second equation.</param>
        /// <returns>The equation with coefficients subtracted.</returns>
        /// <exception cref="ArgumentException">If equations have different number of variables.</exception>
        public static Equation operator -(Equation eq1, Equation eq2) =>
            eq1.Count != eq2.Count
                ? throw new ArgumentException("Equations have different number of variables")
                : new Equation(
                    eq1.coefficients.Zip(eq2.coefficients, (c1, c2) => c1 - c2).ToArray(),
                    eq1.FreeTerm - eq2.FreeTerm);

        /// <summary>Multiplies equation by given constant.</summary>
        /// <param name="eq">The equation.</param>
        /// <param name="constant">The constant.</param>
        /// <returns>The equation with all coefficients multiplied.</returns>
        /// <exception cref="ArithmeticException">If constant is equal to zero.</exception>
        public static Equation operator *(Equation eq, double constant) =>
            constant == 0
                ? throw new ArithmeticException("Constant cannot be zero")
                : new Equation(
                    eq.coefficients.Select(c => c * constant).ToArray(), eq.FreeTerm * constant);

        /// <summary>Multiplies equation by given constant.</summary>
        /// <param name="constant">The constant.</param>
        /// <param name="eq">The equation.</param>
        /// <returns>The equation with all coefficients multiplied.</returns>
        /// <exception cref="ArithmeticException">If constant is equal to zero.</exception>
        public static Equation operator *(double constant, Equation eq) => eq * constant;

        /// <summary>Divides equation by given constant.</summary>
        /// <param name="eq">The equation.</param>
        /// <param name="constant">The constant.</param>
        /// <returns>The equation with all coefficients divided.</returns>
        /// <exception cref="ArithmeticException">If constant is equal to zero.</exception>
        public static Equation operator /(Equation eq, double constant) =>
            constant == 0
                ? throw new ArithmeticException("Constant cannot be zero")
                : new Equation(
                    eq.coefficients.Select(c => c / constant).ToArray(), eq.FreeTerm / constant);

        public override string ToString()
        {
            IEnumerable<string> terms = coefficients.Select((c, i) =>
                    c != 0 ? $"{c.ToString(CultureInfo.InvariantCulture)} x_{i}" : string.Empty)
                .Where(s => !string.IsNullOrEmpty(s));

            return $"{string.Join(" + ", terms)} = {FreeTerm}";
        }

        /// <summary>Checks whether given values solve this equation.</summary>
        /// <param name="solution">Values.</param>
        /// <returns><c>true</c> if solution is correct, otherwise <c>false</c>.</returns>
        public bool HasSolution(double[] solution) =>
            solution.Length == coefficients.Length
                && coefficients.Zip(solution, (c, s) => c * s).Sum() == FreeTerm;
    }
}
