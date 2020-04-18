// Structure of heap
using System;
using System.Collections.Generic;

namespace Algolib.Structures
{
    public class Heap<T>
    {
        private readonly List<T> heap;

        public IComparer<T> Comparer { get; }

        public int Count => heap.Count;

        public Heap() : this(Comparer<T>.Default)
        {
        }

        public Heap(Comparison<T> comparison) : this(Comparer<T>.Create(comparison))
        {
        }

        public Heap(IComparer<T> comparer)
        {
            heap = new List<T>();
            Comparer = comparer;
        }

        /// <summary>Adds new element to this heap.</summary>
        /// <param name="element">new element</param>
        public void Push(T element)
        {
            heap.Add(element);

            int index = heap.Count - 1;

            while(index > 0)
            {
                int nextIndex = (index - 1) / 2;

                if(Comparer.Compare(heap[index], heap[nextIndex]) < 0)
                {
                    swap(index, nextIndex);
                    index = nextIndex;
                }
                else
                    break;
            }
        }

        /// <summary>Retrieves the least element from this heap.</summary>
        /// <returns>the least element</returns>
        /// <exception cref="InvalidOperationException">if the heap is empty</exception>
        public T Get()
        {
            if(Count == 0)
                throw new InvalidOperationException("The heap is empty");

            return heap[0];
        }

        /// <summary>Retrieves and removes the least element from this heap.</summary>
        /// <returns>removed element</returns>
        /// <exception cref="InvalidOperationException">if the heap is empty</exception>
        public T Pop()
        {
            T element = Get();

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

                if(Comparer.Compare(heap[childIndex], heap[index]) < 0)
                {
                    swap(index, childIndex);
                    index = childIndex;
                }
                else
                    break;
            }

            return element;
        }

        private void swap(int indexFirst, int indexSecond)
        {
            T temp = heap[indexFirst];

            heap[indexFirst] = heap[indexSecond];
            heap[indexSecond] = temp;
        }
    }
}
