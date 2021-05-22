﻿// Basic geometric operations in a space
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Geometry.Space
{
    public sealed class Geometry3D
    {
        /// <summary>
        /// Immutably sorts points by their X coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public static List<Point3D> SortByX(List<Point3D> points) =>
            points == null ? throw new ArgumentNullException("Points list is null")
                           : points.OrderBy(pt => pt.X).ToList();

        /// <summary>
        /// Immutably sorts points by their Y coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public static List<Point3D> SortByY(List<Point3D> points) =>
            points == null ? throw new ArgumentNullException("Points list is null")
                           : points.OrderBy(pt => pt.Y).ToList();

        /// <summary>
        /// Immutably sorts points by their Z coordinate. Sorting is guaranteed to be stable.
        /// </summary>
        /// <param name="points">a list of points</param>
        /// <returns>sorted list of points</returns>
        public static List<Point3D> SortByZ(List<Point3D> points) =>
            points == null ? throw new ArgumentNullException("Points list is null")
                           : points.OrderBy(pt => pt.Z).ToList();

        public static double Distance(Point3D p1, Point3D p2) =>
            Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y)
                      + (p2.Z - p1.Z) * (p2.Z - p1.Z));

        public static Point3D Translate(Point3D p, Vector3D v) =>
            new Point3D(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
    }
}
