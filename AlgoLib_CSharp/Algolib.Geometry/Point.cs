// Structure of point on a plane or in a space
using System;

namespace Algolib.Geometry
{
    public struct Point2D : IEquatable<Point2D>
    {
        public static readonly Point2D ZERO = new Point2D(0.0, 0.0);
        public readonly double X;
        public readonly double Y;

        public double Radius => Math.Sqrt(X * X + Y * Y);

        public double AngleRad => Math.Atan2(Y, X);

        public double AngleDeg => (Math.Atan2(Y, X) * 180 / Math.PI + 360) % 360;

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Point2D p1, Point2D p2) => p1.Equals(p2);

        public static bool operator !=(Point2D p1, Point2D p2) => !(p1 == p2);

        public override bool Equals(object obj) => obj is Point2D p && Equals(p);

        public bool Equals(Point2D p) => X == p.X && Y == p.Y;

        public override int GetHashCode() => HashCode.Combine(X, Y, 0x9e3779b9);

        public override string ToString() => $"({X}, {Y})";
    }

    public struct Point3D : IEquatable<Point3D>
    {
        public static readonly Point3D ZERO = new Point3D(0.0, 0.0, 0.0);
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

        public static bool operator ==(Point3D p1, Point3D p2) => p1.Equals(p2);

        public static bool operator !=(Point3D p1, Point3D p2) => !(p1 == p2);

        public override bool Equals(object obj) => obj is Point3D p && Equals(p);

        public bool Equals(Point3D p) => X == p.X && Y == p.Y && Z == p.Z;

        public override int GetHashCode() => HashCode.Combine(X, Y, Z, 0x9e3779b9);

        public override string ToString() => $"({X}, {Y}, {Z})";
    }
}
