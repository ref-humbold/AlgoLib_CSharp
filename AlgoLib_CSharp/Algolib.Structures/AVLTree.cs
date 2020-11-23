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

        private AVLNode<E> Tree
        {
            get => tree;

            set
            {
                tree = value;

                if(tree != null)
                    tree.Parent = null;
            }
        }

        public AVLTree(IComparer<E> comparer = null) => this.comparer = comparer ?? Comparer<E>.Default;

        public AVLTree(IEnumerable<E> elements, IComparer<E> comparer = null) : this(comparer)
        {
            foreach(E element in elements)
                _ = Add(element);
        }

        public IEnumerator<E> GetEnumerator() => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Contains(E element)
            => Count > 0 && findNode(element, (node, elem) => Equals(node.Element, elem)) != null;

        public bool Add(E element) => throw new NotImplementedException();

        void ICollection<E>.Add(E element) => Add(element);

        public bool Remove(E element) => throw new NotImplementedException();

        public void Clear()
        {
            Tree = null;
            Count = 0;
        }

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

        private bool isLeftSon(AVLNode<E> node) => node.Parent != null && node.Parent.Left == node;

        private bool isRightSon(AVLNode<E> node) => node.Parent != null && node.Parent.Right == node;

        // Determines the subtree where given value might be present:
        // - node if element is in it
        // - left child if element is less than node's element
        // - right child if element is greater than node's element
        private AVLNode<E> search(AVLNode<E> node, E element)
        {
            if(Equals(element, node.Element))
                return node;

            int result = comparer.Compare(element, node.Element);

            if(result < 0)
                return node.Left;

            if(result > 0)
                return node.Right;

            return node;
        }

        // Searches for node that satisfies given predicate with given value.
        private AVLNode<E> findNode(E element, Func<AVLNode<E>, E, bool> predicate)
        {
            AVLNode<E> node = tree;

            while(node != null && !predicate.Invoke(node, element))
                node = search(node, element);

            return node;
        }

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
