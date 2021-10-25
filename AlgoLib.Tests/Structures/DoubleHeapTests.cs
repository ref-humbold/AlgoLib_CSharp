using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Structures
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
            numbers = new List<int>() { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
            minimum = numbers.Min();
            maximum = numbers.Max();
        }

        [SetUp]
        public void SetUp() => testObject = new DoubleHeap<int>(numbers);

        [Test]
        public void Count_WhenEmpty_ThenZero()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            int result = testObject.Count;
            // then
            result.Should().Be(0);
        }

        [Test]
        public void Count_WhenContainsElements_ThenNumberOfElements()
        {
            // when
            int result = testObject.Count;
            // then
            result.Should().Be(numbers.Count);
        }

        [Test]
        public void GetEnumerator_WhenContainsElements_ThenFirstMinimumAndLastMaximum()
        {
            // when
            var result = new List<int>();
            IEnumerator<int> enumerator = testObject.GetEnumerator();

            while(enumerator.MoveNext())
                result.Add(enumerator.Current);
            // then
            result.Should().HaveElementAt(0, minimum);
            result.Should().HaveElementAt(result.Count - 1, maximum);
        }

        [Test]
        public void GetEnumerator_WhenEmpty_ThenNoElements()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            var result = new List<int>();
            IEnumerator<int> enumerator = testObject.GetEnumerator();

            while(enumerator.MoveNext())
                result.Add(enumerator.Current);
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void GetMin_WhenEmpty_ThenInvalidOperationException()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            Action action = () => testObject.GetMin();
            // then
            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void GetMin_WhenContainsElements_ThenMinimalElement()
        {
            // when
            int result = testObject.GetMin();
            // then
            result.Should().Be(minimum);
        }

        [Test]
        public void TryGetMin_WhenEmpty_ThenDefaultValue()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            bool result = testObject.TryGetMin(out int resultValue);
            // then
            result.Should().BeFalse();
            resultValue.Should().Be(default);
        }

        [Test]
        public void TryGetMin_WhenContainsElements_ThenMinimalElement()
        {
            // when
            bool result = testObject.TryGetMin(out int resultValue);
            // then
            result.Should().BeTrue();
            resultValue.Should().Be(minimum);
        }

        [Test]
        public void GetMax_WhenEmpty_ThenInvalidOperationException()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            Action action = () => testObject.GetMax();
            // then
            action.Should().Throw<InvalidOperationException>();
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
            result.Should().Be(element);
        }

        [Test]
        public void GetMax_WhenMultipleElements_ThenMaximalElement()
        {
            // when
            int result = testObject.GetMax();
            // then
            result.Should().Be(maximum);
        }

        [Test]
        public void TryGetMax_WhenEmpty_ThenDefaultValue()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            bool result = testObject.TryGetMax(out int resultValue);
            // then
            result.Should().BeFalse();
            resultValue.Should().Be(default);
        }

        [Test]
        public void TryGetMax_WhenContainsElements_ThenMaximalElement()
        {
            // when
            bool result = testObject.TryGetMax(out int resultValue);
            // then
            result.Should().BeTrue();
            resultValue.Should().Be(maximum);
        }

        [Test]
        public void Push_WhenNewElement_ThenAdded()
        {
            // given
            int element = 46;
            // when
            testObject.Push(element);
            // then
            testObject.Should().HaveCount(numbers.Count + 1);
            testObject.GetMin().Should().Be(minimum);
            testObject.GetMax().Should().Be(maximum);
        }

        [Test]
        public void PopMin_WhenEmpty_ThenInvalidOperationException()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            Action action = () => testObject.PopMin();
            // then
            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void PopMin_WhenContainsElements_ThenMinimalElementRemoved()
        {
            // when
            int result = testObject.PopMin();
            // then
            testObject.Should().HaveCount(numbers.Count - 1);
            result.Should().Be(minimum);
        }

        [Test]
        public void TryPopMin_WhenEmpty_ThenDefaultValue()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            bool result = testObject.TryPopMin(out int resultValue);
            // then
            result.Should().BeFalse();
            resultValue.Should().Be(default);
        }

        [Test]
        public void TryPopMin_WhenContainsElements_ThenMinimalElementRemoved()
        {
            // when
            bool result = testObject.TryPopMin(out int resultValue);
            // then
            result.Should().BeTrue();
            testObject.Should().HaveCount(numbers.Count - 1);
            resultValue.Should().Be(minimum);
        }

        [Test]
        public void PopMax_WhenEmpty_ThenInvalidOperationException()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            Action action = () => testObject.PopMax();
            // then
            action.Should().Throw<InvalidOperationException>();
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
            testObject.Should().BeEmpty();
            result.Should().Be(element);
        }

        [Test]
        public void PopMax_WhenMultipleElements_ThenMaximalElementRemoved()
        {
            // when
            int result = testObject.PopMax();
            // then
            testObject.Should().HaveCount(numbers.Count - 1);
            result.Should().Be(maximum);
        }

        [Test]
        public void TryPopMax_WhenEmpty_ThenDefaultValue()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            bool result = testObject.TryPopMax(out int resultValue);
            // then
            result.Should().BeFalse();
            resultValue.Should().Be(default);
        }

        [Test]
        public void TryPopMax_WhenContainsElements_ThenMaximalElementRemoved()
        {
            // when
            bool result = testObject.TryPopMax(out int resultValue);
            // then
            result.Should().BeTrue();
            testObject.Should().HaveCount(numbers.Count - 1);
            resultValue.Should().Be(maximum);
        }
    }
}
