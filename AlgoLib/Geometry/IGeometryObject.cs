using System;

namespace AlgoLib.Geometry
{
    public interface IGeometryObject
    {
        public const double Epsilon = 1e-12;

        public double[] Coordinates
        {
            get;
        }

        protected static bool AreEqual(double d1, double d2) => Math.Abs(d1 - d2) < Epsilon;
    }
}
