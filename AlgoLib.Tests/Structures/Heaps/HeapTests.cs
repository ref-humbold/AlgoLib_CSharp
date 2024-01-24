using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Structures.Heaps;

// Tests: Structure of heap.
[TestFixture]
public class HeapTests
{
    private readonly int[] numbers = new[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
    private readonly int minimum;
    private Heap<int> testObject;

    public HeapTests() => minimum = numbers.Min();

    [SetUp]
    public void SetUp() => testObject = new Heap<int>(numbers);

    [Test]
    public void Count_WhenEmpty_ThenZero()
    {
        // when
        int result = new Heap<int>().Count;
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
        IEnumerator<int> result = new Heap<int>().GetEnumerator();
        // then
        result.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_WhenSingleElement_ThenThisElementOnly()
    {
        // given
        int element = numbers[0];
        // when
        IEnumerator<int> result = new Heap<int>(new[] { element }).GetEnumerator();
        // then
        result.MoveNext().Should().BeTrue();
        result.Current.Should().Be(element);
        result.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_WhenMultipleElements_ThenAllElementsMinimumFirst()
    {
        // when
        var result = testObject.ToList();
        // then
        result.Should().BeEquivalentTo(numbers);
        result.Should().HaveElementAt(0, minimum);
    }

    #endregion
    #region Push & PushRange

    [Test]
    public void Push_WhenEmpty_ThenAdded()
    {
        // given
        int element = numbers[0];

        testObject = new Heap<int>();
        // when
        testObject.Push(element);
        // then
        testObject.Should().HaveCount(1);
        testObject.Peek().Should().Be(element);
    }

    [Test]
    public void Push_WhenNewElementGreaterThanMinimum_ThenAdded()
    {
        // when
        testObject.Push(minimum + 3);
        // then
        testObject.Should().HaveCount(numbers.Length + 1);
        testObject.Peek().Should().Be(minimum);
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
        testObject.Peek().Should().Be(element);
    }

    [Test]
    public void PushRange_WhenNewElements_ThenAllAdded()
    {
        // given
        int[] elements = new[] { minimum - 3, minimum + 5, minimum + 13, minimum + 20 };
        // when
        testObject.PushRange(elements);
        // then
        testObject.Should().HaveCount(numbers.Length + elements.Length);
        testObject.Peek().Should().Be(elements.Min());
    }

    #endregion
    #region Peek & TryPeek

    [Test]
    public void Peek_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => _ = new Heap<int>().Peek();
        // then
        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Peek_WhenSingleElement_ThenThisElement()
    {
        // given
        int element = numbers[0];
        // when
        int result = new Heap<int>(new[] { element }).Peek();
        // then
        result.Should().Be(element);
    }

    [Test]
    public void Peek_WhenMultipleElements_ThenMinimalElement()
    {
        // when
        int result = testObject.Peek();
        // then
        result.Should().Be(minimum);
    }

    [Test]
    public void TryPeek_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new Heap<int>().TryPeek(out int resultValue);
        // then
        result.Should().BeFalse();
        resultValue.Should().Be(default);
    }

    [Test]
    public void TryPeek_WhenNotEmpty_ThenMinimalElement()
    {
        // when
        bool result = testObject.TryPeek(out int resultValue);
        // then
        result.Should().BeTrue();
        resultValue.Should().Be(minimum);
    }

    #endregion
    #region Pop & TryPop

    [Test]
    public void Pop_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => _ = new Heap<int>().Pop();
        // then then
        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Pop_WhenSingleElement_ThenThisElementRemoved()
    {
        // given
        int element = numbers[0];

        testObject = new Heap<int>(new[] { element });
        // when
        int result = testObject.Pop();
        // then
        result.Should().Be(element);
        testObject.Should().BeEmpty();
    }

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
    public void Pop_WhenMultipleCalls_ThenSortedAscendingToComparer()
    {
        // when
        var result = new List<int>();

        while (testObject.Count > 0)
            result.Add(testObject.Pop());
        // then
        result.Should().BeEquivalentTo(numbers.ToList());
        result.Should().BeInAscendingOrder(testObject.Comparer);
    }

    [Test]
    public void TryPop_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new Heap<int>().TryPop(out int resultValue);
        // then
        result.Should().BeFalse();
        resultValue.Should().Be(default);
    }

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

    #endregion
}
