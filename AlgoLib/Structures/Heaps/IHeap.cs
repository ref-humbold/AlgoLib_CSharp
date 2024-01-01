using System;
using System.Collections.Generic;

namespace AlgoLib.Structures.Heaps;

public interface IHeap<T> : IEnumerable<T>
{
    /// <summary>Gets the comparer of this heap.</summary>
    /// <value>The comparer.</value>
    public IComparer<T> Comparer { get; }

    /// <summary>Gets the number of elements in this heap.</summary>
    /// <value>The number of elements.</value>
    public int Count { get; }

    /// <summary>Removes all elements from this heap.</summary>
    public void Clear();

    /// <summary>Retrieves minimal element from this heap.</summary>
    /// <returns>The minimal element.</returns>
    /// <exception cref="InvalidOperationException">If the heap is empty.</exception>
    public T Peek();

    /// <summary>
    /// Retrieves minimal element from this heap and copies it to the <c>result</c> parameter.
    /// </summary>
    /// <param name="result">The minimal element if it's present, otherwise the default value.</param>
    /// <returns><c>true</c> if the element exists, otherwise <c>false</c>.</returns>
    public bool TryPeek(out T result);

    /// <summary>Adds new element to this heap.</summary>
    /// <param name="item">The new element.</param>
    public void Push(T item);

    /// <summary>Adds new elements from given range to this pairing heap.</summary>
    /// <param name="items">The new elements.</param>
    public void PushRange(IEnumerable<T> items);

    /// <summary>Retrieves and removes minimal element from this pairing heap.</summary>
    /// <returns>The removed minimal element.</returns>
    /// <exception cref="InvalidOperationException">If the pairing heap is empty.</exception>
    public T Pop();

    /// <summary>
    /// Removes minimal element from this pairing heap and copies it to the <c>result</c> parameter.
    /// </summary>
    /// <param name="result">
    /// The removed minimal element if it's present, otherwise the default value.
    /// </param>
    /// <returns><c>true</c> if the element exists, otherwise <c>false</c>.</returns>
    public bool TryPop(out T result);
}
