using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Geometry.Dim2;

/// <summary>Algorithms for convex hull in 2D.</summary>
public static class ConvexHull
{
    /// <summary>Computes convex hull of given points using Andrew's monotone chain.</summary>
    /// <param name="points">The points.</param>
    /// <returns>The points in the convex hull.</returns>
    public static IEnumerable<Point2D> FindAndrewConvexHull(IEnumerable<Point2D> points)
    {
        List<Point2D> pointsList = points.ToList();

        if(pointsList.Count < 3)
            return [];

        List<Point2D> sorted = pointsList.SortByX().ToList();
        List<Point2D> lowerHull = collectHull(sorted);
        List<Point2D> upperHull = collectHull(Enumerable.Reverse(sorted));

        lowerHull.RemoveAt(lowerHull.Count - 1);
        upperHull.RemoveAt(upperHull.Count - 1);
        return lowerHull.Concat(upperHull);
    }

    /// <summary>Computes convex hull of given points using Graham's scan.</summary>
    /// <param name="points">The points.</param>
    /// <returns>The points in the convex hull.</returns>
    public static IEnumerable<Point2D> FindGrahamConvexHull(IEnumerable<Point2D> points)
    {
        List<Point2D> pointsList = points.ToList();

        if(pointsList.Count < 3)
            return [];

        Point2D minimal = pointsList.MinBy(pt => (pt.Y, pt.X));
        Vector2D moving = Vector2D.Between(minimal, Point2D.Zero);

        List<Point2D> sorted = pointsList.Select(pt => pt.Translate(moving))
                                         .SortByAngle()
                                         .ToList();

        return collectHull(sorted).Select(pt => pt.Translate(-moving));
    }

    // Collects points of convex hull for given points.
    private static List<Point2D> collectHull(IEnumerable<Point2D> points)
    {
        List<Point2D> hull = [];

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
