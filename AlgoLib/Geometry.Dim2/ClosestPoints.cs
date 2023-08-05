// Algorithm for pair of closest points in 2D
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Geometry.Dim2
{
    public static class ClosestPoints
    {
        /// <summary>Searches for pair of closest points among given points.</summary>
        /// <param name="points">Enumerable of points.</param>
        /// <returns>Pair of the closest points.</returns>
        /// <exception cref="InvalidOperationException">If no points specified</exception>
        public static (Point2D, Point2D) FindClosestPoints(IEnumerable<Point2D> points)
        {
            var pointsList = points.ToList();

            return pointsList.Count == 0
                ? throw new InvalidOperationException("Sequence contains no elements")
                : searchClosest(Geometry2D.SortByX(pointsList), Geometry2D.SortByY(pointsList), ..);
        }

        // Finds pair of closest points among three of them.
        private static (Point2D, Point2D) searchThree(List<Point2D> pointsX, int indexBegin)
        {
            int indexMiddle = indexBegin + 1;
            int indexEnd = indexBegin + 2;
            double distance01 = Geometry2D.Distance(pointsX[indexBegin], pointsX[indexMiddle]);
            double distance12 = Geometry2D.Distance(pointsX[indexMiddle], pointsX[indexEnd]);
            double distance20 = Geometry2D.Distance(pointsX[indexBegin], pointsX[indexEnd]);

            return distance01 <= distance12 && distance01 <= distance20
                ? (pointsX[indexBegin], pointsX[indexMiddle])
                : distance12 <= distance01 && distance12 <= distance20
                    ? (pointsX[indexMiddle], pointsX[indexEnd])
                    : (pointsX[indexBegin], pointsX[indexEnd]);
        }

        // Finds pair of closest points inside a belt of given width. The resulting distance should
        // not be less than belt width.
        private static (Point2D, Point2D)? checkBelt(List<Point2D> pointsY,
                                                     double middleX,
                                                     double width)
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

                    if(pt1.X <= middleX && pt2.X > middleX || pt1.X > middleX && pt2.X <= middleX)
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

        // Searches for pair of closest points in given sublist of points. Points are given sorted
        // by X coordinate and by Y coordinate.
        private static (Point2D, Point2D) searchClosest(List<Point2D> pointsX,
                                                        List<Point2D> pointsY,
                                                        Range range)
        {
            int indexBegin = range.Start.GetOffset(pointsX.Count);
            int indexEnd = range.End.GetOffset(pointsX.Count);

            if(indexEnd - indexBegin <= 2)
                return (pointsX[indexBegin], pointsX[indexEnd - 1]);

            if(indexEnd - indexBegin == 3)
                return searchThree(pointsX, indexBegin);

            int indexMiddle = (indexBegin + indexEnd) / 2;
            double middleX = (pointsX[indexMiddle].X + pointsX[indexMiddle + 1].X) / 2;
            var pointsYLeft = new List<Point2D>();
            var pointsYRight = new List<Point2D>();

            foreach(Point2D pt in pointsY)
                if(pt.X < indexMiddle)
                    pointsYLeft.Add(pt);
                else
                    pointsYRight.Add(pt);

            (Point2D, Point2D) closestLeft = searchClosest(pointsX, pointsYLeft,
                                                           indexBegin..indexMiddle);
            (Point2D, Point2D) closestRight = searchClosest(pointsX, pointsYRight,
                                                            indexMiddle..indexEnd);
            (Point2D, Point2D) closestPoints =
                Geometry2D.Distance(closestLeft.Item1, closestLeft.Item2)
                    <= Geometry2D.Distance(closestRight.Item1, closestRight.Item2)
                    ? closestLeft : closestRight;
            (Point2D, Point2D)? beltPoints =
                checkBelt(pointsY, middleX, Geometry2D.Distance(closestPoints.Item1,
                                                                closestPoints.Item2));

            return beltPoints ?? closestPoints;
        }
    }
}
