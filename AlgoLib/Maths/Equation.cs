using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AlgoLib.Maths;

/// <summary>Structure of linear equation.</summary>
public class Equation
{
    public double[] Coefficients { get; }

    public double FreeTerm { get; }

    /// <summary>Gets the number of variables in this equation.</summary>
    /// <value>The number of variables.</value>
    public int Count => Coefficients.Length;

    public Equation(double[] coefficients, double freeTerm)
    {
        Coefficients = coefficients;
        FreeTerm = freeTerm;
    }

    /// <summary>Copies equation.</summary>
    /// <param name="eq">The equation.</param>
    /// <returns>The copy of the equation.</returns>
    public static Equation operator +(Equation eq) =>
        new(eq.Coefficients.Select(c => +c).ToArray(), +eq.FreeTerm);

    /// <summary>Negates equation.</summary>
    /// <param name="eq">The equation.</param>
    /// <returns>The equation with all coefficients negated.</returns>
    public static Equation operator -(Equation eq) =>
        new(eq.Coefficients.Select(c => -c).ToArray(), -eq.FreeTerm);

    /// <summary>Adds two equations.</summary>
    /// <param name="eq1">The first equation.</param>
    /// <param name="eq2">The second equation.</param>
    /// <returns>The equation with coefficients added.</returns>
    /// <exception cref="ArgumentException">If the equations have different number of variables.</exception>
    public static Equation operator +(Equation eq1, Equation eq2) =>
        eq1.Count != eq2.Count
            ? throw new ArgumentException("Equations have different number of variables")
            : new Equation(
                eq1.Coefficients.Zip(eq2.Coefficients, (c1, c2) => c1 + c2).ToArray(),
                eq1.FreeTerm + eq2.FreeTerm);

    /// <summary>Subtracts two equations.</summary>
    /// <param name="eq1">The first equation.</param>
    /// <param name="eq2">The second equation.</param>
    /// <returns>The equation with coefficients subtracted.</returns>
    /// <exception cref="ArgumentException">If the equations have different number of variables.</exception>
    public static Equation operator -(Equation eq1, Equation eq2) =>
        eq1.Count != eq2.Count
            ? throw new ArgumentException("Equations have different number of variables")
            : new Equation(
                eq1.Coefficients.Zip(eq2.Coefficients, (c1, c2) => c1 - c2).ToArray(),
                eq1.FreeTerm - eq2.FreeTerm);

    /// <summary>Multiplies equation by given constant.</summary>
    /// <param name="eq">The equation.</param>
    /// <param name="constant">The constant.</param>
    /// <returns>The equation with all coefficients multiplied.</returns>
    /// <exception cref="ArithmeticException">If the constant is equal to zero.</exception>
    public static Equation operator *(Equation eq, double constant) =>
        constant == 0
            ? throw new ArithmeticException("Constant cannot be zero")
            : new Equation(
                eq.Coefficients.Select(c => c * constant).ToArray(), eq.FreeTerm * constant);

    /// <summary>Multiplies equation by given constant.</summary>
    /// <param name="constant">The constant.</param>
    /// <param name="eq">The equation.</param>
    /// <returns>The equation with all coefficients multiplied.</returns>
    /// <exception cref="ArithmeticException">If the constant is equal to zero.</exception>
    public static Equation operator *(double constant, Equation eq) => eq * constant;

    /// <summary>Divides equation by given constant.</summary>
    /// <param name="eq">The equation.</param>
    /// <param name="constant">The constant.</param>
    /// <returns>The equation with all coefficients divided.</returns>
    /// <exception cref="ArithmeticException">If the constant is equal to zero.</exception>
    public static Equation operator /(Equation eq, double constant) =>
        constant == 0
            ? throw new ArithmeticException("Constant cannot be zero")
            : new Equation(
                eq.Coefficients.Select(c => c / constant).ToArray(), eq.FreeTerm / constant);

    public override string ToString()
    {
        IEnumerable<string> terms = Coefficients.Select((c, i) =>
                c != 0 ? $"{c.ToString(CultureInfo.InvariantCulture)} x_{i}" : string.Empty)
            .Where(s => !string.IsNullOrEmpty(s));

        return $"{string.Join(" + ", terms)} = {FreeTerm}";
    }

    /// <summary>Checks whether given values solve this equation.</summary>
    /// <param name="solution">The values.</param>
    /// <returns><c>true</c> if the solution is correct, otherwise <c>false</c>.</returns>
    public bool HasSolution(double[] solution) =>
        solution.Length == Coefficients.Length
            && Coefficients.Zip(solution, (c, s) => c * s).Sum() == FreeTerm;
}
