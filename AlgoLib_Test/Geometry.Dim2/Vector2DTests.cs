// Tests: Structure of vector in 2D
using System;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Geometry.Dim2
{
    [TestFixture]
    public class Vector3DTests
    {
        private const double offset = IGeometryObject.Epsilon;

        [Test]
        public void Between_ThenVectorFromBeginToEnd()
        {
            // when
            Vector2D result = Vector2D.Between(Point2D.Of(2.4, 7.8), Point2D.Of(-1.5, 13.2));
            // then
            result.Should().Be(Vector2D.Of(-3.9, 5.4));
        }

        [Test]
        public void Coordinates_ThenArray()
        {
            // when
            double[] result = Vector2D.Of(5.0, -19.0).Coordinates;
            // then
            result.Should().Equal(5.0, -19.0);
        }

        [Test]
        public void Dot_ThenScalarProduct()
        {
            // when
            double result = Vector2D.Dot(Vector2D.Of(1.5, -4.0), Vector2D.Of(9.0, -2.5));
            // then
            result.Should().BeApproximately(23.5, offset);
        }

        [Test]
        public void Dot_WhenOrthogonal_ThenZero()
        {
            // when
            double result = Vector2D.Dot(Vector2D.Of(1.0, 0.0), Vector2D.Of(0.0, -2.0));
            // then
            result.Should().BeApproximately(0.0, offset);
        }

        [Test]
        public void Area_ThenLengthOfCrossProduct()
        {
            // when
            double result = Vector2D.Area(Vector2D.Of(1.5, -4.0), Vector2D.Of(9.0, -2.5));
            // then
            result.Should().BeApproximately(32.25, offset);
        }

        [Test]
        public void Area_WhenParallel_ThenZero()
        {
            // when
            double result = Vector2D.Area(Vector2D.Of(3.0, 3.0), Vector2D.Of(-8.0, -8.0));
            // then
            result.Should().BeApproximately(0.0, offset);
        }

        [Test]
        public void Length_ThenLengthOfVector()
        {
            // when
            double result = Vector2D.Of(8.0, -6.0).Length;
            // then
            result.Should().BeApproximately(10.0, offset);
        }

        [Test]
        public void OperatorPlus_ThenAddEachCoordinate()
        {
            // when
            Vector2D result = Vector2D.Of(5.4, 9.0) + Vector2D.Of(7.9, -8.1);
            // then
            result.Should().Be(Vector2D.Of(13.3, 0.9));
        }

        [Test]
        public void OperatorMinus_ThenSubtractEachCoordinate()
        {
            // when
            Vector2D result = Vector2D.Of(5.4, 9.0) - Vector2D.Of(7.9, -8.1);
            // then
            result.Should().Be(Vector2D.Of(-2.5, 17.1));
        }

        [Test]
        public void OperatorAsterisk_ThenMultiplyEachCoordinate()
        {
            // when
            Vector2D result = Vector2D.Of(5.4, 9.0) * 3;
            // then
            result.Should().Be(Vector2D.Of(16.2, 27.0));
        }

        [Test]
        public void OperatorAsterisk_WhenMultiplicationByZero_ThenZeroVector()
        {
            // when
            Vector2D result = Vector2D.Of(5.4, 9.0) * 0;
            // then
            result.Should().Be(Vector2D.Of(0, 0));
        }

        [Test]
        public void OperatorSlash_ThenDivideEachCoordinate()
        {
            // when
            Vector2D result = Vector2D.Of(5.4, 9.0) / 3;
            // then
            result.Should().Be(Vector2D.Of(1.8, 3.0));
        }

        [Test]
        public void OperatorSlash_WhenDivisionByZero_ThenDivideByZeroException()
        {
            // when
            Action action = () => _ = Vector2D.Of(1.0, 1.0) / 0;
            // then
            action.Should().Throw<DivideByZeroException>();
        }

        [Test]
        public void Deconstruct_ThenCoordinates()
        {
            // when
            (double x, double y) = Vector2D.Of(5.0, -19.0);
            // then
            x.Should().Be(5.0);
            y.Should().Be(-19.0);
        }
    }
}
