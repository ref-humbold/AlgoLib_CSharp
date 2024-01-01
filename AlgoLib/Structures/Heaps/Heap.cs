using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoLib.Structures.Heaps;

/// <summary>Structure of heap.</summary>
/// <typeparam name="T">Type of heap elements.</typeparam>
public class Heap<T> : IHeap<T>
{
    private List<T> heap = new();

    public IComparer<T> Comparer { get; } = Comparer<T>.Default;

    public int Count => heap.Count;

    public Heap()
    {
    }

    public Heap(IEnumerable<T> enumerable)
        : this() => PushRange(enumerable);

    public Heap(Comparison<T> comparison)
        : this(Comparer<T>.Create(comparison))
    {
    }

    public Heap(IComparer<T> comparer) => Comparer = comparer;

    public void Clear() => heap = new List<T>();

    public IEnumerator<T> GetEnumerator() => heap.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => heap.GetEnumerator();

    public T Peek() =>
        Count > 0
            ? heap[0]
            : throw new InvalidOperationException("The heap is empty");

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

    public void PushRange(IEnumerable<T> items)
    {
        foreach(T item in items)
            Push(item);
    }

    public T Pop()
    {
        T element = Peek();

        doPop();
        return element;
    }

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
