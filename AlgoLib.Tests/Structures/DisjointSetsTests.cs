// Tests: Structure of disjoint sets (union-find)
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Structures
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
            result.Should().Be(10);
        }

        [Test]
        public void Contains_WhenPresentElement_ThenTrue()
        {
            // when
            bool result = testObject.Contains(4);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void Contains_WhenPresentElement_ThenFalse()
        {
            // when
            bool result = testObject.Contains(12);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void Add_WhenNewElement_ThenAdded()
        {
            // given
            int elem = 24;
            // when
            testObject.Add(24);
            // then
            testObject.Contains(elem).Should().BeTrue();
            testObject[elem].Should().Be(elem);
        }

        [Test]
        public void Add_WhenPresentElement_ThenArgumentException()
        {
            // when
            Action action = () => testObject.Add(6);
            // then
            action.Should().Throw<ArgumentException>();
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
                testObject.Contains(e).Should().BeTrue();
                testObject[e].Should().Be(e);
            }
        }

        [Test]
        public void AddAll_WhenPresentElements_ThenArgumentException()
        {
            // when
            Action action = () => testObject.AddRange(new List<int> { 20, 7, 35 });
            // then
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void IndexerGet_WhenPresentElement_ThenRepresent()
        {
            // given
            int elem = 4;
            // when
            int result = testObject[elem];
            // then
            result.Should().Be(elem);
        }

        [Test]
        public void IndexerGet_WhenAbsentElement_Then()
        {
            // when
            Action action = () => _ = testObject[17];
            // then
            action.Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public void TryFindSet_WhenPresentElement_ThenRepresent()
        {
            // given
            int elem = 4;
            // when
            bool result = testObject.TryFindSet(elem, out int resultValue);
            // then
            result.Should().BeTrue();
            resultValue.Should().Be(elem);
        }

        [Test]
        public void TryFindSet_WhenAbsentElement_ThenDefaultValue()
        {
            // when
            bool result = testObject.TryFindSet(22, out int resultValue);
            // then
            result.Should().BeFalse();
            resultValue.Should().Be(default);
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
            testObject.IsSameSet(elem1, elem2).Should().BeTrue();
            testObject[elem2].Should().Be(testObject[elem1]);
        }

        [Test]
        public void UnionSet_WhenSingleElement_ThenSameRepresent()
        {
            // given
            int elem = 4;
            // when
            testObject.UnionSet(elem, elem);
            // then
            testObject.IsSameSet(elem, elem).Should().BeTrue();
            testObject[elem].Should().Be(testObject[elem]);
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
            testObject.IsSameSet(elem1, elem2).Should().BeTrue();
            testObject[elem2].Should().Be(testObject[elem1]);
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
            testObject.IsSameSet(elems[0], elems[2]).Should().BeTrue();
            testObject[elems[2]].Should().Be(testObject[elems[0]]);
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
            result.Should().BeFalse();
        }

        [Test]
        public void IsSameSet_WhenSingleElement_ThenTrue()
        {
            // given
            int elem = 4;
            // when
            bool result = testObject.IsSameSet(elem, elem);
            // then
            result.Should().BeTrue();
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
            result.Should().BeTrue();
        }
    }
}
