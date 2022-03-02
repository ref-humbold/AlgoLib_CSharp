using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Structures
{
    [TestFixture]
    public class PairingHeapTests
    {
        private readonly int[] numbers = new[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
        private readonly int minimum;
        private PairingHeap<int> testObject;

        public PairingHeapTests() => minimum = numbers.Min();

        [SetUp]
        public void SetUp()
        {
            testObject = new PairingHeap<int>(numbers);
        }

        [Test]
        public void Count_WhenEmpty_ThenZero()
        {
            // given
            testObject = new PairingHeap<int>();
            // when
            int result = testObject.Count;
            // then
            result.Should().Be(0);
        }

        [Test]
        public void Count_WhenNotEmpty_ThenNumberOfElements()
        {
            // when
            int result = testObject.Count;
            // then
            result.Should().Be(numbers.Length);
        }

        #region Peek & TryPeek

        [Test]
        public void Peek_WhenEmpty_ThenInvalidOperationException()
        {
            // given
            testObject = new PairingHeap<int>();
            // when
            Action action = () => testObject.Peek();
            // then
            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Peek_WhenSingleElement_ThenThisElement()
        {
            // given
            int element = 19;

            testObject = new PairingHeap<int>(new[] { element });
            // when
            int result = testObject.Peek();
            // then
            result.Should().Be(element);
        }

        [Test]
        public void Peek_WhenMultipleElements_ThenMinimalElement()
        {
            // when
            int result = testObject.Peek();
            // then
            result.Should().Be(minimum);
        }

        [Test]
        public void TryPeek_WhenEmpty_ThenDefaultValue()
        {
            // given
            testObject = new PairingHeap<int>();
            // when
            bool result = testObject.TryPeek(out int resultValue);
            // then
            result.Should().BeFalse();
            resultValue.Should().Be(default);
        }

        [Test]
        public void TryPeek_WhenSingleElement_ThenThisElement()
        {
            // given
            int element = 19;

            testObject = new PairingHeap<int>(new[] { element });
            // when
            bool result = testObject.TryPeek(out int resultValue);
            // then
            result.Should().BeTrue();
            resultValue.Should().Be(element);
        }

        [Test]
        public void TryPeek_WhenMultipleElements_ThenMinimalElement()
        {
            // when
            bool result = testObject.TryPeek(out int resultValue);
            // then
            result.Should().BeTrue();
            resultValue.Should().Be(minimum);
        }

        #endregion
        #region Push

        [Test]
        public void Push_WhenNewElement_ThenAdded()
        {
            // given
            int element = 46;
            // when
            testObject.Push(element);
            // then
            testObject.Count.Should().Be(numbers.Length + 1);
            testObject.Peek().Should().Be(minimum);
        }

        [Test]
        public void Push_WhenEmpty_ThenAdded()
        {
            // given
            int element = 19;

            testObject = new PairingHeap<int>();
            // when
            testObject.Push(element);
            // then
            testObject.Count.Should().Be(1);
            testObject.Peek().Should().Be(element);
        }

        [Test]
        public void Push_WhenNewElementIsLessThanMinimum_ThenNewMinimum()
        {
            // given
            int element = minimum - 3;
            // when
            testObject.Push(element);
            // then
            testObject.Count.Should().Be(numbers.Length + 1);
            testObject.Peek().Should().Be(element);
        }

        #endregion
        #region Pop & TryPop

        [Test]
        public void Pop_WhenEmpty_ThenInvalidOperationException()
        {
            // given
            testObject = new PairingHeap<int>();
            // when
            Action action = () => testObject.Pop();
            // then
            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Pop_WhenSingleElement_ThenThisElementRemoved()
        {
            // given
            int element = 19;

            testObject = new PairingHeap<int>(new[] { element });
            // when
            int result = testObject.Pop();
            // then
            testObject.Count.Should().Be(0);
            result.Should().Be(element);
        }

        [Test]
        public void Pop_WhenMultipleElements_ThenMinimalElementRemoved()
        {
            // when
            int result = testObject.Pop();
            // then
            testObject.Count.Should().Be(numbers.Length - 1);
            result.Should().Be(minimum);
        }

        [Test]
        public void Pop_WhenMultipleCalls_ThenSorted()
        {
            // when
            var result = new List<int>();

            while(testObject.Count > 0)
                result.Add(testObject.Pop());
            // then
            result.Should().BeEquivalentTo(numbers.ToList());
            result.Should().BeInAscendingOrder();
        }

        [Test]
        public void TryPop_WhenEmpty_ThenDefaultValue()
        {
            // given
            testObject = new PairingHeap<int>();
            // when
            bool result = testObject.TryPop(out int resultValue);
            // then
            result.Should().BeFalse();
            resultValue.Should().Be(default);
        }

        [Test]
        public void TryPop_WhenSingleElement_ThenThisElementRemoved()
        {
            // given
            int element = 19;

            testObject = new PairingHeap<int>(new[] { element });
            // when
            bool result = testObject.TryPop(out int resultValue);
            // then
            testObject.Count.Should().Be(0);
            result.Should().BeTrue();
            resultValue.Should().Be(element);
        }

        [Test]
        public void TryPop_WhenMultipleElements_ThenMinimalElementRemoved()
        {
            // when
            bool result = testObject.TryPop(out int resultValue);
            // then
            testObject.Count.Should().Be(numbers.Length - 1);
            result.Should().BeTrue();
            resultValue.Should().Be(minimum);
        }

        #endregion
        #region Merge

        [Test]
        public void Merge_WhenEmptyAndNotEmpty_ThenSameAsOther()
        {
            // given
            testObject = new PairingHeap<int>();

            var other = new PairingHeap<int>(numbers);
            // when
            testObject.Merge(other);
            // then
            testObject.Count.Should().Be(numbers.Length);
            testObject.Peek().Should().Be(other.Peek());
        }

        [Test]
        public void Merge_WhenNotEmptyAndEmpty_ThenNoChanges()
        {
            // when
            testObject.Merge(new PairingHeap<int>());
            // then
            testObject.Count.Should().Be(numbers.Length);
            testObject.Peek().Should().Be(minimum);
        }

        [Test]
        public void Merge_WhenOtherHasGreaterMinimum_ThenMinimumRemains()
        {
            // given
            var other = new PairingHeap<int>(new[] { minimum + 5, minimum + 13, minimum + 20 });
            // when
            testObject.Merge(other);
            // then
            testObject.Count.Should().Be(numbers.Length + other.Count);
            testObject.Peek().Should().Be(minimum);
        }

        [Test]
        public void Merge_WhenOtherHasLessMinimum_ThenNewMinimum()
        {
            // given
            int newMinimum = minimum - 4;
            var other = new PairingHeap<int>(new[] { newMinimum, minimum + 5, minimum + 13, minimum + 20 });
            // when
            testObject.Merge(other);
            // then
            testObject.Count.Should().Be(numbers.Length + other.Count);
            testObject.Peek().Should().Be(newMinimum);
        }

        #endregion
    }
}
