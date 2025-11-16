using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Structures.Heaps;

// Tests: Structure of pairing heap.
[TestFixture]
public class PairingHeapTests
{
    private readonly int[] numbers = new[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
    private readonly int minimum;
    private PairingHeap<int> testObject;

    public PairingHeapTests() => minimum = numbers.Min();

    [SetUp]
    public void SetUp() => testObject = new PairingHeap<int>(numbers);

    [Test]
    public void Count_WhenEmpty_ThenZero()
    {
        // when
        int result = new PairingHeap<int>().Count;

        // then
        Assert.That(result, Is.Zero);
    }

    [Test]
    public void Count_WhenNotEmpty_ThenNumberOfElements()
    {
        // when
        int result = testObject.Count;

        // then
        Assert.That(result, Is.EqualTo(numbers.Length));
    }

    [Test]
    public void Clear_WhenNotEmpty_ThenEmpty()
    {
        // when
        testObject.Clear();

        // then
        Assert.That(testObject, Is.Empty);
        Assert.That(testObject.Count, Is.Zero);
    }

    #region GetEnumerator

    [Test]
    public void GetEnumerator_WhenEmpty_ThenNoElements()
    {
        // when
        IEnumerator<int> result = new PairingHeap<int>().GetEnumerator();

        // then
        Assert.That(result.MoveNext(), Is.False);
    }

    [Test]
    public void GetEnumerator_WhenSingleElement_ThenThisElementOnly()
    {
        // given
        int element = numbers[0];

        // when
        IEnumerator<int> result = new PairingHeap<int>(new[] { element }).GetEnumerator();

        // then
        Assert.That(result.MoveNext(), Is.True);
        Assert.That(result.Current, Is.EqualTo(element));
        Assert.That(result.MoveNext(), Is.False);
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

        testObject = new PairingHeap<int>();

        // when
        testObject.Push(element);

        // then
        testObject.Should().HaveCount(1);
        Assert.That(testObject.Peek(), Is.EqualTo(element));
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
        Assert.That(testObject.Peek(), Is.EqualTo(element));
    }

    [Test]
    public void Push_WhenNewElementGreaterThanMinimum_ThenAdded()
    {
        // when
        testObject.Push(minimum + 3);

        // then
        testObject.Should().HaveCount(numbers.Length + 1);
        Assert.That(testObject.Peek(), Is.EqualTo(minimum));
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
        Assert.That(testObject.Peek(), Is.EqualTo(elements.Min()));
    }

    #endregion
    #region Peek & TryPeek

    [Test]
    public void Peek_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => _ = new PairingHeap<int>().Peek();

        // then
        Assert.That(action, Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void Peek_WhenSingleElement_ThenThisElement()
    {
        // given
        int element = numbers[0];

        // when
        int result = new PairingHeap<int>(new[] { element }).Peek();

        // then
        Assert.That(result, Is.EqualTo(element));
    }

    [Test]
    public void Peek_WhenMultipleElements_ThenMinimalElement()
    {
        // when
        int result = testObject.Peek();

        // then
        Assert.That(result, Is.EqualTo(minimum));
    }

    [Test]
    public void TryPeek_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new PairingHeap<int>().TryPeek(out int resultValue);

        // then
        Assert.That(result, Is.False);
        Assert.That(resultValue, Is.Default);
    }

    [Test]
    public void TryPeek_WhenNotEmpty_ThenMinimalElement()
    {
        // when
        bool result = testObject.TryPeek(out int resultValue);

        // then
        Assert.That(result, Is.True);
        Assert.That(resultValue, Is.EqualTo(minimum));
    }

    #endregion
    #region Pop & TryPop

    [Test]
    public void Pop_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => _ = new PairingHeap<int>().Pop();

        // then then
        Assert.That(action, Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void Pop_WhenSingleElement_ThenThisElementRemoved()
    {
        // given
        int element = numbers[0];

        testObject = new PairingHeap<int>(new[] { element });

        // when
        int result = testObject.Pop();

        // then
        Assert.That(result, Is.EqualTo(element));
        Assert.That(testObject, Is.Empty);
    }

    [Test]
    public void Pop_WhenMultipleElements_ThenMinimalElementRemoved()
    {
        // when
        int result = testObject.Pop();

        // then
        Assert.That(result, Is.EqualTo(minimum));
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
        bool result = new PairingHeap<int>().TryPop(out int resultValue);

        // then
        Assert.That(result, Is.False);
        Assert.That(resultValue, Is.Default);
    }

    [Test]
    public void TryPop_WhenNotEmpty_ThenMinimalElementRemoved()
    {
        // when
        bool result = testObject.TryPop(out int resultValue);

        // then
        Assert.That(result, Is.True);
        Assert.That(resultValue, Is.EqualTo(minimum));
        testObject.Should().HaveCount(numbers.Length - 1);
    }

    #endregion
    #region OperatorPlus

    [Test]
    public void OperatorPlus_WhenEmptyAndNotEmpty_ThenSameAsOther()
    {
        // when
        PairingHeap<int> result = new PairingHeap<int>() + testObject;

        // then
        Assert.That(result.Count, Is.EqualTo(numbers.Length));
        Assert.That(result.Peek(), Is.EqualTo(testObject.Peek()));
    }

    [Test]
    public void OperatorPlus_WhenNotEmptyAndEmpty_ThenNoChanges()
    {
        // when
        PairingHeap<int> result = testObject + new PairingHeap<int>();

        // then
        Assert.That(result.Count, Is.EqualTo(numbers.Length));
        Assert.That(result.Peek(), Is.EqualTo(testObject.Peek()));
    }

    [Test]
    public void OperatorPlus_WhenOtherHasLessMinimum_ThenNewMinimum()
    {
        // given
        var other = new PairingHeap<int>(
            new[] { minimum - 3, minimum + 5, minimum + 13, minimum + 20 });

        // when
        PairingHeap<int> result = testObject + other;

        // then
        Assert.That(result.Count, Is.EqualTo(numbers.Length + other.Count));
        Assert.That(result.Peek(), Is.EqualTo(other.Peek()));
    }

    [Test]
    public void OperatorPlus_WhenOtherHasGreaterMinimum_ThenMinimumRemains()
    {
        // given
        var other = new PairingHeap<int>(new[] { minimum + 5, minimum + 13, minimum + 20 });

        // when
        PairingHeap<int> result = testObject + other;

        // then
        Assert.That(result.Count, Is.EqualTo(numbers.Length + other.Count));
        Assert.That(result.Peek(), Is.EqualTo(testObject.Peek()));
    }

    [Test]
    public void OperatorPlus_WhenSharedInnerHeap_ThenChangedOnlyMergingHeap()
    {
        // given
        int[] firstElements = new[] { 10, 20 };
        int[] secondElements = new[] { 4, 8 };

        testObject = new PairingHeap<int>();
        var first = new PairingHeap<int>(firstElements);
        var second = new PairingHeap<int>(secondElements);

        // when
        PairingHeap<int> result1 = testObject + first;
        PairingHeap<int> result2 = result1 + second;

        // then
        Assert.That(result1.Peek(), Is.EqualTo(firstElements.Min()));
        result1.ToArray().Should().BeEquivalentTo(firstElements);
        Assert.That(result2.Peek(), Is.EqualTo(secondElements.Min()));
        result2.ToArray().Should().BeEquivalentTo(firstElements.Concat(secondElements).ToArray());
        Assert.That(testObject, Is.Empty);
        first.ToArray().Should().BeEquivalentTo(firstElements);
        second.ToArray().Should().BeEquivalentTo(secondElements);
    }

    [Test]
    public void OperatorPlusEqual_WhenSharedInnerHeap_ThenChangedOnlyMergingHeap()
    {
        // given
        int[] firstElements = new[] { 10, 20 };
        int[] secondElements = new[] { 4, 8 };

        testObject = new PairingHeap<int>();
        var first = new PairingHeap<int>(firstElements);
        var second = new PairingHeap<int>(secondElements);

        // when
        testObject += first;
        testObject += second;

        // then
        Assert.That(testObject.Peek(), Is.EqualTo(firstElements.Concat(secondElements).Min()));
        testObject.ToArray().Should().BeEquivalentTo(firstElements.Concat(secondElements).ToArray());
        first.ToArray().Should().BeEquivalentTo(firstElements);
        second.ToArray().Should().BeEquivalentTo(secondElements);
    }

    #endregion
}
