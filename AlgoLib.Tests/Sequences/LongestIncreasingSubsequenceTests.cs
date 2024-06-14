using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Sequences;

// Tests: Algorithm for longest increasing subsequence.
[TestFixture]
public class FindLisSubsequenceTests
{
    [Test]
    public void FindLis_WhenIncreasing_ThenAllElements()
    {
        // given
        var sequence = new List<int> { 1, 3, 5, 7, 9, 11, 13, 15 };

        // when
        IEnumerable<int> result = sequence.FindLis(Comparer<int>.Default);

        // then
        result.Should().BeEquivalentTo(sequence);
        result.Should().BeInAscendingOrder();
    }

    [Test]
    public void FindLis_WhenDecreasing_ThenLastElementOnly()
    {
        // given
        var sequence = new List<int> { 12, 10, 8, 6, 4, 2 };

        // when
        IEnumerable<int> result = sequence.FindLis(Comparer<int>.Default);

        // then
        result.Should().Equal(2);
    }

    [Test]
    public void FindLis_WhenMultipleSubsequences_ThenLeastLexicographically()
    {
        // given
        var sequence = new List<int> { 2, 1, 4, 3, 6, 5, 8, 7, 10 };

        // when
        IEnumerable<int> result = sequence.FindLis(Comparer<int>.Default);

        // then
        result.Should().Equal(1, 3, 5, 7, 10);
    }

    [Test]
    public void FindLis_WhenSearchInMiddle_ThenLeastLexicographically()
    {
        // given
        var sequence = new List<int> { 0, 2, 4, 6, 8, 3, 5, 7, 8 };

        // when
        IEnumerable<int> result = sequence.FindLis(Comparer<int>.Default);

        // then
        result.Should().Equal(0, 2, 3, 5, 7, 8);
    }

    [Test]
    public void FindLis_WhenIncreasingAndReversedComparator_ThenLastElementOnly()
    {
        // given
        var sequence = new List<int> { 1, 3, 5, 7, 9, 11, 13, 15 };

        // when
        IEnumerable<int> result = sequence.FindLis((i1, i2) => i2.CompareTo(i1));

        // then
        result.Should().Equal(15);
    }

    [Test]
    public void FindLis_WhenDecreasingAndReversedComparator_ThenAllElements()
    {
        // given
        var sequence = new List<int> { 12, 10, 8, 6, 4, 2 };

        // when
        IEnumerable<int> result = sequence.FindLis((i1, i2) => i2.CompareTo(i1));

        // then
        result.Should().BeEquivalentTo(sequence);
        result.Should().BeInDescendingOrder();
    }
}
