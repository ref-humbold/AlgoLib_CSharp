// Structure of fraction.
using System;

namespace AlgoLib.Maths
{
    public class Fraction :
        IComparable<Fraction>, IComparable<int>, IComparable<long>
    {
        private readonly long numerator;
        private readonly long denominator;

        private Fraction(long numerator, long denominator)
        {
            if(denominator == 0)
                throw new DivideByZeroException("Denominator cannot be zero");

            if(denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            long gcd = Maths.Gcd(numerator, denominator);

            this.numerator = numerator / gcd;
            this.denominator = denominator / gcd;
        }

        public static Fraction Of(long numerator, long denominator = 1) => new(numerator, denominator);

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
        public static implicit operator Fraction(int n) => Of(n);

        /// <summary>Performs an implicit conversion from <see cref="long" />.</summary>
        /// <param name="n">The long integer.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Fraction(long n) => Of(n);

        public static bool operator ==(Fraction f1, Fraction f2) => f1.Equals(f2);

        public static bool operator !=(Fraction f1, Fraction f2) => !(f1 == f2);

        public static bool operator <(Fraction f1, Fraction f2) => f1.CompareTo(f2) < 0;

        public static bool operator <=(Fraction f1, Fraction f2) => f1.CompareTo(f2) <= 0;

        public static bool operator >(Fraction f1, Fraction f2) => f1.CompareTo(f2) > 0;

        public static bool operator >=(Fraction f1, Fraction f2) => f1.CompareTo(f2) >= 0;

        /// <summary>Copies fraction.</summary>
        /// <param name="f">The fraction.</param>
        /// <returns>The copied fraction.</returns>
        public static Fraction operator +(Fraction f) => Of(+f.numerator, +f.denominator);

        /// <summary>Negates fraction.</summary>
        /// <param name="f">The fraction.</param>
        /// <returns>The negated fraction.</returns>
        public static Fraction operator -(Fraction f) => Of(-f.numerator, f.denominator);

        /// <summary>Inverts fraction.</summary>
        /// <param name="f">The fraction.</param>
        /// <returns>The inverted fraction.</returns>
        /// <exception cref="InvalidOperationException">If fraction is equal to zero.</exception>
        public static Fraction operator ~(Fraction f) =>
            f.numerator == 0
                ? throw new InvalidOperationException("Value of zero cannot be inverted")
                : Of(f.denominator, f.numerator);

        /// <summary>Adds two fractions.</summary>
        /// <param name="f1">The first fraction.</param>
        /// <param name="f2">The second fraction.</param>
        /// <returns>The result of the addition.</returns>
        public static Fraction operator +(Fraction f1, Fraction f2) => Of(
               f1.numerator * f2.denominator + f2.numerator * f1.denominator,
               f1.denominator * f2.denominator);

        /// <summary>Subtracts two fractions.</summary>
        /// <param name="f1">The first fraction.</param>
        /// <param name="f2">The second fraction.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Fraction operator -(Fraction f1, Fraction f2) => Of(
            f1.numerator * f2.denominator - f2.numerator * f1.denominator,
            f1.denominator * f2.denominator);

        /// <summary>Multiplies two fractions.</summary>
        /// <param name="f1">The first fraction.</param>
        /// <param name="f2">The second fraction.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Fraction operator *(Fraction f1, Fraction f2) =>
            Of(f1.numerator * f2.numerator, f1.denominator * f2.denominator);

        /// <summary>Divides two fractions.</summary>
        /// <param name="f1">The first fraction.</param>
        /// <param name="f2">The second fraction.</param>
        /// <returns>The result of the multiplication.</returns>
        /// <exception cref="DivideByZeroException">If the divisor is equal to zero.</exception>
        public static Fraction operator /(Fraction f1, Fraction f2) =>
            f2.numerator == 0
                ? throw new DivideByZeroException("Division by zero")
                : Of(f1.numerator * f2.denominator, f1.denominator * f2.numerator);

        public override bool Equals(object obj) =>
            obj is Fraction f && numerator == f.numerator && denominator == f.denominator;

        public override int GetHashCode() => (numerator, denominator).GetHashCode();

        public override string ToString() => $"{numerator}/{denominator}";

        public int CompareTo(Fraction other)
        {
            long lcm = Maths.Lcm(denominator, other.denominator);
            long thisNumerator = lcm / denominator * numerator;
            long otherNumerator = lcm / other.denominator * other.numerator;

            return thisNumerator.CompareTo(otherNumerator);
        }

        public int CompareTo(int other) => CompareTo(Of(other));

        public int CompareTo(long other) => CompareTo(Of(other));
    }
}
