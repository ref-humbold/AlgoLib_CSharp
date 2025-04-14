using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Structures;

// Tests: Structure of disjoint sets (union-find).
[TestFixture]
public class DisjointSetsTests
{
    private readonly int[] numbers = new[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
    private readonly int[] absent = new[] { 111, 140, 187, 253 };
    private readonly int[] present;

    private DisjointSets<int> testObject;

    public DisjointSetsTests() => present = numbers.Where((_, i) => i % 3 == 2).ToArray();

    [SetUp]
    public void SetUp() => testObject = new DisjointSets<int>(numbers.Select(n => new int[] { n }));

    [Test]
    public void Constructor_WhenDuplicatesInDifferentSets_ThenArgumentException()
    {
        // when
        Action action = () => _ = new DisjointSets<int>(
            new int[][] { new int[] { 1, 2, 3 }, new int[] { 1, 11, 21, 31 } });

        // then
        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Constructor_WhenDuplicatesInSameSet_ThenConstructed()
    {
        // given
        int[][] sets = new int[][] { new int[] { 1, 2, 3 }, new int[] { 10, 100, 10 } };

        // when
        testObject = new DisjointSets<int>(sets);

        // then
        testObject.Count.Should().Be(sets.Length);
    }

    [Test]
    public void Count_WhenEmpty_ThenZero()
    {
        // when
        int result = new DisjointSets<int>().Count;

        // then
        result.Should().Be(0);
    }

    [Test]
    public void Count_WhenElements_ThenNumberOfSets()
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

    #region Contains

    [Test]
    public void Contains_WhenEmpty_ThenFalse()
    {
        // when
        bool result = new DisjointSets<int>().Contains(numbers[0]);

        // then
        result.Should().BeFalse();
    }

    [Test]
    public void Contains_WhenPresentElement_ThenTrue()
    {
        // when
        bool result = testObject.Contains(present[0]);

        // then
        result.Should().BeTrue();
    }

    [Test]
    public void Contains_WhenAbsentElement_ThenFalse()
    {
        // when
        bool result = testObject.Contains(absent[0]);

        // then
        result.Should().BeFalse();
    }

    #endregion
    #region Add

    [Test]
    public void Add_WhenEmpty_ThenNewSet()
    {
        // given
        testObject = new DisjointSets<int>();

        // when
        DisjointSets<int> result = testObject.Add(numbers);

        // then
        result.Should().BeSameAs(testObject);

        foreach(int element in numbers)
        {
            testObject.Contains(element).Should().BeTrue();
            testObject[element].Should().Be(numbers[0]);
        }

        testObject.Count.Should().Be(1);
    }

    [Test]
    public void Add_WhenEmptyNewElements_ThenNoChanges()
    {
        // when
        DisjointSets<int> result = testObject.Add(Array.Empty<int>());

        // then
        result.Should().BeSameAs(testObject);
        testObject.Count.Should().Be(numbers.Length);
    }

    [Test]
    public void Add_WhenNewElements_ThenNewSet()
    {
        // when
        DisjointSets<int> result = testObject.Add(absent);

        // then
        result.Should().BeSameAs(testObject);

        foreach(int element in absent)
        {
            testObject.Contains(element).Should().BeTrue();
            testObject[element].Should().Be(absent[0]);
        }

        testObject.Count.Should().Be(numbers.Length + 1);
    }

    [Test]
    public void Add_WhenPresentElements_ThenArgumentException()
    {
        // when
        Action action = () => testObject.Add(present);

        // then
        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Add_WhenNewAndPresentElements_ThenArgumentException()
    {
        // when
        Action action = () => testObject.Add(absent.Concat(present));

        // then
        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Add_WhenEmptyNewElementsToPresentRepresent_ThenNoChanges()
    {
        // when
        DisjointSets<int> result = testObject.Add(Array.Empty<int>(), present[0]);

        // then
        result.Should().BeSameAs(testObject);
        testObject.Count.Should().Be(numbers.Length);
    }

    [Test]
    public void Add_WhenNewElementsToPresentRepresent_ThenAddedToExistingSet()
    {
        // given
        int represent = present[0];

        // when
        DisjointSets<int> result = testObject.Add(absent, represent);

        // then
        result.Should().BeSameAs(testObject);

        foreach(int element in absent)
        {
            testObject.Contains(element).Should().BeTrue();
            testObject[element].Should().Be(testObject[represent]);
        }

        testObject.Count.Should().Be(numbers.Length);
    }

    [Test]
    public void Add_WhenNewElementsToAbsentRepresent_ThenKeyNotFoundException()
    {
        // when
        Action action = () => testObject.Add(absent, absent[0]);

        // then
        action.Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Add_WhenPresentElementsToAbsentRepresent_ThenArgumentException()
    {
        // when
        Action action = () => testObject.Add(present, absent[0]);

        // then
        action.Should().Throw<ArgumentException>();
    }

    #endregion
    #region Indexer & TryFindSet

    [Test]
    public void Indexer_WhenEmpty_ThenKeyNotFoundException()
    {
        // when
        Action action = () => _ = new DisjointSets<int>()[numbers[0]];

        // then
        action.Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Indexer_WhenPresentElement_ThenRepresent()
    {
        // given
        int element = present[0];

        // when
        int result = testObject[element];

        // then
        result.Should().Be(element);
    }

    [Test]
    public void Indexer_WhenAbsentElement_ThenKeyNotFoundException()
    {
        // when
        Action action = () => _ = testObject[absent[0]];

        // then
        action.Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void TryFindSet_WhenPresentElement_ThenRepresent()
    {
        // given
        int element = present[0];

        // when
        bool result = testObject.TryFindSet(element, out int resultValue);

        // then
        result.Should().BeTrue();
        resultValue.Should().Be(element);
    }

    [Test]
    public void TryFindSet_WhenAbsentElement_ThenDefaultValue()
    {
        // when
        bool result = testObject.TryFindSet(absent[0], out int resultValue);

        // then
        result.Should().BeFalse();
        resultValue.Should().Be(default);
    }

    #endregion
    #region UnionSet

    [Test]
    public void UnionSet_WhenDifferentSets_ThenSameRepresent()
    {
        // given
        int element1 = present[0];
        int element2 = present[1];

        // when
        DisjointSets<int> result = testObject.UnionSet(element1, element2);

        // then
        result.Should().BeSameAs(testObject);
        testObject.IsSameSet(element1, element2).Should().BeTrue();
        testObject[element2].Should().Be(testObject[element1]);
        testObject.Count.Should().Be(numbers.Length - 1);
    }

    [Test]
    public void UnionSet_WhenSingleElement_ThenNoChanges()
    {
        // given
        int element = present[0];

        // when
        DisjointSets<int> result = testObject.UnionSet(element, element);

        // then
        result.Should().BeSameAs(testObject);
        testObject.Count.Should().Be(numbers.Length);
    }

    [Test]
    public void UnionSet_WhenSameSet_ThenNoChanges()
    {
        // given
        int element1 = numbers[1];
        int element2 = numbers[2];

        testObject = new DisjointSets<int>(new[] { absent, numbers });

        // when
        DisjointSets<int> result = testObject.UnionSet(element1, element2);

        // then
        result.Should().BeSameAs(testObject);
        testObject.IsSameSet(element1, element2).Should().BeTrue();
        testObject.Count.Should().Be(2);
    }

    [Test]
    public void UnionSet_WhenDifferentSetsInChain_ThenSameRepresent()
    {
        // given
        int first = present[0];
        int last = present[^1];

        // when
        for(int i = 1; i < present.Length; ++i)
            testObject.UnionSet(present[i - 1], present[i]);

        // then
        testObject.IsSameSet(first, last).Should().BeTrue();
        testObject[last].Should().Be(testObject[first]);
        testObject.Count.Should().Be(numbers.Length - present.Length + 1);
    }

    #endregion
    #region IsSameSet

    [Test]
    public void IsSameSet_WhenDifferentSets_ThenFalse()
    {
        // when
        bool result = testObject.IsSameSet(present[0], present[1]);

        // then
        result.Should().BeFalse();
    }

    [Test]
    public void IsSameSet_WhenSingleElement_ThenTrue()
    {
        // given
        int element = present[0];

        // when
        bool result = testObject.IsSameSet(element, element);

        // then
        result.Should().BeTrue();
    }

    [Test]
    public void IsSameSet_WhenSameSet_ThenTrue()
    {
        // given
        int element1 = present[0];
        int element2 = present[1];

        testObject.UnionSet(element1, element2);

        // when
        bool result = testObject.IsSameSet(element2, element1);

        // then
        result.Should().BeTrue();
    }

    #endregion
}
