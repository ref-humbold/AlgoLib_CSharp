using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Structures;

// Tests: Structure of AVL tree.
[TestFixture]
public class AvlTreeTests
{
    private readonly int[] numbers = new[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
    private readonly int[] absent = new[] { 111, 140, 187, 253 };
    private readonly int[] present;

    private AvlTree<int> testObject;

    public AvlTreeTests() => present = numbers.Where((_, i) => i % 3 == 2).ToArray();

    [SetUp]
    public void SetUp() => testObject = new AvlTree<int>(numbers);

    [Test]
    public void Count_WhenEmpty_ThenZero()
    {
        // when
        int result = new AvlTree<int>().Count;
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
    public void ToString_WhenNotEmpty_ThenTextRepresentation()
    {
        // when
        string result = testObject.ToString();
        // then
        result.Should().Be("{|2, 6, 10, 14, 18, 24, 26, 30, 37, 45, 51, 68, 71, 97|}");
    }

    [Test]
    public void Clear_WhenNotEmpty_ThenEmpty()
    {
        // when
        testObject.Clear();
        // then
        testObject.Should().BeEmpty();
    }

    #region GetEnumerator

    [Test]
    public void GetEnumerator_WhenEmpty_ThenNoElements()
    {
        // when
        IEnumerator<int> result = new AvlTree<int>().GetEnumerator();
        // then
        result.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_WhenSingleElement_ThenThisElementOnly()
    {
        // given
        int element = numbers[0];
        // when
        IEnumerator<int> result = new AvlTree<int>(new[] { element }).GetEnumerator();
        // then
        result.MoveNext().Should().BeTrue();
        result.Current.Should().Be(element);
        result.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_WhenMultipleElements_ThenSortedElements()
    {
        // given
        var result = testObject.ToList();
        // then
        result.Should().BeInAscendingOrder();
        result.Should().BeEquivalentTo(numbers);
    }

    #endregion
    #region CopyTo

    [Test]
    public void CopyTo_WhenNotEmpty_ThenAllElementsCopied()
    {
        // given
        int offset = 5;
        int arrayItem = 0;
        int[] array = Enumerable.Repeat(arrayItem, numbers.Length + 2 * offset).ToArray();
        // when
        testObject.CopyTo(array, offset);
        // then
        array.Take(offset).Should().OnlyContain(e => e == arrayItem);
        array.Skip(offset).Take(numbers.Length).Should().BeInAscendingOrder();
        array.Skip(offset).Take(numbers.Length).Should().BeEquivalentTo(numbers);
        array.Skip(offset + numbers.Length).Should().OnlyContain(e => e == arrayItem);
    }

    [Test]
    public void CopyTo_WhenNotEnoughtSpace_ThenArgumentException()
    {
        // given
        int[] array = Enumerable.Repeat(0, numbers.Length / 2).ToArray();
        // when
        Action action = () => testObject.CopyTo(array, 0);
        // then
        action.Should().Throw<ArgumentException>();
    }

    #endregion
    #region Contains

    [Test]
    public void Contains_WhenEmpty_ThenFalse()
    {
        // when
        bool result = new AvlTree<int>().Contains(numbers[0]);
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void Contains_WhenPresentElement_ThenTrue()
    {
        foreach(int i in present)
        {
            // when
            bool result = testObject.Contains(i);
            // then
            result.Should().BeTrue();
        }
    }

    [Test]
    public void Contains_WhenAbsentElement_ThenFalse()
    {
        foreach(int i in absent)
        {
            // when
            bool result = testObject.Contains(i);
            // then
            result.Should().BeFalse();
        }
    }

    #endregion
    #region Add

    [Test]
    public void Add_WhenEmpty_ThenTrue()
    {
        // given
        int element = numbers[0];

        testObject = new AvlTree<int>();
        // when
        bool result = testObject.Add(element);
        // then
        result.Should().BeTrue();
        testObject.Should().Contain(element);
        testObject.Should().HaveCount(1);
    }

    [Test]
    public void Add_WhenNewElement_ThenTrue()
    {
        foreach(int i in absent)
        {
            // when
            bool result = testObject.Add(i);
            // then
            result.Should().BeTrue();
            testObject.Should().Contain(i);
        }

        testObject.Should().HaveCount(numbers.Length + absent.Length);
    }

    [Test]
    public void Add_WhenPresentElement_ThenFalse()
    {
        foreach(int i in present)
        {
            // when
            bool result = testObject.Add(i);
            // then
            result.Should().BeFalse();
            testObject.Should().Contain(i);
        }

        testObject.Should().HaveSameCount(numbers);
    }

    #endregion
    #region Remove

    [Test]
    public void Remove_WhenEmpty_ThenFalse()
    {
        // when
        bool result = new AvlTree<int>().Remove(numbers[0]);
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void Remove_WhenPresentElement_ThenTrue()
    {
        foreach(int i in present)
        {
            // when
            bool result = testObject.Remove(i);
            // then
            result.Should().BeTrue();
            testObject.Should().NotContain(i);
        }

        testObject.Should().HaveCount(numbers.Length - present.Length);
    }

    [Test]
    public void Remove_WhenAbsentElement_ThenFalse()
    {
        foreach(int i in absent)
        {
            // when
            bool result = testObject.Remove(i);
            // then
            result.Should().BeFalse();
            testObject.Should().NotContain(i);
        }

        testObject.Should().HaveSameCount(numbers);
    }

    [Test]
    public void Remove_WhenRootGreaterThanElement_ThenRemoved()
    {
        // given
        int root = absent[1];
        int element = absent[0];

        testObject = new AvlTree<int>(new[] { root, element });
        // when
        bool result = testObject.Remove(root);
        // then
        result.Should().BeTrue();
        testObject.Should().NotContain(root);
        testObject.Should().Contain(element);
        testObject.Should().HaveCount(1);
    }

    [Test]
    public void Remove_WhenRootLessThanElement_ThenRemoved()
    {
        // given
        int root = absent[0];
        int element = absent[1];

        testObject = new AvlTree<int>(new[] { root, element });
        // when
        bool result = testObject.Remove(root);
        // then
        result.Should().BeTrue();
        testObject.Should().NotContain(root);
        testObject.Should().Contain(element);
        testObject.Should().HaveCount(1);
    }

    [Test]
    public void Remove_WhenRootOnly_ThenEmpty()
    {
        // given
        int root = absent[0];

        testObject = new AvlTree<int>(new[] { root });
        // when
        bool result = testObject.Remove(root);
        // then
        result.Should().BeTrue();
        testObject.Should().NotContain(root);
        testObject.Should().BeEmpty();
    }

    #endregion

    [Test]
    public void ExceptWith_WhenPresentElements_ThenRemoved()
    {
        // when
        testObject.ExceptWith(present);
        // then
        foreach(int i in numbers.Except(present))
            testObject.Should().Contain(i);

        foreach(int i in present)
            testObject.Should().NotContain(i);
    }

    [Test]
    public void ExceptWith_WhenAbsentElements_ThenRemoved()
    {
        // when
        testObject.ExceptWith(absent);
        // then
        foreach(int i in numbers)
            testObject.Should().Contain(i);

        foreach(int i in absent)
            testObject.Should().NotContain(i);
    }

    [Test]
    public void IntersectWith_WhenPresentElements_ThenCommonElements()
    {
        // when
        testObject.IntersectWith(present);
        // then
        testObject.Should().HaveSameCount(present);

        foreach(int i in present)
            testObject.Should().Contain(i);
    }

    [Test]
    public void IntersectWith_WhenAbsentElements_ThenEmpty()
    {
        // when
        testObject.IntersectWith(absent);
        // then
        testObject.Should().BeEmpty();
    }

    [Test]
    public void IsProperSubsetOf_WhenPartialSet_ThenFalse()
    {
        // when
        bool result = testObject.IsProperSubsetOf(present);
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void IsProperSubsetOf_WhenWholeSet_ThenFalse()
    {
        // when
        bool result = testObject.IsProperSubsetOf(numbers);
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void IsProperSubsetOf_WhenExtendedSet_ThenTrue()
    {
        // when
        bool result = testObject.IsProperSubsetOf(numbers.Union(absent));
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void IsProperSupersetOf_WhenPartialSet_ThenTrue()
    {
        // when
        bool result = testObject.IsProperSupersetOf(present);
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void IsProperSupersetOf_WhenWholeSet_ThenFalse()
    {
        // when
        bool result = testObject.IsProperSupersetOf(numbers);
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void IsProperSupersetOf_WhenExtendedSet_ThenFalse()
    {
        // when
        bool result = testObject.IsProperSupersetOf(numbers.Union(absent));
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void IsSubsetOf_WhenPartialSet_ThenFalse()
    {
        // when
        bool result = testObject.IsSubsetOf(present);
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void IsSubsetOf_WhenWholeSet_ThenTrue()
    {
        // when
        bool result = testObject.IsSubsetOf(numbers);
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void IsSubsetOf_WhenExtendedSet_ThenTrue()
    {
        // when
        bool result = testObject.IsSubsetOf(numbers.Union(absent));
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void IsSupersetOf_WhenPartialSet_ThenTrue()
    {
        // when
        bool result = testObject.IsSupersetOf(present);
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void IsSupersetOf_WhenWholeSet_ThenTrue()
    {
        // when
        bool result = testObject.IsSupersetOf(numbers);
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void IsSupersetOf_WhenExtendedSet_ThenFalse()
    {
        // when
        bool result = testObject.IsSupersetOf(numbers.Union(absent));
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void Overlaps_WhenPresentElements_ThenTrue()
    {
        // when
        bool result = testObject.Overlaps(present);
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void Overlaps_WhenAbsentElements_ThenFalse()
    {
        // when
        bool result = testObject.Overlaps(absent);
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void Overlaps_WhenPresentAndAbsentElements_ThenTrue()
    {
        // when
        bool result = testObject.Overlaps(absent.Union(present));
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void SetEquals_WhenSameElements_ThenTrue()
    {
        // when
        bool result = testObject.SetEquals(numbers);
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void SetEquals_WhenMissingElements_ThenFalse()
    {
        // when
        bool result = testObject.SetEquals(numbers.Take(5));
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void SetEquals_WhenAdditionalElements_ThenFalse()
    {
        // when
        bool result = testObject.SetEquals(numbers.Union(absent));
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void SymmetricExceptWith_WhenPresentElements_ThenRemoved()
    {
        // when
        testObject.SymmetricExceptWith(present);
        // then
        foreach(int i in numbers.Except(present))
            testObject.Should().Contain(i);

        foreach(int i in present)
            testObject.Should().NotContain(i);
    }

    [Test]
    public void SymmetricExceptWith_WhenPresentAndAbsentElements_ThenPresentRemovedAndAbsentAdded()
    {
        // when
        testObject.SymmetricExceptWith(present.Union(absent));
        // then
        foreach(int i in numbers.Except(present))
            testObject.Should().Contain(i);

        foreach(int i in present)
            testObject.Should().NotContain(i);

        foreach(int i in absent)
            testObject.Should().Contain(i);
    }

    [Test]
    public void SymmetricExceptWith_WhenAbsentElements_ThenAdded()
    {
        // when
        testObject.SymmetricExceptWith(absent);
        // then
        foreach(int i in numbers)
            testObject.Should().Contain(i);

        foreach(int i in absent)
            testObject.Should().Contain(i);
    }

    [Test]
    public void UnionWith_WhenPresentElements_ThenNoChanges()
    {
        // when
        testObject.UnionWith(present);
        // then
        foreach(int i in numbers)
            testObject.Should().Contain(i);
    }

    [Test]
    public void UnionWith_WhenAbsentElements_ThenElementsAdded()
    {
        // when
        testObject.UnionWith(absent);
        // then
        foreach(int i in numbers)
            testObject.Should().Contain(i);

        foreach(int i in absent)
            testObject.Should().Contain(i);
    }
}
