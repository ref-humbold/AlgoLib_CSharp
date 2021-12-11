// Tests: Algorithm for convex hull (monotone chain)
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Geometry.Dim2
{
    [TestFixture]
    public class ConvexHullTest
    {
        [Test]
        public void Find_WhenOnePoint_ThenEmpty()
        {
            // when
            IEnumerable<Point2D> result = ConvexHull.Find(new Point2D[] { Point2D.Of(3.0, 2.0) });
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Find_WhenTwoPoints_ThenEmpty()
        {
            // when
            IEnumerable<Point2D> result =
                ConvexHull.Find(new Point2D[] { Point2D.Of(2.0, 3.0), Point2D.Of(3.0, 2.0) });
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Find_WhenThreePoints_ThenThesePointsInHull()
        {
            // given
            Point2D[] points =
                new Point2D[] { Point2D.Of(1.0, -1.0), Point2D.Of(5.0, 1.0), Point2D.Of(3.0, 4.0) };
            // when
            IEnumerable<Point2D> result = ConvexHull.Find(points);
            // then
            result.Should().BeEquivalentTo(points);
        }

        [Test]
        public void Find_ThenPointsInHull()
        {
            // when
            IEnumerable<Point2D> result = ConvexHull.Find(
                    new Point2D[] { Point2D.Of(1, -3), Point2D.Of(-4, 6), Point2D.Of(-5, -7),
                                    Point2D.Of(-8, -7), Point2D.Of(-3, -4), Point2D.Of(5, 9),
                                    Point2D.Of(-1, -8), Point2D.Of(-5, 10), Point2D.Of(8, 0),
                                    Point2D.Of(3, -6), Point2D.Of(-2, 1), Point2D.Of(-2, 8),
                                    Point2D.Of(10, 2), Point2D.Of(6, 3), Point2D.Of(-7, 7),
                                    Point2D.Of(6, -4) });
            // then
            result.Should().BeEquivalentTo(
                    new Point2D[] { Point2D.Of(-8, -7), Point2D.Of(-1, -8), Point2D.Of(3, -6),
                                    Point2D.Of(6, -4), Point2D.Of(10, 2), Point2D.Of(5, 9),
                                    Point2D.Of(-5, 10), Point2D.Of(-7, 7) });
        }

        [Test]
        public void Find_WhenMultiplePointsAreCollinear_ThenInnerPointsOmitted()
        {
            // when
            IEnumerable<Point2D> result = ConvexHull.Find(
                    new Point2D[] { Point2D.Of(-1, -3), Point2D.Of(-3, -3), Point2D.Of(-3, -1),
                                    Point2D.Of(2, -3), Point2D.Of(-3, 5), Point2D.Of(0, -3),
                                    Point2D.Of(7, -3), Point2D.Of(-3, -2) });
            // then
            result.Should().BeEquivalentTo(
                new Point2D[] { Point2D.Of(-3, -3), Point2D.Of(7, -3), Point2D.Of(-3, 5) });
        }
    }
}
