using System;
using NUnit.Framework;

namespace AlgoLib.Geometry.Dim3;

// Tests: Structure of vector in 3D.
[TestFixture]
public class Vector3DTests
{
    private static readonly double Precision = IGeometryObject.Epsilon;

    [Test]
    public void Coordinates_ThenArray()
    {
        // when
        double[] result = Vector3D.Of(5.0, -19.0, 14.2).Coordinates;

        // then
        Assert.That(result, Is.EqualTo([5.0, -19.0, 14.2]));
    }

    [Test]
    public void Length_ThenLengthOfVector()
    {
        // when
        double result = Vector3D.Of(18.0, -6.0, 13.0).Length;

        // then
        Assert.That(result, Is.EqualTo(23.0).Within(Precision));
    }

    [Test]
    public void Between_ThenVectorFromBeginToEnd()
    {
        // when
        Vector3D result =
            Vector3D.Between(Point3D.Of(2.4, 7.8, -10.3), Point3D.Of(-1.5, 13.2, 15.8));

        // then
        Assert.That(result, Is.EqualTo(Vector3D.Of(-3.9, 5.4, 26.1)));
    }

    [Test]
    public void Dot_ThenScalarProduct()
    {
        // when
        double result = Vector3D.Dot(Vector3D.Of(1.5, -4.0, -3.5), Vector3D.Of(9.0, -2.5, 8.5));

        // then
        Assert.That(result, Is.EqualTo(-6.25).Within(Precision));
    }

    [Test]
    public void Dot_WhenOrthogonal_ThenZero()
    {
        // when
        double result = Vector3D.Dot(Vector3D.Of(1.0, 0.0, 1.0), Vector3D.Of(0.0, -2.0, 0.0));

        // then
        Assert.That(result, Is.Zero.Within(Precision));
    }

    [Test]
    public void Cross_ThenCrossProduct()
    {
        // when
        Vector3D result = Vector3D.Cross(Vector3D.Of(1.5, -4.0, -3.5), Vector3D.Of(9.0, -2.5, 8.5));

        // then
        Assert.That(result, Is.EqualTo(Vector3D.Of(-42.75, -44.25, 32.25)));
    }

    [Test]
    public void Cross_WhenParallel_ThenZero()
    {
        // when
        Vector3D result = Vector3D.Cross(Vector3D.Of(3.0, 3.0, 3.0), Vector3D.Of(-8.0, -8.0, -8.0));

        // then
        Assert.That(result, Is.EqualTo(Vector3D.Of(0.0, 0.0, 0.0)));
    }

    [Test]
    public void Area_ThenLengthOfCrossProduct()
    {
        // when
        double result = Vector3D.Area(Vector3D.Of(1.5, -4.0, -3.5), Vector3D.Of(9.0, -2.5, 8.5));

        // then
        Assert.That(result, Is.EqualTo(69.46716850426538).Within(Precision));
    }

    [Test]
    public void Area_WhenParallel_ThenZero()
    {
        // when
        double result = Vector3D.Area(Vector3D.Of(3.0, 3.0, 3.0), Vector3D.Of(-8.0, -8.0, -8.0));

        // then
        Assert.That(result, Is.Zero.Within(Precision));
    }

    [Test]
    public void Volume_ThenScalarTripleProduct()
    {
        // when
        double result = Vector3D.Volume(
            Vector3D.Of(1.5, -4.0, -3.5), Vector3D.Of(9.0, -2.5, 8.5),
            Vector3D.Of(1.0, -1.0, 1.0));

        // then
        Assert.That(result, Is.EqualTo(33.75).Within(Precision));
    }

    [Test]
    public void Volume_WhenParallel_ThenZero()
    {
        // when
        double result = Vector3D.Volume(
            Vector3D.Of(3.0, 3.0, 3.0), Vector3D.Of(-8.0, -8.0, -8.0),
            Vector3D.Of(2.0, -2.0, 2.0));

        // then
        Assert.That(result, Is.Zero.Within(Precision));
    }

    [Test]
    public void Volume_WhenOrthogonal_ThenZero()
    {
        // when
        double result = Vector3D.Volume(
            Vector3D.Of(3.0, 3.0, 3.0), Vector3D.Of(1.0, 0.0, 1.0),
            Vector3D.Of(0.0, -2.0, 0.0));

        // then
        Assert.That(result, Is.Zero.Within(Precision));
    }

    [Test]
    public void OperatorUnaryPlus_ThenCopied()
    {
        // given
        Vector3D vector = Vector3D.Of(5.4, 9.0, -12.3);

        // when
        Vector3D result = +vector;

        // then
        Assert.That(result, Is.Not.SameAs(vector));
        Assert.That(result, Is.EqualTo(Vector3D.Of(5.4, 9.0, -12.3)));
    }

    [Test]
    public void OperatorUnaryMinus_ThenNegateEachCoordinate()
    {
        // when
        Vector3D result = -Vector3D.Of(5.4, 9.0, -12.3);

        // then
        Assert.That(result, Is.EqualTo(Vector3D.Of(-5.4, -9.0, 12.3)));
    }

    [Test]
    public void OperatorPlus_ThenAddEachCoordinate()
    {
        // when
        Vector3D result = Vector3D.Of(5.4, 9.0, -12.3) + Vector3D.Of(7.9, -8.1, 1.4);

        // then
        Assert.That(result, Is.EqualTo(Vector3D.Of(13.3, 0.9, -10.9)));
    }

    [Test]
    public void OperatorMinus_ThenSubtractEachCoordinate()
    {
        // when
        Vector3D result = Vector3D.Of(5.4, 9.0, -12.3) - Vector3D.Of(7.9, -8.1, 1.4);

        // then
        Assert.That(result, Is.EqualTo(Vector3D.Of(-2.5, 17.1, -13.7)));
    }

    [Test]
    public void OperatorAsterisk_ThenMultiplyEachCoordinate()
    {
        // when
        Vector3D result = Vector3D.Of(5.4, 9.0, -12.3) * 3;

        // then
        Assert.That(result, Is.EqualTo(Vector3D.Of(16.2, 27.0, -36.9)));
    }

    [Test]
    public void OperatorAsterisk_WhenMultiplicationByZero_ThenZeroVector()
    {
        // when
        Vector3D result = 0 * Vector3D.Of(5.4, 9.0, -12.3);

        // then
        Assert.That(result, Is.EqualTo(Vector3D.Of(0.0, 0.0, 0.0)));
    }

    [Test]
    public void OperatorSlash_ThenDivideEachCoordinate()
    {
        // when
        Vector3D result = Vector3D.Of(5.4, 9.0, -12.3) / 3;

        // then
        Assert.That(result, Is.EqualTo(Vector3D.Of(1.8, 3.0, -4.1)));
    }

    [Test]
    public void OperatorSlash_WhenDivisionByZero_ThenDivideByZeroException()
    {
        // when
        Action action = () => _ = Vector3D.Of(1.0, 1.0, 1.0) / 0;

        // then
        Assert.That(action, Throws.TypeOf<DivideByZeroException>());
    }

    [Test]
    public void Deconstruct_ThenCoordinates()
    {
        // when
        (double x, double y, double z) = Vector3D.Of(5.0, -19.0, 14.2);

        // then
        Assert.That(x, Is.EqualTo(5.0));
        Assert.That(y, Is.EqualTo(-19.0));
        Assert.That(z, Is.EqualTo(14.2));
    }
}
