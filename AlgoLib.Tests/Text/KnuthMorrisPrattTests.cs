// Tests: Knuth-Morris-Pratt algorithm for pattern searching
using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Text
{
    [TestFixture]
    public class KnuthMorrisPrattTests
    {
        [Test]
        public void Search_WhenPatternFoundOnce_ThenSingleOccurrence()
        {
            // when
            IEnumerable<int> result = "abcde".Search("a");
            // then
            result.Should().ContainInOrder(0);
        }

        [Test]
        public void Search_WhenPatternFoundTwice_ThenTwoOccurrences()
        {
            // when
            IEnumerable<int> result = "abcdae".Search("a");
            // then
            result.Should().ContainInOrder(0, 4);
        }

        [Test]
        public void Search_WhenPatternFoundTwiceAndIntersects_ThenTwoOccurrences()
        {
            // when
            IEnumerable<int> result = "aaabcde".Search("aa");
            // then
            result.Should().ContainInOrder(0, 1);
        }

        [Test]
        public void Search_WhenPatternNotFound_ThenEmpty()
        {
            // when
            IEnumerable<int> result = "abcde".Search("x");
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Search_WhenPatternIsEmptystring_ThenEmpty()
        {
            // when
            IEnumerable<int> result = "abcde".Search("");
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Search_WhenTextIsEmptystring_ThenEmpty()
        {
            // when
            IEnumerable<int> result = "".Search("a");
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Search_WhenTextIsNull_ThenArgumentNullException()
        {
            // when
            Action action = () => KnuthMorrisPratt.Search(null, "a");
            // then
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Search_WhenPatternIsNull_ThenArgumentNullException()
        {
            // when
            Action action = () => "abcde".Search(null);
            // then
            action.Should().Throw<ArgumentNullException>();
        }
    }
}
