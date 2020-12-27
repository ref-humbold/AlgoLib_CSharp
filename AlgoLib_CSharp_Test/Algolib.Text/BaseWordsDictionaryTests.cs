// Tests: Structure of base words dictionary using Karp-Miller-Rosenberg algorithm
using NUnit.Framework;
using System;

namespace Algolib.Text
{
    [TestFixture]
    public class BaseWordsDictionaryTests
    {
        private BaseWordsDictionary testObject;

        [SetUp]
        public void SetUp() => testObject = new BaseWordsDictionary("mississippi");

        [Test]
        public void Code_WhenEmptyRange_ThenZeroAndZero()
        {
            // when
            (int, int) result = testObject[4..4];
            // then
            Assert.AreEqual((0, 0), result);
        }

        [Test]
        public void Code_WhenInvalidStartIndexGreaterThanEndIndex_ThenZeroAndZero()
        {
            // when
            (int, int) result = testObject[6..2];
            // then
            Assert.AreEqual((0, 0), result);
        }

        [Test]
        public void Code_WhenSingleCharacter_ThenCodeAndZero()
        {
            // when
            (int, int) result1 = testObject[1..2];  // i
            (int, int) result2 = testObject[0..1];  // m
            (int, int) result3 = testObject[8..9];  // p
            (int, int) result4 = testObject[3..4];  // s
            // then
            Assert.AreEqual((1, 0), result1);
            Assert.AreEqual((2, 0), result2);
            Assert.AreEqual((3, 0), result3);
            Assert.AreEqual((4, 0), result4);
        }

        [Test]
        public void Code_WhenBaseWord_ThenCodeAndZero()
        {
            // when
            (int, int) result1 = testObject[..1];  // m
            (int, int) result2 = testObject[4..6];  // is
            (int, int) result3 = testObject[8..10];  // pp
            (int, int) result4 = testObject[7..];  // ippi
            (int, int) result5 = testObject[3..7];  // siss
            // then
            Assert.AreEqual((2, 0), result1);
            Assert.AreEqual((6, 0), result2);
            Assert.AreEqual((9, 0), result3);
            Assert.AreEqual((12, 0), result4);
            Assert.AreEqual((16, 0), result5);
        }

        [Test]
        public void Code_WhenComposedWord_ThenCodeAndCode()
        {
            // when
            (int, int) result1 = testObject[..3];  // mis
            // then
            Assert.AreEqual((7, 6), result1);
        }

        [Test]
        public void Code_WhenInvalidStartIndex_ThenIndexOutOfRangeException()
        {
            // when
            TestDelegate testDelegate = () => _ = testObject[15..17];
            // then
            Assert.Throws<IndexOutOfRangeException>(testDelegate);
        }

        [Test]
        public void Code_WhenInvalidEndIndex_ThenIndexOutOfRangeException()
        {
            // when
            TestDelegate testDelegate = () => _ = testObject[5..15];
            // then
            Assert.Throws<IndexOutOfRangeException>(testDelegate);
        }
    }
}
