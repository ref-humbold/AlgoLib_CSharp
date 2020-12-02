﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Structures
{
    public class AVLTree<E> : ISet<E>, IReadOnlyCollection<E>
    {
        private readonly IComparer<E> comparer;
        private readonly AVLHeaderNode<E> tree = new AVLHeaderNode<E>();

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

        public bool Contains(E item) =>
            Count > 0 && findNode(item, (node, elem) => Equals(node.Element, elem)) != null;

        public void CopyTo(E[] array, int arrayIndex) => throw new NotImplementedException();

        public void ExceptWith(IEnumerable<E> other) => throw new NotImplementedException();

        public IEnumerator<E> GetEnumerator() => throw new NotImplementedException();

        public void IntersectWith(IEnumerable<E> other) => throw new NotImplementedException();

        public bool IsProperSubsetOf(IEnumerable<E> other) => throw new NotImplementedException();

        public bool IsProperSupersetOf(IEnumerable<E> other) => throw new NotImplementedException();

        public bool IsSubsetOf(IEnumerable<E> other) => throw new NotImplementedException();

        public bool IsSupersetOf(IEnumerable<E> other) => throw new NotImplementedException();

        public bool Overlaps(IEnumerable<E> other) => throw new NotImplementedException();

        public bool Remove(E item)
        {
            AVLInnerNode<E> node = findNode(item, (n, elem) => Equals(n.Element, elem));

            if(node == null)
                return false;

            deleteNode(node);
            return true;
        }

        public bool SetEquals(IEnumerable<E> other) => throw new NotImplementedException();

        public void SymmetricExceptWith(IEnumerable<E> other) => throw new NotImplementedException();

        public void UnionWith(IEnumerable<E> other) => throw new NotImplementedException();

        void ICollection<E>.Add(E item) => Add(item);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private bool isLeftChild(AVLInnerNode<E> node) =>
            node.Parent.Height > 0 && node.Parent.Left == node;

        private bool isRightChild(AVLInnerNode<E> node) =>
            node.Parent.Height > 0 && node.Parent.Right == node;

        // Determines the subtree where given value might be present:
        // - node if element is in it
        // - left child if element is less than node's element
        // - right child if element is greater than node's element
        private AVLInnerNode<E> search(AVLInnerNode<E> node, E element)
        {
            if(comparer.Compare(element, node.Element) < 0)
                return node.Left;

            if(comparer.Compare(element, node.Element) > 0)
                return node.Right;

            return node;
        }

        // Searches for node that satisfies specified predicate with specified value.
        private AVLInnerNode<E> findNode(E element, Func<AVLInnerNode<E>, E, bool> predicate)
        {
            AVLInnerNode<E> node = Root;

            while(node != null && !predicate(node, element))
                node = search(node, element);

            return node;
        }

        // Removes inner node from the tree.
        private void deleteNode(AVLInnerNode<E> node)
        {
            if(node.Left != null && node.Right != null)
            {
                AVLInnerNode<E> succ = node.Right.Minimum;
                E temp = succ.Element;

                succ.Element = node.Element;
                node.Element = temp;
                deleteNode(succ);
            }
            else
            {
                AVLInnerNode<E> child = node.Left ?? node.Right;

                if(node.Parent.Height > 0)
                {
                    IAVLNode<E> nodeParent = node.Parent;

                    replaceNode(node, child);
                    balance(nodeParent);
                }
                else
                    Root = child;

                node.Left = null;
                node.Right = null;
                --Count;
            }
        }

        // Replaces the subtree rooted in one node with subtree of another node.
        private void replaceNode(AVLInnerNode<E> node1, AVLInnerNode<E> node2)
        {
            if(isLeftChild(node1))
                node1.Parent.Left = node2;
            else if(isRightChild(node1))
                node1.Parent.Right = node2;
            else
                Root = node2;

            node1.Parent = null;
        }

        // Rotates the node along the edge to its parent.
        private void rotate(AVLInnerNode<E> node)
        {
            if(isRightChild(node))
            {
                AVLInnerNode<E> upperNode = node.Parent as AVLInnerNode<E>;

                upperNode.Right = node.Left;
                replaceNode(upperNode, node);
                node.Left = upperNode;
            }
            else if(isLeftChild(node))
            {
                AVLInnerNode<E> upperNode = node.Parent as AVLInnerNode<E>;

                upperNode.Left = node.Right;
                replaceNode(upperNode, node);
                node.Right = upperNode;
            }
        }

        // Restores balancing on a path from specified node to the root.
        private void balance(IAVLNode<E> node)
        {
            while(node.Height > 0)
            {
                AVLInnerNode<E> theNode = node.Parent as AVLInnerNode<E>;
                int newBalance = countBalance(theNode);

                if(newBalance >= 2)
                {
                    if(countBalance(theNode.Left) > 0)
                        rotate(theNode.Left);
                    else if(countBalance(theNode.Left) < 0)
                    {
                        rotate(theNode.Left.Right);
                        rotate(theNode.Left);
                    }
                }
                else if(newBalance <= -2)
                {
                    if(countBalance(theNode.Right) < 0)
                        rotate(theNode.Right);
                    else if(countBalance(theNode.Right) > 0)
                    {
                        rotate(theNode.Right.Left);
                        rotate(theNode.Right);
                    }
                }

                node = theNode.Parent;
            }
        }

        // Counts current node balance.
        private int countBalance(AVLInnerNode<E> node)
        {
            int leftHeight = node.Left == null ? 0 : node.Left.Height;
            int rightHeight = node.Right == null ? 0 : node.Right.Height;

            return leftHeight - rightHeight;
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