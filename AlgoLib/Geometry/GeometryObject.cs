using System;

namespace Algolib.Geometry
{
    public abstract class GeometryObject
    {
        private const double epsilon = 1e-15;

        protected static bool AreEqual(double d1, double d2) => Math.Abs(d1 - d2) < epsilon;
    }
}
