// Structure of point in 2D
using System;

namespace AlgoLib.Geometry.Dim2
{
    public struct Point2D : IGeometryObject, IEquatable<Point2D>
    {
        public readonly double X;
        public readonly double Y;

        public double[] Coordinates => new[] { X, Y };

        public double Radius => Math.Sqrt(X * X + Y * Y);

        public double AngleRad => Math.Atan2(Y, X);

        public double AngleDeg => (Math.Atan2(Y, X) * 180 / Math.PI + 360) % 360;

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point2D Of(double x, double y) => new Point2D(x, y);

        public override bool Equals(object obj) => obj is Point2D p && Equals(p);

        public bool Equals(Point2D p) =>
            IGeometryObject.AreEqual(X, p.X) && IGeometryObject.AreEqual(Y, p.Y);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"({X}, {Y})";

        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }
    }
}
