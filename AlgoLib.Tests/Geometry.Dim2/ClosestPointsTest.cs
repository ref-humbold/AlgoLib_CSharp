// Tests: Algorithm for searching pair of closest points in 2D.
using System;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Geometry.Dim2
{
    [TestFixture]
    public class ClosestPointsTest
    {
        [Test]
        public void FindClosestPoints_WhenNoPoints_ThenInvalidOperationException()
        {
            // when
            Action action = () => ClosestPoints.FindClosestPoints(Array.Empty<Point2D>());
            // then
            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void FindClosestPoints_WhenOnePoint_ThenThisPoint()
        {
            // when
            (Point2D, Point2D) result = ClosestPoints.FindClosestPoints(new[] { Point2D.Of(2, 2) });
            // then
            result.As<IEquatable<(Point2D, Point2D)>>().Should().Be((Point2D.Of(2, 2), Point2D.Of(2, 2)));
        }

        [Test]
        public void FindClosestPoints_WhenTwoPoints_ThenThesePoints()
        {
            // when
            (Point2D, Point2D) result =
                    ClosestPoints.FindClosestPoints(new[] { Point2D.Of(2, 2), Point2D.Of(4, 4) });
            // then
            result.As<IEquatable<(Point2D, Point2D)>>().Should().Be((Point2D.Of(2, 2), Point2D.Of(4, 4)));
        }

        [Test]
        public void FindClosestPoints_WhenThreePoints_ThenPairOfClosestPoints()
        {
            // when
            (Point2D, Point2D) result =
                ClosestPoints.FindClosestPoints(new[] { Point2D.Of(3, 2), Point2D.Of(1, 1), Point2D.Of(7, 0) });
            // then
            result.As<IEquatable<(Point2D, Point2D)>>().Should().Be((Point2D.Of(1, 1), Point2D.Of(3, 2)));
        }

        [Test]
        public void FindClosestPoints_WhenMultiplePoints_ThenPairOfClosestPoints()
        {
            // when
            (Point2D, Point2D) result = ClosestPoints.FindClosestPoints(
                    new[] { Point2D.Of(1, 1), Point2D.Of(-2, 2), Point2D.Of(-4, 4),
                            Point2D.Of(3, -3), Point2D.Of(0, -5), Point2D.Of(1, 0),
                            Point2D.Of(-7, 2), Point2D.Of(4, 5) });
            // then
            result.As<IEquatable<(Point2D, Point2D)>>().Should().Be((Point2D.Of(1, 0), Point2D.Of(1, 1)));
        }

        [Test]
        public void FindClosestPoints_WhenAllLinearOnX_ThenPairOfClosestPoints()
        {
            // when
            (Point2D, Point2D) result = ClosestPoints.FindClosestPoints(
                    new[] { Point2D.Of(14, -40), Point2D.Of(14, -3), Point2D.Of(14, 36),
                            Point2D.Of(14, 7), Point2D.Of(14, -24), Point2D.Of(14, 1),
                            Point2D.Of(14, -14), Point2D.Of(14, 19) });
            // then
            result.As<IEquatable<(Point2D, Point2D)>>().Should().Be((Point2D.Of(14, -3), Point2D.Of(14, 1)));
        }

        [Test]
        public void FindClosestPoints_WhenAllLinearOnY_ThenPairOfClosestPoints()
        {
            // when
            (Point2D, Point2D) result = ClosestPoints.FindClosestPoints(
                    new[] { Point2D.Of(-27, -6), Point2D.Of(13, -6), Point2D.Of(-8, -6),
                            Point2D.Of(30, -6), Point2D.Of(6, -6), Point2D.Of(-15, -6),
                            Point2D.Of(-3, -6), Point2D.Of(22, -6) });
            // then
            result.As<IEquatable<(Point2D, Point2D)>>().Should().Be((Point2D.Of(-8, -6), Point2D.Of(-3, -6)));
        }
    }
}
