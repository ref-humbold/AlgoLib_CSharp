using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Structures
{
    public class AVLTree<E> : ISet<E>, IReadOnlyCollection<E>
    {
        private readonly IComparer<E> comparer;
        private AVLHeaderNode<E> tree = new AVLHeaderNode<E>();

        public int Count { get; private set; }
        public bool IsReadOnly { get; }

        private AVLInnerNode<E> Root
        {
            get => tree.Parent;
            set => tree.Parent = value;
        }

        public bool Add(E item) => throw new NotImplementedException();

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public bool Contains(E item) => throw new NotImplementedException();

        public void CopyTo(E[] array, int arrayIndex) => throw new NotImplementedException();

        public void ExceptWith(IEnumerable<E> other) => throw new NotImplementedException();

        public IEnumerator<E> GetEnumerator() => throw new NotImplementedException();

        public void IntersectWith(IEnumerable<E> other) => throw new NotImplementedException();

        public bool IsProperSubsetOf(IEnumerable<E> other) => throw new NotImplementedException();

        public bool IsProperSupersetOf(IEnumerable<E> other) => throw new NotImplementedException();

        public bool IsSubsetOf(IEnumerable<E> other) => throw new NotImplementedException();

        public bool IsSupersetOf(IEnumerable<E> other) => throw new NotImplementedException();

        public bool Overlaps(IEnumerable<E> other) => throw new NotImplementedException();

        public bool Remove(E item) => throw new NotImplementedException();

        public bool SetEquals(IEnumerable<E> other) => throw new NotImplementedException();

        public void SymmetricExceptWith(IEnumerable<E> other) => throw new NotImplementedException();

        public void UnionWith(IEnumerable<E> other) => throw new NotImplementedException();

        void ICollection<E>.Add(E item) => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

        private bool isLeftChild(AVLInnerNode<E> node) =>
            node.Parent.Height > 0 && node.Parent.Left == node;

        private bool isRightChild(AVLInnerNode<E> node) =>
            node.Parent.Height > 0 && node.Parent.Right == node;

        private AVLInnerNode<E> search(AVLInnerNode<E> node, E element)
        {
            if(comparer.Compare(element, node.Element) < 0)
                return node.Left;

            if(comparer.Compare(element, node.Element) > 0)
                return node.Right;

            return node;
        }

        private interface IAVLNode<T>
        {
            int Height { get; }

            IAVLNode<T> Left { get; set; }

            IAVLNode<T> Right { get; set; }

            IAVLNode<T> Parent { get; set; }

            // Searches in its subtree for the node with minimal value.
            IAVLNode<T> Minimum { get; }

            // Searches in its subtree for the node with maximal value.
            IAVLNode<T> Maximum { get; }
        }

        private class AVLHeaderNode<T> : IAVLNode<T>
        {
            public int Height => 0;

            IAVLNode<T> IAVLNode<T>.Parent
            {
                get => Parent;
                set => Parent = value as AVLInnerNode<T>;
            }

            public IAVLNode<T> Left
            {
                get => null;
                set { }
            }

            public IAVLNode<T> Right
            {
                get => null;
                set { }
            }

            public AVLInnerNode<T> Parent { get; set; }

            public IAVLNode<T> Minimum => this;

            public IAVLNode<T> Maximum => this;

            public AVLHeaderNode() => Parent = null;
        }

        private class AVLInnerNode<T> : IAVLNode<T>
        {
            private AVLInnerNode<T> left;
            private AVLInnerNode<T> right;

            public int Height { get; private set; }

            IAVLNode<T> IAVLNode<T>.Left
            {
                get => Left;
                set => Left = value as AVLInnerNode<T>;
            }

            IAVLNode<T> IAVLNode<T>.Right
            {
                get => Right;
                set => Right = value as AVLInnerNode<T>;
            }

            public IAVLNode<T> Parent { get; set; }

            IAVLNode<T> IAVLNode<T>.Minimum => Minimum;

            IAVLNode<T> IAVLNode<T>.Maximum => Maximum;

            // Value in the node.
            public T Element { get; set; }

            public AVLInnerNode<T> Left
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

            public AVLInnerNode<T> Right
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

            public AVLInnerNode<T> Minimum => Left == null ? this : Left.Minimum;

            public AVLInnerNode<T> Maximum => Right == null ? this : Right.Maximum;

            public AVLInnerNode(T element)
            {
                left = null;
                right = null;
                Parent = null;
                Height = 1;
                Element = element;
            }

            private void countHeight()
            {
                int leftHeight = left == null ? 0 : left.Height;
                int rightHeight = right == null ? 0 : right.Height;

                Height = Math.Max(leftHeight, rightHeight) + 1;
            }
        }
    }
}
