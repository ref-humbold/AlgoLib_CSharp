// Algorithms for basic geometrical operations in 3D
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Geometry.Dim3
{
    public static class Geometry3D
    {
        /// <summary>
        /// Immutably sorts points by their X coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">A list of points.</param>
        /// <returns>The sorted list of points.</returns>
        public static List<Point3D> SortByX(this List<Point3D> points) =>
            points == null
                ? throw new ArgumentNullException(nameof(points))
                : points.OrderBy(pt => pt.X).ToList();

        /// <summary>
        /// Immutably sorts points by their Y coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">A list of points.</param>
        /// <returns>The sorted list of points.</returns>
        public static List<Point3D> SortByY(this List<Point3D> points) =>
            points == null
                ? throw new ArgumentNullException(nameof(points))
                : points.OrderBy(pt => pt.Y).ToList();

        /// <summary>
        /// Immutably sorts points by their Z coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">A list of points.</param>
        /// <returns>The sorted list of points.</returns>
        public static List<Point3D> SortByZ(this List<Point3D> points) =>
            points == null
                ? throw new ArgumentNullException(nameof(points))
                : points.OrderBy(pt => pt.Z).ToList();

        /// <summary>Counts distance between given points.</summary>
        /// <param name="p1">First point.</param>
        /// <param name="p2">Second point.</param>
        /// <returns>The distance between points.</returns>
        public static double Distance(this Point3D p1, Point3D p2) =>
            Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y)
                    + (p2.Z - p1.Z) * (p2.Z - p1.Z));

        /// <summary>Translates given point by given vector.</summary>
        /// <param name="p">A point.</param>
        /// <param name="v">A vector of translation.</param>
        /// <returns>The translated point.</returns>
        public static Point3D Translate(this Point3D p, Vector3D v) =>
            new(p.X + v.X, p.Y + v.Y, p.Z + v.Z);

        /// <summary>Reflects given point in another point.</summary>
        /// <param name="p">A point.</param>
        /// <param name="centre">A point of reflection.</param>
        /// <returns>The reflected point.</returns>
        public static Point3D Reflect(this Point3D p, Point3D centre) =>
            new(-p.X + 2 * centre.X, -p.Y + 2 * centre.Y, -p.Z + 2 * centre.Z);
    }
}
