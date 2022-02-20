using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Structures
{
    [TestFixture]
    public class AvlTreeTests
    {
        private readonly int[] numbers = new[] { 10, 6, 14, 97, 24, 37, 2, 30, 45, 18, 51, 71, 68, 26 };
        private readonly int[] presentNumbers = new[] { 14, 24, 30, 45 };
        private readonly int[] absentNumbers = new[] { 111, 140, 187 };

        private AvlTree<int> testObject;

        [SetUp]
        public void SetUp() => testObject = new AvlTree<int>(numbers);

        [Test]
        public void Count_WhenEmpty_ThenZero()
        {
            // given
            testObject = new AvlTree<int>();
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
        public void GetEnumerator_WhenNotEmpty_ThenSortedElements()
        {
            // given
            var result = new List<int>();
            IEnumerator<int> enumerator = testObject.GetEnumerator();
            // when
            while(enumerator.MoveNext())
                result.Add(enumerator.Current);
            // then
            result.Should().BeInAscendingOrder();
            result.Should().BeEquivalentTo(numbers);
        }

        [Test]
        public void Contains_WhenPresentElement_ThenTrue()
        {
            foreach(int i in numbers)
            {
                // when
                bool result = testObject.Contains(i);
                // then
                result.Should().BeTrue();
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
                result.Should().BeFalse();
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
                result.Should().BeTrue();
                testObject.Should().Contain(i);
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
                result.Should().BeFalse();
                testObject.Should().Contain(i);
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
                result.Should().BeTrue();
                testObject.Should().NotContain(i);
            }
        }

        [Test]
        public void Remove_WhenRootAndTwoElements1()
        {
            // given
            int root = 27;
            int elem = 11;

            testObject = new AvlTree<int>(new[] { root, elem });
            // when
            bool result = testObject.Remove(root);
            // then
            result.Should().BeTrue();
            testObject.Should().NotContain(root);
            testObject.Should().Contain(elem);
        }

        [Test]
        public void Remove_WhenRootAndTwoElements2()
        {
            // given
            int root = 11;
            int elem = 27;

            testObject = new AvlTree<int>(new[] { root, elem });
            // when
            bool result = testObject.Remove(root);
            // then
            result.Should().BeTrue();
            testObject.Should().NotContain(root);
            testObject.Should().Contain(elem);
        }

        [Test]
        public void Remove_WhenRootAndOneElement()
        {
            // given
            int root = 0;

            testObject = new AvlTree<int>(new[] { root });
            // when
            bool result = testObject.Remove(root);
            // then
            result.Should().BeTrue();
            testObject.Should().NotContain(root);
            testObject.Should().BeEmpty();
        }

        [Test]
        public void Remove_WhenEmpty_ThenFalse()
        {
            // given
            testObject = new AvlTree<int>();
            // when
            bool result = testObject.Remove(0);
            // then
            result.Should().BeFalse();
            testObject.Should().BeEmpty();
        }

        [Test]
        public void Remove_WhenAbsentElement_ThenFalse()
        {
            foreach(int i in absentNumbers)
            {
                // when
                bool result = testObject.Remove(i);
                // then
                result.Should().BeFalse();
                testObject.Should().NotContain(i);
            }
        }

        [Test]
        public void Clear_ThenEmpty()
        {
            // when
            testObject.Clear();
            // then
            testObject.Should().BeEmpty();
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
                testObject.Should().Contain(i);

            foreach(int i in presentNumbers)
                testObject.Should().NotContain(i);
        }

        [Test]
        public void ExceptWith_WhenAbsentElements_ThenRemoved()
        {
            // when
            testObject.ExceptWith(absentNumbers);
            // then
            foreach(int i in numbers)
                testObject.Should().Contain(i);

            foreach(int i in absentNumbers)
                testObject.Should().NotContain(i);
        }

        [Test]
        public void IntersectWith_WhenPresentElements_ThenCommonElements()
        {
            // when
            testObject.IntersectWith(presentNumbers);
            // then
            testObject.Should().HaveSameCount(presentNumbers);

            foreach(int i in presentNumbers)
                testObject.Should().Contain(i);
        }

        [Test]
        public void IntersectWith_WhenAbsentElements_ThenEmpty()
        {
            // when
            testObject.IntersectWith(absentNumbers);
            // then
            testObject.Should().BeEmpty();
        }

        [Test]
        public void IsProperSubsetOf_WhenPartialSet_ThenFalse()
        {
            // when
            bool result = testObject.IsProperSubsetOf(presentNumbers);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void IsProperSubsetOf_WhenWholeSet_ThenFalse()
        {
            // when
            bool result = testObject.IsProperSubsetOf(numbers);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void IsProperSubsetOf_WhenExtendedSet_ThenTrue()
        {
            // when
            bool result = testObject.IsProperSubsetOf(numbers.Union(absentNumbers));
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void IsProperSupersetOf_WhenPartialSet_ThenTrue()
        {
            // when
            bool result = testObject.IsProperSupersetOf(presentNumbers);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void IsProperSupersetOf_WhenWholeSet_ThenFalse()
        {
            // when
            bool result = testObject.IsProperSupersetOf(numbers);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void IsProperSupersetOf_WhenExtendedSet_ThenFalse()
        {
            // when
            bool result = testObject.IsProperSupersetOf(numbers.Union(absentNumbers));
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void IsSubsetOf_WhenPartialSet_ThenFalse()
        {
            // when
            bool result = testObject.IsSubsetOf(presentNumbers);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void IsSubsetOf_WhenWholeSet_ThenTrue()
        {
            // when
            bool result = testObject.IsSubsetOf(numbers);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void IsSubsetOf_WhenExtendedSet_ThenTrue()
        {
            // when
            bool result = testObject.IsSubsetOf(numbers.Union(absentNumbers));
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void IsSupersetOf_WhenPartialSet_ThenTrue()
        {
            // when
            bool result = testObject.IsSupersetOf(presentNumbers);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void IsSupersetOf_WhenWholeSet_ThenTrue()
        {
            // when
            bool result = testObject.IsSupersetOf(numbers);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void IsSupersetOf_WhenExtendedSet_ThenFalse()
        {
            // when
            bool result = testObject.IsSupersetOf(numbers.Union(absentNumbers));
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void Overlaps_WhenPresentElements_ThenTrue()
        {
            // when
            bool result = testObject.Overlaps(presentNumbers);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void Overlaps_WhenAbsentElements_ThenFalse()
        {
            // when
            bool result = testObject.Overlaps(absentNumbers);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void Overlaps_WhenPresentAndAbsentElements_ThenTrue()
        {
            // when
            bool result = testObject.Overlaps(absentNumbers.Union(presentNumbers));
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void SetEquals_WhenSameElements_ThenTrue()
        {
            // when
            bool result = testObject.SetEquals(numbers);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void SetEquals_WhenMissingElements_ThenFalse()
        {
            // when
            bool result = testObject.SetEquals(numbers.Take(5));
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void SetEquals_WhenAdditionalElements_ThenFalse()
        {
            // when
            bool result = testObject.SetEquals(numbers.Append(1000));
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void SymmetricExceptWith_WhenPresentElements_ThenRemoved()
        {
            // when
            testObject.SymmetricExceptWith(presentNumbers);
            // then
            foreach(int i in numbers.Except(presentNumbers))
                testObject.Should().Contain(i);

            foreach(int i in presentNumbers)
                testObject.Should().NotContain(i);
        }

        [Test]
        public void SymmetricExceptWith_WhenPresentAndAbsentElements_ThenPresentRemovedAndAbsentAdded()
        {
            // given
            var elements = presentNumbers.Union(absentNumbers).ToList();
            // when
            testObject.SymmetricExceptWith(elements);
            // then
            foreach(int i in numbers.Except(presentNumbers))
                testObject.Should().Contain(i);

            foreach(int i in presentNumbers)
                testObject.Should().NotContain(i);

            foreach(int i in absentNumbers)
                testObject.Should().Contain(i);
        }

        [Test]
        public void SymmetricExceptWith_WhenAbsentElements_ThenAdded()
        {
            // when
            testObject.SymmetricExceptWith(absentNumbers);
            // then
            foreach(int i in numbers)
                testObject.Should().Contain(i);

            foreach(int i in absentNumbers)
                testObject.Should().Contain(i);
        }

        [Test]
        public void UnionWith_WhenPresentElements_ThenCommonElements()
        {
            // when
            testObject.UnionWith(presentNumbers);
            // then
            foreach(int i in numbers)
                testObject.Should().Contain(i);
        }

        [Test]
        public void UnionWith_WhenAbsentElements_ThenAllElements()
        {
            // when
            testObject.UnionWith(absentNumbers);
            // then
            foreach(int i in numbers)
                testObject.Should().Contain(i);

            foreach(int i in absentNumbers)
                testObject.Should().Contain(i);
        }
    }
}
