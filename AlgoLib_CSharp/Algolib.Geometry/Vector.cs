// Structure of vector on a plane or in a space
using System;

namespace Algolib.Geometry
{
    public struct Vector2D : IEquatable<Vector2D>
    {
        public static readonly Vector2D ZERO = new Vector2D(0.0, 0.0);
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

        public static Vector2D operator /(Vector2D v, double c) =>
            c == 0 ? throw new DivideByZeroException() : new Vector2D(v.X / c, v.Y / c);

        public static double Area(Vector2D v1, Vector2D v2) => v1.X * v2.Y - v1.Y * v2.X;

        public override bool Equals(object obj) => obj is Vector2D v && Equals(v);

        public bool Equals(Vector2D v) => X == v.X && Y == v.Y;

        public override int GetHashCode() => HashCode.Combine(X, Y, 0x9e3d79b9);

        public override string ToString() => $"[{X}, {Y}]";
    }

    public struct Vector3D : IEquatable<Vector3D>
    {
        public static readonly Vector3D ZERO = new Vector3D(0.0, 0.0, 0.0);
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static bool operator ==(Vector3D v1, Vector3D v2) => v1.Equals(v2);

        public static bool operator !=(Vector3D v1, Vector3D v2) => !(v1 == v2);

        public static Vector3D operator +(Vector3D v1, Vector3D v2) =>
            new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

        public static Vector3D operator -(Vector3D v1, Vector3D v2) =>
            new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

        public static double operator *(Vector3D v1, Vector3D v2) =>
            v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

        public static Vector3D operator *(Vector3D v, double c) =>
            new Vector3D(v.X * c, v.Y * c, v.Z * c);

        public static Vector3D operator *(double c, Vector3D v) => v * c;

        public static Vector3D operator /(Vector3D v, double c) =>
            c == 0 ? throw new DivideByZeroException() : new Vector3D(v.X / c, v.Y / c, v.Z / c);

        public static Vector3D operator ^(Vector3D v1, Vector3D v2) =>
            new Vector3D(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z,
                         v1.X * v2.Y - v1.Y * v2.X);

        public static double Area(Vector3D v1, Vector3D v2) => (v1 ^ v2).Length;

        public static double Volume(Vector3D v1, Vector3D v2, Vector3D v3) => v1 * (v2 ^ v3);

        public override bool Equals(object obj) => obj is Vector3D v && Equals(v);

        public bool Equals(Vector3D v) => X == v.X && Y == v.Y && Z == v.Z;

        public override int GetHashCode() => HashCode.Combine(X, Y, Z, 0x9e3d79b9);

        public override string ToString() => $"[{X}, {Y}, {Z}]";
    }
}
