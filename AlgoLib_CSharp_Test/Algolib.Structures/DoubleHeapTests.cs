using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Structures
{
    [TestFixture]
    public class DoubleHeapTests
    {
        private readonly List<int> numbers;
        private readonly int minimum;
        private readonly int maximum;
        private DoubleHeap<int> testObject;

        public DoubleHeapTests()
        {
            numbers =
               new List<int>() { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
            minimum = numbers.Min();
            maximum = numbers.Max();
        }

        [SetUp]
        public void SetUp()
        {
            testObject = new DoubleHeap<int>(numbers);
        }

        [TearDown]
        public void TearDown()
        {
            testObject = null;
        }

        [Test]
        public void Count_WhenEmpty_ThenZero()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            int result = testObject.Count;
            // then
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Count_WhenContainsElements_ThenNumberOfElements()
        {
            // when
            int result = testObject.Count;
            // then

            Assert.AreEqual(numbers.Count, result);
        }

        [Test]
        public void GetEnumerator_WhenContainsElements_ThenFirstMinimumAndLastMaximum()
        {
            // when
            List<int> result = new List<int>();
            IEnumerator<int> enumerator = testObject.GetEnumerator();

            while(enumerator.MoveNext())
                result.Add(enumerator.Current);
            // then
            Assert.AreEqual(minimum, result[0]);
            Assert.AreEqual(maximum, result[^1]);
        }

        [Test]
        public void GetEnumerator_WhenEmpty_ThenNoElements()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            List<int> result = new List<int>();
            IEnumerator<int> enumerator = testObject.GetEnumerator();

            while(enumerator.MoveNext())
                result.Add(enumerator.Current);
            // then
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void GetMin_WhenEmpty_ThenInvalidOperationException()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            TestDelegate testDelegate = () => testObject.GetMin();
            // then
            Assert.Throws<InvalidOperationException>(testDelegate);
        }

        [Test]
        public void GetMin_WhenContainsElements_ThenMinimalElement()
        {
            // when
            int result = testObject.GetMin();
            // then
            Assert.AreEqual(minimum, result);
        }

        [Test]
        public void TryGetMin_WhenEmpty_ThenDefaultValue()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            bool result = testObject.TryGetMin(out int resultValue);
            // then
            Assert.IsFalse(result);
            Assert.AreEqual(default(int), resultValue);
        }

        [Test]
        public void TryGetMin_WhenContainsElements_ThenMinimalElement()
        {
            // when
            bool result = testObject.TryGetMin(out int resultValue);
            // then
            Assert.IsTrue(result);
            Assert.AreEqual(minimum, resultValue);
        }

        [Test]
        public void GetMax_WhenEmpty_ThenInvalidOperationException()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            TestDelegate testDelegate = () => testObject.GetMax();
            // then
            Assert.Throws<InvalidOperationException>(testDelegate);
        }

        [Test]
        public void GetMax_WhenSingleElement_ThenThisElement()
        {
            // given
            int element = 19;
            testObject = new DoubleHeap<int>(new List<int>() { element });
            // when
            int result = testObject.GetMax();
            // then
            Assert.AreEqual(element, result);
        }

        [Test]
        public void GetMax_WhenMultipleElements_ThenMaximalElement()
        {
            // when
            int result = testObject.GetMax();
            // then
            Assert.AreEqual(maximum, result);
        }

        [Test]
        public void TryGetMax_WhenEmpty_ThenDefaultValue()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            bool result = testObject.TryGetMax(out int resultValue);
            // then
            Assert.IsFalse(result);
            Assert.AreEqual(default(int), resultValue);
        }

        [Test]
        public void TryGetMax_WhenContainsElements_ThenMaximalElement()
        {
            // when
            bool result = testObject.TryGetMax(out int resultValue);
            // then
            Assert.IsTrue(result);
            Assert.AreEqual(maximum, resultValue);
        }

        [Test]
        public void Push_WhenNewElement_ThenAdded()
        {
            // given
            int element = 46;
            // when
            testObject.Push(element);
            // then
            Assert.AreEqual(numbers.Count + 1, testObject.Count);
            Assert.AreEqual(minimum, testObject.GetMin());
            Assert.AreEqual(maximum, testObject.GetMax());
        }

        [Test]
        public void PopMin_WhenEmpty_ThenInvalidOperationException()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            TestDelegate testDelegate = () => testObject.PopMin();
            // then
            Assert.Throws<InvalidOperationException>(testDelegate);
        }

        [Test]
        public void PopMin_WhenContainsElements_ThenMinimalElementRemoved()
        {
            // when
            int result = testObject.PopMin();
            // then
            Assert.AreEqual(numbers.Count - 1, testObject.Count);
            Assert.AreEqual(minimum, result);
        }

        [Test]
        public void TryPopMin_WhenEmpty_ThenDefaultValue()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            bool result = testObject.TryPopMin(out int resultValue);
            // then
            Assert.IsFalse(result);
            Assert.AreEqual(default(int), resultValue);
        }

        [Test]
        public void TryPopMin_WhenContainsElements_ThenMinimalElementRemoved()
        {
            // when
            bool result = testObject.TryPopMin(out int resultValue);
            // then
            Assert.IsTrue(result);
            Assert.AreEqual(numbers.Count - 1, testObject.Count);
            Assert.AreEqual(minimum, resultValue);
        }

        [Test]
        public void PopMax_WhenEmpty_ThenInvalidOperationException()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            TestDelegate testDelegate = () => testObject.PopMax();
            // then
            Assert.Throws<InvalidOperationException>(testDelegate);
        }

        [Test]
        public void PopMax_WhenSingleElement_ThenThisElementRemoved()
        {
            // given
            int element = 19;
            testObject = new DoubleHeap<int>(new List<int>() { element });
            // when
            int result = testObject.PopMax();
            // then
            CollectionAssert.IsEmpty(testObject);
            Assert.AreEqual(element, result);
        }

        [Test]
        public void PopMax_WhenMultipleElements_ThenMaximalElementRemoved()
        {
            // when
            int result = testObject.PopMax();
            // then
            Assert.AreEqual(numbers.Count - 1, testObject.Count);
            Assert.AreEqual(maximum, result);
        }

        [Test]
        public void TryPopMax_WhenEmpty_ThenDefaultValue()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            bool result = testObject.TryPopMax(out int resultValue);
            // then
            Assert.IsFalse(result);
            Assert.AreEqual(default(int), resultValue);
        }

        [Test]
        public void TryPopMax_WhenContainsElements_ThenMaximalElementRemoved()
        {
            // when
            bool result = testObject.TryPopMax(out int resultValue);
            // then
            Assert.IsTrue(result);
            Assert.AreEqual(numbers.Count - 1, testObject.Count);
            Assert.AreEqual(maximum, resultValue);
        }
    }
}
