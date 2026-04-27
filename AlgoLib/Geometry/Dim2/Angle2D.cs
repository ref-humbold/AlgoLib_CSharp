using System;
using System.Globalization;

namespace AlgoLib.Geometry.Dim2;

/// <summary>Structure of angle in 2D.</summary>
public readonly struct Angle2D : IEquatable<Angle2D>, IComparable<Angle2D>
{
    private const double FullAngleDeg = 360;
    private static readonly GeometryComparer Comparer = new();

    public double Degrees { get; } = 0;

    public double Radians => double.DegreesToRadians(Degrees);

    private Angle2D(double degrees) => Degrees = degrees;

    public static Angle2D FromDegrees(double degrees) =>
        new((degrees % FullAngleDeg + FullAngleDeg) % FullAngleDeg);

    public static Angle2D FromRadians(double radians) =>
        FromDegrees(double.RadiansToDegrees(radians));

    public static bool operator ==(Angle2D angle1, Angle2D angle2) => angle1.Equals(angle2);

    public static bool operator !=(Angle2D angle1, Angle2D angle2) => !angle1.Equals(angle2);

    public static bool operator <(Angle2D angle1, Angle2D angle2) => angle1.CompareTo(angle2) < 0;

    public static bool operator >(Angle2D angle1, Angle2D angle2) => angle1.CompareTo(angle2) > 0;

    public static bool operator <=(Angle2D angle1, Angle2D angle2) => angle1.CompareTo(angle2) <= 0;

    public static bool operator >=(Angle2D angle1, Angle2D angle2) => angle1.CompareTo(angle2) >= 0;

    public bool Equals(Angle2D other) => Comparer.Compare(Degrees, other.Degrees) == 0;

    public override bool Equals(object obj) => obj is Angle2D other && Equals(other);

    public override int GetHashCode() => Degrees.GetHashCode();

    public int CompareTo(Angle2D other) => Comparer.Compare(Degrees, other.Degrees);

    public override string ToString() =>
        $"Angle<{Degrees.ToString(CultureInfo.InvariantCulture)} deg>";
}
