using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Structures
{
    [TestFixture]
    public class DisjointSetsTests
    {
        private DisjointSets<int> testObject;

        [SetUp]
        public void SetUp()
        {
            testObject = new DisjointSets<int>(Enumerable.Range(0, 10));
        }

        [TearDown]
        public void TearDown()
        {
            testObject = null;
        }

        [Test]
        public void Count_WhenElements_ThenSetsCount()
        {
            // when
            int result = testObject.Count;
            // then
            Assert.AreEqual(10, result);
        }

        [Test]
        public void Contains_WhenPresentElement_ThenTrue()
        {
            // when
            bool result = testObject.Contains(4);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void Contains_WhenPresentElement_ThenFalse()
        {
            // when
            bool result = testObject.Contains(12);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void Add_WhenNewElement_ThenAdded()
        {
            // given
            int elem = 24;
            // when
            testObject.Add(24);
            // then
            Assert.IsTrue(testObject.Contains(elem));
            Assert.AreEqual(elem, testObject[elem]);
        }

        [Test]
        public void Add_WhenPresentElement_ThenArgumentException()
        {
            // when - then
            Assert.Throws<ArgumentException>(() => testObject.Add(6));
        }

        [Test]
        public void AddAll_WhenNewElements_ThenAllAdded()
        {
            // given
            List<int> elems = new List<int> { 20, 17, 35 };
            // when
            testObject.AddAll(elems);
            // then
            foreach(int e in elems)
            {
                Assert.IsTrue(testObject.Contains(e));
                Assert.AreEqual(e, testObject[e]);
            }
        }

        [Test]
        public void AddAll_WhenPresentElements_ThenArgumentException()
        {
            // when - then
            Assert.Throws<ArgumentException>(() => testObject.AddAll(new List<int> { 20, 7, 35 }));
        }

        [Test]
        public void Indexer_WhenPresentElement_ThenRepresent()
        {
            // given
            int elem = 4;
            // when
            int result = testObject[elem];
            // then
            Assert.AreEqual(elem, result);
        }

        [Test]
        public void Indexer_WhenAbsentElement_Then()
        {
            // when - then
            Assert.Throws<KeyNotFoundException>(() => _ = testObject[17]);
        }

        [Test]
        public void FindSet_WhenPresentElement_ThenRepresent()
        {
            // given
            int elem = 4;
            // when
            int result = testObject.FindSet(elem, 0);
            // then
            Assert.AreEqual(elem, result);
        }

        [Test]
        public void FindSet_WhenAbsentElement_ThenDefaultValue()
        {
            // given
            int defaultValue = 10;
            // when
            int result = testObject.FindSet(22, defaultValue);
            // then
            Assert.AreEqual(defaultValue, result);
        }

        [Test]
        public void UnionSet_WhenDifferentSets_ThenSameRepresent()
        {
            // given
            int elem1 = 4;
            int elem2 = 6;

            testObject.UnionSet(elem1, elem2);

            Assert.IsTrue(testObject.IsSameSet(elem1, elem2));
            Assert.AreEqual(testObject[elem1], testObject[elem2]);
        }

        [Test]
        public void UnionSet_WhenSingleElement_ThenSameRepresent()
        {
            // given
            int elem = 4;

            testObject.UnionSet(elem, elem);

            Assert.IsTrue(testObject.IsSameSet(elem, elem));
            Assert.AreEqual(testObject[elem], testObject[elem]);
        }

        [Test]
        public void UnionSet_WhenSameSet_ThenSameRepresent()
        {
            // given
            int elem1 = 3;
            int elem2 = 8;
            testObject.UnionSet(elem1, elem2);

            testObject.UnionSet(elem2, elem1);

            Assert.IsTrue(testObject.IsSameSet(elem1, elem2));
            Assert.AreEqual(testObject[elem1], testObject[elem2]);
        }

        [Test]
        public void UnionSet_WhenNewElementsInChain_ThenSameRepresent()
        {
            // given
            List<int> elems = new List<int> { 20, 17, 35 };

            testObject.AddAll(elems)
                      .UnionSet(elems[0], elems[1])
                      .UnionSet(elems[1], elems[2]);

            Assert.IsTrue(testObject.IsSameSet(elems[0], elems[2]));
            Assert.AreEqual(testObject[elems[0]], testObject[elems[2]]);
        }


        [Test]
        public void IsSameSet_WhenDifferentSets_ThenFalse()
        {
            // given
            int elem1 = 4;
            int elem2 = 6;

            bool result = testObject.IsSameSet(elem1, elem2);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsSameSet_WhenSingleElement_ThenTrue()
        {
            // given
            int elem = 4;

            bool result = testObject.IsSameSet(elem, elem);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsSameSet_WhenSameSet_ThenTrue()
        {
            // given
            int elem1 = 3;
            int elem2 = 8;
            testObject.UnionSet(elem1, elem2);

            bool result = testObject.IsSameSet(elem2, elem1);

            Assert.IsTrue(result);
        }
    }
}