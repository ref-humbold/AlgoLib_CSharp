// Structure of point in a space
using System;

namespace Algolib.Geometry.Space
{
    public struct Point3D : IEquatable<Point3D>
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

        public static bool operator ==(Point3D p1, Point3D p2) => p1.Equals(p2);

        public static bool operator !=(Point3D p1, Point3D p2) => !(p1 == p2);

        public override bool Equals(object obj) => obj is Point3D p && Equals(p);

        public bool Equals(Point3D p) => X == p.X && Y == p.Y && Z == p.Z;

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        public override string ToString() => $"({X}, {Y}, {Z})";
    }
}
