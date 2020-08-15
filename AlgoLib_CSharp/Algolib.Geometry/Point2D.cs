// Structure of point on a plane
using System;

namespace Algolib.Geometry
{
    public struct Point2D : IEquatable<Point2D>
    {
        public readonly double X;
        public readonly double Y;

        public double Radius => Math.Sqrt(X * X + Y * Y);

        public double Angle => (Math.Atan2(Y, X) * 180 / Math.PI + 360) % 360;

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Point2D p1, Point2D p2) => p1.Equals(p2);

        public static bool operator !=(Point2D p1, Point2D p2) => !(p1 == p2);

        public override bool Equals(object obj) => obj is Point2D p && Equals(p);

        public bool Equals(Point2D p) => X == p.X && Y == p.Y;

        public override int GetHashCode() => HashCode.Combine(X, Y, 0x933ff3);

        public override string ToString() => $"({X}, {Y})";
    }
}
