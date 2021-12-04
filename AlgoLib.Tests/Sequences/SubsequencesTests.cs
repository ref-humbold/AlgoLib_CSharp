using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Sequences
{
    [TestFixture]
    public class SubsequencesTests
    {
        #region longestIncreasing

        [Test]
        public void LongestIncreasing_WhenIncreasing_ThenAllElements()
        {
            // given
            var sequence = new List<int> { 1, 3, 5, 7, 9, 11, 13, 15 };
            // when
            IEnumerable<int> result = sequence.LongestIncreasing(Comparer<int>.Default);
            // then
            result.Should().BeEquivalentTo(sequence);
            result.Should().BeInAscendingOrder();
        }

        [Test]
        public void LongestIncreasing_WhenDecreasing_ThenLastElementOnly()
        {
            // given
            var sequence = new List<int> { 12, 10, 8, 6, 4, 2 };
            // when
            IEnumerable<int> result = sequence.LongestIncreasing(Comparer<int>.Default);
            // then
            result.Should().Equal(2);
        }

        [Test]
        public void LongestIncreasing_WhenMultipleSubsequences_ThenLeastLexicographically()
        {
            // given
            var sequence = new List<int> { 2, 1, 4, 3, 6, 5, 8, 7, 10 };
            // when
            IEnumerable<int> result = sequence.LongestIncreasing(Comparer<int>.Default);
            // then
            result.Should().Equal(1, 3, 5, 7, 10);
        }

        [Test]
        public void LongestIncreasing_WhenSearchInMiddle_ThenLeastLexicographically()
        {
            // given
            var sequence = new List<int> { 0, 2, 4, 6, 8, 3, 5, 7, 8 };
            // when
            IEnumerable<int> result = sequence.LongestIncreasing(Comparer<int>.Default);
            // then
            result.Should().Equal(0, 2, 3, 5, 7, 8);
        }

        [Test]
        public void LongestIncreasing_WhenIncreasingAndReversedComparator_ThenLastElementOnly()
        {
            // given
            var sequence = new List<int> { 1, 3, 5, 7, 9, 11, 13, 15 };
            // when
            IEnumerable<int> result = sequence.LongestIncreasing((i1, i2) => i2.CompareTo(i1));
            // then
            result.Should().Equal(15);
        }

        [Test]
        public void LongestIncreasing_WhenDecreasingAndReversedComparator_ThenAllElements()
        {
            // given
            var sequence = new List<int> { 12, 10, 8, 6, 4, 2 };
            // when
            IEnumerable<int> result = sequence.LongestIncreasing((i1, i2) => i2.CompareTo(i1));
            // then
            result.Should().BeEquivalentTo(sequence);
            result.Should().BeInDescendingOrder();
        }

        #endregion
        #region MaximumSubarray

        [Test]
        public void MaximumSubarray_WhenNegativeIsLessThanSubsum_ThenIncludeNegative()
        {
            // given
            var sequence = new List<double> { 3.5, 4.8, -1.6, 7.7, 2.1, -9.3, 0.8 };
            // when
            List<double> result = sequence.MaximumSubarray();
            // then
            result.Should().Equal(3.5, 4.8, -1.6, 7.7, 2.1);
        }

        [Test]
        public void MaximumSubarray_WhenNegativeIsGreaterThanSubsum_ThenExcludeNegative()
        {
            // given
            var sequence = new List<double> { -9.3, -1.2, 3.5, 4.8, -10.6, 7.7, 2.1, 0.8, 4.0 };
            // when
            List<double> result = sequence.MaximumSubarray();
            // then
            result.Should().Equal(7.7, 2.1, 0.8, 4.0);
        }

        [Test]
        public void MaximumSubarray_WhenAllElementsAreNegative_ThenEmpty()
        {
            // given
            var sequence = new List<double> { -9.0, -2.4, -3.07, -1.93, -12.67 };
            // when
            List<double> result = sequence.MaximumSubarray();
            // then
            result.Should().BeEmpty();
        }

        #endregion
        #region MaximalSubsum

        [Test]
        public void MaximalSubsum_WhenNegativeIsLessThanSubsum_ThenIncludeNegative()
        {
            // given
            var sequence = new List<double> { 3.5, 4.8, -1.6, 7.7, 2.1, -9.3, 0.8 };
            // when
            double result = sequence.MaximalSubsum();
            // then
            result.Should().BeApproximately(16.5, 1e-6);
        }

        [Test]
        public void MaximalSubsum_WhenNegativeIsGreaterThanSubsum_ThenExcludeNegative()
        {
            // given
            var sequence = new List<double> { -9.3, -1.2, 3.5, 4.8, -10.6, 7.7, 2.1, 0.8, 4.0 };
            // when
            double result = sequence.MaximalSubsum();
            // then
            result.Should().BeApproximately(14.6, 1e-6);
        }

        [Test]
        public void MaximumalSubsum_WhenAllElementsAreNegative_ThenZero()
        {
            // given
            var sequence = new List<double> { -9.0, -2.4, -3.07, -1.93, -12.67 };
            // when
            double result = sequence.MaximalSubsum();
            // then
            result.Should().Be(0.0);
        }

        #endregion
    }
}
