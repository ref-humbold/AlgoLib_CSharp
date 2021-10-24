// Structure of fraction
using System;

namespace AlgoLib.Mathmat
{
    public class Fraction :
        IComparable<Fraction>, IComparable<double>, IComparable<int>, IComparable<long>
    {
        private readonly long numerator, denominator;

        private Fraction(long numerator, long denominator)
        {
            if(denominator == 0)
                throw new DivideByZeroException("Denominator cannot be equal to zero");

            if(denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            long gcd = Maths.GCD(numerator, denominator);

            this.numerator = numerator / gcd;
            this.denominator = denominator / gcd;
        }

        public static Fraction Of(long numerator, long denominator = 1) => new Fraction(numerator, denominator);

        public static bool operator ==(Fraction f1, Fraction f2) => f1.Equals(f2);

        public static bool operator !=(Fraction f1, Fraction f2) => !(f1 == f2);

        public static bool operator <(Fraction f1, Fraction f2) => f1.CompareTo(f2) < 0;

        public static bool operator <=(Fraction f1, Fraction f2) => f1.CompareTo(f2) <= 0;

        public static bool operator >(Fraction f1, Fraction f2) => f1.CompareTo(f2) > 0;

        public static bool operator >=(Fraction f1, Fraction f2) => f1.CompareTo(f2) >= 0;

        public static bool operator <(Fraction f, double d) => f.CompareTo(d) < 0;

        public static bool operator <=(Fraction f, double d) => f.CompareTo(d) <= 0;

        public static bool operator >(Fraction f, double d) => f.CompareTo(d) > 0;

        public static bool operator >=(Fraction f, double d) => f.CompareTo(d) >= 0;

        public static Fraction operator +(Fraction f1) => Of(+f1.numerator, +f1.denominator);

        public static Fraction operator -(Fraction f1) => Of(-f1.numerator, f1.denominator);

        public static Fraction operator ~(Fraction f) =>
            f.numerator == 0
                ? throw new InvalidOperationException("Value of zero cannot be inverted")
                : Of(f.denominator, f.numerator);

        public static Fraction operator +(Fraction f1, Fraction f2) =>
           Of(f1.numerator * f2.denominator + f2.numerator * f1.denominator,
              f1.denominator * f2.denominator);

        public static Fraction operator -(Fraction f1, Fraction f2) =>
            Of(f1.numerator * f2.denominator - f2.numerator * f1.denominator,
               f1.denominator * f2.denominator);

        public static Fraction operator *(Fraction f1, Fraction f2) =>
            Of(f1.numerator * f2.numerator, f1.denominator * f2.denominator);

        public static Fraction operator /(Fraction f1, Fraction f2) =>
            f2.numerator == 0
                ? throw new DivideByZeroException("Division by zero")
                : Of(f1.numerator * f2.denominator, f1.denominator * f2.numerator);

        public static explicit operator int(Fraction f) => (int)(f.numerator / f.denominator);

        public static explicit operator long(Fraction f) => f.numerator / f.denominator;

        public static explicit operator float(Fraction f) => 1.0f * f.numerator / f.denominator;

        public static explicit operator double(Fraction f) => 1.0 * f.numerator / f.denominator;

        public static explicit operator decimal(Fraction f) => 1.0m * f.numerator / f.denominator;

        public static implicit operator Fraction(int n) => Of(n);

        public static implicit operator Fraction(long n) => Of(n);

        public override bool Equals(object obj) =>
            obj is Fraction f && numerator == f.numerator && denominator == f.denominator;

        public override int GetHashCode() => (numerator, denominator).GetHashCode();

        public override string ToString() => $"{numerator}/{denominator}";

        public int CompareTo(Fraction other)
        {
            long lcm = Maths.LCM(denominator, other.denominator);
            long thisNumerator = lcm / denominator * numerator;
            long otherNumerator = lcm / other.denominator * other.numerator;

            return thisNumerator.CompareTo(otherNumerator);
        }

        public int CompareTo(double other) => ((double)this).CompareTo(other);

        public int CompareTo(int other) => CompareTo(Of(other));

        public int CompareTo(long other) => CompareTo(Of(other));
    }
}
