// Algorithm for pair of closest points in 2D
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Geometry.Dim2
{
    public static class ClosestPoints
    {
        /// <summary>Searches for a pair of closest points among given points.</summary>
        /// <param name="points">The points.</param>
        /// <returns>The pair of closest points.</returns>
        /// <exception cref="InvalidOperationException">If no points specified.</exception>
        public static (Point2D Closest1, Point2D Closest2) FindClosestPoints(
            IEnumerable<Point2D> points)
        {
            var pointsList = points.ToList();

            if(pointsList.Count == 0)
                throw new InvalidOperationException("Sequence contains no elements");

            List<Point2D> pointsY = Geometry2D.SortByY(pointsList);

            return searchClosest(Geometry2D.SortByX(pointsY), pointsY, ..);
        }

        // Searches for closest points among three of them.
        private static (Point2D Closest1, Point2D Closest2) searchThree(
            Point2D point1, Point2D point2, Point2D point3)
        {
            double distance12 = Geometry2D.Distance(point1, point2);
            double distance23 = Geometry2D.Distance(point2, point3);
            double distance31 = Geometry2D.Distance(point3, point1);

            return distance12 <= distance23 && distance12 <= distance31
                ? (point1, point2)
                : distance23 <= distance12 && distance23 <= distance31
                    ? (point2, point3)
                    : (point1, point3);
        }

        // Searches for closest points inside a belt of given width.
        // The resulting distance should not be less than belt width.
        private static (Point2D Closest1, Point2D Closest2)? checkBelt(
            List<Point2D> pointsY, double middleX, double width)
        {
            (Point2D, Point2D)? closestPoints = null;
            var beltPoints = new List<int>();
            double minDistance = width;

            for(int i = 0; i < pointsY.Count; ++i)
                if(pointsY[i].X >= middleX - width && pointsY[i].X <= middleX + width)
                    beltPoints.Add(i);

            for(int i = 0; i < beltPoints.Count; ++i)
                for(int j = i + 1; j < beltPoints.Count; ++j)
                {
                    Point2D pt1 = pointsY[beltPoints[i]];
                    Point2D pt2 = pointsY[beltPoints[j]];

                    if(pt2.Y > pt1.Y + width)
                        break;

                    if(pt1.X <= middleX && pt2.X >= middleX || pt1.X > middleX && pt2.X <= middleX)
                    {
                        double actualDistance = Geometry2D.Distance(pt1, pt2);

                        if(actualDistance < minDistance)
                        {
                            minDistance = actualDistance;
                            closestPoints = (pt1, pt2);
                        }
                    }
                }

            return closestPoints;
        }

        // Searches for closest points in given sublist of points.
        // Points are given sorted by X coordinate and by Y coordinate.
        private static (Point2D Closest1, Point2D Closest2) searchClosest(
            List<Point2D> pointsX, List<Point2D> pointsY, Range range)
        {
            int indexBegin = range.Start.GetOffset(pointsX.Count);
            int indexEnd = range.End.GetOffset(pointsX.Count);

            if(indexEnd - indexBegin <= 2)
                return (pointsX[indexBegin], pointsX[indexEnd - 1]);

            if(indexEnd - indexBegin == 3)
                return searchThree(pointsX[indexBegin], pointsX[indexBegin + 1],
                                   pointsX[indexBegin + 2]);

            int indexMiddle = (indexBegin + indexEnd) / 2;
            var closetsYL = new List<Point2D>();
            var closetsYR = new List<Point2D>();

            foreach(Point2D pt in pointsY)
                if(pt.X < pointsX[indexMiddle].X
                        || pt.X == pointsX[indexMiddle].X && pt.Y < pointsX[indexMiddle].Y)
                    closetsYL.Add(pt);
                else
                    closetsYR.Add(pt);

            (Point2D Closest1, Point2D Closest2) closestLeft =
                searchClosest(pointsX, closetsYL, indexBegin..indexMiddle);
            (Point2D Closest1, Point2D Closest2) closestRight =
                searchClosest(pointsX, closetsYR, indexMiddle..indexEnd);
            (Point2D Closest1, Point2D Closest2) closestPoints =
                Geometry2D.Distance(closestLeft.Closest1, closestLeft.Closest2)
                    <= Geometry2D.Distance(closestRight.Closest1, closestRight.Closest2)
                    ? closestLeft : closestRight;
            (Point2D Closest1, Point2D Closest2)? beltPoints =
                checkBelt(pointsY, pointsX[indexMiddle].X,
                          Geometry2D.Distance(closestPoints.Closest1, closestPoints.Closest2));

            return beltPoints ?? closestPoints;
        }
    }
}
