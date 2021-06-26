// Algorithm for pair of closest points in 2D
using System.Collections.Generic;

namespace Algolib.Geometry.Plane
{
    public sealed class ClosestPoints
    {
        /// <summary>Searches for a pair closest of points among given points.</summary>
        /// <param name="points">A list of points</param>
        /// <returns>Pair of closest points</returns>
        public static (Point2D, Point2D) Find(List<Point2D> points) =>
            searchClosest(Geometry2D.SortByX(points), Geometry2D.SortByY(points), 0, -1);

        // Finds closest pair of points among three of them.
        private static (Point2D, Point2D) searchThree(List<Point2D> pointsX, int index_begin,
                                                      int index_end)
        {
            int index_middle = index_begin + 1;
            double distance12 = Geometry2D.Distance(pointsX[index_begin], pointsX[index_middle]);
            double distance23 = Geometry2D.Distance(pointsX[index_middle], pointsX[index_end]);
            double distance31 = Geometry2D.Distance(pointsX[index_begin], pointsX[index_end]);

            if(distance12 <= distance23 && distance12 <= distance31)
                return (pointsX[index_begin], pointsX[index_middle]);

            if(distance23 <= distance12 && distance23 <= distance31)
                return (pointsX[index_middle], pointsX[index_end]);

            return (pointsX[index_begin], pointsX[index_end]);
        }

        // Finds closest pair inside a belt of given width. The resulting distance should not be
        // less than belt width.
        private static (Point2D, Point2D)? checkBelt(List<Point2D> pointsY, double middleX,
                                                     double beltWidth)
        {
            (Point2D, Point2D)? closestPoints = null;
            List<int> beltPoints = new List<int>();
            double minDistance = beltWidth;

            for(int i = 0; i < pointsY.Count; ++i)
                if(pointsY[i].X >= middleX - beltWidth && pointsY[i].X <= middleX + beltWidth)
                    beltPoints.Add(i);

            for(int i = 1; i < beltPoints.Count; ++i)
                for(int j = i + 1; j < beltPoints.Count; ++j)
                {
                    Point2D pt1 = pointsY[beltPoints[i]];
                    Point2D pt2 = pointsY[beltPoints[j]];

                    if(pt2.Y > pt1.Y + beltWidth)
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

        // Searches for a pair of closest points in given sublist of points. Points are given sorted
        // by X coordinate and by Y coordinate. (index_begin & index_end inclusive)
        private static (Point2D, Point2D) searchClosest(List<Point2D> pointsX, List<Point2D> pointsY,
                                                        int index_begin, int index_end)
        {
            index_begin = (index_begin + pointsX.Count) % pointsX.Count;
            index_end = (index_end + pointsX.Count) % pointsX.Count;

            if(index_end - index_begin <= 1)
                return (pointsX[index_begin], pointsX[index_end]);

            if(index_end - index_begin == 2)
                return searchThree(pointsX, index_begin, index_end);

            int index_middle = (index_begin + index_end) / 2;
            double middleX = (pointsX[index_middle].X + pointsX[index_middle + 1].X) / 2;
            List<Point2D> pointsYL = new List<Point2D>();
            List<Point2D> pointsYR = new List<Point2D>();

            foreach(Point2D pt in pointsY)
                if(pt.X <= index_middle)
                    pointsYL.Add(pt);
                else
                    pointsYR.Add(pt);

            (Point2D, Point2D) closestL = searchClosest(pointsX, pointsYL, index_begin, index_middle);
            (Point2D, Point2D) closestR = searchClosest(pointsX, pointsYR, index_middle + 1, index_end);
            (Point2D, Point2D) closestPoints =
                    Geometry2D.Distance(closestL.Item1, closestL.Item2)
                        <= Geometry2D.Distance(closestR.Item1, closestR.Item2) ? closestL : closestR;
            (Point2D, Point2D)? beltPoints = checkBelt(pointsY, middleX,
                                                       Geometry2D.Distance(closestPoints.Item1,
                                                                           closestPoints.Item2));

            return beltPoints ?? closestPoints;
        }
    }
}
