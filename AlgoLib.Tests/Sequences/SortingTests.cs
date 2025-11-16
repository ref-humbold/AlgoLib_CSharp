using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AlgoLib.Sequences;

// Tests: Algorithms for sequence sorting.
[TestFixture]
public class SortingTests
{
    #region HeapSort

    [Test]
    public void HeapSort_ThenSortedAscending()
    {
        // given
        var sequence = new List<int> { 3, 17, -6, 0, 9, -12, 7, 4, 2 };

        // when
        sequence.HeapSort();

        // then
        Assert.That(sequence, Is.Ordered.Ascending);
    }

    [Test]
    public void HeapSort_WhenEmptyList_ThenEmpty()
    {
        // given
        var sequence = new List<int>();

        // when
        sequence.HeapSort();

        // then
        Assert.That(sequence, Is.Empty);
    }

    [Test]
    public void HeapSort_WhenNull_ThenArgumentNullException()
    {
        // when
        Action action = () => Sorting.HeapSort<int>(null);

        // then
        Assert.That(action, Throws.ArgumentNullException);
    }

    #endregion
    #region TopDownMergeSort

    [Test]
    public void TopDownMergeSort_ThenSortedAscending()
    {
        // given
        var sequence = new List<int> { 3, 17, -6, 0, 9, -12, 7, 4, 2 };

        // when
        sequence.TopDownMergeSort();

        // then
        Assert.That(sequence, Is.Ordered.Ascending);
    }

    [Test]
    public void TopDownMergeSort_WhenSortingPairsByFirst_ThenSortingIsStable()
    {
        // given
        var sequence = new List<IntPair>
        {
            new(3, 17), new(-6, 0), new(9, 12), new(3, 4), new(9, -14), new(-1, 7), new(0, 2)
        };

        // when
        sequence.TopDownMergeSort();

        // then
        Assert.That(sequence, Is.Ordered.Ascending);
        Assert.That(
            sequence.IndexOf(new IntPair(3, 17)),
            Is.LessThan(sequence.IndexOf(new IntPair(3, 4))));
        Assert.That(
            sequence.IndexOf(new IntPair(9, 12)),
            Is.LessThan(sequence.IndexOf(new IntPair(9, -14))));
    }

    [Test]
    public void TopDownMergeSort_WhenEmptyList_ThenEmpty()
    {
        // given
        var sequence = new List<int>();

        // when
        sequence.TopDownMergeSort();

        // then
        Assert.That(sequence, Is.Empty);
    }

    [Test]
    public void TopDownMergeSort_WhenNull_ThenArgumentNullException()
    {
        // when
        Action action = () => Sorting.TopDownMergeSort<int>(null);

        // then
        Assert.That(action, Throws.ArgumentNullException);
    }

    #endregion
    #region BottomUpMergeSort

    [Test]
    public void BottomUpMergeSort_ThenSortedAscending()
    {
        // given
        var sequence = new List<int> { 3, 17, -6, 0, 9, -12, 7, 4, 2 };

        // when
        sequence.BottomUpMergeSort();

        // then
        Assert.That(sequence, Is.Ordered.Ascending);
    }

    [Test]
    public void BottomUpMergeSort_WhenSortingPairsByFirst_ThenSortingIsStable()
    {
        // given
        var sequence = new List<IntPair>
        {
            new(3, 17), new(-6, 0), new(9, 12), new(3, 4),
            new(9, -14), new(-1, 7), new(0, 2)
        };

        // when
        sequence.BottomUpMergeSort();

        // then
        Assert.That(sequence, Is.Ordered.Ascending);
        Assert.That(
            sequence.IndexOf(new IntPair(3, 17)),
            Is.LessThan(sequence.IndexOf(new IntPair(3, 4))));
        Assert.That(
            sequence.IndexOf(new IntPair(9, 12)),
            Is.LessThan(sequence.IndexOf(new IntPair(9, -14))));
    }

    [Test]
    public void BottomUpMergeSort_WhenEmptyList_ThenEmpty()
    {
        // given
        var sequence = new List<int>();

        // when
        sequence.BottomUpMergeSort();

        // then
        Assert.That(sequence, Is.Empty);
    }

    [Test]
    public void BottomUpMergeSort_WhenNull_ThenArgumentNullException()
    {
        // when
        Action action = () => Sorting.BottomUpMergeSort<int>(null);

        // then
        Assert.That(action, Throws.ArgumentNullException);
    }

    #endregion
    #region QuickSort

    [Test]
    public void QuickSort_ThenSortedAscending()
    {
        // given
        var sequence = new List<int> { 3, 17, -6, 0, 9, -12, 7, 4, 2 };

        // when
        sequence.QuickSort();

        // then
        Assert.That(sequence, Is.Ordered.Ascending);
    }

    [Test]
    public void QuickSort_WhenEmptyList_ThenEmpty()
    {
        // given
        var sequence = new List<int>();

        // when
        sequence.QuickSort();

        // then
        Assert.That(sequence, Is.Empty);
    }

    [Test]
    public void QuickSort_WhenNull_ThenArgumentNullException()
    {
        // when
        Action action = () => Sorting.QuickSort<int>(null);

        // then
        Assert.That(action, Throws.ArgumentNullException);
    }

    #endregion

    private readonly record struct IntPair(int First, int Second)
        : IComparable, IComparable<IntPair>
    {
        public int CompareTo(object obj) =>
            obj is IntPair pair
                ? CompareTo(pair)
                : throw new ArgumentException(
                    "Compared object is not of type IntPair", nameof(obj));

        public int CompareTo(IntPair other) => First.CompareTo(other.First);

        public bool Equals(IntPair other) => First == other.First && Second == other.Second;

        public override int GetHashCode() => (First, Second).GetHashCode();

        public override string ToString() => $"IntPair({First}, {Second})";
    }
}
