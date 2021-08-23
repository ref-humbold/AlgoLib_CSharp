// Algorithms for basic geometrical operations in 2D
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Geometry.Dim2
{
    public static class Geometry2D
    {
        /// <summary>
        /// Immutably sorts points by their X coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public static List<Point2D> SortByX(this List<Point2D> points) =>
            points == null ? throw new ArgumentNullException("Points list is null")
                           : points.OrderBy(pt => pt.X).ToList();

        /// <summary>
        /// Immutably sorts points by their Y coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public static List<Point2D> SortByY(this List<Point2D> points) =>
            points == null ? throw new ArgumentNullException("Points list is null")
                           : points.OrderBy(pt => pt.Y).ToList();

        /// <summary>
        /// Immutably sorts points by their polar coordinates. First sorts by angle, then by radius.
        /// Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public static List<Point2D> SortByAngle(this List<Point2D> points) =>
            points == null ? throw new ArgumentNullException("Points list is null")
                           : points.OrderBy(pt => pt.AngleDeg).ThenBy(pt => pt.Radius).ToList();

        /// <summary>Counts distance between points.</summary>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        /// <returns>Distance between points</returns>
        public static double Distance(this Point2D p1, Point2D p2) =>
            Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));

        /// <summary>Translates a point by a vector.</summary>
        /// <param name="p">Point</param>
        /// <param name="v">Vector of translation</param>
        /// <returns>Translated point</returns>
        public static Point2D Translate(this Point2D p, Vector2D v) =>
            new Point2D(p.X + v.X, p.Y + v.Y);

        /// <summary>Reflects given point in another point.</summary>
        /// <param name="p">A point to be reflected</param>
        /// <param name="centre">A point of reflection</param>
        /// <returns>The reflected point</returns>
        public static Point2D Reflect(this Point2D p, Point2D centre) =>
            new Point2D(-p.X + 2 * centre.X, -p.Y + 2 * centre.Y);
    }
}
