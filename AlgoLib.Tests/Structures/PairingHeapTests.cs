﻿// Tests: Structure of pairing heap.
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Structures;

[TestFixture]
public class PairingHeapTests
{
    private readonly int[] numbers = new[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
    private PairingHeap<int> testObject;

    [SetUp]
    public void SetUp()
    {
        testObject = new PairingHeap<int>(numbers);
    }

    [Test]
    public void Count_WhenEmpty_ThenZero()
    {
        // given
        testObject = new PairingHeap<int>();
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
        testObject.Count.Should().Be(0);
    }

    [Test]
    public void GetEnumerator_WhenEmpty_ThenNoElements()
    {
        // given
        testObject = new PairingHeap<int>();
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

        testObject = new PairingHeap<int>(new[] { element });
        // when
        IEnumerator<int> result = testObject.GetEnumerator();
        // then
        result.MoveNext().Should().BeTrue();
        result.Current.Should().Be(element);
        result.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_WhenMultipleElements_ThenFirstMinimumAndLastMaximum()
    {
        // when
        var result = new List<int>();
        IEnumerator<int> enumerator = testObject.GetEnumerator();

        while(enumerator.MoveNext())
            result.Add(enumerator.Current);
        // then
        result.Should().HaveElementAt(0, numbers.Min());
    }

    #region Peek & TryPeek

    [Test]
    public void Peek_WhenEmpty_ThenInvalidOperationException()
    {
        // given
        testObject = new PairingHeap<int>();
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

        testObject = new PairingHeap<int>(new[] { element });
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
        testObject = new PairingHeap<int>();
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

        testObject = new PairingHeap<int>(new[] { element });
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

        testObject = new PairingHeap<int>();
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
        testObject = new PairingHeap<int>();
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

        testObject = new PairingHeap<int>(new[] { element });
        // when
        int result = testObject.Pop();
        // then
        testObject.Count.Should().Be(0);
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
        testObject = new PairingHeap<int>();
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

        testObject = new PairingHeap<int>(new[] { element });
        // when
        bool result = testObject.TryPop(out int resultValue);
        // then
        testObject.Count.Should().Be(0);
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
    #region Merge

    [Test]
    public void Merge_WhenEmptyAndNotEmpty_ThenSameAsOther()
    {
        // given
        testObject = new PairingHeap<int>();

        var other = new PairingHeap<int>(numbers);
        // when
        testObject.Merge(other);
        // then
        testObject.Count.Should().Be(numbers.Length);
        testObject.Peek().Should().Be(other.Peek());
    }

    [Test]
    public void Merge_WhenNotEmptyAndEmpty_ThenNoChanges()
    {
        // when
        testObject.Merge(new PairingHeap<int>());
        // then
        testObject.Count.Should().Be(numbers.Length);
        testObject.Peek().Should().Be(numbers.Min());
    }

    [Test]
    public void Merge_WhenOtherHasGreaterMinimum_ThenMinimumRemains()
    {
        // given
        var other = new PairingHeap<int>(new[] { numbers.Min() + 5, numbers.Min() + 13, numbers.Min() + 20 });
        // when
        testObject.Merge(other);
        // then
        testObject.Count.Should().Be(numbers.Length + other.Count);
        testObject.Peek().Should().Be(numbers.Min());
    }

    [Test]
    public void Merge_WhenOtherHasLessMinimum_ThenNewMinimum()
    {
        // given
        int newMinimum = numbers.Min() - 4;
        var other = new PairingHeap<int>(new[] { newMinimum, numbers.Min() + 5, numbers.Min() + 13, numbers.Min() + 20 });
        // when
        testObject.Merge(other);
        // then
        testObject.Count.Should().Be(numbers.Length + other.Count);
        testObject.Peek().Should().Be(newMinimum);
    }

    #endregion
}
