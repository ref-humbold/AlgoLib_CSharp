using System.Collections.Generic;
using NUnit.Framework;

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
            Assert.AreNotSame(sequence, result);
            CollectionAssert.AreEqual(new List<Point2D> {
                    Point2D.Of(-3.0, 2.0), Point2D.Of(-3.0, -2.0), Point2D.Of(-2.0, -3.0), Point2D.Of(-2.0, 3.0),
                    Point2D.Of(0.0, 0.0), Point2D.Of(2.0, 3.0), Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0),
                    Point2D.Of(3.0, 2.0) }, result);
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
            Assert.AreNotSame(sequence, result);
            CollectionAssert.AreEqual(new List<Point2D> {
                    Point2D.Of(-2.0, -3.0), Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0),
                    Point2D.Of(-3.0, -2.0), Point2D.Of(0.0, 0.0), Point2D.Of(-3.0, 2.0),
                    Point2D.Of(3.0, 2.0), Point2D.Of(2.0, 3.0), Point2D.Of(-2.0, 3.0) }, result);
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
            Assert.AreNotSame(sequence, result);
            CollectionAssert.AreEqual(new List<Point3D> {
                    Point3D.Of(-3.0, 2.0, 5.0), Point3D.Of(-2.0, -3.0, 5.0), Point3D.Of(-2.0, -3.0, -5.0),
                    Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(2.0, -3.0, -5.0),
                    Point3D.Of(3.0, 2.0, 5.0) }, result);
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
            Assert.AreNotSame(sequence, result);
            CollectionAssert.AreEqual(new List<Point3D> {
                    Point3D.Of(-2.0, -3.0, 5.0), Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0),
                    Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(3.0, 2.0, 5.0), Point3D.Of(-3.0, 2.0, 5.0),
                    Point3D.Of(2.0, 3.0, -5.0) }, result);
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
            Assert.AreNotSame(sequence, result);
            CollectionAssert.AreEqual(new List<Point3D> {
                    Point3D.Of(2.0, 3.0, -5.0), Point3D.Of(2.0, -3.0, -5.0), Point3D.Of(-2.0, -3.0, -5.0),
                    Point3D.Of(0.0, 0.0, 0.0), Point3D.Of(-2.0, -3.0, 5.0), Point3D.Of(3.0, 2.0, 5.0),
                    Point3D.Of(-3.0, 2.0, 5.0) }, result);
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
            Assert.AreNotSame(sequence, result);
            CollectionAssert.AreEqual(new List<Point2D> {
                    Point2D.Of(0.0, 0.0), Point2D.Of(3.0, 2.0), Point2D.Of(2.0, 3.0),
                    Point2D.Of(-2.0, 3.0), Point2D.Of(-3.0, 2.0), Point2D.Of(-3.0, -2.0),
                    Point2D.Of(-2.0, -3.0), Point2D.Of(2.0, -3.0), Point2D.Of(3.0, -2.0) }, result);
        }
    }
}
