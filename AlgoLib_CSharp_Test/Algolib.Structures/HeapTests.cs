﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Structures
{
    [TestFixture]
    public class HeapTests
    {
        private Comparison<int> comparison = (n, m) => m.CompareTo(n);
        private Heap<int> testObject;

        [SetUp]
        public void SetUp()
        {
            testObject = new Heap<int>(comparison);
        }

        [TearDown]
        public void TearDown()
        {
            testObject = null;
        }

        [Test]
        public void Push_WhenNewElement_ThenAddedToHeap()
        {
            // when
            testObject.Push(19);
            // then
            Assert.AreEqual(1, testObject.Count);
        }

        [Test]
        public void Push_Pop_WhenMultipleElements_ThenElementsAccordingToComparer()
        {
            // given
            List<int> elements = new List<int> { 11, 4, 6, 18, 13, 7 };
            // when
            List<int> result = new List<int>();

            elements.ForEach(e => testObject.Push(e));

            while(testObject.Count > 0)
                result.Add(testObject.Pop());
            // then
            elements.Sort(comparison);
            Assert.AreEqual(elements, result);
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
            Assert.AreEqual(elements.Max(), result);
            Assert.AreEqual(elements.Count, testObject.Count);
        }

        [Test]
        public void Get_WhenEmptyHeap_ThenInvalidOperationException()
        {
            // when
            TestDelegate testDelegate = () => _ = testObject.Get();
            // then
            Assert.Throws<InvalidOperationException>(testDelegate);
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
            Assert.AreEqual(elements.Max(), result);
            Assert.AreEqual(elements.Count - 1, testObject.Count);
        }

        [Test]
        public void Pop_WhenEmptyHeap_ThenInvalidOperationException()
        {
            // when
            TestDelegate testDelegate = () => _ = testObject.Pop();
            // then
            Assert.Throws<InvalidOperationException>(testDelegate);
        }
    }
}
