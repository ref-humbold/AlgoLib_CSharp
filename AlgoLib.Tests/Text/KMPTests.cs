// Tests: Knuth-Morris-Pratt algorithm
using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Text
{
    [TestFixture()]
    public class KMPTests
    {
        [Test]
        public void Kmp_WhenPatternFoundOnce_ThenSingleOccurrence()
        {
            // when
            IEnumerable<int> result = "abcde".Kmp("a");
            // then
            result.Should().ContainInOrder(0);
        }

        [Test]
        public void Kmp_WhenPatternFoundTwice_ThenTwoOccurrences()
        {
            // when
            IEnumerable<int> result = "abcdae".Kmp("a");
            // then
            result.Should().ContainInOrder(0, 4);
        }

        [Test]
        public void Kmp_WhenPatternFoundTwiceAndIntersects_ThenTwoOccurrences()
        {
            // when
            IEnumerable<int> result = "aaabcde".Kmp("aa");
            // then
            result.Should().ContainInOrder(0, 1);
        }

        [Test]
        public void Kmp_WhenPatternNotFound_ThenEmpty()
        {
            // when
            IEnumerable<int> result = "abcde".Kmp("x");
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Kmp_WhenPatternIsEmptystring_ThenEmpty()
        {
            // when
            IEnumerable<int> result = "abcde".Kmp("");
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Kmp_WhenTextIsEmptystring_ThenEmpty()
        {
            // when
            IEnumerable<int> result = "".Kmp("a");
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Kmp_WhenTextIsNull_ThenArgumentNullException()
        {
            // when
            Action action = () => KMP.Kmp(null, "a");
            // then
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Kmp_WhenPatternIsNull_ThenArgumentNullException()
        {
            // when
            Action action = () => "abcde".Kmp(null);
            // then
            action.Should().Throw<ArgumentNullException>();
        }
    }
}
