using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Assert.That(testObject, Has.Count.EqualTo(1));
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
            Assert.That(result, Is.EqualTo(elements));
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
            Assert.That(result, Is.EqualTo(elements.Max()));
            Assert.That(testObject, Has.Count.EqualTo(elements.Count));
        }

        [Test]
        public void Get_WhenEmpty_ThenInvalidOperationException()
        {
            // when
            TestDelegate testDelegate = () => _ = testObject.Get();
            // then
            Assert.That(testDelegate, Throws.TypeOf<InvalidOperationException>());
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
            Assert.That(result, Is.True);
            Assert.That(resultValue, Is.EqualTo(elements.Max()));
            Assert.That(testObject, Has.Count.EqualTo(elements.Count));
        }

        [Test]
        public void TryGet_WhenEmpty_ThenDefaultValue()
        {
            // when
            bool result = testObject.TryGet(out int resultValue);
            // then
            Assert.That(result, Is.False);
            Assert.That(resultValue, Is.EqualTo(default(int)));
            Assert.That(testObject, Has.Count.EqualTo(0));
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
            Assert.That(result, Is.EqualTo(elements.Max()));
            Assert.That(testObject, Has.Count.EqualTo(elements.Count - 1));
        }

        [Test]
        public void Pop_WhenEmpty_ThenInvalidOperationException()
        {
            // when
            TestDelegate testDelegate = () => _ = testObject.Pop();
            // then then
            Assert.That(testDelegate, Throws.TypeOf<InvalidOperationException>());
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
            Assert.That(result, Is.True);
            Assert.That(resultValue, Is.EqualTo(elements.Max()));
            Assert.That(testObject, Has.Count.EqualTo(elements.Count - 1));
        }

        [Test]
        public void TryPop_WhenEmpty_ThenDefaultValue()
        {
            // when
            bool result = testObject.TryPop(out int resultValue);
            // then
            Assert.That(result, Is.False);
            Assert.That(resultValue, Is.EqualTo(default(int)));
            Assert.That(testObject, Has.Count.EqualTo(0));
        }
    }
}
