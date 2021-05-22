using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Geometry.Plane
{
    [TestFixture]
    public class Geometry3DTests
    {
        [Test]
        public void SortByX_ThenSortedStablyAscendingByXCoordinate()
        {
            // given
            List<Point2D> sequence = new List<Point2D> {
                Point2D.Of(0.0, 0.0), Point2D.Of(-2.0, -3.0), Point2D.Of(-3.0, 2.0),
                Point2D.Of(2.0, 3.0), Point2D.Of(3.0, -2.0), Point2D.Of(-2.0, 3.0),
                Point2D.Of(3.0, 2.0), Point2D.Of(2.0, -3.0), Point2D.Of(-3.0, -2.0) };
            // when
            List<Point2D> result = Geometry2D.SortByX(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point2D> {
                Point2D.Of(-3.0, 2.0), Point2D.Of(-3.0, -2.0), Point2D.Of(-2.0, -3.0), Point2D.Of(-2.0, 3.0),
                Point2D.Of(0.0, 0.0), Point2D.Of(2.0, 3.0), Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0),
                Point2D.Of(3.0, 2.0) });
        }

        [Test]
        public void SortByY_ThenSortedStablyAscendingByYCoordinate()
        {
            // given
            List<Point2D> sequence = new List<Point2D> {
                Point2D.Of(0.0, 0.0), Point2D.Of(-2.0, -3.0), Point2D.Of(-3.0, 2.0),
                Point2D.Of(2.0, 3.0), Point2D.Of(3.0, -2.0), Point2D.Of(-2.0, 3.0),
                Point2D.Of(3.0, 2.0), Point2D.Of(2.0, -3.0), Point2D.Of(-3.0, -2.0) };
            // when
            List<Point2D> result = Geometry2D.SortByY(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point2D> {
                Point2D.Of(-2.0, -3.0), Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0),
                Point2D.Of(-3.0, -2.0), Point2D.Of(0.0, 0.0), Point2D.Of(-3.0, 2.0),
                Point2D.Of(3.0, 2.0), Point2D.Of(2.0, 3.0), Point2D.Of(-2.0, 3.0) });
        }

        [Test]
        public void SortByAngle_ThenSortedAscendingByAngleInDegrees()
        {
            // given
            List<Point2D> sequence = new List<Point2D> {
                Point2D.Of(0.0, 0.0), Point2D.Of(-2.0, -3.0), Point2D.Of(-3.0, 2.0),
                Point2D.Of(2.0, 3.0), Point2D.Of(3.0, -2.0), Point2D.Of(-2.0, 3.0),
                Point2D.Of(3.0, 2.0), Point2D.Of(2.0, -3.0), Point2D.Of(-3.0, -2.0) };
            // when
            List<Point2D> result = Geometry2D.SortByAngle(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point2D> {
                Point2D.Of(0.0, 0.0), Point2D.Of(3.0, 2.0), Point2D.Of(2.0, 3.0),
                Point2D.Of(-2.0, 3.0), Point2D.Of(-3.0, 2.0), Point2D.Of(-3.0, -2.0),
                Point2D.Of(-2.0, -3.0), Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0) });
        }
    }
}
