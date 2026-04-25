using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AlgoLib.Geometry.Dim2;

// Tests: Structure of angle.
[TestFixture]
public class AngleTest
{
    private static IEnumerable<double[]> ParamsFor_Degrees_WhenFromRadians =>
    [
        [0.0, 0.0], [Math.PI / 6, 30.0], [Math.PI / 4, 45.0], [Math.PI / 3, 60.0],
        [Math.PI / 2, 90.0], [Math.PI, 180.0], [2 * Math.PI, 0.0], [3 * Math.PI, 180.0],
        [-Math.PI / 6, 330.0], [-Math.PI / 4, 315.0], [-Math.PI / 3, 300.0],
        [-Math.PI / 2, 270.0], [-Math.PI, 180.0], [-2 * Math.PI, 0.0], [-3 * Math.PI, 180.0]
    ];

    private static IEnumerable<double[]> ParamsFor_Radians_WhenFromDegrees =>
    [
        [0.0, 0.0], [30.0, Math.PI / 6], [45.0, Math.PI / 4], [60.0, Math.PI / 3],
        [90.0, Math.PI / 2], [180.0, Math.PI], [360.0, 0.0], [540.0, Math.PI],
        [-30.0, 11 * Math.PI / 6], [-45.0, 7 * Math.PI / 4], [-60.0, 5 * Math.PI / 3],
        [-90.0, 3 * Math.PI / 2], [-180.0, Math.PI], [-360.0, 0.0], [-540.0, Math.PI]
    ];

    [TestCaseSource(nameof(ParamsFor_Degrees_WhenFromRadians))]
    public void Degrees_WhenFromRadians_ThenPositiveDegreesInRange(double radians, double degrees)
    {
        // given
        Angle angle = Angle.FromRadians(radians);

        // when
        double result = angle.Degrees;

        // then
        Assert.That(result, Is.EqualTo(degrees).Within(1e-6));
    }

    [TestCaseSource(nameof(ParamsFor_Radians_WhenFromDegrees))]
    public void Radians_WhenFromDegrees_ThenPositiveRadiansInRange(double degrees, double radians)
    {
        // given
        Angle angle = Angle.FromDegrees(degrees);

        // when
        double result = angle.Radians;

        // then
        Assert.That(result, Is.EqualTo(radians).Within(1e-6));
    }

    [Test]
    public void ToString_ThenStringRepresentation()
    {
        // when
        var result = Angle.FromDegrees(150.123456789).ToString();

        // then
        Assert.That(result, Is.EqualTo("Angle(150.123456789 deg)"));
    }
}
