// Tests: Algorithms for basic geometrical operations in 2D
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Geometry.Dim2
{
    [TestFixture]
    public class Geometry2DTests
    {
        [Test]
        public void SortByX_ThenSortedStablyAscending()
        {
            // given
            List<Point2D> sequence = new List<Point2D> {
                Point2D.Of(0.0, 0.0), Point2D.Of(-2.0, -3.0), Point2D.Of(-3.0, 2.0),
                Point2D.Of(2.0, 3.0), Point2D.Of(3.0, -2.0), Point2D.Of(-2.0, 3.0),
                Point2D.Of(3.0, 2.0), Point2D.Of(2.0, -3.0), Point2D.Of(-3.0, -2.0) };
            // when
            List<Point2D> result = sequence.SortByX();
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(Point2D.Of(-3.0, 2.0), Point2D.Of(-3.0, -2.0), Point2D.Of(-2.0, -3.0),
                                  Point2D.Of(-2.0, 3.0), Point2D.Of(0.0, 0.0), Point2D.Of(2.0, 3.0),
                                  Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0), Point2D.Of(3.0, 2.0));
        }

        [Test]
        public void SortByY_ThenSortedStablyAscending()
        {
            // given
            List<Point2D> sequence = new List<Point2D> {
                Point2D.Of(0.0, 0.0), Point2D.Of(-2.0, -3.0), Point2D.Of(-3.0, 2.0),
                Point2D.Of(2.0, 3.0), Point2D.Of(3.0, -2.0), Point2D.Of(-2.0, 3.0),
                Point2D.Of(3.0, 2.0), Point2D.Of(2.0, -3.0), Point2D.Of(-3.0, -2.0) };
            // when
            List<Point2D> result = sequence.SortByY();
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(Point2D.Of(-2.0, -3.0), Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0),
                                  Point2D.Of(-3.0, -2.0), Point2D.Of(0.0, 0.0), Point2D.Of(-3.0, 2.0),
                                  Point2D.Of(3.0, 2.0), Point2D.Of(2.0, 3.0), Point2D.Of(-2.0, 3.0));
        }

        [Test]
        public void SortByAngle_ThenSortedStablyAscending()
        {
            // given
            List<Point2D> sequence = new List<Point2D> {
                Point2D.Of(0.0, 0.0), Point2D.Of(-2.0, -3.0), Point2D.Of(-3.0, 2.0),
                Point2D.Of(2.0, 3.0), Point2D.Of(3.0, -2.0), Point2D.Of(-2.0, 3.0),
                Point2D.Of(3.0, 2.0), Point2D.Of(2.0, -3.0), Point2D.Of(-3.0, -2.0) };
            // when
            List<Point2D> result = sequence.SortByAngle();
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(Point2D.Of(0.0, 0.0), Point2D.Of(3.0, 2.0), Point2D.Of(2.0, 3.0),
                                  Point2D.Of(-2.0, 3.0), Point2D.Of(-3.0, 2.0), Point2D.Of(-3.0, -2.0),
                                  Point2D.Of(-2.0, -3.0), Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0));
        }

        [Test]
        public void Distance_WhenDifferentPoints_ThenDistance()
        {
            // when
            double result = Point2D.Of(4.0, 5.0).Distance(Point2D.Of(-2.0, -3.0));
            // then
            result.Should().Be(10.0);
        }

        [Test]
        public void Distance_WhenSamePoint_ThenZero()
        {
            // given
            Point2D point = Point2D.Of(13.5, 6.5);
            // when
            double result = point.Distance(point);
            // then
            result.Should().Be(0.0);
        }

        [Test]
        public void Translate_ThenPointTranslated()
        {
            // when
            Point2D result = Point2D.Of(13.7, 6.5).Translate(Vector2D.Of(-10.4, 3.3));
            // then
            result.Should().Be(Point2D.Of(3.3, 9.8));
        }

        [Test]
        public void Translate_WhenZeroVector_ThenSamePoint()
        {
            // given
            Point2D point = Point2D.Of(13.5, 6.5);
            // when
            Point2D result = point.Translate(Vector2D.Of(0.0, 0.0));
            // then
            result.Should().Be(point);
        }

        [Test]
        public void Reflect_ThenPointReflected()
        {
            // when
            Point2D result = Point2D.Of(13.5, 6.5).Reflect(Point2D.Of(2.0, -3.0));
            // then
            result.Should().Be(Point2D.Of(-9.5, -12.5));
        }

        [Test]
        public void Reflect_WhenZeroPoint_ThenPointReflected()
        {
            // when
            Point2D result = Point2D.Of(13.5, 6.5).Reflect(Point2D.Of(0.0, 0.0));
            // then
            result.Should().Be(Point2D.Of(-13.5, -6.5));
        }

        [Test]
        public void Reflect_WhenSamePoint_ThenSamePoint()
        {
            // given
            Point2D point = Point2D.Of(13.5, 6.5);
            // when
            Point2D result = point.Reflect(point);
            // then
            result.Should().Be(point);
        }
    }
}
