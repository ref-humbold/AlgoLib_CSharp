// Structure of point on a plane
using System;

namespace Algolib.Geometry
{
    public struct Point2D : IEquatable<Point2D>, IComparable<Point2D>
    {
        public readonly double X;
        public readonly double Y;

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Radius => Math.Sqrt(X * X + Y * Y);

        public double Angle => (Math.Atan2(Y, X) * 180 / Math.PI + 360) % 360;

        public static bool operator ==(Point2D pt1, Point2D pt2) => pt1.Equals(pt2);

        public static bool operator !=(Point2D pt1, Point2D pt2) => !(pt1 == pt2);

        public static bool operator <(Point2D pt1, Point2D pt2) => pt1.CompareTo(pt2) < 0;

        public static bool operator <=(Point2D pt1, Point2D pt2) => pt1.CompareTo(pt2) <= 0;

        public static bool operator >(Point2D pt1, Point2D pt2) => !(pt1 <= pt2);

        public static bool operator >=(Point2D pt1, Point2D pt2) => !(pt1 < pt2);

        public override bool Equals(object obj) => obj is Point2D pt && Equals(pt);

        public bool Equals(Point2D pt) => X == pt.X && Y == pt.Y;

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"({X}, {Y})";

        public int CompareTo(Point2D pt)
        {
            int comparedX = X.CompareTo(pt.X);

            return comparedX != 0 ? comparedX : Y.CompareTo(pt.Y);
        }
    }
}
