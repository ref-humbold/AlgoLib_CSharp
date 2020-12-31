using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Structures
{
    [TestFixture]
    public class AVLTreeTests
    {
        private readonly int[] numbers = new int[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
        private readonly int[] presentNumbers = new int[] { 14, 24, 30, 45 };
        private readonly int[] absentNumbers = new int[] { 111, 140, 187 };

        private AVLTree<int> testObject;

        [SetUp]
        public void SetUp() => testObject = new AVLTree<int>(numbers);

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
            foreach(int i in absentNumbers)
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
            foreach(int i in absentNumbers)
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
            foreach(int i in presentNumbers)
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
            foreach(int i in presentNumbers)
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
            foreach(int i in absentNumbers)
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
            // when
            testObject.ExceptWith(presentNumbers);
            // then
            foreach(int i in numbers.Except(presentNumbers))
                CollectionAssert.Contains(testObject, i);

            foreach(int i in presentNumbers)
                CollectionAssert.DoesNotContain(testObject, i);
        }

        [Test]
        public void ExceptWith_WhenAbsentElements_ThenRemoved()
        {
            // when
            testObject.ExceptWith(absentNumbers);
            // then
            foreach(int i in numbers)
                CollectionAssert.Contains(testObject, i);

            foreach(int i in absentNumbers)
                CollectionAssert.DoesNotContain(testObject, i);
        }

        [Test]
        public void IntersectWith_WhenPresentElements_ThenCommonElements()
        {
            // when
            testObject.IntersectWith(presentNumbers);
            // then
            Assert.AreEqual(presentNumbers.Length, testObject.Count);

            foreach(int i in presentNumbers)
                CollectionAssert.Contains(testObject, i);
        }

        [Test]
        public void IntersectWith_WhenAbsentElements_ThenEmpty()
        {
            // when
            testObject.IntersectWith(absentNumbers);
            // then
            CollectionAssert.IsEmpty(testObject);
        }

        [Test]
        public void IsProperSubsetOf_WhenPartialSet_ThenFalse()
        {
            // when
            bool result = testObject.IsProperSubsetOf(presentNumbers);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsProperSubsetOf_WhenWholeSet_ThenFalse()
        {
            // when
            bool result = testObject.IsProperSubsetOf(numbers);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsProperSubsetOf_WhenExtendedSet_ThenTrue()
        {
            // when
            bool result = testObject.IsProperSubsetOf(numbers.Union(absentNumbers));
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsProperSupersetOf_WhenPartialSet_ThenTrue()
        {
            // when
            bool result = testObject.IsProperSupersetOf(presentNumbers);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsProperSupersetOf_WhenWholeSet_ThenFalse()
        {
            // when
            bool result = testObject.IsProperSupersetOf(numbers);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsProperSupersetOf_WhenExtendedSet_ThenFalse()
        {
            // when
            bool result = testObject.IsProperSupersetOf(numbers.Union(absentNumbers));
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSubsetOf_WhenPartialSet_ThenFalse()
        {
            // when
            bool result = testObject.IsSubsetOf(presentNumbers);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSubsetOf_WhenWholeSet_ThenTrue()
        {
            // when
            bool result = testObject.IsSubsetOf(numbers);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsSubsetOf_WhenExtendedSet_ThenTrue()
        {
            // when
            bool result = testObject.IsSubsetOf(numbers.Union(absentNumbers));
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsSupersetOf_WhenPartialSet_ThenTrue()
        {
            // when
            bool result = testObject.IsSupersetOf(presentNumbers);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsSupersetOf_WhenWholeSet_ThenTrue()
        {
            // when
            bool result = testObject.IsSupersetOf(numbers);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsSupersetOf_WhenExtendedSet_ThenFalse()
        {
            // when
            bool result = testObject.IsSupersetOf(numbers.Union(absentNumbers));
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void Overlaps_WhenPresentElements_ThenTrue()
        {
            // when
            bool result = testObject.Overlaps(presentNumbers);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void Overlaps_WhenAbsentElements_ThenFalse()
        {
            // when
            bool result = testObject.Overlaps(absentNumbers);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void Overlaps_WhenPresentAndAbsentElements_ThenTrue()
        {
            // when
            bool result = testObject.Overlaps(absentNumbers.Union(presentNumbers));
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void SetEquals_WhenSameElements_ThenTrue()
        {
            // when
            bool result = testObject.SetEquals(numbers);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void SetEquals_WhenMissingElements_ThenFalse()
        {
            // when
            bool result = testObject.SetEquals(numbers.Take(5));
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void SetEquals_WhenAdditionalElements_ThenFalse()
        {
            // when
            bool result = testObject.SetEquals(numbers.Append(1000));
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void SymmetricExceptWith_WhenPresentElements_ThenRemoved()
        {
            // when
            testObject.SymmetricExceptWith(presentNumbers);
            // then
            foreach(int i in numbers.Except(presentNumbers))
                CollectionAssert.Contains(testObject, i);

            foreach(int i in presentNumbers)
                CollectionAssert.DoesNotContain(testObject, i);
        }

        [Test]
        public void SymmetricExceptWith_WhenPresentAndAbsentElements_ThenPresentRemovedAndAbsentAdded()
        {
            // given
            List<int> elements = presentNumbers.Union(absentNumbers).ToList();
            // when
            testObject.SymmetricExceptWith(elements);
            // then
            foreach(int i in numbers.Except(presentNumbers))
                CollectionAssert.Contains(testObject, i);

            foreach(int i in presentNumbers)
                CollectionAssert.DoesNotContain(testObject, i);

            foreach(int i in absentNumbers)
                CollectionAssert.Contains(testObject, i);
        }

        [Test]
        public void SymmetricExceptWith_WhenAbsentElements_ThenAdded()
        {
            // when
            testObject.SymmetricExceptWith(absentNumbers);
            // then
            foreach(int i in numbers)
                CollectionAssert.Contains(testObject, i);

            foreach(int i in absentNumbers)
                CollectionAssert.Contains(testObject, i);
        }

        [Test]
        public void UnionWith_WhenPresentElements_ThenCommonElements()
        {
            // when
            testObject.UnionWith(presentNumbers);
            // then
            foreach(int i in numbers)
                CollectionAssert.Contains(testObject, i);
        }

        [Test]
        public void UnionWith_WhenAbsentElements_ThenAllElements()
        {
            // when
            testObject.UnionWith(absentNumbers);
            // then
            foreach(int i in numbers)
                CollectionAssert.Contains(testObject, i);

            foreach(int i in absentNumbers)
                CollectionAssert.Contains(testObject, i);
        }
    }
}
