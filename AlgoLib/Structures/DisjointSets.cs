using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Structures;

/// <summary>Structure of disjoint sets (union-find).</summary>
/// <typeparam name="T">Type of sets elements.</typeparam>
public class DisjointSets<T>
{
    private readonly Dictionary<T, T> represents = new();

    /// <summary>Gets the number of sets in this structure.</summary>
    /// <value>The number of sets.</value>
    public int Count { get; private set; }

    public DisjointSets()
    {
    }

    public DisjointSets(IEnumerable<IEnumerable<T>> sets)
    {
        T[][] setsArray = sets.Select(s => s.Distinct().ToArray()).ToArray();
        validateDuplicates(setsArray);

        foreach(T[] set in setsArray)
            foreach(T element in set)
                represents[element] = set[0];

        Count = setsArray.Length;
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

    /// <summary>Removes all sets from this structure.</summary>
    public void Clear()
    {
        represents.Clear();
        Count = 0;
    }

    /// <summary>Checks whether given element belongs to any set.</summary>
    /// <param name="item">The element.</param>
    /// <returns><c>true</c> if the element belongs to the structure, otherwise <c>false</c>.</returns>
    public bool Contains(T item) => represents.ContainsKey(item);

    /// <summary>Adds new elements as a new set.</summary>
    /// <param name="items">The new elements.</param>
    /// <returns><c>this</c> for method chaining.</returns>
    /// <exception cref="ArgumentException">If any of the elements is already present.</exception>
    public DisjointSets<T> Add(IEnumerable<T> items)
    {
        T[] itemsArray = items.ToArray();

        foreach(T item in itemsArray)
            if(Contains(item))
                throw new ArgumentException($"Value {item} already present.");

        foreach(T item in itemsArray)
            represents[item] = itemsArray[0];

        if(itemsArray.Length > 0)
            ++Count;

        return this;
    }

    /// <summary>Adds new elements to the existing set represented by another element.</summary>
    /// <param name="items">The new elements.</param>
    /// <param name="represent">The represent of the set.</param>
    /// <returns><c>this</c> for method chaining.</returns>
    /// <exception cref="ArgumentException">If any of the elements is already present.</exception>
    /// <exception cref="KeyNotFoundException">If the represent is not present.</exception>
    public DisjointSets<T> Add(IEnumerable<T> items, T represent)
    {
        foreach(T item in items)
            if(Contains(item))
                throw new ArgumentException($"Value {item} already present.");

        foreach(T item in items)
            represents[item] = this[represent];

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
    /// <returns><c>true</c> if the elements are in the same set, otherwise <c>false</c>.</returns>
    /// <exception cref="KeyNotFoundException">If either element is not present.</exception>
    public bool IsSameSet(T item1, T item2) => this[item1].Equals(this[item2]);

    private static void validateDuplicates(T[][] setsArray)
    {
        T[] duplicates = setsArray.SelectMany(s => s)
                                  .GroupBy(e => e)
                                  .Where(group => group.Count() != 1)
                                  .Select(group => group.Key)
                                  .ToArray();

        if(duplicates.Length > 0)
            throw new ArgumentException(
                $"Duplicate elements found: {string.Join(", ", duplicates)}");
    }
}
