using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Structures
{
    public class AVLTree<E> : ISet<E>, IReadOnlyCollection<E>
    {
        private readonly IComparer<E> comparer;
        private AVLNode<E> tree = null;

        public int Count { get; private set; }

        public bool IsReadOnly { get; }

        public AVLTree(IComparer<E> comparer = null)
        {
            this.comparer = comparer ?? Comparer<E>.Default;
        }

        public AVLTree(IEnumerable<E> elements, IComparer<E> comparer = null) : this(comparer)
        {
            foreach(E element in elements)
                _ = Add(element);
        }

        public IEnumerator<E> GetEnumerator() => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Contains(E element) => throw new NotImplementedException();

        public bool Add(E element) => throw new NotImplementedException();

        void ICollection<E>.Add(E element) => Add(element);

        public bool Remove(E element) => throw new NotImplementedException();

        public void Clear() => throw new NotImplementedException();

        public void CopyTo(E[] array, int arrayIndex) => throw new NotImplementedException();

        public void ExceptWith(IEnumerable<E> elements) => throw new NotImplementedException();

        public void IntersectWith(IEnumerable<E> elements) => throw new NotImplementedException();

        public bool IsProperSubsetOf(IEnumerable<E> elements) => throw new NotImplementedException();

        public bool IsProperSupersetOf(IEnumerable<E> elements) => throw new NotImplementedException();

        public bool IsSubsetOf(IEnumerable<E> elements) => throw new NotImplementedException();

        public bool IsSupersetOf(IEnumerable<E> elements) => throw new NotImplementedException();

        public bool Overlaps(IEnumerable<E> elements) => throw new NotImplementedException();

        public bool SetEquals(IEnumerable<E> elements) => throw new NotImplementedException();

        public void SymmetricExceptWith(IEnumerable<E> elements) => throw new NotImplementedException();

        public void UnionWith(IEnumerable<E> elements) => throw new NotImplementedException();

        private class AVLNode<T>
        {
            private AVLNode<T> left;
            private AVLNode<T> right;

            /// <summary>Value in the node</summary>
            internal T Element { get; set; }

            internal int Height { get; private set; }

            internal AVLNode<T> Left
            {
                get => left;

                set
                {
                    left = value;

                    if(left != null)
                        left.Parent = this;

                    countHeight();
                }
            }

            internal AVLNode<T> Right
            {
                get => right;

                set
                {
                    right = value;

                    if(right != null)
                        right.Parent = this;

                    countHeight();
                }
            }

            internal AVLNode<T> Parent { get; set; }

            internal AVLNode(T element) => Element = element;

            private void countHeight()
            {
                int leftHeight = left == null ? 0 : left.Height;
                int rightHeight = right == null ? 0 : right.Height;

                Height = Math.Max(leftHeight, rightHeight) + 1;
            }

            private AVLNode<T> minimum() => left == null ? this : left.minimum();

            private AVLNode<T> maximum() => right == null ? this : right.maximum();
        }
    }
}
