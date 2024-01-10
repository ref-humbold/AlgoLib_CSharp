using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoLib.Structures.Heaps;

/// <summary>Structure of pairing heap.</summary>
/// <typeparam name="T">Type of heap elements.</typeparam>
public class PairingHeap<T> : IHeap<T>
    where T : IComparable<T>
{
    private static readonly IComparer<T> DefaultComparer = Comparer<T>.Default;
    private HeapNode heap = null;

    public IComparer<T> Comparer => DefaultComparer;

    public int Count { get; private set; } = 0;

    public PairingHeap()
    {
    }

    public PairingHeap(IEnumerable<T> enumerable) => PushRange(enumerable);

    /// <summary>Merges given heaps.</summary>
    /// <param name="heap1">The first heap.</param>
    /// <param name="heap2">The second heap.</param>
    /// <returns>The merged heap.</returns>
    public static PairingHeap<T> operator +(PairingHeap<T> heap1, PairingHeap<T> heap2) =>
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
        heap = heap?.Add(item) ?? new HeapNode(item);
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

    private record HeapNodeList(HeapNode Node, HeapNodeList Next = null) : IEnumerable<HeapNode>
    {
        public IEnumerator<HeapNode> GetEnumerator()
        {
            yield return Node;

            if(Next is not null)
                foreach(HeapNode node in Next)
                    yield return node;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private record HeapNode(T Element, HeapNodeList Children = null)
    {
        public static HeapNode operator +(HeapNode node1, HeapNode node2) =>
            node1 == null
                ? node2
                : node2 == null
                    ? node1
                    : DefaultComparer.Compare(node1.Element, node2.Element) <= 0
                        ? node1 with { Children = new HeapNodeList(node2, node1.Children) }
                        : node2 with { Children = new HeapNodeList(node1, node2.Children) };

        public HeapNode Add(T item) =>
            DefaultComparer.Compare(Element, item) <= 0
                ? this with { Children = new HeapNodeList(new HeapNode(item), Children) }
                : new HeapNode(item, new HeapNodeList(this));

        public HeapNode Pop() => mergePairs(Children);

        private static HeapNode mergePairs(HeapNodeList list) =>
            list?.Next == null
                ? list?.Node
                : list.Node + list.Next.Node + mergePairs(list.Next.Next);
    }

    private sealed class HeapEnumerator : IEnumerator<T>
    {
        private readonly HeapNode heapNode;
        private readonly Queue<HeapNode> queue = new();
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
            bool hasNode = queue.TryDequeue(out HeapNode node);

            if(hasNode && node?.Children is not null)
            {
                foreach(HeapNode child in node.Children)
                    queue.Enqueue(child);
            }

            currentNode = node;
            return hasNode;
        }

        public void Reset()
        {
            queue.Clear();

            if(heapNode is not null)
                queue.Enqueue(heapNode);
        }

        public void Dispose()
        {
        }
    }
}
