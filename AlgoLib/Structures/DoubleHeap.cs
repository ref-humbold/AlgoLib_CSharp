// Structure of double heap.
using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoLib.Structures;

public class DoubleHeap<T> : IEnumerable<T>
{
    private static readonly int IndexMin = 0;
    private static readonly int IndexMax = 1;
    private List<T> heap = new();

    /// <summary>Gets the comparer of this double heap.</summary>
    /// <value>The comparer.</value>
    public IComparer<T> Comparer { get; }

    /// <summary>Gets the number of elements in this double heap.</summary>
    /// <value>The number of elements.</value>
    public int Count => heap.Count;

    public DoubleHeap()
        : this(Comparer<T>.Default)
    {
    }

    public DoubleHeap(IEnumerable<T> enumerable)
        : this() => PushRange(enumerable);

    public DoubleHeap(Comparison<T> comparison)
        : this(Comparer<T>.Create(comparison))
    {
    }

    public DoubleHeap(IComparer<T> comparer) => Comparer = comparer;

    /// <summary>Removes all elements from this double heap.</summary>
    public void Clear() => heap = new List<T>();

    public IEnumerator<T> GetEnumerator() => new HeapEnumerator(heap);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Retrieves minimal element from this double heap.</summary>
    /// <returns>The minimal element.</returns>
    /// <exception cref="InvalidOperationException">If the double heap is empty.</exception>
    public T PeekMin() =>
        Count > 0
            ? heap[IndexMin]
            : throw new InvalidOperationException("The double heap is empty");

    /// <summary>
    /// Retrieves minimal element from this double heap and copies it to the <c>result</c> parameter.
    /// </summary>
    /// <param name="result">The minimal element if it's present, otherwise the default value.</param>
    /// <returns><c>true</c> if the element exists, otherwise <c>false</c>.</returns>
    public bool TryPeekMin(out T result)
    {
        if(Count == 0)
        {
            result = default;
            return false;
        }

        result = heap[IndexMin];
        return true;
    }

    /// <summary>Retrieves maximal element from this double heap.</summary>
    /// <returns>The maximal element.</returns>
    /// <exception cref="InvalidOperationException">If the double heap is empty.</exception>
    public T PeekMax() =>
        Count switch
        {
            0 => throw new InvalidOperationException("The double heap is empty"),
            1 => heap[IndexMin],
            _ => heap[IndexMax]
        };

    /// <summary>
    /// Retrieves maximal element from this double heap and copies it to the <c>result</c> parameter.
    /// </summary>
    /// <param name="result">The maximal element if it's present, otherwise the default value.</param>
    /// <returns><c>true</c> if the element exists, otherwise <c>false</c>.</returns>
    public bool TryPeekMax(out T result)
    {
        switch(Count)
        {
            case 0:
                result = default;
                return false;

            case 1:
                result = heap[IndexMin];
                return true;

            default:
                result = heap[IndexMax];
                return true;
        }
    }

    /// <summary>Adds new element to this double heap.</summary>
    /// <param name="item">The new element.</param>
    public void Push(T item)
    {
        heap.Add(item);

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

    /// <summary>Adds new elements to this double heap.</summary>
    /// <param name="items">The new elements.</param>
    public void PushRange(IEnumerable<T> items)
    {
        foreach(T item in items)
            Push(item);
    }

    /// <summary>Retrieves and removes minimal element from this double heap.</summary>
    /// <returns>The removed minimal element.</returns>
    /// <exception cref="InvalidOperationException">If the double heap is empty.</exception>
    public T PopMin()
    {
        T minimal = PeekMin();

        heap[IndexMin] = heap[^1];
        heap.RemoveAt(heap.Count - 1);
        moveToMax(IndexMin);
        return minimal;
    }

    /// <summary>
    /// Removes minimal element from this double heap and copies it to the <c>result</c> parameter.
    /// </summary>
    /// <param name="result">
    /// The removed minimal element if it's present, otherwise the default value.
    /// </param>
    /// <returns><c>true</c> if the element exists, otherwise <c>false</c>.</returns>
    public bool TryPopMin(out T result)
    {
        bool wasPresent = TryPeekMin(out result);

        if(wasPresent)
        {
            heap[IndexMin] = heap[^1];
            heap.RemoveAt(heap.Count - 1);
            moveToMax(IndexMin);
        }

        return wasPresent;
    }

    /// <summary>Retrieves and removes maximal element from this double heap.</summary>
    /// <returns>The removed maximal element.</returns>
    /// <exception cref="InvalidOperationException">If the double heap is empty.</exception>
    public T PopMax()
    {
        if(Count == 1)
            return PopMin();

        T maximal = PeekMax();

        heap[IndexMax] = heap[^1];
        heap.RemoveAt(heap.Count - 1);
        moveToMin(IndexMax);
        return maximal;
    }

    /// <summary>
    /// Removes maximal element from this double heap and copies it to the <c>result</c> parameter.
    /// </summary>
    /// <param name="result">
    /// The removed maximal element if it's present, otherwise the default value.
    /// </param>
    /// <returns><c>true</c> if the element exists, otherwise <c>false</c>.</returns>
    public bool TryPopMax(out T result)
    {
        if(Count == 1)
            return TryPeekMin(out result);

        bool wasPresent = TryPeekMax(out result);

        if(wasPresent)
        {
            heap[IndexMax] = heap[^1];
            heap.RemoveAt(heap.Count - 1);
            moveToMin(IndexMax);
        }

        return wasPresent;
    }

    // Compares two elements using comparator.
    private int compare(int index1, int index2) => Comparer.Compare(heap[index1], heap[index2]);

    // Moves element from given index towards minimum.
    private void moveToMin(int index)
    {
        if(index == IndexMin)
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

    // Performs a single step of movement towards minimum.
    private void stepToMin(int index, int nextIndex)
    {
        if(compare(index, nextIndex) < 0)
        {
            swap(index, nextIndex);
            moveToMin(nextIndex);
        }
    }

    // Moves element from given index towards maximum.
    private void moveToMax(int index)
    {
        if(index == IndexMax)
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

    // Performs a single step of movement towards maximum.
    private void stepToMax(int index, int nextIndex)
    {
        if(compare(index, nextIndex) > 0)
        {
            swap(index, nextIndex);
            moveToMax(nextIndex);
        }
    }

    // Swaps two elements in the double heap.
    private void swap(int index1, int index2) =>
        (heap[index2], heap[index1]) = (heap[index1], heap[index2]);

    private sealed class HeapEnumerator : IEnumerator<T>
    {
        private readonly List<T> orderList = new();
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

        private static List<T> createOrderedMinimalList(List<T> heap)
        {
            var indices = new Queue<int>();
            var minimalList = new List<T>();

            if(heap.Count > IndexMin)
                indices.Enqueue(IndexMin);

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

        private static List<T> createOrderedMaximalList(List<T> heap)
        {
            var indices = new Queue<int>();
            var maximalList = new List<T>();

            if(heap.Count > IndexMax)
                indices.Enqueue(IndexMax);

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
