// Algorithms for sorting lists of points
using System;
using System.Collections.Generic;

namespace Algolib.Geometry
{
    public class PointsSorting
    {
        /// <summary>
        /// Mutably sorts points by their cartesian coordinates. First sorts by X coordinate, then
        /// by Y coordinate.
        /// </summary>
        /// <param name="points">a list of points</param>
        public void SortByX(List<Point2D> points)
        {
            if(points == null)
                throw new ArgumentNullException("Points list is null");

            points.Sort((pt1, pt2) => pt1.X == pt2.X ? pt1.Y.CompareTo(pt2.Y)
                                                     : pt1.X.CompareTo(pt2.X));
        }

        /// <summary>
        /// Mutably sorts points by their cartesian coordinates. First sorts by Y coordinate, then
        /// by X coordinate.
        /// </summary>
        /// <param name="points">a list of points</param>
        public void SortByY(List<Point2D> points)
        {
            if(points == null)
                throw new ArgumentNullException("Points list is null");

            points.Sort((pt1, pt2) => pt1.Y == pt2.Y ? pt1.X.CompareTo(pt2.X)
                                                     : pt1.Y.CompareTo(pt2.Y));
        }

        /// <summary>
        /// Mutably sorts points by their polar coordinates. First sorts by angle, then by radius.
        /// </summary>
        /// <param name="points">a list of points</param>
        public void SortByAngle(List<Point2D> points)
        {
            if(points == null)
                throw new ArgumentNullException("Points list is null");

            points.Sort((pt1, pt2) => pt1.AngleDeg == pt2.AngleDeg
                                          ? pt1.Radius.CompareTo(pt2.Radius)
                                          : pt1.AngleDeg.CompareTo(pt2.AngleDeg));
        }
    }
}
