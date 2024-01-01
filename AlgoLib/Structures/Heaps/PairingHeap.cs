using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoLib.Structures.Heaps;

/// <summary>Structure of pairing heap.</summary>
/// <typeparam name="T">Type of heap elements.</typeparam>
public class PairingHeap<T> : IHeap<T>
    where T : IComparable<T>
{
    private HeapNode heap = null;

    public IComparer<T> Comparer { get; } = Comparer<T>.Default;

    public int Count { get; private set; } = 0;

    public PairingHeap()
    {
    }

    public PairingHeap(IEnumerable<T> enumerable) => PushRange(enumerable);

    public void Clear()
    {
        heap = null;
        Count = 0;
    }

    public IEnumerator<T> GetEnumerator() => new HeapEnumerator(heap);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public T Peek() =>
        heap != null
            ? heap.Element
            : throw new InvalidOperationException("The pairing heap is empty");

    public bool TryPeek(out T result)
    {
        if(heap == null)
        {
            result = default;
            return false;
        }

        result = heap.Element;
        return true;
    }

    public void Push(T item)
    {
        heap = heap == null
            ? new HeapNode(Comparer) { Element = item, Children = null }
            : heap.Add(item);
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

    /// <summary>Merges given pairing heap to this heap.</summary>
    /// <param name="other">The pairing heap.</param>
    public void Merge(PairingHeap<T> other)
    {
        heap = heap == null ? other.heap : heap.Merge(other.heap);
        Count += other.Count;
    }

    // Removes minimal element.
    private void doPop()
    {
        heap = heap.Pop();
        --Count;
    }

    private class HeapNodeList
    {
        public HeapNode Node { get; set; }

        public HeapNodeList Next { get; set; }
    }

    private class HeapNode
    {
        public T Element { get; set; }

        public HeapNodeList Children { get; set; }

        private IComparer<T> Comparer { get; }

        public HeapNode(IComparer<T> comparer) => Comparer = comparer;

        public HeapNode Add(T item) =>
            Comparer.Compare(Element, item) <= 0
                ? new HeapNode(Comparer)
                {
                    Element = Element,
                    Children = new HeapNodeList
                    {
                        Node = new HeapNode(Comparer) { Element = item, Children = null },
                        Next = Children
                    }
                }
                : new HeapNode(Comparer)
                {
                    Element = item,
                    Children = new HeapNodeList { Node = this, Next = null }
                };

        public HeapNode Pop() => mergePairs(Children);

        public HeapNode Merge(HeapNode node) =>
            node == null
                ? this
                : Comparer.Compare(Element, node.Element) <= 0
                    ? new HeapNode(Comparer)
                    {
                        Element = Element,
                        Children = new HeapNodeList { Node = node, Next = Children }
                    }
                    : new HeapNode(Comparer)
                    {
                        Element = node.Element,
                        Children = new HeapNodeList { Node = this, Next = node.Children }
                    };

        private static HeapNode mergePairs(HeapNodeList list) =>
            list?.Next == null
                ? list?.Node
                : list.Node.Merge(list.Next.Node).Merge(mergePairs(list.Next.Next));
    }

    private sealed class HeapEnumerator : IEnumerator<T>
    {
        private readonly List<T> elements = new();
        private readonly IEnumerator<T> elementsEnumerator;

        public T Current => elementsEnumerator.Current;

        object IEnumerator.Current => Current;

        public HeapEnumerator(HeapNode node)
        {
            if(node != null)
                extractElements(node);

            elementsEnumerator = elements.GetEnumerator();
        }

        public bool MoveNext() => elementsEnumerator.MoveNext();

        public void Reset() => elementsEnumerator.Reset();

        public void Dispose() => elementsEnumerator.Dispose();

        private void extractElements(HeapNode node)
        {
            var queue = new Queue<HeapNode>(new[] { node });

            while(queue.Count > 0)
            {
                HeapNode current = queue.Dequeue();
                HeapNodeList list = current.Children;

                elements.Add(current.Element);

                while(list != null)
                {
                    queue.Enqueue(list.Node);
                    list = list.Next;
                }
            }
        }
    }
}
