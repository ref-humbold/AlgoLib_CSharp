// Structure of point in 3D
using System;

namespace Algolib.Geometry.Dim3
{
    public struct Point3D : IGeometryObject, IEquatable<Point3D>
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public double[] Coordinates => new double[] { X, Y, Z };

        public double Radius => Math.Sqrt(X * X + Y * Y + Z * Z);

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point3D Of(double x, double y, double z) => new Point3D(x, y, z);

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
