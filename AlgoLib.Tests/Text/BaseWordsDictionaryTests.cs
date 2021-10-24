// Tests: Structure of base words dictionary using Karp-Miller-Rosenberg algorithm
using System;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Text
{
    [TestFixture]
    public class BaseWordsDictionaryTests
    {
        private BaseWordsDictionary testObject;

        [SetUp]
        public void SetUp() => testObject = new BaseWordsDictionary("mississippi");

        [Test]
        public void Indexer_WhenEmptyRange_ThenZeroAndZero()
        {
            // when
            (int, int) result = testObject[4..4];
            // then
            result.Should().Be((0, 0));
        }

        [Test]
        public void Indexer_WhenInvalidStartIndexGreaterThanEndIndex_ThenZeroAndZero()
        {
            // when
            (int, int) result = testObject[6..2];
            // then
            result.Should().Be((0, 0));
        }

        [Test]
        public void Indexer_WhenSingleCharacter_ThenIndexerAndZero()
        {
            // when
            (int, int) result1 = testObject[1..2];  // i
            (int, int) result2 = testObject[0..1];  // m
            (int, int) result3 = testObject[8..9];  // p
            (int, int) result4 = testObject[3..4];  // s
            // then
            result1.Should().Be((1, 0));
            result2.Should().Be((2, 0));
            result3.Should().Be((3, 0));
            result4.Should().Be((4, 0));
        }

        [Test]
        public void Indexer_WhenBaseWord_ThenIndexerAndZero()
        {
            // when
            (int, int) result1 = testObject[..1];  // m
            (int, int) result2 = testObject[4..6];  // is
            (int, int) result3 = testObject[8..10];  // pp
            (int, int) result4 = testObject[7..];  // ippi
            (int, int) result5 = testObject[3..7];  // siss
            // then
            result1.Should().Be((2, 0));
            result2.Should().Be((6, 0));
            result3.Should().Be((9, 0));
            result4.Should().Be((12, 0));
            result5.Should().Be((16, 0));
        }

        [Test]
        public void Indexer_WhenComposedWord_ThenIndexerAndIndexer()
        {
            // when
            (int, int) result1 = testObject[..3];  // mis
            // then
            result1.Should().Be((7, 6));
        }

        [Test]
        public void Indexer_WhenInvalidStartIndex_ThenIndexOutOfRangeException()
        {
            // when
            Action action = () => _ = testObject[15..17];
            // then
            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void Indexer_WhenInvalidEndIndex_ThenIndexOutOfRangeException()
        {
            // when
            Action action = () => _ = testObject[5..15];
            // then
            action.Should().Throw<IndexOutOfRangeException>();
        }
    }
}
