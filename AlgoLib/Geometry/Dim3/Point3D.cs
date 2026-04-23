using System;
using System.Globalization;
using System.Linq;

namespace AlgoLib.Geometry.Dim3;

/// <summary>Structure of point in 3D.</summary>
public readonly record struct Point3D(double X, double Y, double Z)
{
    public static readonly Point3D Zero = Of(0, 0, 0);
    private static readonly GeometryComparer Comparer = new();

    public double[] Coordinates => [X, Y, Z];

    public double Radius => Math.Sqrt(X * X + Y * Y + Z * Z);

    public static Point3D Of(double x, double y, double z) => new(x, y, z);

    public bool Equals(Point3D p) =>
        Comparer.Compare(X, p.X) == 0 && Comparer.Compare(Y, p.Y) == 0
        && Comparer.Compare(Z, p.Z) == 0;

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    public override string ToString() =>
        $"({string.Join(", ", Coordinates.Select(c => c.ToString(CultureInfo.InvariantCulture)))})";

    public void Deconstruct(out double x, out double y, out double z)
    {
        x = X;
        y = Y;
        z = Z;
    }
}
