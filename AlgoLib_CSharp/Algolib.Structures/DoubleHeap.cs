using System;
using System.Collections;
using System.Collections.Generic;

namespace Algolib.Structures
{
    public class DoubleHeap<T> : IEnumerable<T>
    {
        private static readonly int indexMin = 0;
        private static readonly int indexMax = 1;
        private readonly List<T> heap;

        /// <summary>The comparer.</summary>
        public IComparer<T> Comparer
        {
            get;
        }

        /// <summary>Number of elements.</summary>
        public int Count => heap.Count;

        public DoubleHeap() : this(Comparer<T>.Default)
        {
        }

        public DoubleHeap(IEnumerable<T> enumerable) : this()
        {
            foreach(T element in enumerable)
                Push(element);
        }

        public DoubleHeap(Comparison<T> comparison) : this(Comparer<T>.Create(comparison))
        {
        }

        public DoubleHeap(IComparer<T> comparer)
        {
            heap = new List<T>();
            Comparer = comparer;
        }

        public IEnumerator<T> GetEnumerator() => new HeapEnumerator(heap);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Adds a new value to this double heap.</summary>
        /// <param name="element">Value to add</param>
        public void Push(T element)
        {
            heap.Add(element);

            if(heap.Count > 1)
            {
                int index = heap.Count - 1;

                if(index % 2 == 1)
                {
                    if(compare(index, index - 1) < 0)
                    {
                        swap(index, index - 1);
                        moveToMin(index - 1);
                    }
                    else
                        moveToMax(index);
                }
                else
                {
                    int newIndex = ((index + 1) / 2 - 1) / 2 * 2 + 1;

                    if(compare(index, newIndex) > 0)
                    {
                        swap(index, newIndex);
                        moveToMax(newIndex);
                    }
                    else
                        moveToMin(index);
                }
            }
        }

        /// <summary>Retrieves the minimal element from this double heap.</summary>
        /// <returns>Minimal element</returns>
        /// <exception cref="InvalidOperationException">If the double heap is empty</exception>
        public T GetMin() => Count == 0 ? throw new InvalidOperationException("The double heap is empty")
                                        : heap[indexMin];

        /// <summary>
        /// Retrieves the minimal element from this double heap and copies it to the <c>result</c> parameter.
        /// </summary>
        /// <param name="result">The least element if it's present, otherwise the default value</param>
        /// <returns><c>true</c> if the element exists, otherwise <c>false</c></returns>
        public bool TryGetMin(out T result)
        {
            if(Count == 0)
            {
                result = default;
                return false;
            }

            result = heap[indexMin];
            return true;
        }

        /// <summary>Retrieves the maximal element from this double heap.</summary>
        /// <returns>Maximal element</returns>
        /// <exception cref="InvalidOperationException">If the double heap is empty</exception>
        public T GetMax() =>
            Count switch
            {
                0 => throw new InvalidOperationException("The double heap is empty"),
                1 => heap[indexMin],
                _ => heap[indexMax]
            };

        /// <summary>
        /// Retrieves the maximal element from this double heap and copies it to the <c>result</c> parameter.
        /// </summary>
        /// <param name="result">The least element if it's present, otherwise the default value</param>
        /// <returns><c>true</c> if the element exists, otherwise <c>false</c></returns>
        public bool TryGetMax(out T result)
        {
            switch(Count)
            {
                case 0:
                    result = default;
                    return false;

                case 1:
                    result = heap[indexMin];
                    return true;

                default:
                    result = heap[indexMax];
                    return true;
            }
        }

        /// <summary>Retrieves and removes the minimal element from this double heap.</summary>
        /// <returns>Removed element</returns>
        /// <exception cref="InvalidOperationException">If the double heap is empty</exception>
        public T PopMin()
        {
            T minimal = GetMin();

            doPopAt(indexMin);
            return minimal;
        }

        /// <summary>
        /// Removes the minimal element from this double heap and copies it to the <c>result</c> parameter.
        /// </summary>
        /// <param name="result">The least element if it's present, otherwise the default value</param>
        /// <returns><c>true</c> if the element exists, otherwise <c>false</c></returns>
        public bool TryPopMin(out T result)
        {
            bool wasPresent = TryGetMin(out result);

            if(wasPresent)
                doPopAt(indexMin);

            return wasPresent;
        }

        /// <summary>Retrieves and removes the maximal element from this double heap.</summary>
        /// <returns>Removed element</returns>
        /// <exception cref="InvalidOperationException">If the double heap is empty</exception>
        public T PopMax()
        {
            if(Count == 1)
                return PopMin();

            T maximal = GetMax();

            doPopAt(indexMax);
            return maximal;
        }

