// Structure of vector in 2D.
using System;

namespace AlgoLib.Geometry.Dim2
{
    public readonly struct Vector2D : IGeometryObject, IEquatable<Vector2D>
    {
        public readonly double X { get; init; }

        public readonly double Y { get; init; }

        public double[] Coordinates => new[] { X, Y };

        public double Length => Math.Sqrt(X * X + Y * Y);

        private Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D Of(double x, double y) => new(x, y);

        public static Vector2D Between(Point2D begin, Point2D end) =>
            Of(end.X - begin.X, end.Y - begin.Y);

        public static double Dot(Vector2D v1, Vector2D v2) => v1.X * v2.X + v1.Y * v2.Y;

        public static double Area(Vector2D v1, Vector2D v2) => v1.X * v2.Y - v1.Y * v2.X;

        public static bool operator ==(Vector2D v1, Vector2D v2) => v1.Equals(v2);

        public static bool operator !=(Vector2D v1, Vector2D v2) => !(v1 == v2);

        public static Vector2D operator +(Vector2D v) => Of(+v.X, +v.Y);

        public static Vector2D operator -(Vector2D v) => Of(-v.X, -v.Y);

        public static Vector2D operator +(Vector2D v1, Vector2D v2) => Of(v1.X + v2.X, v1.Y + v2.Y);

        public static Vector2D operator -(Vector2D v1, Vector2D v2) => Of(v1.X - v2.X, v1.Y - v2.Y);

        public static Vector2D operator *(Vector2D v, double c) => Of(v.X * c, v.Y * c);

        public static Vector2D operator *(double c, Vector2D v) => v * c;

        public static Vector2D operator /(Vector2D v, double c) =>
            c == 0 ? throw new DivideByZeroException() : Of(v.X / c, v.Y / c);

        public override bool Equals(object obj) => obj is Vector2D v && Equals(v);

        public bool Equals(Vector2D v) =>
            IGeometryObject.AreEqual(X, v.X) && IGeometryObject.AreEqual(Y, v.Y);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"[{X}, {Y}]";

        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }
    }
}
