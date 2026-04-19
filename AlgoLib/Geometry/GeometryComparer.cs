using System;
using System.Collections.Generic;

namespace AlgoLib.Geometry;

public sealed class GeometryComparer : IComparer<double>
{
    private const double Epsilon = 1e-12;

    public int Compare(double d1, double d2) => Math.Abs(d1 - d2) < Epsilon ? 0 : d1.CompareTo(d2);
}
