using NUnit.Framework;

namespace AlgoLib.Geometry.Dim2;

public class Point2DTests
{
    private const double Precision = 1e-12;

    [Test]
    public void Coordinates_ThenArray()
    {
        // when
        double[] result = Point2D.Of(150.123456789, -3700.987654321).Coordinates;

        // then
        Assert.That(result, Is.EqualTo([150.123456789, -3700.987654321]));
    }

    [Test]
    [Sequential]
    public void Angle_ThenCounterClockwiseAngleFromXAxis(
        [Values(0.0, 7.0, 7.0, 0.0, -7.0, -7.0, -7.0, 0.0, 7.0)] double x,
        [Values(0.0, 0.0, 7.0, 7.0, 7.0, 0.0, -7.0, -7.0, -7.0)] double y,
        [Values(0.0, 0.0, 45.0, 90.0, 135.0, 180.0, 225.0, 270.0, 315.0)] double expected)
    {
        // when
        Angle result = Point2D.Of(x, y).Angle;

        // then
        Assert.That(result, Is.EqualTo(Angle.FromDegrees(expected)));
    }

    [Test]
    [Sequential]
    public void Radius_ThenDistanceFromZeroPoint(
        [Values(0.0, 14.0, 8.0, 0.0, -8.0, -14.0, -8.0, 0.0, 8.0)] double x,
        [Values(0.0, 0.0, 6.0, 14.0, 6.0, 0.0, -6.0, -14.0, -6.0)] double y,
        [Values(0.0, 14.0, 10.0, 14.0, 10.0, 14.0, 10.0, 14.0, 10.0)] double expected)
    {
        // when
        double result = Point2D.Of(x, y).Radius;

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
