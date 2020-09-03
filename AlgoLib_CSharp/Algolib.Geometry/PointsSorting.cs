// Algorithms for sorting lists of points
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Geometry
{
    public sealed class PointsSorting
    {
        /// <summary>
        /// Immutably sorts points by their X coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public List<Point2D> SortByX(List<Point2D> points) =>
            points == null
                    ? throw new ArgumentNullException("Points list is null")
                    : points.OrderBy(pt => pt.X).ToList();

        /// <summary>
        /// Immutably sorts points by their X coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public List<Point3D> SortByX(List<Point3D> points) =>
            points == null
                    ? throw new ArgumentNullException("Points list is null")
                    : points.OrderBy(pt => pt.X).ToList();

        /// <summary>
        /// Immutably sorts points by their Y coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public List<Point2D> SortByY(List<Point2D> points) =>
            points == null
                    ? throw new ArgumentNullException("Points list is null")
                    : points.OrderBy(pt => pt.Y).ToList();

        /// <summary>
        /// Immutably sorts points by their Y coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public List<Point3D> SortByY(List<Point3D> points) =>
            points == null
                    ? throw new ArgumentNullException("Points list is null")
                     : points.OrderBy(pt => pt.Y).ToList();

        /// <summary>
        /// Immutably sorts points by their Z coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public List<Point3D> SortByZ(List<Point3D> points) =>
            points == null
                    ? throw new ArgumentNullException("Points list is null")
                    : points.OrderBy(pt => pt.Z).ToList();

        /// <summary>
        /// Immutably sorts points by their polar coordinates. First sorts by angle, then by radius.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public List<Point2D> SortByAngle(List<Point2D> points) =>
            points == null
                    ? throw new ArgumentNullException("Points list is null")
                    : points.OrderBy(pt => pt.AngleDeg).ThenBy(pt => pt.Radius).ToList();
    }
}
