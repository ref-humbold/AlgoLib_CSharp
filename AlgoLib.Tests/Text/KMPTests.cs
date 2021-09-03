// Tests: Knuth-Morris-Pratt algorithm
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Text
{
    [TestFixture()]
    public class KMPTests
    {
        [Test]
        public void Kmp_WhenPatternFoundOnce_ThenSingleOccurrences()
        {
            // when
            List<int> result = "abcde".Kmp("a").ToList();
            // then
            result.Should().ContainInOrder(0);
        }

        [Test]
        public void Kmp_WhenPatternFoundTwice_ThenBothOccurrences()
        {
            // when
            List<int> result = "abcdae".Kmp("a").ToList();
            // then
            result.Should().ContainInOrder(0, 4);
        }

        [Test]
        public void Kmp_WhenPatternFoundTwiceAndIntersects_ThenBothOccurrences()
        {
            // when
            List<int> result = "aaabcde".Kmp("aa").ToList();
            // then
            result.Should().ContainInOrder(0, 1);
        }

        [Test]
        public void Kmp_WhenPatternNotFound_ThenEmpty()
        {
            // when
            List<int> result = "abcde".Kmp("x").ToList();
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Kmp_WhenPatternIsEmptystring_ThenEmpty()
        {
            // when
            List<int> result = "abcde".Kmp("").ToList();
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Kmp_WhenPatternIsNull_ThenArgumentNullException()
        {
            // when
            Action action = () => "abcde".Kmp(null);
            // then
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Kmp_WhenTextIsEmptystring_ThenEmpty()
        {
            // when
            List<int> result = "".Kmp("a").ToList();
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
    }
}
