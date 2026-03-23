using System;

namespace AlgoLib.Maths;

/// <summary>Structure of fraction.</summary>
public readonly record struct Fraction :
    IComparable<Fraction>, IComparable<int>, IComparable<long>
{
    private readonly long numerator;
    private readonly long denominator;

    public Fraction()
        : this(0)
    {
    }

    public Fraction(long numerator, long denominator = 1)
    {
        switch(denominator)
        {
            case 0:
                throw new DivideByZeroException("Denominator cannot be zero");

            case < 0:
                numerator = -numerator;
                denominator = -denominator;
                break;
        }

        long gcd = Integers.Gcd(numerator, denominator);

        this.numerator = numerator / gcd;
        this.denominator = denominator / gcd;
    }

    /// <summary>Performs an explicit conversion to <see cref="int" />.</summary>
    /// <param name="f">The fraction.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator int(Fraction f) => (int)(f.numerator / f.denominator);

    /// <summary>Performs an explicit conversion to <see cref="long" />.</summary>
    /// <param name="f">The fraction.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator long(Fraction f) => f.numerator / f.denominator;

    /// <summary>Performs an explicit conversion to <see cref="float" />.</summary>
    /// <param name="f">The fraction.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator float(Fraction f) => 1.0f * f.numerator / f.denominator;

    /// <summary>Performs an explicit conversion to <see cref="double" />.</summary>
    /// <param name="f">The fraction.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator double(Fraction f) => 1.0 * f.numerator / f.denominator;

    /// <summary>Performs an explicit conversion to <see cref="decimal" />.</summary>
    /// <param name="f">The fraction.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator decimal(Fraction f) => 1.0m * f.numerator / f.denominator;

    /// <summary>Performs an implicit conversion from <see cref="int" />.</summary>
    /// <param name="n">The integer.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Fraction(int n) => new(n);

    /// <summary>Performs an implicit conversion from <see cref="long" />.</summary>
    /// <param name="n">The long integer.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Fraction(long n) => new(n);

    public static bool operator <(Fraction f1, Fraction f2) => f1.CompareTo(f2) < 0;

    public static bool operator <=(Fraction f1, Fraction f2) => f1.CompareTo(f2) <= 0;

    public static bool operator >(Fraction f1, Fraction f2) => f1.CompareTo(f2) > 0;

    public static bool operator >=(Fraction f1, Fraction f2) => f1.CompareTo(f2) >= 0;

    /// <summary>Copies fraction.</summary>
    /// <param name="f">The fraction.</param>
    /// <returns>The copied fraction.</returns>
    public static Fraction operator +(Fraction f)
    {
        long denominator1 = +f.denominator;
        return new Fraction(+f.numerator, denominator1);
    }

    /// <summary>Negates fraction.</summary>
    /// <param name="f">The fraction.</param>
    /// <returns>The negated fraction.</returns>
    public static Fraction operator -(Fraction f) => new(-f.numerator, f.denominator);

    /// <summary>Inverts fraction.</summary>
    /// <param name="f">The fraction.</param>
    /// <returns>The inverted fraction.</returns>
    /// <exception cref="InvalidOperationException">If the fraction is equal to zero.</exception>
    public static Fraction operator ~(Fraction f) =>
        f.numerator == 0
            ? throw new InvalidOperationException("Value of zero cannot be inverted")
            : new Fraction(f.denominator, f.numerator);

    /// <summary>Adds two fractions.</summary>
    /// <param name="f1">The first fraction.</param>
    /// <param name="f2">The second fraction.</param>
    /// <returns>The result of addition.</returns>
    public static Fraction operator +(Fraction f1, Fraction f2)
    {
        long denominator1 = f1.denominator * f2.denominator;
        return new Fraction(
            f1.numerator * f2.denominator + f2.numerator * f1.denominator, denominator1);
    }

    /// <summary>Subtracts two fractions.</summary>
    /// <param name="f1">The first fraction.</param>
    /// <param name="f2">The second fraction.</param>
    /// <returns>The result of subtraction.</returns>
    public static Fraction operator -(Fraction f1, Fraction f2)
    {
        long denominator1 = f1.denominator * f2.denominator;
        return new Fraction(
            f1.numerator * f2.denominator - f2.numerator * f1.denominator, denominator1);
    }

    /// <summary>Multiplies two fractions.</summary>
    /// <param name="f1">The first fraction.</param>
    /// <param name="f2">The second fraction.</param>
    /// <returns>The result of multiplication.</returns>
    public static Fraction operator *(Fraction f1, Fraction f2)
    {
        long denominator1 = f1.denominator * f2.denominator;
        return new Fraction(f1.numerator * f2.numerator, denominator1);
    }

    /// <summary>Divides two fractions.</summary>
    /// <param name="f1">The first fraction.</param>
    /// <param name="f2">The second fraction.</param>
    /// <returns>The result of division.</returns>
    /// <exception cref="DivideByZeroException">If the divisor is equal to zero.</exception>
    public static Fraction operator /(Fraction f1, Fraction f2)
    {
        long denominator1 = f1.denominator * f2.numerator;

        return f2.numerator == 0
            ? throw new DivideByZeroException("Division by zero")
            : new Fraction(f1.numerator * f2.denominator, denominator1);
    }

    public override string ToString() => $"{numerator}/{denominator}";

    public int CompareTo(Fraction other)
    {
        long lcm = Integers.Lcm(denominator, other.denominator);
        long thisNumerator = lcm / denominator * numerator;
        long otherNumerator = lcm / other.denominator * other.numerator;

        return thisNumerator.CompareTo(otherNumerator);
    }

    public int CompareTo(int other) => CompareTo(new Fraction(other));

    public int CompareTo(long other) => CompareTo(new Fraction(other));
}
