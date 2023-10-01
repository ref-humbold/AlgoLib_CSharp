// Structure of pairing heap.
using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgoLib.Structures
{
    public class PairingHeap<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private HeapNode heap;

        /// <summary>Gets the number of elements.</summary>
        /// <value>The number of elements.</value>
        public int Count { get; private set; }

        public PairingHeap()
        {
            heap = null;
            Count = 0;
        }

        public PairingHeap(IEnumerable<T> enumerable)
            : this() => PushRange(enumerable);

        /// <summary>Removes all elements from this pairing heap.</summary>
        public void Clear()
        {
            heap = null;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator() => new HeapEnumerator(heap);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Retrieves minimal element from this pairing heap.</summary>
        /// <returns>The minimal element.</returns>
        /// <exception cref="InvalidOperationException">If the pairing heap is empty.</exception>
        public T Peek() =>
            heap != null
                ? heap.Element
                : throw new InvalidOperationException("The pairing heap is empty");

        /// <summary>
        /// Retrieves minimal element from this pairing heap and copies it to the <c>result</c> parameter.
        /// </summary>
        /// <param name="result">The minimal element if it's present, otherwise the default value.</param>
        /// <returns><c>true</c> if the element exists, otherwise <c>false</c>.</returns>
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

        /// <summary>Adds new element to this pairing heap.</summary>
        /// <param name="item">The new element.</param>
        public void Push(T item)
        {
            heap = heap == null
                ? new HeapNode { Element = item, Children = null }
                : heap.Add(item);
            ++Count;
        }

        /// <summary>Adds new elements from given range to this pairing heap.</summary>
        /// <param name="items">The new elements.</param>
        public void PushRange(IEnumerable<T> items)
        {
            foreach(T item in items)
                Push(item);
        }

        /// <summary>Retrieves and removes minimal element from this pairing heap.</summary>
        /// <returns>The removed minimal element.</returns>
        /// <exception cref="InvalidOperationException">If the pairing heap is empty.</exception>
        public T Pop()
        {
            T element = Peek();

            doPop();
            return element;
        }

        /// <summary>
        /// Removes minimal element from this pairing heap and copies it to the <c>result</c> parameter.
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

            public HeapNode Add(T item) =>
                Element.CompareTo(item) <= 0
                    ? new HeapNode
                    {
                        Element = Element,
                        Children = new HeapNodeList
                        {
                            Node = new HeapNode { Element = item, Children = null },
                            Next = Children
                        }
                    }
                    : new HeapNode
                    {
                        Element = item,
                        Children = new HeapNodeList { Node = this, Next = null }
                    };

            public HeapNode Pop() => mergePairs(Children);

            public HeapNode Merge(HeapNode node) =>
                node == null
                    ? this
                    : Element.CompareTo(node.Element) <= 0
                        ? new HeapNode
                        {
                            Element = Element,
                            Children = new HeapNodeList { Node = node, Next = Children }
                        }
                        : new HeapNode
                        {
                            Element = node.Element,
                            Children = new HeapNodeList { Node = this, Next = node.Children }
                        };

            private HeapNode mergePairs(HeapNodeList list) =>
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
}
