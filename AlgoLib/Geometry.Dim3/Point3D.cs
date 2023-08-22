// Structure of point in 3D
using System;

namespace AlgoLib.Geometry.Dim3
{
    public readonly struct Point3D : IGeometryObject, IEquatable<Point3D>
    {
        public readonly double X { get; init; }

        public readonly double Y { get; init; }

        public readonly double Z { get; init; }

        public double[] Coordinates => new[] { X, Y, Z };

        public double Radius => Math.Sqrt(X * X + Y * Y + Z * Z);

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point3D Of(double x, double y, double z) => new(x, y, z);

        public static bool operator ==(Point3D left, Point3D right) => left.Equals(right);

        public static bool operator !=(Point3D left, Point3D right) => !(left == right);

        public override bool Equals(object obj) => obj is Point3D p && Equals(p);

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
}
