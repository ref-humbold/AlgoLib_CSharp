﻿using System;
using System.Collections.Generic;

namespace Algolib.Sequences
{
    public class Sorting
    {
        /// <summary>Mutably sorts given sequence using a heap.</summary>
        /// <param name="sequence">A sequence of elements</param>
        public static void HeapSort<T>(List<T> sequence) where T : IComparable<T>
        {
            if(sequence == null)
                throw new ArgumentNullException("Sequence is null");

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

        /// <summary>Mutably sorts given sequence using a top-down merge-sort algorithm.</summary>
        /// <param name="sequence">A sequence of elements</param>
        public static void MergedownSort<T>(List<T> sequence) where T : IComparable<T>
        {
            if(sequence == null)
                throw new ArgumentNullException("Sequence is null");

            doMergeSort(sequence, 0, sequence.Count);
        }

        /// <summary>Mutably sorts given sequence using a bottom-up merge-sort algorithm.</summary>
        /// <param name="sequence">A sequence of elements</param>
        public static void MergeupSort<T>(List<T> sequence) where T : IComparable<T>
        {
            if(sequence == null)
                throw new ArgumentNullException("Sequence is null");

            if(sequence.Count <= 1)
                return;

            for(int half_step = 1; half_step < sequence.Count; half_step *= 2)
                for(int i = 0; i < sequence.Count; i += half_step + half_step)
                    merge(sequence, i, Math.Min(i + half_step, sequence.Count),
                          Math.Min(i + half_step + half_step, sequence.Count));
        }

        /// <summary>Mutably sorts given sequence using a quick-sort algorithm.</summary>
        /// <param name="sequence">A sequence of elements</param>
        public static void QuickSort<T>(List<T> sequence) where T : IComparable<T>
        {
            if(sequence == null)
                throw new ArgumentNullException("Sequence is null");

            doQuickSort(sequence, 0, sequence.Count);
        }

        // Move element down inside given heap.
        private static void moveDown<T>(List<T> heap, int vertex, int indexEnd) where T : IComparable<T>
        {
            int nextVertex = -1;
            int leftVertex = vertex + vertex + 1;
            int rightVertex = vertex + vertex + 2;

            if(rightVertex < indexEnd)
                nextVertex = heap[rightVertex].CompareTo(heap[leftVertex]) < 0 ? leftVertex
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
        private static void doMergeSort<T>(List<T> sequence, int indexBegin, int indexEnd)
            where T : IComparable<T>
        {
            if(indexEnd - indexBegin <= 1)
                return;

            int indexMiddle = (indexBegin + indexEnd) / 2;

            doMergeSort(sequence, indexBegin, indexMiddle);
            doMergeSort(sequence, indexMiddle, indexEnd);
            merge(sequence, indexBegin, indexMiddle, indexEnd);
        }

        // Merges two sorted fragments of a sequence.
        private static void merge<T>(List<T> sequence, int indexBegin, int indexMiddle, int indexEnd)
            where T : IComparable<T>
        {
            List<T> ordered = new List<T>();
            int iter1 = indexBegin;
            int iter2 = indexMiddle;

            while(iter1 < indexMiddle && iter2 < indexEnd)
                if(sequence[iter1].CompareTo(sequence[iter2]) < 0)
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
        private static void doQuickSort<T>(List<T> sequence, int indexBegin, int indexEnd)
            where T : IComparable<T>
        {
            if(indexEnd - indexBegin <= 1)
                return;

            int indexPivot = indexBegin + choosePivot(indexEnd - indexBegin);

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

            doQuickSort(sequence, indexBegin, indexPivot);
            doQuickSort(sequence, indexPivot + 1, indexEnd);
        }

        // Randomly chooses pivot for quick-sort algorithm.
        private static int choosePivot(int count)
        {
            Random random = new Random();
            int candidate1 = random.Next(count);
            int candidate2 = random.Next(count);
            int candidate3 = random.Next(count);

            if(Math.Min(candidate2, candidate3) <= candidate1
               && candidate1 <= Math.Max(candidate2, candidate3))
                return candidate2;

            if(Math.Min(candidate1, candidate3) <= candidate2
               && candidate2 <= Math.Max(candidate1, candidate3))
                return candidate2;

            return candidate3;
        }

        // Swaps two elements in given sequence.
        private static void swap<T>(List<T> sequence, int index1, int index2) where T : IComparable<T>
        {
            T temp = sequence[index1];

            sequence[index1] = sequence[index2];
            sequence[index2] = temp;
        }
    }
}
