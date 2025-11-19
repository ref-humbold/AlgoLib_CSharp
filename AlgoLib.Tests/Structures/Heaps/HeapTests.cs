using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgoLib.Structures.Heaps;

// Tests: Structure of heap.
[TestFixture]
public class HeapTests
{
    private readonly int[] numbers = [10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26];
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
        IEnumerator<int> result = new Heap<int>().GetEnumerator();

        // then
        Assert.That(result.MoveNext(), Is.False);
    }

    [Test]
    public void GetEnumerator_WhenSingleElement_ThenThisElementOnly()
    {
        // given
        int element = numbers[0];

        // when
        IEnumerator<int> result = new Heap<int>([element]).GetEnumerator();

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
        Assert.That(result, Is.EquivalentTo(numbers));
        Assert.That(result, Has.ItemAt(0).EqualTo(minimum));
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
        Assert.That(testObject, Has.Count.EqualTo(1));
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
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length + 1));
        Assert.That(testObject.Peek(), Is.EqualTo(element));
    }

    [Test]
    public void Push_WhenNewElementGreaterThanMinimum_ThenAdded()
    {
        // when
        testObject.Push(minimum + 3);

        // then
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length + 1));
        Assert.That(testObject.Peek(), Is.EqualTo(minimum));
    }

    [Test]
    public void PushRange_WhenNewElements_ThenAllAdded()
    {
        // given
        int[] elements = [minimum - 3, minimum + 5, minimum + 13, minimum + 20];

        // when
        testObject.PushRange(elements);

        // then
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length + elements.Length));
        Assert.That(testObject.Peek(), Is.EqualTo(elements.Min()));
    }

    #endregion
    #region Peek & TryPeek

    [Test]
    public void Peek_WhenEmpty_ThenInvalidOperationException()
    {
        // when
        Action action = () => _ = new Heap<int>().Peek();

        // then
        Assert.That(action, Throws.InvalidOperationException);
    }

    [Test]
    public void Peek_WhenSingleElement_ThenThisElement()
    {
        // given
        int element = numbers[0];

        // when
        int result = new Heap<int>([element]).Peek();

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
        bool result = new Heap<int>().TryPeek(out int resultValue);

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
        Action action = () => _ = new Heap<int>().Pop();

        // then then
        Assert.That(action, Throws.InvalidOperationException);
    }

    [Test]
    public void Pop_WhenSingleElement_ThenThisElementRemoved()
    {
        // given
        int element = numbers[0];

        testObject = new Heap<int>([element]);

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
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length - 1));
    }

    [Test]
    public void Pop_WhenMultipleCalls_ThenSortedAscendingToComparer()
    {
        // when
        List<int> result = [];

        while(testObject.Count > 0)
            result.Add(testObject.Pop());

        // then
        Assert.That(result, Is.EquivalentTo(numbers));
        Assert.That(result, Is.Ordered.Ascending.Using(testObject.Comparer));
    }

    [Test]
    public void TryPop_WhenEmpty_ThenDefaultValue()
    {
        // when
        bool result = new Heap<int>().TryPop(out int resultValue);

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
        Assert.That(testObject, Has.Count.EqualTo(numbers.Length - 1));
    }

    #endregion
}
