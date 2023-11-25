using System;

namespace AlgoLib.Geometry.Dim3;

/// <summary>Structure of point in 3D.</summary>
public readonly record struct Point3D(double X, double Y, double Z) : IGeometryObject
{
    public double[] Coordinates => new[] { X, Y, Z };

    public double Radius => Math.Sqrt(X * X + Y * Y + Z * Z);

    public static Point3D Of(double x, double y, double z) => new(x, y, z);

    public bool Equals(Point3D p) =>
        IGeometryObject.AreEqual(X, p.X) && IGeometryObject.AreEqual(Y, p.Y)
            && IGeometryObject.AreEqual(Z, p.Z);

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    public override string ToString() => $"({X}, {Y}, {Z})";

    public void Deconstruct(out double x, out double y, out double z)
    {
        x = X;
        y = Y;
        z = Z;
    }
}
