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
            Tuple<int, int> result = testObject.Code(4, 4);
            // then
            Assert.AreEqual(Tuple.Create(0, 0), result);
        }

        [Test]
        public void Code_WhenInvalidStartIndexGreaterThanEndIndex_ThenZeroAndZero()
        {
            // when
            Tuple<int, int> result = testObject.Code(6, 2);
            // then
            Assert.AreEqual(Tuple.Create(0, 0), result);
        }

        [Test]
        public void Code_WhenSingleCharacter_ThenCodeAndZero()
        {
            // when
            Tuple<int, int> result1 = testObject.Code(1, 2);  // i
            Tuple<int, int> result2 = testObject.Code(0, 1);  // m
            Tuple<int, int> result3 = testObject.Code(8, 9);  // p
            Tuple<int, int> result4 = testObject.Code(3, 4);  // s
                                                              // then
            Assert.AreEqual(Tuple.Create(1, 0), result1);
            Assert.AreEqual(Tuple.Create(2, 0), result2);
            Assert.AreEqual(Tuple.Create(3, 0), result3);
            Assert.AreEqual(Tuple.Create(4, 0), result4);
        }

        [Test]
        public void Code_WhenBaseWord_ThenCodeAndZero()
        {
            // when
            Tuple<int, int> result1 = testObject.Code(0, 1);  // m
            Tuple<int, int> result2 = testObject.Code(4, 6);  // is
            Tuple<int, int> result3 = testObject.Code(8, 10);  // pp
            Tuple<int, int> result4 = testObject.Code(7);  // ippi
            Tuple<int, int> result5 = testObject.Code(3, 7);  // siss
                                                              // then
            Assert.AreEqual(Tuple.Create(2, 0), result1);
            Assert.AreEqual(Tuple.Create(6, 0), result2);
            Assert.AreEqual(Tuple.Create(9, 0), result3);
            Assert.AreEqual(Tuple.Create(12, 0), result4);
            Assert.AreEqual(Tuple.Create(16, 0), result5);
        }

        [Test]
        public void Code_WhenComposedWord_ThenCodeAndCode()
        {
            // when
            Tuple<int, int> result1 = testObject.Code(0, 3);  // mis
                                                              // then
            Assert.AreEqual(Tuple.Create(7, 6), result1);
        }

        [Test]
        public void Code_WhenInvalidStartIndex_ThenIndexOutOfRangeException()
        {
            // when
            TestDelegate testDelegate = () => testObject.Code(-1);
            // then
            Assert.Throws<IndexOutOfRangeException>(testDelegate);
        }

        [Test]
        public void Code_WhenInvalidEndIndex_ThenIndexOutOfRangeException()
        {
            // when
            TestDelegate testDelegate = () => testObject.Code(5, 15);
            // then
            Assert.Throws<IndexOutOfRangeException>(testDelegate);
        }
    }
}
