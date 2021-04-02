using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Structures
{
    [TestFixture]
    public class HeapTests
    {
        private readonly Comparison<int> comparison = (n, m) => m.CompareTo(n);
        private Heap<int> testObject;

        [SetUp]
        public void SetUp() => testObject = new Heap<int>(comparison);

        [Test]
        public void Push_WhenNewElement_ThenAddedToHeap()
        {
            // when
            testObject.Push(19);
            // then
            testObject.Should().HaveCount(1);
        }

        [Test]
        public void Push_Pop_WhenMultipleElements_ThenElementsAccordingToComparer()
        {
            // given
            List<int> elements = new List<int> { 11, 4, 6, 18, 13, 7 };
            // when
            elements.ForEach(e => testObject.Push(e));

            List<int> result = new List<int>();

            while(testObject.Count > 0)
                result.Add(testObject.Pop());
            // then
            elements.Sort(comparison);
            result.Should().BeInDescendingOrder();
            result.Should().Equal(elements);
        }

        [Test]
        public void Get_WhenContainsElements_ThenElementAccordingToComparer()
        {
            // given
            List<int> elements = new List<int> { 11, 4, 6, 18, 13, 7 };

            elements.ForEach(e => testObject.Push(e));
            // when
            int result = testObject.Get();
            // then
            result.Should().Be(elements.Max());
            testObject.Should().HaveSameCount(elements);
        }

        [Test]
        public void Get_WhenEmpty_ThenInvalidOperationException()
        {
            // when
            Action action = () => _ = testObject.Get();
            // then
            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void TryGet_WhenContainsElements_ThenElementAccordingToComparer()
        {
            // given
            List<int> elements = new List<int> { 11, 4, 6, 18, 13, 7 };

            elements.ForEach(e => testObject.Push(e));
            // when
            bool result = testObject.TryGet(out int resultValue);
            // then
            result.Should().BeTrue();
            resultValue.Should().Be(elements.Max());
            testObject.Should().HaveSameCount(elements);
        }

        [Test]
        public void TryGet_WhenEmpty_ThenDefaultValue()
        {
            // when
            bool result = testObject.TryGet(out int resultValue);
            // then
            result.Should().BeFalse();
            resultValue.Should().Be(default);
            testObject.Should().HaveCount(0);
        }

        [Test]
        public void Pop_WhenContainsElements_ThenElementRemoved()
        {
            // given
            List<int> elements = new List<int> { 11, 4, 6, 18, 13, 7 };

            elements.ForEach(e => testObject.Push(e));
            // when
            int result = testObject.Pop();
            // then
            result.Should().Be(elements.Max());
            testObject.Should().HaveCountLessThan(elements.Count)
                .And.HaveCount(elements.Count - 1);
        }

        [Test]
        public void Pop_WhenEmpty_ThenInvalidOperationException()
        {
            // when
            Action action = () => _ = testObject.Pop();
            // then then
            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void TryPop_WhenContainsElements_ThenElementRemoved()
        {
            // given
            List<int> elements = new List<int> { 11, 4, 6, 18, 13, 7 };

            elements.ForEach(e => testObject.Push(e));
            // when
            bool result = testObject.TryPop(out int resultValue);
            // then
            result.Should().BeTrue();
            resultValue.Should().Be(elements.Max());
            testObject.Should().HaveCountLessThan(elements.Count)
                .And.HaveCount(elements.Count - 1);
        }

        [Test]
        public void TryPop_WhenEmpty_ThenDefaultValue()
        {
            // when
            bool result = testObject.TryPop(out int resultValue);
            // then
            result.Should().BeFalse();
            resultValue.Should().Be(default);
            testObject.Should().HaveCount(0);
        }
    }
}
