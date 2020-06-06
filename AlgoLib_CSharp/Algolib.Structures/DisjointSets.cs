// Structure of disjoint sets (union-find)
using System;
using System.Collections.Generic;

namespace Algolib.Structures
{
    public class DisjointSets<E>
    {
        // Map of element represents
        private readonly Dictionary<E, E> represents;

        /// <summary>Number of sets</summary>
        public int Count { get; private set; }

        public DisjointSets(IEnumerable<E> universe)
        {
            represents = new Dictionary<E, E>();

            foreach(E e in universe)
                represents[e] = e;

            Count = represents.Count;
        }

        /// <summary>Finds represent of element in set</summary>
        /// <param name="element">element from structure</param>
        /// <returns>represent of the element</returns>
        /// <exception cref="KeyNotFoundException">if element not present</exception>
        public E this[E element]
        {
            get
            {
                if(!represents[element].Equals(element))
                    represents[element] = this[represents[element]];

                return represents[element];
            }
        }

        /// <summary>Checks whether element belongs to any set</summary>
        /// <param name="element">element</param>
        /// <returns><c>true</c> if element is contained, otherwise <c>false</c></returns>
        public bool Contains(E element)
        {
            return represents.ContainsKey(element);
        }

        /// <summary>Adds new element as singleton set</summary>
        /// <param name="element">new element</param>
        /// <returns><c>this</c> for method chaining</returns>
        /// <exception cref="ArgumentException">if any value is already present</exception>
        public DisjointSets<E> Add(E element)
        {
            if(Contains(element))
                throw new ArgumentException($"Value {element} already present.");

            represents[element] = element;
            ++Count;
            return this;
        }

        /// <summary>Adds new elements as singleton sets</summary>
        /// <param name="elements">new elements</param>
        /// <returns><c>this</c> for method chaining</returns>
        /// <exception cref="ArgumentException">if any value is already present</exception>
        public DisjointSets<E> AddAll(IEnumerable<E> elements)
        {
            foreach(E elem in elements)
                if(Contains(elem))
                    throw new ArgumentException($"Value {elem} already present.");

            foreach(E elem in elements)
            {
                represents[elem] = elem;
                ++Count;
            }

            return this;
        }

        /// <summary>Finds represent of element in set</summary>
        /// <param name="element">element</param>
        /// <param name="default_value">value to return when element not present</param>
        /// <returns>represent of the element or default value</returns>
        public E FindSet(E element, E default_value)
        {
            try
            {
                return this[element];
            }
            catch(KeyNotFoundException)
            {
                return default_value;
            }
        }

        /// <summary>Joins two sets together</summary>
        /// <param name="element1">element from first set</param>
        /// <param name="element2">element from second set</param>
        /// <returns><c>this</c> for method chaining</returns>
        /// <exception cref="KeyNotFoundException">if either element is not present</exception>
        public DisjointSets<E> UnionSet(E element1, E element2)
        {
            if(!IsSameSet(element1, element2))
            {
                represents[this[element1]] = this[element2];
                --Count;
            }

            return this;
        }

        /// <summary>Checks whether elements belong to the same set</summary>
        /// <param name="element1">element from first set</param>
        /// <param name="element2">element from second set</param>
        /// <returns><c>true</c> if elements are in same set, otherwise <c>false</c></returns>
        /// <exception cref="KeyNotFoundException">if either element is not present</exception>
        public bool IsSameSet(E element1, E element2)
        {
            return this[element1].Equals(this[element2]);
        }
    }
}
