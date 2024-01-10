using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoLib.Structures.Heaps;

/// <summary>Structure of leftist heap.</summary>
/// <typeparam name="T">Type of heap elements.</typeparam>
public class LeftistHeap<T> : IHeap<T>
    where T : IComparable<T>
{
    private static readonly IComparer<T> DefaultComparer = Comparer<T>.Default;
    private HeapNode heap = null;

    public IComparer<T> Comparer => DefaultComparer;

    public int Count { get; private set; } = 0;

    public LeftistHeap()
    {
    }

    public LeftistHeap(IEnumerable<T> enumerable) => PushRange(enumerable);

    /// <summary>Merges given heaps.</summary>
    /// <param name="heap1">The first heap.</param>
    /// <param name="heap2">The second heap.</param>
    /// <returns>The merged heap.</returns>
    public static LeftistHeap<T> operator +(LeftistHeap<T> heap1, LeftistHeap<T> heap2) =>
        new()
        {
            heap = heap1.heap + heap2.heap,
            Count = heap1.Count + heap2.Count
        };

    public void Clear()
    {
        heap = null;
        Count = 0;
    }

    public IEnumerator<T> GetEnumerator() => new HeapEnumerator(heap);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public T Peek() =>
        heap is not null
            ? heap.Element
            : throw new InvalidOperationException("The heap is empty");

    public bool TryPeek(out T result)
    {
        if(heap is null)
        {
            result = default;
            return false;
        }

        result = heap.Element;
        return true;
    }

    public void Push(T item)
    {
        heap += new HeapNode(item);
        ++Count;
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
        heap = heap.Pop();
        --Count;
    }

    private class HeapNode
    {
        public int Rank { get; }

        public T Element { get; }

        public HeapNode Left { get; }

        public HeapNode Right { get; }

        public HeapNode(T element, HeapNode node1 = null, HeapNode node2 = null)
        {
            int rank1 = node1?.Rank ?? 0;
            int rank2 = node2?.Rank ?? 0;

            Element = element;

            if(rank1 < rank2)
            {
                Rank = rank1 + 1;
                Left = node2;
                Right = node1;
            }
            else
            {
                Rank = rank2 + 1;
                Left = node1;
                Right = node2;
            }
        }

        public static HeapNode operator +(HeapNode node1, HeapNode node2) =>
            node1 is null
                ? node2
                : node2 is null
                    ? node1
                    : DefaultComparer.Compare(node1.Element, node2.Element) < 0
                        ? new HeapNode(node1.Element, node1.Left, node1.Right + node2)
                        : new HeapNode(node2.Element, node2.Left, node2.Right + node1);

        public HeapNode Pop() => Left + Right;
    }

    private sealed class HeapEnumerator : IEnumerator<T>
    {
        private readonly HeapNode heapNode;
        private readonly Stack<HeapNode> stack = new();
        private HeapNode currentNode;

        public T Current => currentNode != default
            ? currentNode.Element
            : throw new InvalidOperationException();

        object IEnumerator.Current => Current;

        public HeapEnumerator(HeapNode node)
        {
            heapNode = node;
            Reset();
        }

        public bool MoveNext()
        {
            bool hasNode = stack.TryPop(out HeapNode node);

            if(hasNode)
            {
                if(node.Left is not null)
                    stack.Push(node.Left);

                if(node.Right is not null)
                    stack.Push(node.Right);
            }

            currentNode = node;
            return hasNode;
        }

        public void Reset()
        {
            stack.Clear();

            if(heapNode is not null)
                stack.Push(heapNode);
        }

        public void Dispose()
        {
        }
    }
}
