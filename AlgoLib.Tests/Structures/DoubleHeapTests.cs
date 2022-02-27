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
        private readonly int[] numbers =
            new[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };

        private readonly int minimum;
        private readonly int maximum;
        private DoubleHeap<int> testObject;

        public DoubleHeapTests()
        {
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
        public void Count_WhenNotEmpty_ThenNumberOfElements()
        {
            // when
            int result = testObject.Count;
            // then
            result.Should().Be(numbers.Length);
        }

        [Test]
        public void GetEnumerator_WhenEmpty_ThenNoElements()
        {
            // given
            testObject = new DoubleHeap<int>();
            // when
            IEnumerator<int> result = testObject.GetEnumerator();
            // then
            result.MoveNext().Should().BeFalse();
        }

        [Test]
        public void GetEnumerator_WhenSingleElement_ThenThisElementOnly()
        {
            // given
            int element = 17;

            testObject = new DoubleHeap<int>(new[] { element });
            // when
            IEnumerator<int> result = testObject.GetEnumerator();
            // then
            result.MoveNext().Should().BeTrue();
            result.Current.Should().Be(element);
            result.MoveNext().Should().BeFalse();
        }

        [Test]
        public void GetEnumerator_WhenMultipleElements_ThenFirstMinimumAndLastMaximum()
        {
            // when
            var result = new List<int>();
            IEnumerator<int> enumerator = testObject.GetEnumerator();

            while(enumerator.MoveNext())
                result.Add(enumerator.Current);
            // then
            result.Should().HaveElementAt(0, minimum)
                .And.HaveElementAt(result.Count - 1, maximum);
        }

        #region Get

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
        public void GetMin_WhenSingleElement_ThenThisElement()
        {
            // given
            int element = 19;

            testObject = new DoubleHeap<int>(new[] { element });
            // when
            int result = testObject.GetMin();
            // then
            result.Should().Be(element);
        }

        [Test]
        public void GetMin_WhenMultipleElements_ThenMinimalElement()
        {
            // when
            int result = testObject.GetMin();
            // then
            result.Should().Be(minimum);
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

            testObject = new DoubleHeap<int>(new[] { element });
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

        #endregion
        #region TryGet

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
        public void TryGetMin_WhenNotEmpty_ThenMinimalElement()
        {
            // when
            bool result = testObject.TryGetMin(out int resultValue);
            // then
            result.Should().BeTrue();
            resultValue.Should().Be(minimum);
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
        public void TryGetMax_WhenNotEmpty_ThenMaximalElement()
        {
            // when
            bool result = testObject.TryGetMax(out int resultValue);
            // then
            result.Should().BeTrue();
            resultValue.Should().Be(maximum);
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
            testObject.Should().HaveCount(numbers.Length + 1);
            testObject.GetMin().Should().Be(minimum);
            testObject.GetMax().Should().Be(maximum);
        }

        [Test]
        public void Push_WhenEmpty_ThenAdded()
        {
            // given
            int element = 19;

            testObject = new DoubleHeap<int>();
            // when
            testObject.Push(element);
            // then
            testObject.Should().HaveCount(1);
            testObject.GetMin().Should().Be(element);
            testObject.GetMax().Should().Be(element);
        }

        [Test]
        public void Push_WhenNewElementIsLessThanMinimum_ThenNewMinimum()
        {
            // given
            int element = minimum - 3;
            // when
            testObject.Push(element);
            // then
            testObject.Should().HaveCount(numbers.Length + 1);
            testObject.GetMin().Should().Be(element);
            testObject.GetMax().Should().Be(maximum);
        }

        [Test]
        public void Push_WhenNewElementIsGreaterThanMaximum_ThenNewMaximum()
        {
            // given
            int element = maximum + 3;
            // when
            testObject.Push(element);
            // then
            testObject.Should().HaveCount(numbers.Length + 1);
            testObject.GetMin().Should().Be(minimum);
            testObject.GetMax().Should().Be(element);
        }

        #endregion
        #region Pop

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
        public void PopMin_WhenSingleElement_ThenThisElementRemoved()
        {
            // given
            int element = 19;

            testObject = new DoubleHeap<int>(new[] { element });
            // when
            int result = testObject.PopMin();
            // then
            result.Should().Be(element);
            testObject.Should().BeEmpty();
        }

        [Test]
        public void PopMin_WhenMultipleElements_ThenMinimalElementRemoved()
        {
            // when
            int result = testObject.PopMin();
            // then
            result.Should().Be(minimum);
            testObject.Should().HaveCount(numbers.Length - 1);
        }

        [Test]
        public void PopMin_WhenMultipleCalls_ThenSortedAsComparer()
        {
            // given
            testObject = new DoubleHeap<int>((n, m) => m.CompareTo(n));
            Array.ForEach(numbers, n => testObject.Push(n));
            // when
            var result = new List<int>();

            while(testObject.Count > 0)
                result.Add(testObject.PopMin());
            // then
            result.Should().BeEquivalentTo(numbers.ToList());
            result.Should().BeInAscendingOrder(testObject.Comparer);
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

            testObject = new DoubleHeap<int>(new[] { element });
            // when
            int result = testObject.PopMax();
            // then
            result.Should().Be(element);
            testObject.Should().BeEmpty();
        }

        [Test]
        public void PopMax_WhenMultipleElements_ThenMaximalElementRemoved()
        {
            // when
            int result = testObject.PopMax();
            // then
            result.Should().Be(maximum);
            testObject.Should().HaveCount(numbers.Length - 1);
        }

        [Test]
        public void PopMax_WhenMultipleCalls_ThenSortedAsComparer()
        {
            // given
            testObject = new DoubleHeap<int>((n, m) => m.CompareTo(n));
            Array.ForEach(numbers, n => testObject.Push(n));
            // when
            var result = new List<int>();

            while(testObject.Count > 0)
                result.Add(testObject.PopMax());
            // then
            result.Should().BeEquivalentTo(numbers.ToList());
            result.Should().BeInDescendingOrder(testObject.Comparer);
        }

        #endregion
        #region TryPop

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
        public void TryPopMin_WhenNotEmpty_ThenMinimalElementRemoved()
        {
            // when
            bool result = testObject.TryPopMin(out int resultValue);
            // then
            result.Should().BeTrue();
            resultValue.Should().Be(minimum);
            testObject.Should().HaveCount(numbers.Length - 1);
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
        public void TryPopMax_WhenNotEmpty_ThenMaximalElementRemoved()
        {
            // when
            bool result = testObject.TryPopMax(out int resultValue);
            // then
            result.Should().BeTrue();
            resultValue.Should().Be(maximum);
            testObject.Should().HaveCount(numbers.Length - 1);
        }

        #endregion
    }
}
