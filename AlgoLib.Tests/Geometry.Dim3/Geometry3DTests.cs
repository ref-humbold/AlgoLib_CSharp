// Tests: Algorithms for basic geometrical operations in 3D
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Geometry.Dim3
{
    [TestFixture]
    public class Geometry3DTests
    {
        [Test]
        public void SortByX_ThenSortedStablyAscending()
        {
            // given
            List<Point3D> sequence = new List<Point3D> {
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(-2.0, -3.0, 5.0),
                Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(3.0, 2.0, 5.0),
                Point3D.Of(-3.0, 2.0, 5.0) };
            // when
            List<Point3D> result = sequence.SortByX();
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(Point3D.Of(-3.0, 2.0, 5.0), Point3D.Of(-2.0, -3.0, 5.0),
                                  Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(0.0, 0.0, 0.0),
                                  Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(2.0, -3.0, -5.0),
                                  Point3D.Of(3.0, 2.0, 5.0));
        }

        [Test]
        public void SortByY_ThenSortedStablyAscending()
        {
            // given
            List<Point3D> sequence = new List<Point3D> {
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(-2.0, -3.0, 5.0),
                Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(3.0, 2.0, 5.0),
                Point3D.Of(-3.0, 2.0, 5.0) };
            // when
            List<Point3D> result = sequence.SortByY();
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(Point3D.Of(-2.0, -3.0, 5.0), Point3D.Of(2.0, -3.0, -5.0),
                                  Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(0.0, 0.0, 0.0),
                                  Point3D.Of(3.0, 2.0, 5.0), Point3D.Of(-3.0, 2.0, 5.0),
                                  Point3D.Of(2.0, 3.0, -5.0));
        }

        [Test]
        public void SortByZ_ThenSortedStablyAscending()
        {
            // given
            List<Point3D> sequence = new List<Point3D> {
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(-2.0, -3.0, 5.0),
                Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(3.0, 2.0, 5.0),
                Point3D.Of(-3.0, 2.0, 5.0) };
            // when
            List<Point3D> result = sequence.SortByZ();
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(2.0, -3.0, -5.0),
                                  Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(0.0, 0.0, 0.0),
                                  Point3D.Of(-2.0, -3.0, 5.0), Point3D.Of(3.0, 2.0, 5.0),
                                  Point3D.Of(-3.0, 2.0, 5.0));
        }

        [Test]
        public void Distance_WhenDifferentPoints_ThenDistance()
        {
            // when
            double result = Point3D.Of(4.0, 8.0, 5.0).Distance(Point3D.Of(-2.0, -1.0, 3.0));
            // then
            result.Should().Be(11.0);
        }

        [Test]
        public void Distance_WhenSamePoint_ThenZero()
        {
            // given
            Point3D point = Point3D.Of(13.5, 6.5, -4.2);
            // when
            double result = point.Distance(point);
            // then
            result.Should().Be(0.0);
        }

        [Test]
        public void Translate_ThenPointTranslated()
        {
            // when
            Point3D result = Point3D.Of(13.7, 6.5, -4.2).Translate(Vector3D.Of(-10.4, 3.3, 1.1));
            // then
            result.Should().Be(Point3D.Of(3.3, 9.8, -3.1));
        }

        [Test]
        public void Translate_WhenZeroVector_ThenSamePoint()
        {
            // given
            Point3D point = Point3D.Of(13.5, 6.5, -4.2);
            // when
            Point3D result = point.Translate(Vector3D.Of(0.0, 0.0, 0.0));
            // then
            result.Should().Be(point);
        }

        [Test]
        public void Reflect_ThenPointReflected()
        {
            // when
            Point3D result = Point3D.Of(13.5, 6.5, -4.2).Reflect(Point3D.Of(2.0, -1.0, -3.0));
            // then
            result.Should().Be(Point3D.Of(-9.5, -8.5, -1.8));
        }

        [Test]
        public void Reflect_WhenZeroPoint_ThenPointReflected()
        {
            // when
            Point3D result = Point3D.Of(13.5, 6.5, -4.2).Reflect(Point3D.Of(0.0, 0.0, 0.0));
            // then
            result.Should().Be(Point3D.Of(-13.5, -6.5, 4.2));
        }

        [Test]
        public void Reflect_WhenSamePoint_ThenSamePoint()
        {
            // given
            Point3D point = Point3D.Of(13.5, 6.5, -4.2);
            // when
            Point3D result = point.Reflect(point);
            // then
            result.Should().Be(point);
        }
    }
}
