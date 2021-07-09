using System;

namespace Algolib.Geometry
{
    public interface IGeometryObject
    {
        private const double epsilon = 1e-15;

        public double[] Coordinates
        {
            get;
        }

        protected static bool AreEqual(double d1, double d2) => Math.Abs(d1 - d2) < epsilon;
    }
}
