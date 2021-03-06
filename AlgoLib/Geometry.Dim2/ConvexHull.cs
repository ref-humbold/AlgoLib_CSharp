﻿// Algorithm for convex hull in 2D (monotone chain)
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Geometry.Dim2
{
    public sealed class ConvexHull
    {
        /// <summary>Constructs a convex hull of given points.</summary>
        /// <param name="points">A list of points</param>
        /// <returns>List of hull points</returns>
        public static IEnumerable<Point2D> Find(List<Point2D> points)
        {
            if(points.Count < 3)
                return Enumerable.Empty<Point2D>();

            List<Point2D> sorted = Geometry2D.SortByX(points);
            List<Point2D> lowerHull = createHalfHull(sorted);
            List<Point2D> upperHull = createHalfHull(Enumerable.Reverse(sorted));

            lowerHull.RemoveAt(lowerHull.Count - 1);
            upperHull.RemoveAt(upperHull.Count - 1);
            lowerHull.AddRange(upperHull);
            return lowerHull;
        }

        // Creates a half of a convex hull for given points.
        private static List<Point2D> createHalfHull(IEnumerable<Point2D> points)
        {
            List<Point2D> hull = new List<Point2D>();

            foreach(Point2D pt in points)
            {
                while(hull.Count > 1 && crossProduct(hull[^2], hull[^1], pt) >= 0)
                    hull.RemoveAt(hull.Count - 1);

                hull.Add(pt);
            }

            return hull;
        }

        private static double crossProduct(Point2D pt1, Point2D pt2, Point2D pt3) =>
            Vector2D.Area(Vector2D.Between(pt2, pt1), Vector2D.Between(pt2, pt3));
    }
}
