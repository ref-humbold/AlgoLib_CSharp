using System;

namespace AlgoLib.Geometry.Dim2;

/// <summary>Structure of point in 2D.</summary>
public readonly record struct Point2D(double X, double Y)
{
    public static readonly Point2D Zero = Of(0, 0);
    private static readonly GeometryComparer Comparer = new();

    public double[] Coordinates => [X, Y];

    public double Radius => Math.Sqrt(X * X + Y * Y);

    public Angle Angle => Angle.FromRadians(Math.Atan2(Y, X));

    public static Point2D Of(double x, double y) => new(x, y);

    public bool Equals(Point2D p) => Comparer.Compare(X, p.X) == 0 && Comparer.Compare(Y, p.Y) == 0;

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"({X}, {Y})";

    public void Deconstruct(out double x, out double y)
    {
        x = X;
        y = Y;
    }
}
