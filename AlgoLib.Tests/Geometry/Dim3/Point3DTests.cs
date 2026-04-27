using System.Collections.Generic;
using NUnit.Framework;

namespace AlgoLib.Geometry.Dim3;

// Tests: Structure of point in 3D.
public class Point3DTests
{
    private const double Precision = 1e-12;

    private static IEnumerable<object[]> ParamsFor_Radius =>
    [
        [Point3D.Zero, 0.0], [Point3D.Of(14.0, 0.0, 0.0), 14.0],
        [Point3D.Of(-14.0, 0.0, 0.0), 14.0], [Point3D.Of(0.0, 14.0, 0.0), 14.0],
        [Point3D.Of(0.0, -14.0, 0.0), 14.0], [Point3D.Of(0.0, 0.0, 14.0), 14.0],
        [Point3D.Of(0.0, 0.0, -14.0), 14.0], [Point3D.Of(8.0, 6.0, 0.0), 10.0],
        [Point3D.Of(8.0, -6.0, 0.0), 10.0], [Point3D.Of(-8.0, 6.0, 0.0), 10.0],
        [Point3D.Of(-8.0, -6.0, 0.0), 10.0], [Point3D.Of(8.0, 0.0, 6.0), 10.0],
        [Point3D.Of(8.0, 0.0, -6.0), 10.0], [Point3D.Of(-8.0, 0.0, 6.0), 10.0],
        [Point3D.Of(-8.0, 0.0, -6.0), 10.0], [Point3D.Of(0.0, 8.0, 6.0), 10.0],
        [Point3D.Of(0.0, 8.0, -6.0), 10.0], [Point3D.Of(0.0, -8.0, 6.0), 10.0],
        [Point3D.Of(0.0, -8.0, -6.0), 10.0], [Point3D.Of(18.0, 6.0, 13.0), 23.0],
        [Point3D.Of(18.0, 6.0, -13.0), 23.0], [Point3D.Of(18.0, -6.0, 13.0), 23.0],
        [Point3D.Of(18.0, -6.0, -13.0), 23.0], [Point3D.Of(-18.0, 6.0, 13.0), 23.0],
        [Point3D.Of(-18.0, 6.0, -13.0), 23.0], [Point3D.Of(-18.0, -6.0, 13.0), 23.0],
        [Point3D.Of(-18.0, -6.0, -13.0), 23.0]
    ];

    [Test]
    public void Coordinates_ThenArray()
    {
        // when
        double[] result = Point3D.Of(150.123456789, -3700.987654321, 0.55555555).Coordinates;

        // then
        Assert.That(result, Is.EqualTo([150.123456789, -3700.987654321, 0.55555555]));
    }

    [TestCaseSource(nameof(ParamsFor_Radius))]
    public void Radius_ThenDistanceFromZeroPoint(Point3D point, double expected)
    {
        // when
        double result = point.Radius;

        // then
        Assert.That(result, Is.EqualTo(expected).Within(Precision));
    }

    [Test]
    public void ToString_ThenStringRepresentation()
    {
        // when
        string result = Point3D.Of(150.123456789, -3700.987654321, 0.55555555).ToString();

        // then
        Assert.That(result, Is.EqualTo("(150.123456789, -3700.987654321, 0.55555555)"));
    }

    [Test]
    public void Deconstruct_ThenCoordinates()
    {
        // when
        (double x, double y, double z) = Point3D.Of(150.123456789, -3700.987654321, 0.55555555);

        // then
        Assert.That(x, Is.EqualTo(150.123456789));
        Assert.That(y, Is.EqualTo(-3700.987654321));
        Assert.That(z, Is.EqualTo(0.55555555));
    }
}
