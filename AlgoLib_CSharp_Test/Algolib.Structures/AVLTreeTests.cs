using NUnit.Framework;
using System.Collections.Generic;

namespace Algolib.Structures
{
    [TestFixture]
    public class AVLTreeTests
    {
        private readonly int[] numbers =
            new int[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };

        private AVLTree<int> testObject;

        [SetUp]
        public void SetUp() => testObject = new AVLTree<int>(numbers);

        [TearDown]
        public void TearDown() => testObject = null;

        [Test]
        public void Count_WhenEmpty_ThenZero()
        {
            // given
            testObject = new AVLTree<int>();
            // when
            int result = testObject.Count;
            // then
            Assert.Zero(result);
        }

        [Test]
        public void Count_WhenNotEmpty_ThenNumberOfElements()
        {
            // when
            int result = testObject.Count;
            // then
            Assert.AreEqual(numbers.Length, result);
        }

        [Test]
        public void GetEnumerator_WhenNotEmpty_ThenSortedElements()
        {
            // given
            List<int> result = new List<int>();
            IEnumerator<int> enumerator = testObject.GetEnumerator();
            // when
            while(enumerator.MoveNext())
                result.Add(enumerator.Current);
            // then
            CollectionAssert.IsOrdered(result);
            CollectionAssert.AreEquivalent(numbers, result);
        }

        [Test]
        public void Contains_WhenPresentElement_ThenTrue()
        {
            foreach(int i in numbers)
            {
                // when
                bool result = testObject.Contains(i);
                // then
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void Contains_WhenAbsentElement_ThenFalse()
        {
            foreach(int i in new int[] { 111, 140, 187 })
            {
                // when
                bool result = testObject.Contains(i);
                // then
                Assert.IsFalse(result);
            }
        }

        [Test]
        public void Add_WhenNewElement_ThenTrue()
        {
            foreach(int i in new int[] { 111, 140, 187 })
            {
                // when
                bool result = testObject.Add(i);
                // then
                Assert.IsTrue(result);
                CollectionAssert.Contains(testObject, i);
            }
        }

        [Test]
        public void Add_WhenPresentElement_ThenFalse()
        {
            foreach(int i in new int[] { 14, 24, 30, 45 })
            {
                // when
                bool result = testObject.Add(i);
                // then
                Assert.IsFalse(result);
                CollectionAssert.Contains(testObject, i);
            }
        }

        [Test]
        public void Remove_WhenPresentElement_ThenTrue()
        {
            foreach(int i in new int[] { 14, 24, 30, 45 })
            {
                // when
                bool result = testObject.Remove(i);
                // then
                Assert.IsTrue(result);
                CollectionAssert.DoesNotContain(testObject, i);
            }
        }

        [Test]
        public void Remove_WhenRootAndTwoElements1()
        {
            // given
            int root = 27;
            int elem = 11;

            testObject = new AVLTree<int>(new int[] { root, elem });
            // when
            bool result = testObject.Remove(root);
            // then
            Assert.IsTrue(result);
            CollectionAssert.DoesNotContain(testObject, root);
            CollectionAssert.Contains(testObject, elem);
        }

        [Test]
        public void Remove_WhenRootAndTwoElements2()
        {
            // given
            int root = 11;
            int elem = 27;

            testObject = new AVLTree<int>(new int[] { root, elem });
            // when
            bool result = testObject.Remove(root);
            // then
            Assert.IsTrue(result);
            CollectionAssert.DoesNotContain(testObject, root);
            CollectionAssert.Contains(testObject, elem);
        }

        [Test]
        public void Remove_WhenRootAndOneElement()
        {
            // given
            int root = 0;

            testObject = new AVLTree<int>(new int[] { root });
            // when
            bool result = testObject.Remove(root);
            // then
            Assert.IsTrue(result);
            CollectionAssert.DoesNotContain(testObject, root);
            CollectionAssert.IsEmpty(testObject);
        }

        [Test]
        public void Remove_WhenEmpty_ThenFalse()
        {
            // given
            testObject = new AVLTree<int>();
            // when
            bool result = testObject.Remove(0);
            // then
            Assert.IsFalse(result);
            CollectionAssert.IsEmpty(testObject);
        }

        [Test]
        public void Remove_WhenAbsentElement_ThenFalse()
        {
            foreach(int i in new int[] { 111, 140, 187 })
            {
                // when
                bool result = testObject.Remove(i);
                // then
                Assert.IsFalse(result);
                CollectionAssert.DoesNotContain(testObject, i);
            }
        }

        [Test]
        public void Clear_ThenEmpty()
        {
            // when
            testObject.Clear();
            // then
            CollectionAssert.IsEmpty(testObject);
        }

        [Test]
        public void CopyTo_When_Then()
        {
        }

        [Test]
        public void ExceptWith_WhenPresentElements_ThenRemoved()
        {
            // given
            List<int> elements = new List<int> { 14, 24, 30, 45 };
            // when
            testObject.ExceptWith(elements);
            // then
            foreach(int i in elements)
                CollectionAssert.DoesNotContain(testObject, i);
        }

        [Test]
        public void ExceptWith_WhenPresentAndAbsentElements_ThenRemoved()
        {
            // given
            List<int> elements = new List<int> { 14, 162, 30, 195 };
            // when
            testObject.ExceptWith(elements);
            // then
            foreach(int i in elements)
                CollectionAssert.DoesNotContain(testObject, i);
        }

        [Test]
        public void ExceptWith_WhenAbsentElements_ThenRemoved()
        {
            // given
            List<int> elements = new List<int> { 111, 140, 187 };
            // when
            testObject.ExceptWith(elements);
            // then
            foreach(int i in elements)
                CollectionAssert.DoesNotContain(testObject, i);
        }

        [Test]
        public void IntersectWith_When_Then()
        {
        }

        [Test]
        public void IsProperSubsetOf_When_Then()
        {
        }

        [Test]
        public void IsProperSupersetOf_When_Then()
        {
        }

        [Test]
        public void IsSubsetOf_When_Then()
        {
        }

        [Test]
        public void IsSupersetOf_When_Then()
        {
        }

        [Test]
        public void Overlaps_When_Then()
        {
        }

        [Test]
        public void SetEquals_When_Then()
        {
        }

        [Test]
        public void SymmetricExceptWith_When_Then()
        {
        }

        [Test]
        public void UnionWith_When_Then()
        {
        }
    }
}
