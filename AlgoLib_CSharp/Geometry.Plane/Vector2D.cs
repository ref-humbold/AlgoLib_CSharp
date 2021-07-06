// Structure of vector in 2D
using System;

namespace Algolib.Geometry.Plane
{
    public class Vector2D : GeometryObject
    {
        public readonly double X;
        public readonly double Y;

        public double Length => Math.Sqrt(X * X + Y * Y);

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D Of(double x, double y) => new Vector2D(x, y);

        public static Vector2D operator +(Vector2D v1, Vector2D v2) =>
            new Vector2D(v1.X + v2.X, v1.Y + v2.Y);

        public static Vector2D operator -(Vector2D v1, Vector2D v2) =>
            new Vector2D(v1.X - v2.X, v1.Y - v2.Y);

        public static Vector2D operator *(Vector2D v, double c) => new Vector2D(v.X * c, v.Y * c);

        public static Vector2D operator *(double c, Vector2D v) => v * c;

        public static Vector2D operator /(Vector2D v, double c) =>
            c == 0 ? throw new DivideByZeroException() : new Vector2D(v.X / c, v.Y / c);

        public static Vector2D Between(Point2D begin, Point2D end) =>
            new Vector2D(end.X - begin.X, end.Y - begin.Y);

        public static double Dot(Vector2D v1, Vector2D v2) => v1.X * v2.X + v1.Y * v2.Y;

        public static double Area(Vector2D v1, Vector2D v2) => v1.X * v2.Y - v1.Y * v2.X;

        public override bool Equals(object obj) => obj is Vector2D v && AreEqual(X, v.X) && AreEqual(Y, v.Y);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"[{X}, {Y}]";
    }
}
