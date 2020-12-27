using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Algolib.Sequences
{
    [TestFixture]
    public class SortingTests
    {
        private struct IntPair : IComparable, IComparable<IntPair>, IEquatable<IntPair>
        {
            public int First { get; set; }
            public int Second { get; set; }

            public IntPair(int first, int second)
            {
                First = first;
                Second = second;
            }

            public int CompareTo(object obj) =>
                obj is IntPair pair ? CompareTo(pair) : throw new ArgumentException();

            public int CompareTo(IntPair other) => First.CompareTo(other.First);

            public override bool Equals(object obj) => obj is IntPair pair && Equals(pair);

            public bool Equals(IntPair other) => First == other.First && Second == other.Second;

            public override int GetHashCode() => (First, Second).GetHashCode();

            public override string ToString() => $"IntPair({First}, {Second})";
        }

        #region HeapSort

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

        #endregion
        #region TopDownMergeSort

        [Test]
        public void TopDownMergeSort_ThenSortedAscending()
        {
            // given
            List<int> sequence = new List<int>() { 3, 17, -6, 0, 9, -12, 7, 4, 2 };
            // when
            Sorting.TopDownMergeSort(sequence);
            // then
            CollectionAssert.IsOrdered(sequence);
        }

        [Test]
        public void TopDownMergeSort_WhenSortingPairsByFirst_ThenSortingIsStable()
        {
            // given
            List<IntPair> sequence = new List<IntPair>() {
                new IntPair(3, 17), new IntPair(-6, 0), new IntPair(9, 12), new IntPair(3, 4),
                new IntPair(9, -14), new IntPair(-1, 7), new IntPair(0, 2)
            };
            // when
            Sorting.TopDownMergeSort(sequence);
            // then
            CollectionAssert.IsOrdered(sequence);
            Assert.Less(sequence.IndexOf(new IntPair(3, 17)), sequence.IndexOf(new IntPair(3, 4)));
            Assert.Less(sequence.IndexOf(new IntPair(9, 12)), sequence.IndexOf(new IntPair(9, -14)));
        }

        [Test]
        public void TopDownMergeSort_WhenEmptyList_ThenEmpty()
        {
            // given
            List<int> sequence = new List<int>();
            // when
            Sorting.TopDownMergeSort(sequence);
            // then
            CollectionAssert.IsEmpty(sequence);
        }

        [Test]
        public void TopDownMergeSort_WhenNull_ThenNullPointerException()
        {
            // when
            TestDelegate testDelegate = () => Sorting.TopDownMergeSort<int>(null);
            // then
            Assert.Throws<ArgumentNullException>(testDelegate);
        }

        #endregion
        #region BottomUpMergeSort

        [Test]
        public void BottomUpMergeSort_ThenSortedAscending()
        {
            // given
            List<int> sequence = new List<int>() { 3, 17, -6, 0, 9, -12, 7, 4, 2 };
            // when
            Sorting.BottomUpMergeSort(sequence);
            // then
            CollectionAssert.IsOrdered(sequence);
        }

        [Test]
        public void BottomUpMergeSort_WhenSortingPairsByFirst_ThenSortingIsStable()
        {
            // given
            List<IntPair> sequence = new List<IntPair>() {
                new IntPair(3, 17), new IntPair(-6, 0), new IntPair(9, 12), new IntPair(3, 4),
                new IntPair(9, -14), new IntPair(-1, 7), new IntPair(0, 2)
            };
            // when
            Sorting.BottomUpMergeSort(sequence);
            // then
            CollectionAssert.IsOrdered(sequence);
            Assert.Less(sequence.IndexOf(new IntPair(3, 17)), sequence.IndexOf(new IntPair(3, 4)));
            Assert.Less(sequence.IndexOf(new IntPair(9, 12)), sequence.IndexOf(new IntPair(9, -14)));
        }

        [Test]
        public void BottomUpMergeSort_WhenEmptyList_ThenEmpty()
        {
            // given
            List<int> sequence = new List<int>();
            // when
            Sorting.BottomUpMergeSort(sequence);
            // then
            CollectionAssert.IsEmpty(sequence);
        }

        [Test]
        public void BottomUpMergeSort_WhenNull_ThenNullPointerException()
        {
            // when
            TestDelegate testDelegate = () => Sorting.BottomUpMergeSort<int>(null);
            // then
            Assert.Throws<ArgumentNullException>(testDelegate);
        }

        #endregion
        #region QuickSort

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

        #endregion
    }
}
