// Algorithms for sorting
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Sequences
{
    public static class Sorting
    {
        private static readonly Random Random = new();

        /// <summary>Mutably sorts given sequence using a heap.</summary>
        /// <typeparam name="T">The type of sequence elements.</typeparam>
        /// <param name="sequence">The sequence of elements.</param>
        public static void HeapSort<T>(this List<T> sequence)
            where T : IComparable<T>
        {
            if(sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            int heapCount = sequence.Count;

            if(heapCount <= 1)
                return;

            for(int i = heapCount / 2; i >= 0; --i)
                moveDown(sequence, i, sequence.Count);

            while(heapCount > 1)
            {
                int indexHeap = heapCount - 1;

                swap(sequence, indexHeap, 0);
                moveDown(sequence, 0, indexHeap);
                --heapCount;
            }
        }

        /// <summary>
        /// Mutably sorts given sequence using a top-down merge-sort algorithm. Sorting is
        /// guaranteed to be stable.
        /// </summary>
        /// <typeparam name="T">The type of sequence elements.</typeparam>
        /// <param name="sequence">The sequence of elements.</param>
        public static void TopDownMergeSort<T>(this List<T> sequence)
            where T : IComparable<T>
        {
            if(sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            doMergeSort(sequence, ..);
        }

        /// <summary>
        /// Mutably sorts given sequence using a bottom-up merge-sort algorithm. Sorting is
        /// guaranteed to be stable.
        /// </summary>
        /// <typeparam name="T">The type of sequence elements.</typeparam>
        /// <param name="sequence">The sequence of elements.</param>
        public static void BottomUpMergeSort<T>(this List<T> sequence)
            where T : IComparable<T>
        {
            if(sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            if(sequence.Count <= 1)
                return;

            for(int halfStep = 1; halfStep < sequence.Count; halfStep *= 2)
                for(int i = 0; i < sequence.Count; i += halfStep + halfStep)
                    merge(sequence, i, Math.Min(i + halfStep, sequence.Count),
                          Math.Min(i + halfStep + halfStep, sequence.Count));
        }

        /// <summary>Mutably sorts given sequence using a quick-sort algorithm.</summary>
        /// <typeparam name="T">The type of sequence elements.</typeparam>
        /// <param name="sequence">The sequence of elements.</param>
        public static void QuickSort<T>(this List<T> sequence)
            where T : IComparable<T>
        {
            if(sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            doQuickSort(sequence, ..);
        }

        // Move element down inside given heap.
        private static void moveDown<T>(List<T> heap, int vertex, int indexEnd)
            where T : IComparable<T>
        {
            int nextVertex = -1;
            int leftVertex = vertex + vertex + 1;
            int rightVertex = vertex + vertex + 2;

            if(rightVertex < indexEnd)
                nextVertex = heap[rightVertex].CompareTo(heap[leftVertex]) < 0
                    ? leftVertex
                    : rightVertex;

            if(leftVertex == indexEnd - 1)
                nextVertex = leftVertex;

            if(nextVertex < 0)
                return;

            if(heap[nextVertex].CompareTo(heap[vertex]) > 0)
                swap(heap, nextVertex, vertex);

            moveDown(heap, nextVertex, indexEnd);
        }

        // Mutably sorts given sequence using a recursive merge-sort algorithm.
        private static void doMergeSort<T>(List<T> sequence, Range range)
            where T : IComparable<T>
        {
            int indexBegin = range.Start.GetOffset(sequence.Count);
            int indexEnd = range.End.GetOffset(sequence.Count);

            if(indexEnd - indexBegin <= 1)
                return;

            int indexMiddle = (indexBegin + indexEnd) / 2;

            doMergeSort(sequence, indexBegin..indexMiddle);
            doMergeSort(sequence, indexMiddle..indexEnd);
            merge(sequence, indexBegin, indexMiddle, indexEnd);
        }

        // Merges two sorted fragments of a sequence. Guaranteed to be stable.
        private static void merge<T>(List<T> sequence, int indexBegin, int indexMiddle, int indexEnd)
            where T : IComparable<T>
        {
            var ordered = new List<T>();
            int iter1 = indexBegin;
            int iter2 = indexMiddle;

            while(iter1 < indexMiddle && iter2 < indexEnd)
                if(sequence[iter1].CompareTo(sequence[iter2]) <= 0)
                {
                    ordered.Add(sequence[iter1]);
                    ++iter1;
                }
                else
                {
                    ordered.Add(sequence[iter2]);
                    ++iter2;
                }

            for(int i = iter1; i < indexMiddle; ++i)
                ordered.Add(sequence[i]);

            for(int i = iter2; i < indexEnd; ++i)
                ordered.Add(sequence[i]);

            for(int i = 0; i < ordered.Count; ++i)
                sequence[indexBegin + i] = ordered[i];
        }

        // Mutably sorts given sequence using a quick-sort algorithm.
        private static void doQuickSort<T>(List<T> sequence, Range range)
            where T : IComparable<T>
        {
            int indexBegin = range.Start.GetOffset(sequence.Count);
            int indexEnd = range.End.GetOffset(sequence.Count);

            if(indexEnd - indexBegin <= 1)
                return;

            int indexPivot = choosePivot(indexBegin, indexEnd);

            swap(sequence, indexPivot, indexBegin);
            indexPivot = indexBegin;

            int indexFront = indexBegin + 1;
            int indexBack = indexEnd - 1;

            while(indexPivot < indexBack)
                if(sequence[indexFront].CompareTo(sequence[indexPivot]) < 0)
                {
                    swap(sequence, indexPivot, indexFront);
                    indexPivot = indexFront;
                    ++indexFront;
                }
                else
                {
                    swap(sequence, indexBack, indexFront);
                    --indexBack;
                }

            doQuickSort(sequence, indexBegin..indexPivot);
            doQuickSort(sequence, (indexPivot + 1)..indexEnd);
        }

        // Randomly chooses pivot for quick-sort algorithm.
        private static int choosePivot(int indexBegin, int indexEnd) =>
            Enumerable.Empty<int>()
                      .Append(Random.Next(indexBegin, indexEnd))
                      .Append(Random.Next(indexBegin, indexEnd))
                      .Append(Random.Next(indexBegin, indexEnd))
                      .OrderBy(c => c)
                      .Skip(1)
                      .First();

        // Swaps two elements in given sequence.
        private static void swap<T>(List<T> sequence, int index1, int index2) =>
            (sequence[index2], sequence[index1]) = (sequence[index1], sequence[index2]);
    }
}
