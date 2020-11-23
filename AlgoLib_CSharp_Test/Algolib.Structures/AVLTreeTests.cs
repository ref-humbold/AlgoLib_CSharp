using NUnit.Framework;

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
        public void GetEnumerator_When_Then()
        {
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
        public void Remove_When_Then()
        {
        }

        [Test]
        public void Clear_When_Then()
        {
        }

        [Test]
        public void CopyTo_When_Then()
        {
        }

        [Test]
        public void ExceptWith_When_Then()
        {
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
