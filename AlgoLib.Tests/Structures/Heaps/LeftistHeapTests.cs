using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Structures.Heaps;

// Tests: Structure of leftist heap.
[TestFixture]
public class LeftistHeapTests
{
    private readonly int[] numbers = new[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
    private readonly int minimum;
    private LeftistHeap<int> testObject;

    public LeftistHeapTests() => minimum = numbers.Min();

    [SetUp]
    public void SetUp() => testObject = new LeftistHeap<int>(numbers);

    [Test]
    public void Count_WhenEmpty_ThenZero()
    {
        // when
        int result = new LeftistHeap<int>().Count;

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
        IEnumerator<int> result = new LeftistHeap<int>().GetEnumerator();

        // then
        result.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_WhenSingleElement_ThenThisElementOnly()
    {
        // given
        int element = numbers[0];

        // when
        IEnumerator<int> result = new LeftistHeap<int>(new[] { element }).GetEnumerator();

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

        testObject = new LeftistHeap<int>();

        // when
        testObject.Push(element);

        // then
        testObject.Should().HaveCount(1);
        testObject.Peek().Should().Be(element);
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
    public void Push_WhenNewElementGreaterThanMinimum_ThenAdded()
    {
        // when
        testObject.Push(minimum + 3);

        // then
        testObject.Should().HaveCount(numbers.Length + 1);
        testObject.Peek().Should().Be(minimum);
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
        Action action = () => _ = new LeftistHeap<int>().Peek();

        // then
        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Peek_WhenSingleElement_ThenThisElement()
    {
        // given
        int element = numbers[0];

        // when
        int result = new LeftistHeap<int>(new[] { element }).Peek();

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
        bool result = new LeftistHeap<int>().TryPeek(out int resultValue);

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
        Action action = () => _ = new LeftistHeap<int>().Pop();

        // then then
        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Pop_WhenSingleElement_ThenThisElementRemoved()
    {
        // given
        int element = numbers[0];

        testObject = new LeftistHeap<int>(new[] { element });

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
        bool result = new LeftistHeap<int>().TryPop(out int resultValue);

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
    #region OperatorPlus

    [Test]
    public void OperatorPlus_WhenEmptyAndNotEmpty_ThenSameAsOther()
    {
        // when
        LeftistHeap<int> result = new LeftistHeap<int>() + testObject;

        // then
        result.Count.Should().Be(numbers.Length);
        result.Peek().Should().Be(testObject.Peek());
    }

    [Test]
    public void OperatorPlus_WhenNotEmptyAndEmpty_ThenNoChanges()
    {
        // when
        LeftistHeap<int> result = testObject + new LeftistHeap<int>();

        // then
        result.Count.Should().Be(numbers.Length);
        result.Peek().Should().Be(testObject.Peek());
    }

    [Test]
    public void OperatorPlus_WhenOtherHasLessMinimum_ThenNewMinimum()
    {
        // given
        var other = new LeftistHeap<int>(
            new[] { minimum - 3, minimum + 5, minimum + 13, minimum + 20 });

        // when
        LeftistHeap<int> result = testObject + other;

        // then
        result.Count.Should().Be(numbers.Length + other.Count);
        result.Peek().Should().Be(other.Peek());
    }

    [Test]
    public void OperatorPlus_WhenOtherHasGreaterMinimum_ThenMinimumRemains()
    {
        // given
        var other = new LeftistHeap<int>(new[] { minimum + 5, minimum + 13, minimum + 20 });

        // when
        LeftistHeap<int> result = testObject + other;

        // then
        result.Count.Should().Be(numbers.Length + other.Count);
        result.Peek().Should().Be(testObject.Peek());
    }

    [Test]
    public void OperatorPlus_WhenSharedInnerHeap_ThenChangedOnlyMergingHeap()
    {
        // given
        int[] firstElements = new[] { 10, 20 };
        int[] secondElements = new[] { 4, 8 };

        testObject = new LeftistHeap<int>();
        var first = new LeftistHeap<int>(firstElements);
        var second = new LeftistHeap<int>(secondElements);

        // when
        LeftistHeap<int> result1 = testObject + first;
        LeftistHeap<int> result2 = result1 + second;

        // then
        result1.Peek().Should().Be(firstElements.Min());
        result1.ToArray().Should().BeEquivalentTo(firstElements);
        result2.Peek().Should().Be(secondElements.Min());
        result2.ToArray().Should().BeEquivalentTo(firstElements.Concat(secondElements).ToArray());
        testObject.Should().BeEmpty();
        first.ToArray().Should().BeEquivalentTo(firstElements);
        second.ToArray().Should().BeEquivalentTo(secondElements);
    }

    [Test]
    public void OperatorPlusEqual_WhenSharedInnerHeap_ThenChangedOnlyMergingHeap()
    {
        // given
        int[] firstElements = new[] { 10, 20 };
        int[] secondElements = new[] { 4, 8 };

        testObject = new LeftistHeap<int>();
        var first = new LeftistHeap<int>(firstElements);
        var second = new LeftistHeap<int>(secondElements);

        // when
        testObject += first;
        testObject += second;

        // then
        testObject.Peek().Should().Be(firstElements.Concat(secondElements).Min());
        testObject.ToArray().Should().BeEquivalentTo(firstElements.Concat(secondElements).ToArray());
        first.ToArray().Should().BeEquivalentTo(firstElements);
        second.ToArray().Should().BeEquivalentTo(secondElements);
    }

    #endregion
}
