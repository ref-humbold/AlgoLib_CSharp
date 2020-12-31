// Tests: Structure of disjoint sets (union-find)
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Algolib.Structures
{
    [TestFixture]
    public class DisjointSetsTests
    {
        private DisjointSets<int> testObject;

        [SetUp]
        public void SetUp() => testObject = new DisjointSets<int>(Enumerable.Range(0, 10));

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
            // when
            TestDelegate testDelegate = () => testObject.Add(6);
            // then
            Assert.Throws<ArgumentException>(testDelegate);
        }

        [Test]
        public void AddAll_WhenNewElements_ThenAllAdded()
        {
            // given
            List<int> elems = new List<int> { 20, 17, 35 };
            // when
            testObject.AddRange(elems);
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
            // when
            TestDelegate testDelegate = () => testObject.AddRange(new List<int> { 20, 7, 35 });
            // then
            Assert.Throws<ArgumentException>(testDelegate);
        }

        [Test]
        public void IndexerGet_WhenPresentElement_ThenRepresent()
        {
            // given
            int elem = 4;
            // when
            int result = testObject[elem];
            // then
            Assert.AreEqual(elem, result);
        }

        [Test]
        public void IndexerGet_WhenAbsentElement_Then()
        {
            // when
            TestDelegate testDelegate = () => _ = testObject[17];
            // then
            Assert.Throws<KeyNotFoundException>(testDelegate);
        }

        [Test]
        public void TryFindSet_WhenPresentElement_ThenRepresent()
        {
            // given
            int elem = 4;
            // when
            bool result = testObject.TryFindSet(elem, out int resultValue);
            // then
            Assert.IsTrue(result);
            Assert.AreEqual(elem, resultValue);
        }

        [Test]
        public void TryFindSet_WhenAbsentElement_ThenDefaultValue()
        {
            // when
            bool result = testObject.TryFindSet(22, out int resultValue);
            // then
            Assert.IsFalse(result);
            Assert.AreEqual(default(int), resultValue);
        }

        [Test]
        public void UnionSet_WhenDifferentSets_ThenSameRepresent()
        {
            // given
            int elem1 = 4;
            int elem2 = 6;
            // when
            testObject.UnionSet(elem1, elem2);
            // then
            Assert.IsTrue(testObject.IsSameSet(elem1, elem2));
            Assert.AreEqual(testObject[elem1], testObject[elem2]);
        }

        [Test]
        public void UnionSet_WhenSingleElement_ThenSameRepresent()
        {
            // given
            int elem = 4;
            // when
            testObject.UnionSet(elem, elem);
            // then
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
            // when
            testObject.UnionSet(elem2, elem1);
            // then
            Assert.IsTrue(testObject.IsSameSet(elem1, elem2));
            Assert.AreEqual(testObject[elem1], testObject[elem2]);
        }

        [Test]
        public void UnionSet_WhenNewElementsInChain_ThenSameRepresent()
        {
            // given
            List<int> elems = new List<int> { 20, 17, 35 };
            // when
            testObject.AddRange(elems)
                      .UnionSet(elems[0], elems[1])
                      .UnionSet(elems[1], elems[2]);
            // then
            Assert.IsTrue(testObject.IsSameSet(elems[0], elems[2]));
            Assert.AreEqual(testObject[elems[0]], testObject[elems[2]]);
        }

        [Test]
        public void IsSameSet_WhenDifferentSets_ThenFalse()
        {
            // given
            int elem1 = 4;
            int elem2 = 6;
            // when
            bool result = testObject.IsSameSet(elem1, elem2);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSameSet_WhenSingleElement_ThenTrue()
        {
            // given
            int elem = 4;
            // when
            bool result = testObject.IsSameSet(elem, elem);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsSameSet_WhenSameSet_ThenTrue()
        {
            // given
            int elem1 = 3;
            int elem2 = 8;
            testObject.UnionSet(elem1, elem2);
            // when
            bool result = testObject.IsSameSet(elem2, elem1);
            // then
            Assert.IsTrue(result);
        }
    }
}
