using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Text;

// Tests: Knuth-Morris-Pratt algorithm for pattern searching.
[TestFixture]
public class KnuthMorrisPrattTests
{
    [Test]
    public void KmpSearch_WhenPatternFound_ThenAllOccurrences()
    {
        // when
        IEnumerable<int> result =
            "abcdecdcdefgcdcdecdcdecdcdehijcdecdcdek".KmpSearch("cdecdcde");

        // then
        result.Should().ContainInOrder(2, 14, 19, 30);
    }

    [Test]
    public void KmpSearch_WhenPatternFoundOnce_ThenSingleOccurrence()
    {
        // when
        IEnumerable<int> result = "abcde".KmpSearch("a");

        // then
        result.Should().ContainInOrder(0);
    }

    [Test]
    public void KmpSearch_WhenPatternFoundTwice_ThenTwoOccurrences()
    {
        // when
        IEnumerable<int> result = "abcdae".KmpSearch("a");

        // then
        result.Should().ContainInOrder(0, 4);
    }

    [Test]
    public void KmpSearch_WhenPatternFoundTwiceAndIntersects_ThenTwoOccurrences()
    {
        // when
        IEnumerable<int> result = "aaaabcde".KmpSearch("aaa");

        // then
        result.Should().ContainInOrder(0, 1);
    }

    [Test]
    public void KmpSearch_WhenPatternNotFound_ThenEmpty()
    {
        // when
        IEnumerable<int> result = "abcde".KmpSearch("x");

        // then
        result.Should().BeEmpty();
    }

    [Test]
    public void KmpSearch_WhenPatternIsEmptystring_ThenEmpty()
    {
        // when
        IEnumerable<int> result = "abcde".KmpSearch(string.Empty);

        // then
        result.Should().BeEmpty();
    }

    [Test]
    public void KmpSearch_WhenTextIsEmptystring_ThenEmpty()
    {
        // when
        IEnumerable<int> result = string.Empty.KmpSearch("a");

        // then
        result.Should().BeEmpty();
    }

    [Test]
    public void KmpSearch_WhenTextIsNull_ThenArgumentNullException()
    {
        // when
        Action action = () => KnuthMorrisPratt.KmpSearch(null, "a");

        // then
        action.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void KmpSearch_WhenPatternIsNull_ThenArgumentNullException()
    {
        // when
        Action action = () => "abcde".KmpSearch(null);

        // then
        action.Should().Throw<ArgumentNullException>();
    }
}
