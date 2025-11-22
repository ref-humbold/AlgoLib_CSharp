using System;
using System.Collections.Generic;
using System.Linq;
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
        IEnumerator<int> result = new DoubleHeap<int>().GetEnumerator();

        // then
        Assert.That(result.MoveNext(), Is.False);
    }

    [Test]
    public void GetEnumerator_WhenSingleElement_ThenThisElementOnly()
    {
        // given
        int element = numbers[0];

        // when
        IEnumerator<int> result = new DoubleHeap<int>(new[] { element }).GetEnumerator();

        // then
        Assert.That(result.MoveNext(), Is.True);
        Assert.That(result.Current, Is.EqualTo(element));
        Assert.That(result.MoveNext(), Is.False);
    }

    [Test]
    public void GetEnumerator_WhenMultipleElements_ThenAllElementsMinimumFirstMaximumLast()
    {
        // when
        List<int> result = testObject.ToList();

        // then
        Assert.That(result, Is.EquivalentTo(numbers));
        Assert.That(result, Has.ItemAt(0).EqualTo(minimum));
        Assert.That(result, Has.ItemAt(result.Count - 1).EqualTo(maximum));
    }

    #endregion
    #region Push & PushRange

    [Test]
    public void Push_WhenEmpty_ThenAdded()
    {
        // given
        int element = numbers[0];

        testObject = new DoubleHeap<int>();

        // when
        testObject.Push(element);

        // then
        Assert.That(testObject, Has.Count.EqualTo(1));
        Assert.That(testObject.PeekMin(), Is.EqualTo(element));
        Assert.That(testObject.PeekMax(), Is.EqualTo(element));
    }

    [Test]
    public void Push_WhenNewElementBetweenMinimumAndMaximum_ThenAdded()
    {
        // when
        testObject.Push((minimum + maximum) / 2);

        // then
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length + 1));
        Assert.That(testObject.PeekMin(), Is.EqualTo(minimum));
        Assert.That(testObject.PeekMax(), Is.EqualTo(maximum));
    }

    [Test]
    public void Push_WhenNewElementLessThanMinimum_ThenNewMinimum()
    {
        // given
        int element = minimum - 3;

        // when
        testObject.Push(element);

        // then
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length + 1));
        Assert.That(testObject.PeekMin(), Is.EqualTo(element));
        Assert.That(testObject.PeekMax(), Is.EqualTo(maximum));
    }

    [Test]
    public void Push_WhenNewElementGreaterThanMaximum_ThenNewMaximum()
    {
        // given
        int element = maximum + 3;

        // when
        testObject.Push(element);

        // then
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length + 1));
        Assert.That(testObject.PeekMin(), Is.EqualTo(minimum));
        Assert.That(testObject.PeekMax(), Is.EqualTo(element));
    }

    [Test]
    public void PushRange_WhenNewElements_ThenAllAdded()
    {
        // given
        int[] elements = new[]
            { minimum - 3, minimum + 5, minimum + 13, minimum + 20, maximum + 3 };

        // when
        testObject.PushRange(elements);

        // then
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length + elements.Length));
        Assert.That(testObject.PeekMin(), Is.EqualTo(elements.Min()));
        Assert.That(testObject.PeekMax(), Is.EqualTo(elements.Max()));
    }

    #endregion
    #region Peek*

    [Test]
    public void Peek_WhenMultipleElements_ThenMinimalElement()
    {
        // when
        int result = testObject.Peek();

        // then
        Assert.That(result, Is.EqualTo(minimum));
    }

    [Test]
    public void PeekMin_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => new DoubleHeap<int>().PeekMin();

        // then
        Assert.That(action, Throws.InvalidOperationException);
    }

    [Test]
    public void PeekMin_WhenSingleElement_ThenThisElement()
    {
        // given
        int element = numbers[0];

        // when
        int result = new DoubleHeap<int>(new[] { element }).PeekMin();

        // then
        Assert.That(result, Is.EqualTo(element));
    }

    [Test]
    public void PeekMin_WhenMultipleElements_ThenMinimalElement()
    {
        // when
        int result = testObject.PeekMin();

        // then
        Assert.That(result, Is.EqualTo(minimum));
    }

    [Test]
    public void PeekMax_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => new DoubleHeap<int>().PeekMax();

        // then
        Assert.That(action, Throws.InvalidOperationException);
    }

    [Test]
    public void PeekMax_WhenSingleElement_ThenThisElement()
    {
        // given
        int element = numbers[0];

        // when
        int result = new DoubleHeap<int>(new[] { element }).PeekMax();

        // then
        Assert.That(result, Is.EqualTo(element));
    }

    [Test]
    public void PeekMax_WhenMultipleElements_ThenMaximalElement()
    {
        // when
        int result = testObject.PeekMax();

        // then
        Assert.That(result, Is.EqualTo(maximum));
    }

    #endregion
    #region TryPeek*

    [Test]
    public void TryPeek_WhenNotEmpty_ThenMinimalElement()
    {
        // when
        bool result = testObject.TryPeek(out int resultValue);

        // then
        Assert.That(result, Is.True);
        Assert.That(resultValue, Is.EqualTo(minimum));
    }

    [Test]
    public void TryPeekMin_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new DoubleHeap<int>().TryPeekMin(out int resultValue);

        // then
        Assert.That(result, Is.False);
        Assert.That(resultValue, Is.Default);
    }

    [Test]
    public void TryPeekMin_WhenNotEmpty_ThenMinimalElement()
    {
        // when
        bool result = testObject.TryPeekMin(out int resultValue);

        // then
        Assert.That(result, Is.True);
        Assert.That(resultValue, Is.EqualTo(minimum));
    }

    [Test]
    public void TryPeekMax_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new DoubleHeap<int>().TryPeekMax(out int resultValue);

        // then
        Assert.That(result, Is.False);
        Assert.That(resultValue, Is.Default);
    }

    [Test]
    public void TryPeekMax_WhenNotEmpty_ThenMaximalElement()
    {
        // when
        bool result = testObject.TryPeekMax(out int resultValue);

        // then
        Assert.That(result, Is.True);
        Assert.That(resultValue, Is.EqualTo(maximum));
    }

    #endregion
    #region Pop*

    [Test]
    public void Pop_WhenMultipleElements_ThenMinimalElementRemoved()
    {
        // when
        int result = testObject.Pop();

        // then
        Assert.That(result, Is.EqualTo(minimum));
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length - 1));
    }

    [Test]
    public void PopMin_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => new DoubleHeap<int>().PopMin();

        // then
        Assert.That(action, Throws.InvalidOperationException);
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
        Assert.That(result, Is.EqualTo(element));
        Assert.That(testObject, Is.Empty);
    }

    [Test]
    public void PopMin_WhenMultipleElements_ThenMinimalElementRemoved()
    {
        // when
        int result = testObject.PopMin();

        // then
        Assert.That(result, Is.EqualTo(minimum));
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length - 1));
    }

    [Test]
    public void PopMin_WhenMultipleCalls_ThenSortedAscendingToComparer()
    {
        // when
        var result = new List<int>();

        while(testObject.Count > 0)
            result.Add(testObject.PopMin());

        // then
        Assert.That(result, Is.EquivalentTo(numbers));
        Assert.That(result, Is.Ordered.Ascending.Using(testObject.Comparer));
    }

    [Test]
    public void PopMax_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => new DoubleHeap<int>().PopMax();

        // then
        Assert.That(action, Throws.InvalidOperationException);
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
        Assert.That(result, Is.EqualTo(element));
        Assert.That(testObject, Is.Empty);
    }

    [Test]
    public void PopMax_WhenMultipleElements_ThenMaximalElementRemoved()
    {
        // when
        int result = testObject.PopMax();

        // then
        Assert.That(result, Is.EqualTo(maximum));
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length - 1));
    }

    [Test]
    public void PopMax_WhenMultipleCalls_ThenSortedDescendingToComparer()
    {
        // when
        var result = new List<int>();

        while(testObject.Count > 0)
            result.Add(testObject.PopMax());

        // then
        Assert.That(result, Is.EquivalentTo(numbers));
        Assert.That(result, Is.Ordered.Descending.Using(testObject.Comparer));
    }

    #endregion
    #region TryPop*

    [Test]
    public void TryPop_WhenNotEmpty_ThenMinimalElementRemoved()
    {
        // when
        bool result = testObject.TryPop(out int resultValue);

        // then
        Assert.That(result, Is.True);
        Assert.That(resultValue, Is.EqualTo(minimum));
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length - 1));
    }

    [Test]
    public void TryPopMin_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new DoubleHeap<int>().TryPopMin(out int resultValue);

        // then
        Assert.That(result, Is.False);
        Assert.That(resultValue, Is.Default);
    }

    [Test]
    public void TryPopMin_WhenNotEmpty_ThenMinimalElementRemoved()
    {
        // when
        bool result = testObject.TryPopMin(out int resultValue);

        // then
        Assert.That(result, Is.True);
        Assert.That(resultValue, Is.EqualTo(minimum));
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length - 1));
    }

    [Test]
    public void TryPopMax_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new DoubleHeap<int>().TryPopMax(out int resultValue);

        // then
        Assert.That(result, Is.False);
        Assert.That(resultValue, Is.Default);
    }

    [Test]
    public void TryPopMax_WhenNotEmpty_ThenMaximalElementRemoved()
    {
        // when
        bool result = testObject.TryPopMax(out int resultValue);

        // then
        Assert.That(result, Is.True);
        Assert.That(resultValue, Is.EqualTo(maximum));
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length - 1));
    }

    #endregion
}
