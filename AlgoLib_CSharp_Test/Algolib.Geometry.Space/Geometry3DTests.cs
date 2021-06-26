// Tests: Algorithms for basic geometrical operations in 3D
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Geometry.Space
{
    [TestFixture]
    public class Geometry3DTests
    {
        [Test]
        public void SortByX_ThenSortedStablyAscendingByXCoordinate()
        {
            // given
            List<Point3D> sequence = new List<Point3D> {
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(-2.0, -3.0, 5.0),
                Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(3.0, 2.0, 5.0),
                Point3D.Of(-3.0, 2.0, 5.0) };
            // when
            List<Point3D> result = Geometry3D.SortByX(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point3D> {
                Point3D.Of(-3.0, 2.0, 5.0), Point3D.Of(-2.0, -3.0, 5.0), Point3D.Of(-2.0, -3.0, -5.0),
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(2.0, -3.0, -5.0),
                Point3D.Of(3.0, 2.0, 5.0) });
        }

        [Test]
        public void SortByY_ThenSortedStablyAscendingByYCoordinate()
        {
            // given
            List<Point3D> sequence = new List<Point3D> {
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(-2.0, -3.0, 5.0),
                Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(3.0, 2.0, 5.0),
                Point3D.Of(-3.0, 2.0, 5.0) };
            // when
            List<Point3D> result = Geometry3D.SortByY(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point3D> {
                Point3D.Of(-2.0, -3.0, 5.0), Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0),
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(3.0, 2.0, 5.0), Point3D.Of(-3.0, 2.0, 5.0),
                Point3D.Of(2.0, 3.0, -5.0) });
        }

        [Test]
        public void SortByZ_ThenSortedStablyAscendingByZCoordinate()
        {
            // given
            List<Point3D> sequence = new List<Point3D> {
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(-2.0, -3.0, 5.0),
                Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0), Point3D.Of(3.0, 2.0, 5.0),
                Point3D.Of(-3.0, 2.0, 5.0) };
            // when
            List<Point3D> result = Geometry3D.SortByZ(sequence);
            // then
            result.Should().NotBeSameAs(sequence);
            result.Should().Equal(new List<Point3D> {
                Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0),
                Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(-2.0, -3.0, 5.0), Point3D.Of(3.0, 2.0, 5.0),
                Point3D.Of(-3.0, 2.0, 5.0) });
        }
    }
}
