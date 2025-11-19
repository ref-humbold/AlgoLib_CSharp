using System.Collections.Generic;
using System.Linq;
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
        List<int> sequence = [1, 3, 5, 7, 9, 11, 13, 15];

        // when
        List<int> result = sequence.FindLis(Comparer<int>.Default).ToList();

        // then
        Assert.That(result, Is.EquivalentTo(sequence));
        Assert.That(result, Is.Ordered.Ascending);
    }

    [Test]
    public void FindLis_WhenDecreasing_ThenLastElementOnly()
    {
        // given
        List<int> sequence = [12, 10, 8, 6, 4, 2];

        // when
        List<int> result = sequence.FindLis(Comparer<int>.Default).ToList();

        // then
        Assert.That(result, Is.EqualTo([2]));
    }

    [Test]
    public void FindLis_WhenMultipleSubsequences_ThenLeastLexicographically()
    {
        // given
        List<int> sequence = [2, 1, 4, 3, 6, 5, 8, 7, 10];

        // when
        IEnumerable<int> result = sequence.FindLis(Comparer<int>.Default);

        // then
        Assert.That(result, Is.EqualTo([1, 3, 5, 7, 10]));
    }

    [Test]
    public void FindLis_WhenSearchInMiddle_ThenLeastLexicographically()
    {
        // given
        List<int> sequence = [0, 2, 4, 6, 8, 3, 5, 7, 8];

        // when
        IEnumerable<int> result = sequence.FindLis(Comparer<int>.Default);

        // then
        Assert.That(result, Is.EqualTo([0, 2, 3, 5, 7, 8]));
    }

    [Test]
    public void FindLis_WhenIncreasingAndReversedComparator_ThenLastElementOnly()
    {
        // given
        List<int> sequence = [1, 3, 5, 7, 9, 11, 13, 15];

        // when
        IEnumerable<int> result = sequence.FindLis((i1, i2) => i2.CompareTo(i1));

        // then
        Assert.That(result, Is.EqualTo([15]));
    }

    [Test]
    public void FindLis_WhenDecreasingAndReversedComparator_ThenAllElements()
    {
        // given
        List<int> sequence = [12, 10, 8, 6, 4, 2];

        // when
        IEnumerable<int> result = sequence.FindLis((i1, i2) => i2.CompareTo(i1));

        // then
        int[] resultArray = result.ToArray();

        Assert.That(resultArray, Is.EquivalentTo(sequence));
        Assert.That(resultArray, Is.Ordered.Descending);
    }
}
