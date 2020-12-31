using NUnit.Framework;
using System;

namespace Algolib.Text
{
    // Tests: Structure of base words dictionary using Karp-Miller-Rosenberg algorithm
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
            Assert.That(result, Is.EqualTo((0, 0)));
        }

        [Test]
        public void Indexer_WhenInvalidStartIndexGreaterThanEndIndex_ThenZeroAndZero()
        {
            // when
            (int, int) result = testObject[6..2];
            // then
            Assert.That(result, Is.EqualTo((0, 0)));
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
            Assert.That(result1, Is.EqualTo((1, 0)));
            Assert.That(result2, Is.EqualTo((2, 0)));
            Assert.That(result3, Is.EqualTo((3, 0)));
            Assert.That(result4, Is.EqualTo((4, 0)));
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
            Assert.That(result1, Is.EqualTo((2, 0)));
            Assert.That(result2, Is.EqualTo((6, 0)));
            Assert.That(result3, Is.EqualTo((9, 0)));
            Assert.That(result4, Is.EqualTo((12, 0)));
            Assert.That(result5, Is.EqualTo((16, 0)));
        }

        [Test]
        public void Indexer_WhenComposedWord_ThenIndexerAndIndexer()
        {
            // when
            (int, int) result1 = testObject[..3];  // mis
            // then
            Assert.That(result1, Is.EqualTo((7, 6)));
        }

        [Test]
        public void Indexer_WhenInvalidStartIndex_ThenIndexOutOfRangeException()
        {
            // when
            TestDelegate testDelegate = () => _ = testObject[15..17];
            // then
            Assert.That(testDelegate, Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void Indexer_WhenInvalidEndIndex_ThenIndexOutOfRangeException()
        {
            // when
            TestDelegate testDelegate = () => _ = testObject[5..15];
            // then
            Assert.That(testDelegate, Throws.TypeOf<IndexOutOfRangeException>());
        }
    }
}
