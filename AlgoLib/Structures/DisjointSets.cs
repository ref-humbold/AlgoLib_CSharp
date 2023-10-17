// Structure of disjoint sets (union-find).
using System;
using System.Collections.Generic;

namespace AlgoLib.Structures;

public class DisjointSets<T>
{
    private readonly Dictionary<T, T> represents = new();

    /// <summary>Gets the number of sets in this structure.</summary>
    /// <value>The number of sets.</value>
    public int Count { get; private set; }

    public DisjointSets(IEnumerable<T> universe)
    {
        foreach(T e in universe)
            represents[e] = e;

        Count = represents.Count;
    }

    /// <summary>Searches for represent of given element.</summary>
    /// <param name="item">The element.</param>
    /// <returns>The represent of the element.</returns>
    /// <exception cref="KeyNotFoundException">If the element is not present.</exception>
    public T this[T item]
    {
        get
        {
            if(!represents[item].Equals(item))
                represents[item] = this[represents[item]];

            return represents[item];
        }
    }

    /// <summary>Checks whether given element belongs to any set.</summary>
    /// <param name="item">The element.</param>
    /// <returns><c>true</c> if the element belongs to the structure, otherwise <c>false</c>.</returns>
    public bool Contains(T item) => represents.ContainsKey(item);

    /// <summary>Adds new element as singleton set.</summary>
    /// <param name="item">The new element.</param>
    /// <returns><c>this</c> for method chaining.</returns>
    /// <exception cref="ArgumentException">If the element is already present.</exception>
    public DisjointSets<T> Add(T item)
    {
        if(Contains(item))
            throw new ArgumentException($"Value {item} already present.");

        represents[item] = item;
        ++Count;
        return this;
    }

    /// <summary>Adds new elements as singleton sets.</summary>
    /// <param name="items">The new elements.</param>
    /// <returns><c>this</c> for method chaining.</returns>
    /// <exception cref="ArgumentException">If any of the elements is already present.</exception>
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

    /// <summary>Searches for represent of given element.</summary>
    /// <param name="item">The element.</param>
    /// <param name="result">
    /// The represent of the element if it's present, otherwise the default value.
    /// </param>
    /// <returns><c>true</c> if the represent exists, otherwise <c>false</c>.</returns>
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
    /// <param name="item1">The element from the first set.</param>
    /// <param name="item2">The element from the second set.</param>
    /// <returns><c>this</c> for method chaining.</returns>
    /// <exception cref="KeyNotFoundException">If either element is not present.</exception>
    public DisjointSets<T> UnionSet(T item1, T item2)
    {
        if(!IsSameSet(item1, item2))
        {
            represents[this[item1]] = this[item2];
            --Count;
        }

        return this;
    }

    /// <summary>Checks whether given elements belong to the same set.</summary>
    /// <param name="item1">The element from the first set.</param>
    /// <param name="item2">The element from the second set.</param>
    /// <returns><c>true</c> if the elements are in same set, otherwise <c>false</c>.</returns>
    /// <exception cref="KeyNotFoundException">If either element is not present.</exception>
    public bool IsSameSet(T item1, T item2) => this[item1].Equals(this[item2]);
}
