// Basic geometric operations on a plane
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Geometry.Plane
{
    public sealed class Geometry2D
    {
        /// <summary>
        /// Immutably sorts points by their X coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public static List<Point2D> SortByX(List<Point2D> points) =>
            points == null ? throw new ArgumentNullException("Points list is null")
                           : points.OrderBy(pt => pt.X).ToList();

        /// <summary>
        /// Immutably sorts points by their Y coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public static List<Point2D> SortByY(List<Point2D> points) =>
            points == null ? throw new ArgumentNullException("Points list is null")
                           : points.OrderBy(pt => pt.Y).ToList();

        /// <summary>
        /// Immutably sorts points by their polar coordinates. First sorts by angle, then by radius.
        /// Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public static List<Point2D> SortByAngle(List<Point2D> points) =>
            points == null ? throw new ArgumentNullException("Points list is null")
                           : points.OrderBy(pt => pt.AngleDeg).ThenBy(pt => pt.Radius).ToList();

        /// <summary>Counts distance between points.</summary>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        /// <returns>Distance between points</returns>
        public static double Distance(Point2D p1, Point2D p2) =>
            Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));

        /// <summary>Translates a point by a vector.</summary>
        /// <param name="p">Point</param>
        /// <param name="v">Vector of translation</param>
        /// <returns>Translated point</returns>
        public static Point2D Translate(Point2D p, Vector2D v) =>
            new Point2D(p.X + v.X, p.Y + v.Y);
    }
}