        /// <summary>
        /// Removes the maximal element from this double heap and copies it to the <c>result</c> parameter.
        /// </summary>
        /// <param name="result">The least element if it's present, otherwise the default value</param>
        /// <returns><c>true</c> if the element exists, otherwise <c>false</c></returns>
        public bool TryPopMax(out T result)
        {
            bool wasPresent = TryGetMax(out result);

            if(wasPresent)
                doPopAt(indexMax);

            return wasPresent;
        }

        private int compare(int index1, int index2) => Comparer.Compare(heap[index1], heap[index2]);

        private void doPopAt(int index)
        {
            heap[index] = heap[^1];
            heap.RemoveAt(heap.Count - 1);
            moveToMax(index);
        }

        private void moveToMin(int index)
        {
            if(index == indexMin)
                return;

            if(index % 2 == 0)
                stepToMin(index, (index / 2 - 1) / 2 * 2);
            else
            {
                int leftIndex = index + index + 1;
                int rightIndex = index + index + 3;

                if(rightIndex < heap.Count)
                {
                    int childIndex = compare(leftIndex, rightIndex) > 0 ? leftIndex : rightIndex;

                    stepToMin(index, childIndex);
                }
                else if(leftIndex < heap.Count)
                    stepToMin(index, leftIndex);
                else if(index < heap.Count)
                    stepToMin(index, index - 1);
            }
        }

        private void stepToMin(int index, int nextIndex)
        {
            if(compare(index, nextIndex) < 0)
            {
                swap(index, nextIndex);
                moveToMin(nextIndex);
            }
        }

        private void moveToMax(int index)
        {
            if(index == indexMax)
                return;

            if(index % 2 == 1)
                stepToMax(index, (index / 2 - 1) / 2 * 2 + 1);
            else
            {
                int leftIndex = index + index + 2;
                int rightIndex = index + index + 4;

                if(rightIndex < heap.Count)
                {
                    int childIndex = compare(leftIndex, rightIndex) < 0 ? leftIndex : rightIndex;

                    stepToMax(index, childIndex);
                }
                else if(leftIndex < heap.Count)
                    stepToMax(index, leftIndex);
                else if(index + 1 < heap.Count)
                    stepToMax(index, index + 1);
            }
        }

        private void stepToMax(int index, int nextIndex)
        {
            if(compare(index, nextIndex) > 0)
            {
                swap(index, nextIndex);
                moveToMax(nextIndex);
            }
        }

        private void swap(int index1, int index2)
        {
            T temp = heap[index1];

            heap[index1] = heap[index2];
            heap[index2] = temp;
        }

        public class HeapEnumerator : IEnumerator<T>
        {
            private readonly List<T> orderList = new List<T>();
            private readonly IEnumerator<T> orderListEnumerator;

            public T Current => orderListEnumerator.Current;

            object IEnumerator.Current => Current;

            public HeapEnumerator(List<T> heap)
            {
                List<T> minimalList = createOrderedMinimalList(heap);
                List<T> maximalList = createOrderedMaximalList(heap);

                maximalList.Reverse();
                orderList.AddRange(minimalList);
                orderList.AddRange(maximalList);
                orderListEnumerator = orderList.GetEnumerator();
            }

            public bool MoveNext() => orderListEnumerator.MoveNext();

            public void Reset() => orderListEnumerator.Reset();

            public void Dispose() => orderListEnumerator.Dispose();

            private List<T> createOrderedMinimalList(List<T> heap)
            {
                Queue<int> indices = new Queue<int>();
                List<T> minimalList = new List<T>();

                if(indexMin < heap.Count)
                    indices.Enqueue(indexMin);

                while(indices.Count > 0)
                {
                    int index = indices.Dequeue();

                    minimalList.Add(heap[index]);

                    if(index + index + 2 < heap.Count)
                        indices.Enqueue(index + index + 2);

                    if(index + index + 4 < heap.Count)
                        indices.Enqueue(index + index + 4);
                }

                return minimalList;
            }

            private List<T> createOrderedMaximalList(List<T> heap)
            {
                Queue<int> indices = new Queue<int>();
                List<T> maximalList = new List<T>();

                if(indexMax < heap.Count)
                    indices.Enqueue(indexMax);

                while(indices.Count > 0)
                {
                    int index = indices.Dequeue();

                    maximalList.Add(heap[index]);

                    if(index + index + 1 < heap.Count)
                        indices.Enqueue(index + index + 1);

                    if(index + index + 3 < heap.Count)
                        indices.Enqueue(index + index + 3);
                }

                return maximalList;
            }
        }
    }
}
