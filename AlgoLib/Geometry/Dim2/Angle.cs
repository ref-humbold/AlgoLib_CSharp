using System;

namespace AlgoLib.Geometry.Dim2;

/// <summary>Structure of angle.</summary>
public readonly struct Angle : IEquatable<Angle>, IComparable<Angle>
{
    private const double FullAngleDeg = 360;

    public double Degrees { get; } = 0;

    public double Radians => double.DegreesToRadians(Degrees);

    private Angle(double degrees) => Degrees = degrees;

    public static Angle FromDegrees(double degrees) =>
        new((degrees % FullAngleDeg + FullAngleDeg) % FullAngleDeg);

    public static Angle FromRadians(double radians) =>
        FromDegrees(double.RadiansToDegrees(radians));

    public static bool operator ==(Angle angle1, Angle angle2) => angle1.Equals(angle2);

    public static bool operator !=(Angle angle1, Angle angle2) => !angle1.Equals(angle2);

    public static bool operator <(Angle angle1, Angle angle2) => angle1.CompareTo(angle2) < 0;

    public static bool operator >(Angle angle1, Angle angle2) => angle1.CompareTo(angle2) > 0;

    public static bool operator <=(Angle angle1, Angle angle2) => angle1.CompareTo(angle2) <= 0;

    public static bool operator >=(Angle angle1, Angle angle2) => angle1.CompareTo(angle2) >= 0;

    public bool Equals(Angle other) => Degrees.Equals(other.Degrees);

    public override bool Equals(object obj) => obj is Angle other && Equals(other);

    public override int GetHashCode() => Degrees.GetHashCode();

    public int CompareTo(Angle other) => Degrees.CompareTo(other.Degrees);
}
