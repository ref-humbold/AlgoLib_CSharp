using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Sequences
{
    [TestFixture]
    public class MaximumSubarrayTests
    {
        #region FindMaximumSubarray

        [Test]
        public void FindMaximumSubarray_WhenNegativeIsLessThanSubsum_ThenIncludeNegative()
        {
            // given
            var sequence = new List<double> { 3.5, 4.8, -1.6, 7.7, 2.1, -9.3, 0.8 };
            // when
            List<double> result = sequence.FindMaximumSubarray();
            // then
            result.Should().Equal(3.5, 4.8, -1.6, 7.7, 2.1);
        }

        [Test]
        public void FindMaximumSubarray_WhenNegativeIsGreaterThanSubsum_ThenExcludeNegative()
        {
            // given
            var sequence = new List<double> { -9.3, -1.2, 3.5, 4.8, -10.6, 7.7, 2.1, 0.8, 4.0 };
            // when
            List<double> result = sequence.FindMaximumSubarray();
            // then
            result.Should().Equal(7.7, 2.1, 0.8, 4.0);
        }

        [Test]
        public void FindMaximumSubarray_WhenAllElementsAreNegative_ThenEmpty()
        {
            // given
            var sequence = new List<double> { -9.0, -2.4, -3.07, -1.93, -12.67 };
            // when
            List<double> result = sequence.FindMaximumSubarray();
            // then
            result.Should().BeEmpty();
        }

        #endregion
        #region CountMaximalSubsum

        [Test]
        public void CountMaximalSubsum_WhenNegativeIsLessThanSubsum_ThenIncludeNegative()
        {
            // given
            var sequence = new List<double> { 3.5, 4.8, -1.6, 7.7, 2.1, -9.3, 0.8 };
            // when
            double result = sequence.CountMaximalSubsum();
            // then
            result.Should().BeApproximately(16.5, 1e-6);
        }

        [Test]
        public void CountMaximalSubsum_WhenNegativeIsGreaterThanSubsum_ThenExcludeNegative()
        {
            // given
            var sequence = new List<double> { -9.3, -1.2, 3.5, 4.8, -10.6, 7.7, 2.1, 0.8, 4.0 };
            // when
            double result = sequence.CountMaximalSubsum();
            // then
            result.Should().BeApproximately(14.6, 1e-6);
        }

        [Test]
        public void CountMaximumalSubsum_WhenAllElementsAreNegative_ThenZero()
        {
            // given
            var sequence = new List<double> { -9.0, -2.4, -3.07, -1.93, -12.67 };
            // when
            double result = sequence.CountMaximalSubsum();
            // then
            result.Should().Be(0.0);
        }

        #endregion
    }
}
