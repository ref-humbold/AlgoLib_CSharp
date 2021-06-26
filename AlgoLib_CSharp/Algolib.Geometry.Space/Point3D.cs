// Structure of point in 3D
using System;

namespace Algolib.Geometry.Space
{
    public class Point3D : GeometryObject
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public double Radius => Math.Sqrt(X * X + Y * Y + Z * Z);

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point3D Of(double x, double y, double z) => new Point3D(x, y, z);

        public override bool Equals(object obj) =>
            obj is Point3D p && AreEqual(X, p.X) && AreEqual(Y, p.Y) && AreEqual(Z, p.Z);

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        public override string ToString() => $"({X}, {Y}, {Z})";
    }
}
