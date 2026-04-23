using System.Collections.Generic;
using NUnit.Framework;

namespace AlgoLib.Geometry.Dim2;

// Tests: Algorithms for convex hull in 2D.
[TestFixture]
public class ConvexHullTest
{
    #region FindAndrewConvexHull

    [Test]
    public void FindAndrewConvexHull_WhenOnePoint_ThenEmpty()
    {
        // when
        IEnumerable<Point2D> result = ConvexHull.FindAndrewConvexHull([Point2D.Of(3.0, 2.0)]);

        // then
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void FindAndrewConvexHull_WhenTwoPoints_ThenEmpty()
    {
        // when
        IEnumerable<Point2D> result =
            ConvexHull.FindAndrewConvexHull([Point2D.Of(2.0, 3.0), Point2D.Of(3.0, 2.0)]);

        // then
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void FindAndrewConvexHull_WhenThreePoints_ThenThesePointsInHull()
    {
        // given
        Point2D[] points =
            [Point2D.Of(1.0, -1.0), Point2D.Of(5.0, 1.0), Point2D.Of(3.0, 4.0)];

        // when
        IEnumerable<Point2D> result = ConvexHull.FindAndrewConvexHull(points);

        // then
        Assert.That(result, Is.EquivalentTo(points));
    }

    [Test]
    public void FindAndrewConvexHull_ThenPointsInHull()
    {
        // when
        IEnumerable<Point2D> result = ConvexHull.FindAndrewConvexHull(
            [
                Point2D.Of(1, -3), Point2D.Of(-4, 6), Point2D.Of(-5, -7),
                Point2D.Of(-8, -7), Point2D.Of(-3, -4), Point2D.Of(5, 9),
                Point2D.Of(-1, -8), Point2D.Of(-5, 10), Point2D.Of(8, 0),
                Point2D.Of(3, -6), Point2D.Of(-2, 1), Point2D.Of(-2, 8),
                Point2D.Of(10, 2), Point2D.Of(6, 3), Point2D.Of(-7, 7),
                Point2D.Of(6, -4)
            ]);

        // then
        Assert.That(
            result, Is.EquivalentTo(
                [
                    Point2D.Of(-8, -7), Point2D.Of(-1, -8), Point2D.Of(3, -6),
                    Point2D.Of(6, -4), Point2D.Of(10, 2), Point2D.Of(5, 9),
                    Point2D.Of(-5, 10), Point2D.Of(-7, 7)
                ]));
    }

    [Test]
    public void FindAndrewConvexHull_WhenMultiplePointsAreCollinear_ThenInnerPointsOmitted()
    {
        // when
        IEnumerable<Point2D> result = ConvexHull.FindAndrewConvexHull(
            [
                Point2D.Of(-1, -3), Point2D.Of(-3, -3), Point2D.Of(-3, -1),
                Point2D.Of(2, -3), Point2D.Of(-3, 5), Point2D.Of(0, -3),
                Point2D.Of(7, -3), Point2D.Of(-3, -2)
            ]);

        // then
        Assert.That(
            result, Is.EquivalentTo(
            [Point2D.Of(-3, -3), Point2D.Of(7, -3), Point2D.Of(-3, 5)]));
    }

    #endregion
    #region FindGrahamConvexHull

    [Test]
    public void FindGrahamConvexHull_WhenOnePoint_ThenEmpty()
    {
        // when
        IEnumerable<Point2D> result = ConvexHull.FindGrahamConvexHull([Point2D.Of(3.0, 2.0)]);

        // then
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void FindGrahamConvexHull_WhenTwoPoints_ThenEmpty()
    {
        // when
        IEnumerable<Point2D> result =
            ConvexHull.FindGrahamConvexHull([Point2D.Of(2.0, 3.0), Point2D.Of(3.0, 2.0)]);

        // then
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void FindGrahamConvexHull_WhenThreePoints_ThenThesePointsInHull()
    {
        // given
        Point2D[] points =
            [Point2D.Of(1.0, -1.0), Point2D.Of(5.0, 1.0), Point2D.Of(3.0, 4.0)];

        // when
        IEnumerable<Point2D> result = ConvexHull.FindGrahamConvexHull(points);

        // then
        Assert.That(result, Is.EquivalentTo(points));
    }

    [Test]
    public void FindGrahamConvexHull_ThenPointsInHull()
    {
        // when
        IEnumerable<Point2D> result = ConvexHull.FindGrahamConvexHull(
            [
                Point2D.Of(1, -3), Point2D.Of(-4, 6), Point2D.Of(-5, -7),
                Point2D.Of(-8, -7), Point2D.Of(-3, -4), Point2D.Of(5, 9),
                Point2D.Of(-1, -8), Point2D.Of(-5, 10), Point2D.Of(8, 0),
                Point2D.Of(3, -6), Point2D.Of(-2, 1), Point2D.Of(-2, 8),
                Point2D.Of(10, 2), Point2D.Of(6, 3), Point2D.Of(-7, 7),
                Point2D.Of(6, -4)
            ]);

        // then
        Assert.That(
            result, Is.EquivalentTo(
                [
                    Point2D.Of(-8, -7), Point2D.Of(-1, -8), Point2D.Of(3, -6),
                    Point2D.Of(6, -4), Point2D.Of(10, 2), Point2D.Of(5, 9),
                    Point2D.Of(-5, 10), Point2D.Of(-7, 7)
                ]));
    }

    [Test]
    public void FindGrahamConvexHull_WhenMultiplePointsAreCollinear_ThenInnerPointsOmitted()
    {
        // when
        IEnumerable<Point2D> result = ConvexHull.FindGrahamConvexHull(
            [
                Point2D.Of(-1, -3), Point2D.Of(-3, -3), Point2D.Of(-3, -1),
                Point2D.Of(2, -3), Point2D.Of(-3, 5), Point2D.Of(0, -3),
                Point2D.Of(7, -3), Point2D.Of(-3, -2)
            ]);

        // then
        Assert.That(
            result, Is.EquivalentTo(
            [Point2D.Of(-3, -3), Point2D.Of(7, -3), Point2D.Of(-3, 5)]));
    }

    #endregion
}
