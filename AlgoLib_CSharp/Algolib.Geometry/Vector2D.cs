// Structure of point on a plane
using System;

namespace Algolib.Geometry
{
    public struct Vector2D : IEquatable<Vector2D>
    {
        public readonly double X;
        public readonly double Y;

        public double Length => Math.Sqrt(X * X + Y * Y);

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Vector2D v1, Vector2D v2) => v1.Equals(v2);

        public static bool operator !=(Vector2D v1, Vector2D v2) => !(v1 == v2);

        public static Vector2D operator +(Vector2D v1, Vector2D v2) =>
            new Vector2D(v1.X + v2.X, v1.Y + v2.Y);

        public static Vector2D operator -(Vector2D v1, Vector2D v2) =>
            new Vector2D(v1.X - v2.X, v1.Y - v2.Y);

        public static double operator *(Vector2D v1, Vector2D v2) => v1.X * v2.X + v1.Y * v2.Y;

        public static Vector2D operator *(Vector2D v, double c) => new Vector2D(v.X * c, v.Y * c);

        public static Vector2D operator *(double c, Vector2D v) => v * c;

        public static Vector2D operator /(Vector2D v, double c)
        {
            if(c == 0)
                throw new DivideByZeroException();

            return new Vector2D(v.X / c, v.Y / c);
        }

        public static double Area(Vector2D v1, Vector2D v2) => v1.X * v2.Y - v1.Y * v2.X;

        public override bool Equals(object obj) => obj is Vector2D v && Equals(v);

        public bool Equals(Vector2D v) => X == v.X && Y == v.Y;

        public override int GetHashCode() => HashCode.Combine(X, Y, 0x953fe5);

        public override string ToString() => $"[{X}, {Y}]";
    }
}
