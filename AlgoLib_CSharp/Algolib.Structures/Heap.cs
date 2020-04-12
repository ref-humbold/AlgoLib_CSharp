// Structure of heap
using System;
using System.Collections.Generic;

namespace Algolib.Structures
{
    public class Heap<T> where T : IComparable<T>
    {
        private readonly List<T> heap;

        public Heap()
        {
            heap = new List<T>();
        }

        public Heap(int capacity)
        {
            heap = new List<T>(capacity);
        }

        public int Count => heap.Count;

        /// <summary>Adds new element to this heap.</summary>
        /// <param name="element">new element</param>
        public void Push(T element)
        {
            heap.Add(element);

            int index = heap.Count - 1;

            while(index > 0)
            {
                int nextIndex = (index - 1) / 2;

                if(heap[nextIndex].CompareTo(heap[index]) < 0)
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
        public T Get()
        {
            return heap[0];
        }

        /// <summary>Removes the least element from this heap.</summary>
        /// <returns>removed element</returns>
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
                        : heap[index + index + 1].CompareTo(heap[index + index + 2]) < 0
                            ? index + index + 1
                            : index + index + 2;

                if(heap[childIndex].CompareTo(heap[index]) < 0)
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
