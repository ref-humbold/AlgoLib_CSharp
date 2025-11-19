using System;
using NUnit.Framework;

namespace AlgoLib.Geometry.Dim2;

// Tests: Structure of vector in 2D.
[TestFixture]
public class Vector2DTests
{
    private static readonly double Precision = IGeometryObject.Epsilon;

    [Test]
    public void Coordinates_ThenArray()
    {
        // when
        double[] result = Vector2D.Of(5.0, -19.0).Coordinates;

        // then
        Assert.That(result, Is.EqualTo([5.0, -19.0]));
    }

    [Test]
    public void Length_ThenLengthOfVector()
    {
        // when
        double result = Vector2D.Of(8.0, -6.0).Length;

        // then
        Assert.That(result, Is.EqualTo(10.0).Within(Precision));
    }

    [Test]
    public void Between_ThenVectorFromBeginToEnd()
    {
        // when
        Vector2D result = Vector2D.Between(Point2D.Of(2.4, 7.8), Point2D.Of(-1.5, 13.2));

        // then
        Assert.That(result, Is.EqualTo(Vector2D.Of(-3.9, 5.4)));
    }

    [Test]
    public void Dot_ThenScalarProduct()
    {
        // when
        double result = Vector2D.Dot(Vector2D.Of(1.5, -4.0), Vector2D.Of(9.0, -2.5));

        // then
        Assert.That(result, Is.EqualTo(23.5).Within(Precision));
    }

    [Test]
    public void Dot_WhenOrthogonal_ThenZero()
    {
        // when
        double result = Vector2D.Dot(Vector2D.Of(1.0, 0.0), Vector2D.Of(0.0, -2.0));

        // then
        Assert.That(result, Is.Zero.Within(Precision));
    }

    [Test]
    public void Area_ThenLengthOfCrossProduct()
    {
        // when
        double result = Vector2D.Area(Vector2D.Of(1.5, -4.0), Vector2D.Of(9.0, -2.5));

        // then
        Assert.That(result, Is.EqualTo(32.25).Within(Precision));
    }

    [Test]
    public void Area_WhenParallel_ThenZero()
    {
        // when
        double result = Vector2D.Area(Vector2D.Of(3.0, 3.0), Vector2D.Of(-8.0, -8.0));

        // then
        Assert.That(result, Is.Zero.Within(Precision));
    }

    [Test]
    public void OperatorUnaryPlus_ThenCopied()
    {
        // given
        Vector2D vector = Vector2D.Of(5.4, 9.0);

        // when
        Vector2D result = +vector;

        // then
        Assert.That(result, Is.Not.SameAs(vector));
        Assert.That(result, Is.EqualTo(Vector2D.Of(5.4, 9.0)));
    }

    [Test]
    public void OperatorUnaryMinus_ThenNegateEachCoordinate()
    {
        // when
        Vector2D result = -Vector2D.Of(5.4, 9.0);

        // then
        Assert.That(result, Is.EqualTo(Vector2D.Of(-5.4, -9.0)));
    }

    [Test]
    public void OperatorPlus_ThenAddEachCoordinate()
    {
        // when
        Vector2D result = Vector2D.Of(5.4, 9.0) + Vector2D.Of(7.9, -8.1);

        // then
        Assert.That(result, Is.EqualTo(Vector2D.Of(13.3, 0.9)));
    }

    [Test]
    public void OperatorMinus_ThenSubtractEachCoordinate()
    {
        // when
        Vector2D result = Vector2D.Of(5.4, 9.0) - Vector2D.Of(7.9, -8.1);

        // then
        Assert.That(result, Is.EqualTo(Vector2D.Of(-2.5, 17.1)));
    }

    [Test]
    public void OperatorAsterisk_ThenMultiplyEachCoordinate()
    {
        // when
        Vector2D result = Vector2D.Of(5.4, 9.0) * 3;

        // then
        Assert.That(result, Is.EqualTo(Vector2D.Of(16.2, 27.0)));
    }

    [Test]
    public void OperatorAsterisk_WhenMultiplicationByZero_ThenZeroVector()
    {
        // when
        Vector2D result = 0 * Vector2D.Of(5.4, 9.0);

        // then
        Assert.That(result, Is.EqualTo(Vector2D.Of(0, 0)));
    }

    [Test]
    public void OperatorSlash_ThenDivideEachCoordinate()
    {
        // when
        Vector2D result = Vector2D.Of(5.4, 9.0) / 3;

        // then
        Assert.That(result, Is.EqualTo(Vector2D.Of(1.8, 3.0)));
    }

    [Test]
    public void OperatorSlash_WhenDivisionByZero_ThenDivideByZeroException()
    {
        // when
        Action action = () => _ = Vector2D.Of(1.0, 1.0) / 0;

        // then
        Assert.That(action, Throws.TypeOf<DivideByZeroException>());
    }

    [Test]
    public void Deconstruct_ThenCoordinates()
    {
        // when
        (double x, double y) = Vector2D.Of(5.0, -19.0);

        // then
        Assert.That(x, Is.EqualTo(5.0));
        Assert.That(y, Is.EqualTo(-19.0));
    }
}
