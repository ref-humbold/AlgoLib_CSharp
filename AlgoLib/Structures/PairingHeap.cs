using System;
using System.Collections.Generic;

namespace AlgoLib.Structures
{
    public class PairingHeap<T> where T : IComparable<T>
    {
        private HeapNode heap;

        public int Count
        {
            get;
            private set;
        }

        public PairingHeap()
        {
            heap = null;
            Count = 0;
        }

        public PairingHeap(IEnumerable<T> enumerable) : this()
        {
            foreach(T element in enumerable)
                Push(element);
        }

        /// <summary>Retrieves minimal element from this pairing heap.</summary>
        /// <returns>Minimal element.</returns>
        /// <exception cref="InvalidOperationException">If the pairing heap is empty.</exception>
        public T Peek() =>
            heap != null ? heap.Element : throw new InvalidOperationException("The pairing heap is empty");

        /// <summary>
        /// Retrieves minimal element from this pairing heap and copies it to the <c>result</c> parameter.
        /// </summary>
        /// <param name="result">Minimal element if it's present, otherwise the default value.</param>
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

        /// <summary>Adds new value to this pairing heap.</summary>
        /// <param name="item">The new value.</param>
        public void Push(T item)
        {
            if(heap == null)
                heap = new HeapNode { Element = item, Children = null };
            else if(item.CompareTo(heap.Element) < 0)
                heap = new HeapNode
                {
                    Element = item,
                    Children = new HeapNodeList { Node = heap, Next = null }
                };
            else
                heap = new HeapNode
                {
                    Element = heap.Element,
                    Children = new HeapNodeList
                    {
                        Node = new HeapNode { Element = item, Children = null },
                        Next = heap.Children
                    }
                };

            ++Count;
        }

        /// <summary>Retrieves and removes minimal element from this pairing heap.</summary>
        /// <returns>Removed minimal element.</returns>
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
        /// Removed minimal element if it's present, otherwise the default value.
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
            public HeapNode Node;
            public HeapNodeList Next;
        }

        private class HeapNode
        {
            public T Element;
            public HeapNodeList Children;

            public HeapNode Pop()
            {
                return mergePairs(Children);
            }

            public HeapNode Merge(HeapNode node)
            {
                if(node == null)
                    return this;

                return Element.CompareTo(node.Element) <= 0
                    ? new HeapNode
                    {
                        Element = Element,
                        Children = new HeapNodeList { Node = node, Next = this.Children }
                    }
                    : new HeapNode
                    {
                        Element = node.Element,
                        Children = new HeapNodeList { Node = this, Next = node.Children }
                    };
            }

            private HeapNode mergePairs(HeapNodeList list) =>
                list?.Next == null ? list?.Node
                                   : list.Node.Merge(list.Next.Node).Merge(mergePairs(list.Next.Next));
        }
    }
}
