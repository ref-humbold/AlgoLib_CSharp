using System.Collections.Generic;
using NUnit.Framework;

namespace AlgoLib.Sequences;

// Tests: Algorithms for maximum subarray.
[TestFixture]
public class MaximumSubarrayTests
{
    private const double Precision = 1e-6;

    #region FindMaximumSubarray

    [Test]
    public void FindMaximumSubarray_WhenNegativeIsLessThanSubsum_ThenIncludeNegative()
    {
        // given
        List<double> sequence = [3.5, 4.8, -1.6, 7.7, 2.1, -9.3, 0.8];

        // when
        List<double> result = sequence.FindMaximumSubarray();

        // then
        Assert.That(result, Is.EqualTo([3.5, 4.8, -1.6, 7.7, 2.1]));
    }

    [Test]
    public void FindMaximumSubarray_WhenNegativeIsGreaterThanSubsum_ThenExcludeNegative()
    {
        // given
        List<double> sequence = [-9.3, -1.2, 3.5, 4.8, -10.6, 7.7, 2.1, 0.8, 4.0];

        // when
        List<double> result = sequence.FindMaximumSubarray();

        // then
        Assert.That(result, Is.EqualTo([7.7, 2.1, 0.8, 4.0]));
    }

    [Test]
    public void FindMaximumSubarray_WhenAllElementsAreNegative_ThenEmpty()
    {
        // given
        List<double> sequence = [-9.0, -2.4, -3.07, -1.93, -12.67];

        // when
        List<double> result = sequence.FindMaximumSubarray();

        // then
        Assert.That(result, Is.Empty);
    }

    #endregion
    #region CountMaximalSubsum

    [Test]
    public void CountMaximalSubsum_WhenNegativeIsLessThanSubsum_ThenIncludeNegative()
    {
        // given
        List<double> sequence = [3.5, 4.8, -1.6, 7.7, 2.1, -9.3, 0.8];

        // when
        double result = sequence.CountMaximalSubsum();

        // then
        Assert.That(result, Is.EqualTo(16.5).Within(Precision));
    }

    [Test]
    public void CountMaximalSubsum_WhenNegativeIsGreaterThanSubsum_ThenExcludeNegative()
    {
        // given
        List<double> sequence = [-9.3, -1.2, 3.5, 4.8, -10.6, 7.7, 2.1, 0.8, 4.0];

        // when
        double result = sequence.CountMaximalSubsum();

        // then
        Assert.That(result, Is.EqualTo(14.6).Within(Precision));
    }

    [Test]
    public void CountMaximalSubsum_WhenAllElementsAreNegative_ThenZero()
    {
        // given
        List<double> sequence = [-9.0, -2.4, -3.07, -1.93, -12.67];

        // when
        double result = sequence.CountMaximalSubsum();

        // then
        Assert.That(result, Is.Zero);
    }

    #endregion
}
