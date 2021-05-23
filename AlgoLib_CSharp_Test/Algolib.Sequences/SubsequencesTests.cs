using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;

namespace Algolib.Sequences
{
    [TestFixture]
    public class SubsequencesTests
    {
        [Test]
        public void MaximumSubarray_WhenNegativeElementIsLessThenCurrentSum_ThenCounted()
        {
            // given
            List<double> sequence = new List<double> { 3.5, 4.8, -1.6, 7.7, 2.1, -9.3, 0.8 };
            // when
            List<double> result = Subsequences.MaximumSubarray(sequence);
            // then
            result.Should().Equal(3.5, 4.8, -1.6, 7.7, 2.1);
        }

        [Test]
        public void MaximumSubarray_WhenNegativeElementIsGreaterThenCurrentSum_ThenDiscarded()
        {
            // given
            List<double> sequence = new List<double> { -9.3, -1.2, 3.5, 4.8, -10.6, 7.7, 2.1, 0.8, 4.0 };
            // when
            List<double> result = Subsequences.MaximumSubarray(sequence);
            // then
            result.Should().Equal(7.7, 2.1, 0.8, 4.0);
        }

        [Test]
        public void MaximumSubarray_WhenAllElementsAreNegative_ThenEmpty()
        {
            // given
            List<double> sequence = new List<double> { -9.0, -2.4, -3.07, -1.93, -12.67 };
            // when
            List<double> result = Subsequences.MaximumSubarray(sequence);
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void MaximalSubsum_WhenNegativeElementIsLessThenCurrentSum_ThenCounted()
        {
            // given
            List<double> sequence = new List<double> { 3.5, 4.8, -1.6, 7.7, 2.1, -9.3, 0.8 };
            // when
            double result = Subsequences.MaximalSubsum(sequence);
            // then
            result.Should().BeApproximately(16.5, 0.000001);
        }

        [Test]
        public void MaximalSubsum_WhenNegativeElementIsGreaterThenCurrentSum_ThenDiscarded()
        {
            // given
            List<double> sequence = new List<double> { -9.3, -1.2, 3.5, 4.8, -10.6, 7.7, 2.1, 0.8, 4.0 };
            // when
            double result = Subsequences.MaximalSubsum(sequence);
            // then
            result.Should().BeApproximately(14.6, 0.000001);
        }

        [Test]
        public void MaximumalSubsum_WhenAllElementsAreNegative_ThenZero()
        {
            // given
            List<double> sequence = new List<double> { -9.0, -2.4, -3.07, -1.93, -12.67 };
            // when
            double result = Subsequences.MaximalSubsum(sequence);
            // then
            result.Should().Be(0.0);
        }
    }
}
