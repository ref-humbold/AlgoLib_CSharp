using System;
using NUnit.Framework;

namespace AlgoLib.Geometry.Dim2;

// Tests: Structure of angle.
[TestFixture]
public class AngleTest
{
    [Test]
    [Sequential]
    public void Degrees_WhenRadians_ThenPositiveDegreesInRange(
        [Values(
            0.0, Math.PI / 6, Math.PI / 4, Math.PI / 3, Math.PI / 2, Math.PI, 2 * Math.PI,
            3 * Math.PI, -Math.PI / 6, -Math.PI / 4, -Math.PI / 3, -Math.PI / 2, -Math.PI,
            -2 * Math.PI, -3 * Math.PI)]
        double radians,
        [Values(
            0.0, 30.0, 45.0, 60.0, 90.0, 180.0, 0.0, 180.0, 330.0, 315.0, 300.0, 270.0, 180.0, 0.0,
            180.0)]
        double degrees)
    {
        // given
        Angle angle = Angle.FromRadians(radians);

        // when
        double result = angle.Degrees;

        // then
        Assert.That(result, Is.EqualTo(degrees).Within(1e-6));
    }

    [Test]
    [Sequential]
    public void Radians_WhenOfDegrees_ThenPositiveRadiansInRange(
        [Values(
            0.0, 30.0, 45.0, 60.0, 90.0, 180.0, 360.0, 540.0, -30.0, -45.0, -60.0, -90.0, -180.0,
            -360.0, -540.0)]
        double degrees,
        [Values(
            0.0, Math.PI / 6, Math.PI / 4, Math.PI / 3, Math.PI / 2, Math.PI, 0.0, Math.PI,
            11 * Math.PI / 6, 7 * Math.PI / 4, 5 * Math.PI / 3, 3 * Math.PI / 2, Math.PI, 0.0,
            Math.PI)]
        double radians)
    {
        // given
        Angle angle = Angle.FromDegrees(degrees);

        // when
        double result = angle.Radians;

        // then
        Assert.That(result, Is.EqualTo(radians).Within(1e-6));
    }
}
