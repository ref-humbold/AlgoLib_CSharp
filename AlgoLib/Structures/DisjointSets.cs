// Structure of disjoint sets (union-find)
using System;
using System.Collections.Generic;

namespace Algolib.Structures
{
    public class DisjointSets<T>
    {
        // Map of element represents
        private readonly Dictionary<T, T> represents;

        /// <summary>Number of sets.</summary>
        public int Count
        {
            get;
            private set;
        }

        public DisjointSets(IEnumerable<T> universe)
        {
            represents = new Dictionary<T, T>();

            foreach(T e in universe)
                represents[e] = e;

            Count = represents.Count;
        }

        /// <summary>Finds represent of element in set.</summary>
        /// <param name="item">Element from structure</param>
        /// <returns>Represent of the element</returns>
        /// <exception cref="KeyNotFoundException">If element not present</exception>
        public T this[T item]
        {
            get
            {
                if(!represents[item].Equals(item))
                    represents[item] = this[represents[item]];

                return represents[item];
            }
        }

        /// <summary>Checks whether an element belongs to any set.</summary>
        /// <param name="item">Element to check</param>
        /// <returns><c>true</c> if element is contained, otherwise <c>false</c></returns>
        public bool Contains(T item) => represents.ContainsKey(item);

        /// <summary>Adds a new element as singleton set.</summary>
        /// <param name="item">New element</param>
        /// <returns><c>this</c> for method chaining</returns>
        /// <exception cref="ArgumentException">if any value is already present</exception>
        public DisjointSets<T> Add(T item)
        {
            if(Contains(item))
                throw new ArgumentException($"Value {item} already present.");

            represents[item] = item;
            ++Count;
            return this;
        }

        /// <summary>Adds a new elements as singleton sets.</summary>
        /// <param name="items">New elements</param>
        /// <returns><c>this</c> for method chaining</returns>
        /// <exception cref="ArgumentException">if any value is already present</exception>
        public DisjointSets<T> AddRange(IEnumerable<T> items)
        {
            foreach(T elem in items)
                if(Contains(elem))
                    throw new ArgumentException($"Value {elem} already present.");

            foreach(T elem in items)
            {
                represents[elem] = elem;
                ++Count;
            }

            return this;
        }

        /// <summary>Finds represent of an element in set.</summary>
        /// <param name="item">Element from structure</param>
        /// <param name="result">
        /// Represent of the element if it's present, otherwise the default value
        /// </param>
        /// <returns><c>true</c> if the represent exists, otherwise <c>false</c></returns>
        public bool TryFindSet(T item, out T result)
        {
            try
            {
                result = this[item];
                return true;
            }
            catch(KeyNotFoundException)
            {
                result = default;
                return false;
            }
        }

        /// <summary>Joins two sets together.</summary>
        /// <param name="item1">Element from first set</param>
        /// <param name="item2">Element from second set</param>
        /// <returns><c>this</c> for method chaining</returns>
        /// <exception cref="KeyNotFoundException">If either element is not present</exception>
        public DisjointSets<T> UnionSet(T item1, T item2)
        {
            if(!IsSameSet(item1, item2))
            {
                represents[this[item1]] = this[item2];
                --Count;
            }

            return this;
        }

        /// <summary>Checks whether elements belong to the same set.</summary>
        /// <param name="item1">Element from first set</param>
        /// <param name="item2">Element from second set</param>
        /// <returns><c>true</c> if elements are in same set, otherwise <c>false</c></returns>
        /// <exception cref="KeyNotFoundException">If either element is not present</exception>
        public bool IsSameSet(T item1, T item2) => this[item1].Equals(this[item2]);
    }
}
