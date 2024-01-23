using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Structures.Heaps;

// Tests: Structure of double heap.
[TestFixture]
public class DoubleHeapTests
{
    private readonly int[] numbers = new[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
    private readonly int minimum;
    private readonly int maximum;
    private DoubleHeap<int> testObject;

    public DoubleHeapTests()
    {
        minimum = numbers.Min();
        maximum = numbers.Max();
    }

    [SetUp]
    public void SetUp() => testObject = new DoubleHeap<int>(numbers);

    [Test]
    public void Count_WhenEmpty_ThenZero()
    {
        // when
        int result = new DoubleHeap<int>().Count;
        // then
        result.Should().Be(0);
    }

    [Test]
    public void Count_WhenNotEmpty_ThenNumberOfElements()
    {
        // when
        int result = testObject.Count;
        // then
        result.Should().Be(numbers.Length);
    }

    [Test]
    public void Clear_WhenNotEmpty_ThenEmpty()
    {
        // when
        testObject.Clear();
        // then
        testObject.Should().BeEmpty();
        testObject.Count.Should().Be(0);
    }

    #region GetEnumerator

    [Test]
    public void GetEnumerator_WhenEmpty_ThenNoElements()
    {
        // when
        IEnumerator<int> result = new DoubleHeap<int>().GetEnumerator();
        // then
        result.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_WhenSingleElement_ThenThisElementOnly()
    {
        // given
        int element = numbers[0];
        // when
        IEnumerator<int> result = new DoubleHeap<int>(new[] { element }).GetEnumerator();
        // then
        result.MoveNext().Should().BeTrue();
        result.Current.Should().Be(element);
        result.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_WhenMultipleElements_ThenAllElementsMinimumFirstMaximumLast()
    {
        // when
        var result = testObject.ToList();
        // then
        result.Should().BeEquivalentTo(numbers);
        result.Should().HaveElementAt(0, minimum)
            .And.HaveElementAt(result.Count - 1, maximum);
    }

    #endregion
    #region Push*

    [Test]
    public void Push_WhenEmpty_ThenAdded()
    {
        // given
        int element = numbers[0];

        testObject = new DoubleHeap<int>();
        // when
        testObject.Push(element);
        // then
        testObject.Should().HaveCount(1);
        testObject.PeekMin().Should().Be(element);
        testObject.PeekMax().Should().Be(element);
    }

    [Test]
    public void Push_WhenNewElementBetweenMinimumAndMaximum_ThenAdded()
    {
        // when
        testObject.Push((minimum + maximum) / 2);
        // then
        testObject.Should().HaveCount(numbers.Length + 1);
        testObject.PeekMin().Should().Be(minimum);
        testObject.PeekMax().Should().Be(maximum);
    }

    [Test]
    public void Push_WhenNewElementLessThanMinimum_ThenNewMinimum()
    {
        // given
        int element = minimum - 3;
        // when
        testObject.Push(element);
        // then
        testObject.Should().HaveCount(numbers.Length + 1);
        testObject.PeekMin().Should().Be(element);
        testObject.PeekMax().Should().Be(maximum);
    }

    [Test]
    public void Push_WhenNewElementGreaterThanMaximum_ThenNewMaximum()
    {
        // given
        int element = maximum + 3;
        // when
        testObject.Push(element);
        // then
        testObject.Should().HaveCount(numbers.Length + 1);
        testObject.PeekMin().Should().Be(minimum);
        testObject.PeekMax().Should().Be(element);
    }

    [Test]
    public void PushRange_WhenNewElements_ThenAllAdded()
    {
        // given
        int[] elements = new[] { minimum - 4, minimum + 5, minimum + 13, minimum + 20, maximum + 4 };
        // when
        testObject.PushRange(elements);
        // then
        testObject.Should().HaveCount(numbers.Length + elements.Length);
        testObject.PeekMin().Should().Be(elements.Min());
        testObject.PeekMax().Should().Be(elements.Max());
    }

    #endregion
    #region Peek*

    [Test]
    public void Peek_WhenMultipleElements_ThenMinimalElement()
    {
        // when
        int result = testObject.Peek();
        // then
        result.Should().Be(minimum);
    }

    [Test]
    public void PeekMin_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => new DoubleHeap<int>().PeekMin();
        // then
        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void PeekMin_WhenSingleElement_ThenThisElement()
    {
        // given
        int element = numbers[0];
        // when
        int result = new DoubleHeap<int>(new[] { element }).PeekMin();
        // then
        result.Should().Be(element);
    }

    [Test]
    public void PeekMin_WhenMultipleElements_ThenMinimalElement()
    {
        // when
        int result = testObject.PeekMin();
        // then
        result.Should().Be(minimum);
    }

    [Test]
    public void PeekMax_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => new DoubleHeap<int>().PeekMax();
        // then
        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void PeekMax_WhenSingleElement_ThenThisElement()
    {
        // given
        int element = numbers[0];
        // when
        int result = new DoubleHeap<int>(new[] { element }).PeekMax();
        // then
        result.Should().Be(element);
    }

    [Test]
    public void PeekMax_WhenMultipleElements_ThenMaximalElement()
    {
        // when
        int result = testObject.PeekMax();
        // then
        result.Should().Be(maximum);
    }

    #endregion
    #region TryPeek*

    [Test]
    public void TryPeek_WhenNotEmpty_ThenMinimalElement()
    {
        // when
        bool result = testObject.TryPeek(out int resultValue);
        // then
        result.Should().BeTrue();
        resultValue.Should().Be(minimum);
    }

    [Test]
    public void TryPeekMin_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new DoubleHeap<int>().TryPeekMin(out int resultValue);
        // then
        result.Should().BeFalse();
        resultValue.Should().Be(default);
    }

    [Test]
    public void TryPeekMin_WhenNotEmpty_ThenMinimalElement()
    {
        // when
        bool result = testObject.TryPeekMin(out int resultValue);
        // then
        result.Should().BeTrue();
        resultValue.Should().Be(minimum);
    }

    [Test]
    public void TryPeekMax_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new DoubleHeap<int>().TryPeekMax(out int resultValue);
        // then
        result.Should().BeFalse();
        resultValue.Should().Be(default);
    }

    [Test]
    public void TryPeekMax_WhenNotEmpty_ThenMaximalElement()
    {
        // when
        bool result = testObject.TryPeekMax(out int resultValue);
        // then
        result.Should().BeTrue();
        resultValue.Should().Be(maximum);
    }

    #endregion
    #region Pop*

    [Test]
    public void Pop_WhenMultipleElements_ThenMinimalElementRemoved()
    {
        // when
        int result = testObject.Pop();
        // then
        result.Should().Be(minimum);
        testObject.Should().HaveCount(numbers.Length - 1);
    }

    [Test]
    public void PopMin_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => new DoubleHeap<int>().PopMin();
        // then
        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void PopMin_WhenSingleElement_ThenThisElementRemoved()
    {
        // given
        int element = numbers[0];

        testObject = new DoubleHeap<int>(new[] { element });
        // when
        int result = testObject.PopMin();
        // then
        result.Should().Be(element);
        testObject.Should().BeEmpty();
    }

    [Test]
    public void PopMin_WhenMultipleElements_ThenMinimalElementRemoved()
    {
        // when
        int result = testObject.PopMin();
        // then
        result.Should().Be(minimum);
        testObject.Should().HaveCount(numbers.Length - 1);
    }

    [Test]
    public void PopMin_WhenMultipleCalls_ThenSortedAscendingToComparer()
    {
        // when
        var result = new List<int>();

        while(testObject.Count > 0)
            result.Add(testObject.PopMin());
        // then
        result.Should().BeEquivalentTo(numbers.ToList());
        result.Should().BeInAscendingOrder(testObject.Comparer);
    }

    [Test]
    public void PopMax_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => new DoubleHeap<int>().PopMax();
        // then
        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void PopMax_WhenSingleElement_ThenThisElementRemoved()
    {
        // given
        int element = numbers[0];

        testObject = new DoubleHeap<int>(new[] { element });
        // when
        int result = testObject.PopMax();
        // then
        result.Should().Be(element);
        testObject.Should().BeEmpty();
    }

    [Test]
    public void PopMax_WhenMultipleElements_ThenMaximalElementRemoved()
    {
        // when
        int result = testObject.PopMax();
        // then
        result.Should().Be(maximum);
        testObject.Should().HaveCount(numbers.Length - 1);
    }

    [Test]
    public void PopMax_WhenMultipleCalls_ThenSortedDescendingToComparer()
    {
        // when
        var result = new List<int>();

        while(testObject.Count > 0)
            result.Add(testObject.PopMax());
        // then
        result.Should().BeEquivalentTo(numbers.ToList());
        result.Should().BeInDescendingOrder(testObject.Comparer);
    }

    #endregion
    #region TryPop*

    [Test]
    public void TryPop_WhenNotEmpty_ThenMinimalElementRemoved()
    {
        // when
        bool result = testObject.TryPop(out int resultValue);
        // then
        result.Should().BeTrue();
        resultValue.Should().Be(minimum);
        testObject.Should().HaveCount(numbers.Length - 1);
    }

    [Test]
    public void TryPopMin_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new DoubleHeap<int>().TryPopMin(out int resultValue);
        // then
        result.Should().BeFalse();
        resultValue.Should().Be(default);
    }

    [Test]
    public void TryPopMin_WhenNotEmpty_ThenMinimalElementRemoved()
    {
        // when
        bool result = testObject.TryPopMin(out int resultValue);
        // then
        result.Should().BeTrue();
        resultValue.Should().Be(minimum);
        testObject.Should().HaveCount(numbers.Length - 1);
    }

    [Test]
    public void TryPopMax_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new DoubleHeap<int>().TryPopMax(out int resultValue);
        // then
        result.Should().BeFalse();
        resultValue.Should().Be(default);
    }

    [Test]
    public void TryPopMax_WhenNotEmpty_ThenMaximalElementRemoved()
    {
        // when
        bool result = testObject.TryPopMax(out int resultValue);
        // then
        result.Should().BeTrue();
        resultValue.Should().Be(maximum);
        testObject.Should().HaveCount(numbers.Length - 1);
    }

    #endregion
}
