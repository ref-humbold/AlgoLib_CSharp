// Structure of heap.
using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoLib.Structures
{
    public class Heap<T> : IEnumerable<T>
    {
        private List<T> heap = new();

        /// <summary>Gets the comparer.</summary>
        /// <value>The comparer.</value>
        public IComparer<T> Comparer { get; }

        /// <summary>Gets the number of elelemnts.</summary>
        /// <value>The number of elements.</value>
        public int Count => heap.Count;

        public Heap()
            : this(Comparer<T>.Default)
        {
        }

        public Heap(IEnumerable<T> enumerable)
            : this() => PushRange(enumerable);

        public Heap(Comparison<T> comparison)
            : this(Comparer<T>.Create(comparison))
        {
        }

        public Heap(IComparer<T> comparer) => Comparer = comparer;

        /// <summary>Removes all elements from this heap.</summary>
        public void Clear() => heap = new List<T>();

        public IEnumerator<T> GetEnumerator() => heap.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => heap.GetEnumerator();

        /// <summary>Retrieves minimal element from this heap.</summary>
        /// <returns>The minimal element.</returns>
        /// <exception cref="InvalidOperationException">If the heap is empty.</exception>
        public T Peek() =>
            Count > 0
                ? heap[0]
                : throw new InvalidOperationException("The heap is empty");

        /// <summary>
        /// Retrieves minimal element from this heap and copies it to the <c>result</c> parameter.
        /// </summary>
        /// <param name="result">The minimal element if it's present, otherwise the default value.</param>
        /// <returns><c>true</c> if the element exists, otherwise <c>false</c>.</returns>
        public bool TryPeek(out T result)
        {
            if(Count == 0)
            {
                result = default;
                return false;
            }

            result = heap[0];
            return true;
        }

        /// <summary>Adds new element to this heap.</summary>
        /// <param name="item">The new element.</param>
        public void Push(T item)
        {
            heap.Add(item);

            int index = heap.Count - 1;

            while(index > 0)
            {
                int nextIndex = (index - 1) / 2;

                if(Comparer.Compare(heap[index], heap[nextIndex]) >= 0)
                    break;

                swap(index, nextIndex);
                index = nextIndex;
            }
        }

        /// <summary>Adds new elements from given range to this heap.</summary>
        /// <param name="items">The new elements.</param>
        public void PushRange(IEnumerable<T> items)
        {
            foreach(T item in items)
                Push(item);
        }

        /// <summary>Retrieves and removes minimal element from this heap.</summary>
        /// <returns>The removed minimal element.</returns>
        /// <exception cref="InvalidOperationException">If the heap is empty.</exception>
        public T Pop()
        {
            T element = Peek();

            doPop();
            return element;
        }

        /// <summary>
        /// Removes minimal element from this heap and copies it to the <c>result</c> parameter.
        /// </summary>
        /// <param name="result">
        /// The removed minimal element if it's present, otherwise the default value.
        /// </param>
        /// <returns><c>true</c> if the element exists, otherwise <c>false</c>.</returns>
        public bool TryPop(out T result)
        {
            bool wasPresent = TryPeek(out result);

            if(wasPresent)
                doPop();

            return wasPresent;
        }

        // Removes minimal element.
        private void doPop()
        {
            heap[0] = heap[^1];
            heap.RemoveAt(heap.Count - 1);

            int index = 0;

            while(index + index + 1 < heap.Count)
            {
                int childIndex = index + index + 2 == heap.Count
                        ? index + index + 1
                        : Comparer.Compare(heap[index + index + 1], heap[index + index + 2]) < 0
                            ? index + index + 1
                            : index + index + 2;

                if(Comparer.Compare(heap[childIndex], heap[index]) >= 0)
                    break;

                swap(index, childIndex);
                index = childIndex;
            }
        }

        // Swaps two elements in the heap.
        private void swap(int indexFirst, int indexSecond) =>
            (heap[indexSecond], heap[indexFirst]) = (heap[indexFirst], heap[indexSecond]);
    }
}
