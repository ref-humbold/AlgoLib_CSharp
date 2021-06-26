// Structure of vector in 3D
using System;

namespace Algolib.Geometry.Space
{
    public class Vector3D : GeometryObject
    {
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

        public static Vector3D Of(double x, double y, double z) => new Vector3D(x, y, z);

        public static Vector3D operator +(Vector3D v1, Vector3D v2) =>
            new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

        public static Vector3D operator -(Vector3D v1, Vector3D v2) =>
            new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

        public static Vector3D operator *(Vector3D v, double c) =>
            new Vector3D(v.X * c, v.Y * c, v.Z * c);

        public static Vector3D operator *(double c, Vector3D v) => v * c;

        public static Vector3D operator /(Vector3D v, double c) =>
            c == 0 ? throw new DivideByZeroException() : new Vector3D(v.X / c, v.Y / c, v.Z / c);

        public static Vector3D Between(Point3D begin, Point3D end) =>
            new Vector3D(end.X - begin.X, end.Y - begin.Y, end.Z - begin.Z);

        public static double Dot(Vector3D v1, Vector3D v2) =>
            v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

        public static Vector3D Cross(Vector3D v1, Vector3D v2) =>
            new Vector3D(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z,
                         v1.X * v2.Y - v1.Y * v2.X);

        public static double Area(Vector3D v1, Vector3D v2) => Cross(v1, v2).Length;

        public static double Volume(Vector3D v1, Vector3D v2, Vector3D v3) => Dot(v1, Cross(v2, v3));

        public override bool Equals(object obj) =>
            obj is Vector3D v && AreEqual(X, v.X) && AreEqual(Y, v.Y) && AreEqual(Z, v.Z);

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        public override string ToString() => $"[{X}, {Y}, {Z}]";
    }
}
