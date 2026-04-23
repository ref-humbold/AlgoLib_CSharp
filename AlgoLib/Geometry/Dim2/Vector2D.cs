using System;
using System.Globalization;
using System.Linq;

namespace AlgoLib.Geometry.Dim2;

/// <summary>Structure of vector in 2D.</summary>
public readonly record struct Vector2D(double X, double Y)
{
    public static readonly Vector2D Zero = Of(0, 0);
    private static readonly GeometryComparer Comparer = new();

    public double[] Coordinates => [X, Y];

    public double Length => Math.Sqrt(X * X + Y * Y);

    public static Vector2D Of(double x, double y) => new(x, y);

    public static Vector2D Between(Point2D begin, Point2D end) =>
        Of(end.X - begin.X, end.Y - begin.Y);

    public static double Dot(Vector2D v1, Vector2D v2) => v1.X * v2.X + v1.Y * v2.Y;

    public static double Area(Vector2D v1, Vector2D v2) => v1.X * v2.Y - v1.Y * v2.X;

    public static Vector2D operator +(Vector2D v) => Of(+v.X, +v.Y);

    public static Vector2D operator -(Vector2D v) => Of(-v.X, -v.Y);

    public static Vector2D operator +(Vector2D v1, Vector2D v2) => Of(v1.X + v2.X, v1.Y + v2.Y);

    public static Vector2D operator -(Vector2D v1, Vector2D v2) => Of(v1.X - v2.X, v1.Y - v2.Y);

    public static Vector2D operator *(Vector2D v, double c) => Of(v.X * c, v.Y * c);

    public static Vector2D operator *(double c, Vector2D v) => v * c;

    public static Vector2D operator /(Vector2D v, double c) =>
        c == 0 ? throw new DivideByZeroException() : Of(v.X / c, v.Y / c);

    public bool Equals(Vector2D v) =>
        Comparer.Compare(X, v.X) == 0 && Comparer.Compare(Y, v.Y) == 0;

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() =>
        $"[{string.Join(", ", Coordinates.Select(c => c.ToString(CultureInfo.InvariantCulture)))}]";

    public void Deconstruct(out double x, out double y)
    {
        x = X;
        y = Y;
    }
}
