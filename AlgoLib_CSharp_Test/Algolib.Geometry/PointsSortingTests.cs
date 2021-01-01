using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;

namespace Algolib.Geometry
{
    [TestFixture]
    public class PointsSortingTest
    {
        [Test]
        public void SortByX_When2D_ThenSortedStablyAscendingByXCoordinate()
        {
            // given
            List<Point2D> sequence = new List<Point2D> {
                Point2D.Of(0.0, 0.0), Point2D.Of(-2.0, -3.0), Point2D.Of(-3.0, 2.0),
                Point2D.Of(2.0, 3.0), Point2D.Of(3.0, -2.0), Point2D.Of(-2.0, 3.0),
                Point2D.Of(3.0, 2.0), Point2D.Of(2.0, -3.0), Point2D.Of(-3.0, -2.0) };
            // when
            List<Point2D> result = PointsSorting.SortByX(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point2D> {
                Point2D.Of(-3.0, 2.0), Point2D.Of(-3.0, -2.0), Point2D.Of(-2.0, -3.0), Point2D.Of(-2.0, 3.0),
                Point2D.Of(0.0, 0.0), Point2D.Of(2.0, 3.0), Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0),
                Point2D.Of(3.0, 2.0) });
        }

        [Test]
        public void SortByY_When2D_ThenSortedStablyAscendingByYCoordinate()
        {
            // given
            List<Point2D> sequence = new List<Point2D> {
                Point2D.Of(0.0, 0.0), Point2D.Of(-2.0, -3.0), Point2D.Of(-3.0, 2.0),
                Point2D.Of(2.0, 3.0), Point2D.Of(3.0, -2.0), Point2D.Of(-2.0, 3.0),
                Point2D.Of(3.0, 2.0), Point2D.Of(2.0, -3.0), Point2D.Of(-3.0, -2.0) };
            // when
            List<Point2D> result = PointsSorting.SortByY(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point2D> {
                Point2D.Of(-2.0, -3.0), Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0),
                Point2D.Of(-3.0, -2.0), Point2D.Of(0.0, 0.0), Point2D.Of(-3.0, 2.0),
                Point2D.Of(3.0, 2.0), Point2D.Of(2.0, 3.0), Point2D.Of(-2.0, 3.0) });
        }

        [Test]
        public void SortByX_When3D_ThenSortedStablyAscendingByXCoordinate()
        {
            // given
            List<Point3D> sequence = new List<Point3D> {
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(-2.0, -3.0, 5.0),
                Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(3.0, 2.0, 5.0),
                Point3D.Of(-3.0, 2.0, 5.0) };
            // when
            List<Point3D> result = PointsSorting.SortByX(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point3D> {
                Point3D.Of(-3.0, 2.0, 5.0), Point3D.Of(-2.0, -3.0, 5.0), Point3D.Of(-2.0, -3.0, -5.0),
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(2.0, -3.0, -5.0),
                Point3D.Of(3.0, 2.0, 5.0) });
        }

        [Test]
        public void SortByY_When3D_ThenSortedStablyAscendingByYCoordinate()
        {
            // given
            List<Point3D> sequence = new List<Point3D> {
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(-2.0, -3.0, 5.0),
                Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(3.0, 2.0, 5.0),
                Point3D.Of(-3.0, 2.0, 5.0) };
            // when
            List<Point3D> result = PointsSorting.SortByY(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point3D> {
                Point3D.Of(-2.0, -3.0, 5.0), Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0),
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(3.0, 2.0, 5.0), Point3D.Of(-3.0, 2.0, 5.0),
                Point3D.Of(2.0, 3.0, -5.0) });
        }

        [Test]
        public void SortByZ_When3D_ThenSortedStablyAscendingByZCoordinate()
        {
            // given
            List<Point3D> sequence = new List<Point3D> {
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(-2.0, -3.0, 5.0),
                Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(3.0, 2.0, 5.0),
                Point3D.Of(-3.0, 2.0, 5.0) };
            // when
            List<Point3D> result = PointsSorting.SortByZ(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point3D> {
                Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0),
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(-2.0, -3.0, 5.0), Point3D.Of(3.0, 2.0, 5.0),
                Point3D.Of(-3.0, 2.0, 5.0) });
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
            List<Point2D> result = PointsSorting.SortByAngle(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point2D> {
                Point2D.Of(0.0, 0.0), Point2D.Of(3.0, 2.0), Point2D.Of(2.0, 3.0),
                Point2D.Of(-2.0, 3.0), Point2D.Of(-3.0, 2.0), Point2D.Of(-3.0, -2.0),
                Point2D.Of(-2.0, -3.0), Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0) });
        }
    }
}
