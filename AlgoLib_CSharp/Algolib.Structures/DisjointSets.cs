// Disjoint sets structure (union-find)
using System;
using System.Collections.Generic;

namespace Algolib.Structures
{
    public class DisjointSets<E>
    {
        /// <summary>Map of element represents</summary>
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

        /// <summary>Checks whether element belongs to any set</summary>
        /// <param name="element">element</param>
        /// <returns><code>true</code> if element is contained, otherwise <code>false</code></returns>
        public bool Contains(E element)
        {
            return represents.ContainsKey(element);
        }

        /// <summary>Adds new elements as singleton sets</summary>
        /// <param name="elements">new elements</param>
        public void Add(IEnumerable<E> elements)
        {
            foreach(E elem in elements)
                if(Contains(elem))
                    throw new ArgumentException($"Value {elem} already present.");

            foreach(E elem in elements)
            {
                represents[elem] = elem;
                ++Count;
            }
        }

        /// <summary>Finds represent of element in set</summary>
        /// <param name="element">element from structure</param>
        /// <returns>represent of the element</returns>
        public E FindSet(E element)
        {
            if(!represents[element].Equals(element))
                represents[element] = FindSet(represents[element]);

            return represents[element];
        }

        /// <summary>Joins two sets</summary>
        /// <param name="element1">element from first set</param>
        /// <param name="element2">element from second set</param>
        public void UnionSet(E element1, E element2)
        {
            if(!IsSameSet(element1, element2))
            {
                represents[FindSet(element1)] = FindSet(element2);
                --Count;
            }
        }

        /// <summary>Checks whether elements belong to the same set</summary>
        /// <param name="element1">element from first set</param>
        /// <param name="element2">element from second set</param>
        /// <returns><code>true</code> if elements are in same set, otherwise <code>false</code></returns>
        public bool IsSameSet(E element1, E element2)
        {
            return FindSet(element1).Equals(FindSet(element2));
        }
    }
}
