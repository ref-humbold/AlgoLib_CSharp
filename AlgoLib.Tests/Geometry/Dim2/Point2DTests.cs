using System.Collections.Generic;
using NUnit.Framework;

namespace AlgoLib.Geometry.Dim2;

public class Point2DTests
{
    private const double Precision = 1e-12;

    private static IEnumerable<object[]> ParamsFor_Angle =>
    [
        [Point2D.Zero, 0.0], [Point2D.Of(7.0, 0.0), 0.0], [Point2D.Of(7.0, 7.0), 45.0],
        [Point2D.Of(0.0, 7.0), 90.0], [Point2D.Of(-7.0, 7.0), 135.0],
        [Point2D.Of(-7.0, 0.0), 180.0], [Point2D.Of(-7.0, -7.0), 225.0],
        [Point2D.Of(0.0, -7.0), 270.0], [Point2D.Of(7.0, -7.0), 315.0]
    ];

    private static IEnumerable<object[]> ParamsFor_Radius =>
    [
        [Point2D.Zero, 0.0], [Point2D.Of(14.0, 0.0), 14.0], [Point2D.Of(-14.0, 0.0), 14.0],
        [Point2D.Of(0.0, 14.0), 14.0], [Point2D.Of(0.0, -14.0), 14.0],
        [Point2D.Of(8.0, 6.0), 10.0], [Point2D.Of(8.0, -6.0), 10.0],
        [Point2D.Of(-8.0, 6.0), 10.0], [Point2D.Of(-8.0, -6.0), 10.0]
    ];

    [Test]
    public void Coordinates_ThenArray()
    {
        // when
        double[] result = Point2D.Of(150.123456789, -3700.987654321).Coordinates;

        // then
        Assert.That(result, Is.EqualTo([150.123456789, -3700.987654321]));
    }

    [TestCaseSource(nameof(ParamsFor_Angle))]
    public void Angle_ThenCounterClockwiseAngleFromXAxis(Point2D point, double expected)
    {
        // when
        Angle result = point.Angle;

        // then
        Assert.That(result, Is.EqualTo(Angle.FromDegrees(expected)));
    }

    [TestCaseSource(nameof(ParamsFor_Radius))]
    public void Radius_ThenDistanceFromZeroPoint(Point2D point, double expected)
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
        string result = Point2D.Of(150.123456789, -3700.987654321).ToString();

        // then
        Assert.That(result, Is.EqualTo("(150.123456789, -3700.987654321)"));
    }

    [Test]
    public void Deconstruct_ThenCoordinates()
    {
        // when
        (double x, double y) = Point2D.Of(150.123456789, -3700.987654321);

        // then
        Assert.That(x, Is.EqualTo(150.123456789));
        Assert.That(y, Is.EqualTo(-3700.987654321));
    }
}
