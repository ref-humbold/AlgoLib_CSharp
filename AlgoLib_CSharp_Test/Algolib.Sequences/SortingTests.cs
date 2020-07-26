using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AlgoLib.Sequences
{
    [TestFixture]
    public class SortingTests
    {
        [Test]
        public void HeapSort_ThenSortedAscending()
        {
            // given
            List<int> sequence = new List<int>() { 3, 17, -6, 0, 9, -12, 7, 4, 2 };
            // when
            Sorting.HeapSort(sequence);
            // then
            CollectionAssert.IsOrdered(sequence);
        }

        [Test]
        public void HeapSort_WhenEmptyList_ThenEmpty()
        {
            // given
            List<int> sequence = new List<int>();
            // when
            Sorting.HeapSort(sequence);
            // then
            CollectionAssert.IsEmpty(sequence);
        }

        [Test]
        public void HeapSort_WhenNull_ThenNullPointerException()
        {
            // when
            TestDelegate testDelegate = () => Sorting.HeapSort<int>(null);
            // then
            Assert.Throws<ArgumentNullException>(testDelegate);
        }

        [Test]
        public void MergedownSort_ThenSortedAscending()
        {
            // given
            List<int> sequence = new List<int>() { 3, 17, -6, 0, 9, -12, 7, 4, 2 };
            // when
            Sorting.MergedownSort(sequence);
            // then
            CollectionAssert.IsOrdered(sequence);
        }

        [Test]
        public void MergedownSort_WhenEmptyList_ThenEmpty()
        {
            // given
            List<int> sequence = new List<int>();
            // when
            Sorting.MergedownSort(sequence);
            // then
            CollectionAssert.IsEmpty(sequence);
        }

        [Test]
        public void MergedownSort_WhenNull_ThenNullPointerException()
        {
            // when
            TestDelegate testDelegate = () => Sorting.MergedownSort<int>(null);
            // then
            Assert.Throws<ArgumentNullException>(testDelegate);
        }

        [Test]
        public void MergeupSort_ThenSortedAscending()
        {
            // given
            List<int> sequence = new List<int>() { 3, 17, -6, 0, 9, -12, 7, 4, 2 };
            // when
            Sorting.MergeupSort(sequence);
            // then
            CollectionAssert.IsOrdered(sequence);
        }

        [Test]
        public void MergeupSort_WhenEmptyList_ThenEmpty()
        {
            // given
            List<int> sequence = new List<int>();
            // when
            Sorting.MergeupSort(sequence);
            // then
            CollectionAssert.IsEmpty(sequence);
        }

        [Test]
        public void MergeupSort_WhenNull_ThenNullPointerException()
        {
            // when
            TestDelegate testDelegate = () => Sorting.MergeupSort<int>(null);
            // then
            Assert.Throws<ArgumentNullException>(testDelegate);
        }

        [Test]
        public void QuickSort_ThenSortedAscending()
        {
            // given
            List<int> sequence = new List<int>() { 3, 17, -6, 0, 9, -12, 7, 4, 2 };
            // when
            Sorting.QuickSort(sequence);
            // then
            CollectionAssert.IsOrdered(sequence);
        }

        [Test]
        public void QuickSort_WhenEmptyList_ThenEmpty()
        {
            // given
            List<int> sequence = new List<int>();
            // when
            Sorting.QuickSort(sequence);
            // then
            CollectionAssert.IsEmpty(sequence);
        }

        [Test]
        public void QuickSort_WhenNull_ThenNullPointerException()
        {
            // when
            TestDelegate testDelegate = () => Sorting.QuickSort<int>(null);
            // then
            Assert.Throws<ArgumentNullException>(testDelegate);
        }
    }
}
