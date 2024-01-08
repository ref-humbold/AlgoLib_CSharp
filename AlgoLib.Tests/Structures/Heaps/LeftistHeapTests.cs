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
    private LeftistHeap<int> testObject;

    [SetUp]
    public void SetUp()
    {
        testObject = new LeftistHeap<int>(numbers);
    }

    [Test]
    public void Count_WhenEmpty_ThenZero()
    {
        // given
        testObject = new LeftistHeap<int>();
        // when
        int result = testObject.Count;
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

    [Test]
    public void GetEnumerator_WhenEmpty_ThenNoElements()
    {
        // given
        testObject = new LeftistHeap<int>();
        // when
        IEnumerator<int> result = testObject.GetEnumerator();
        // then
        result.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_WhenSingleElement_ThenThisElementOnly()
    {
        // given
        int element = 17;

        testObject = new LeftistHeap<int>(new[] { element });
        // when
        IEnumerator<int> result = testObject.GetEnumerator();
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
        result.Should().HaveElementAt(0, numbers.Min());
    }

    #region Peek & TryPeek

    [Test]
    public void Peek_WhenEmpty_ThenInvalidOperationException()
    {
        // given
        testObject = new LeftistHeap<int>();
        // when
        Action action = () => testObject.Peek();
        // then
        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Peek_WhenSingleElement_ThenThisElement()
    {
        // given
        int element = 19;

        testObject = new LeftistHeap<int>(new[] { element });
        // when
        int result = testObject.Peek();
        // then
        result.Should().Be(element);
    }

    [Test]
    public void Peek_WhenMultipleElements_ThenMinimalElement()
    {
        // when
        int result = testObject.Peek();
        // then
        result.Should().Be(numbers.Min());
    }

    [Test]
    public void TryPeek_WhenEmpty_ThenDefaultValue()
    {
        // given
        testObject = new LeftistHeap<int>();
        // when
        bool result = testObject.TryPeek(out int resultValue);
        // then
        result.Should().BeFalse();
        resultValue.Should().Be(default);
    }

    [Test]
    public void TryPeek_WhenSingleElement_ThenThisElement()
    {
        // given
        int element = 19;

        testObject = new LeftistHeap<int>(new[] { element });
        // when
        bool result = testObject.TryPeek(out int resultValue);
        // then
        result.Should().BeTrue();
        resultValue.Should().Be(element);
    }

    [Test]
    public void TryPeek_WhenMultipleElements_ThenMinimalElement()
    {
        // when
        bool result = testObject.TryPeek(out int resultValue);
        // then
        result.Should().BeTrue();
        resultValue.Should().Be(numbers.Min());
    }

    #endregion
    #region Push & PushRange

    [Test]
    public void Push_WhenNewElement_ThenAdded()
    {
        // given
        int element = 46;
        // when
        testObject.Push(element);
        // then
        testObject.Count.Should().Be(numbers.Length + 1);
        testObject.Peek().Should().Be(numbers.Min());
    }

    [Test]
    public void Push_WhenEmpty_ThenAdded()
    {
        // given
        int element = 19;

        testObject = new LeftistHeap<int>();
        // when
        testObject.Push(element);
        // then
        testObject.Count.Should().Be(1);
        testObject.Peek().Should().Be(element);
    }

    [Test]
    public void Push_WhenNewElementIsLessThanMinimum_ThenNewMinimum()
    {
        // given
        int element = numbers.Min() - 3;
        // when
        testObject.Push(element);
        // then
        testObject.Count.Should().Be(numbers.Length + 1);
        testObject.Peek().Should().Be(element);
    }

    [Test]
    public void PushRange_WhenNewElements_ThenAllAdded()
    {
        // given
        int[] elements = new[] { 46, 111, 14, 29 };
        // when
        testObject.PushRange(elements);
        // then
        testObject.Count.Should().Be(numbers.Length + elements.Length);
        testObject.Peek().Should().Be(Math.Min(numbers.Min(), elements.Min()));
    }

    #endregion
    #region Pop & TryPop

    [Test]
    public void Pop_WhenEmpty_ThenInvalidOperationException()
    {
        // given
        testObject = new LeftistHeap<int>();
        // when
        Action action = () => testObject.Pop();
        // then
        action.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Pop_WhenSingleElement_ThenThisElementRemoved()
    {
        // given
        int element = 19;

        testObject = new LeftistHeap<int>(new[] { element });
        // when
        int result = testObject.Pop();
        // then
        testObject.Should().BeEmpty();
        result.Should().Be(element);
    }

    [Test]
    public void Pop_WhenMultipleElements_ThenMinimalElementRemoved()
    {
        // when
        int result = testObject.Pop();
        // then
        testObject.Count.Should().Be(numbers.Length - 1);
        result.Should().Be(numbers.Min());
    }

    [Test]
    public void Pop_WhenMultipleCalls_ThenSorted()
    {
        // when
        var result = new List<int>();

        while(testObject.Count > 0)
            result.Add(testObject.Pop());
        // then
        result.Should().BeEquivalentTo(numbers.ToList());
        result.Should().BeInAscendingOrder();
    }

    [Test]
    public void TryPop_WhenEmpty_ThenDefaultValue()
    {
        // given
        testObject = new LeftistHeap<int>();
        // when
        bool result = testObject.TryPop(out int resultValue);
        // then
        result.Should().BeFalse();
        resultValue.Should().Be(default);
    }

    [Test]
    public void TryPop_WhenSingleElement_ThenThisElementRemoved()
    {
        // given
        int element = 19;

        testObject = new LeftistHeap<int>(new[] { element });
        // when
        bool result = testObject.TryPop(out int resultValue);
        // then
        testObject.Should().BeEmpty();
        result.Should().BeTrue();
        resultValue.Should().Be(element);
    }

    [Test]
    public void TryPop_WhenMultipleElements_ThenMinimalElementRemoved()
    {
        // when
        bool result = testObject.TryPop(out int resultValue);
        // then
        testObject.Count.Should().Be(numbers.Length - 1);
        result.Should().BeTrue();
        resultValue.Should().Be(numbers.Min());
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
        result.Peek().Should().Be(numbers.Min());
    }

    [Test]
    public void OperatorPlus_WhenOtherHasGreaterMinimum_ThenMinimumRemains()
    {
        // given
        var other = new LeftistHeap<int>(new[] {
            numbers.Min() + 5, numbers.Min() + 13, numbers.Min() + 20
        });
        // when
        LeftistHeap<int> result = testObject + other;
        // then
        result.Count.Should().Be(numbers.Length + other.Count);
        result.Peek().Should().Be(numbers.Min());
    }

    [Test]
    public void OperatorPlus_WhenOtherHasLessMinimum_ThenNewMinimum()
    {
        // given
        int newMinimum = numbers.Min() - 4;
        var other = new LeftistHeap<int>(new[] {
            newMinimum, numbers.Min() + 5, numbers.Min() + 13, numbers.Min() + 20
        });
        // when
        LeftistHeap<int> result = testObject + other;
        // then
        result.Count.Should().Be(numbers.Length + other.Count);
        result.Peek().Should().Be(newMinimum);
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
        result1.Peek().Should().Be(10);
        result1.ToArray().Should().BeEquivalentTo(firstElements);
        result2.Peek().Should().Be(4);
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
        testObject.Peek().Should().Be(4);
        testObject.ToArray().Should().BeEquivalentTo(firstElements.Concat(secondElements).ToArray());
        first.ToArray().Should().BeEquivalentTo(firstElements);
        second.ToArray().Should().BeEquivalentTo(secondElements);
    }

    #endregion
}
