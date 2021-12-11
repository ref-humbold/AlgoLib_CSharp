// Tests: Algorithm for pair of closest points
using FluentAssertions;
using NUnit.Framework;
using System;

namespace AlgoLib.Geometry.Dim2
{
    [TestFixture]
    public class ClosestPointsTest
    {
        [Test]
        public void FindClosestPoints_WhenOnePoint_ThenThisPoint()
        {
            // when
            (Point2D, Point2D) result = ClosestPoints.Find(new Point2D[] { Point2D.Of(2, 2) });
            // then
            result.As<IEquatable<(Point2D, Point2D)>>().Should().Be((Point2D.Of(2, 2), Point2D.Of(2, 2)));
        }

        [Test]
        public void FindClosestPoints_WhenTwoPoints_ThenThesePoints()
        {
            // when
            (Point2D, Point2D) result =
                    ClosestPoints.Find(new Point2D[] { Point2D.Of(2, 2), Point2D.Of(4, 4) });
            // then
            result.As<IEquatable<(Point2D, Point2D)>>().Should().Be((Point2D.Of(2, 2), Point2D.Of(4, 4)));
        }

        [Test]
        public void FindClosestPoints_WhenThreePoints_ThenPairOfClosestPoints()
        {
            // when
            (Point2D, Point2D) result =
                ClosestPoints.Find(new Point2D[] { Point2D.Of(3, 2), Point2D.Of(1, 1), Point2D.Of(7, 0) });
            // then
            result.As<IEquatable<(Point2D, Point2D)>>().Should().Be((Point2D.Of(1, 1), Point2D.Of(3, 2)));
        }

        [Test]
        public void FindClosestPoints_WhenMultiplePoints_ThenPairOfClosestPoints()
        {
            // when
            (Point2D, Point2D) result = ClosestPoints.Find(
                    new Point2D[] { Point2D.Of(1, 1), Point2D.Of(-2, 2), Point2D.Of(-4, 4),
                                    Point2D.Of(3, -3), Point2D.Of(0, -5), Point2D.Of(1, 0),
                                    Point2D.Of(-7, 2), Point2D.Of(4, 5) });
            // then
            result.As<IEquatable<(Point2D, Point2D)>>().Should().Be((Point2D.Of(1, 1), Point2D.Of(1, 0)));
        }
    }
}
