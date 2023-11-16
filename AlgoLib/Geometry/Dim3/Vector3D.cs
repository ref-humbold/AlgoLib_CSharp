using System;

namespace AlgoLib.Geometry.Dim3;

/// <summary>Structure of vector in 3D.</summary>
public readonly struct Vector3D : IGeometryObject, IEquatable<Vector3D>
{
    public readonly double X { get; init; }

    public readonly double Y { get; init; }

    public readonly double Z { get; init; }

    public double[] Coordinates => new[] { X, Y, Z };

    public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

    private Vector3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vector3D Of(double x, double y, double z) => new(x, y, z);

    public static Vector3D Between(Point3D begin, Point3D end) =>
        Of(end.X - begin.X, end.Y - begin.Y, end.Z - begin.Z);

    public static double Dot(Vector3D v1, Vector3D v2) =>
        v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

    public static Vector3D Cross(Vector3D v1, Vector3D v2) =>
        Of(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);

    public static double Area(Vector3D v1, Vector3D v2) => Cross(v1, v2).Length;

    public static double Volume(Vector3D v1, Vector3D v2, Vector3D v3) => Dot(v1, Cross(v2, v3));

    public static bool operator ==(Vector3D v1, Vector3D v2) => v1.Equals(v2);

    public static bool operator !=(Vector3D v1, Vector3D v2) => !(v1 == v2);

    public static Vector3D operator +(Vector3D v) => Of(+v.X, +v.Y, +v.Z);

    public static Vector3D operator -(Vector3D v) => Of(-v.X, -v.Y, -v.Z);

    public static Vector3D operator +(Vector3D v1, Vector3D v2) =>
        Of(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

    public static Vector3D operator -(Vector3D v1, Vector3D v2) =>
        Of(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

    public static Vector3D operator *(Vector3D v, double c) => Of(v.X * c, v.Y * c, v.Z * c);

    public static Vector3D operator *(double c, Vector3D v) => v * c;

    public static Vector3D operator /(Vector3D v, double c) =>
        c == 0 ? throw new DivideByZeroException() : Of(v.X / c, v.Y / c, v.Z / c);

    public override bool Equals(object obj) => obj is Vector3D v && Equals(v);

    public bool Equals(Vector3D v) =>
        IGeometryObject.AreEqual(X, v.X) && IGeometryObject.AreEqual(Y, v.Y)
            && IGeometryObject.AreEqual(Z, v.Z);

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    public override string ToString() => $"[{X}, {Y}, {Z}]";

    public void Deconstruct(out double x, out double y, out double z)
    {
        x = X;
        y = Y;
        z = Z;
    }
}
