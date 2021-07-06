using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Structures
{
    public class AVLTree<E> : ISet<E>, IReadOnlyCollection<E>
    {
        private readonly IComparer<E> comparer;
        private readonly AVLHeaderNode<E> tree = new AVLHeaderNode<E>();

        public int Count
        {
            get; private set;
        }

        public bool IsReadOnly
        {
            get;
        }

        private AVLInnerNode<E> Root
        {
            get => tree.Parent;
            set => tree.Parent = value;
        }

        public AVLTree(IComparer<E> comparer = null) => this.comparer = comparer ?? Comparer<E>.Default;

        public AVLTree(IEnumerable<E> enumerable, IComparer<E> comparer = null) : this(comparer)
        {
            foreach(E e in enumerable)
                _ = Add(e);
        }

        public override string ToString()
        {
            string repr = "{|";
            IEnumerator<E> enumerator = GetEnumerator();
            bool hasNext = enumerator.MoveNext();

            while(hasNext)
            {
                repr += enumerator.Current.ToString();
                hasNext = enumerator.MoveNext();

                if(hasNext)
                    repr += ", ";
            }

            return repr + "|}";
        }

        public bool Add(E item)
        {
            AVLInnerNode<E> node_parent =
                findNode(item, (n, e) => search(n, e) == null
                                         || comparer.Compare(search(n, e).Element, e) == 0);

            if(node_parent == null)
            {
                Root = new AVLInnerNode<E>(item);
                ++Count;
                return true;
            }

            AVLInnerNode<E> theNode = search(node_parent, item);

            if(theNode != null)
                return false;

            AVLInnerNode<E> newNode = new AVLInnerNode<E>(item);

            if(comparer.Compare(item, node_parent.Element) < 0)
                node_parent.Left = newNode;
            else
                node_parent.Right = newNode;

            balance(node_parent);
            ++Count;
            return true;
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public bool Contains(E item) =>
            Count > 0 && findNode(item, (node, elem) => Equals(node.Element, elem)) != null;

        public void CopyTo(E[] array, int arrayIndex) => throw new NotImplementedException();

        public void ExceptWith(IEnumerable<E> other)
        {
            foreach(E e in other)
                _ = Remove(e);
        }

        public IEnumerator<E> GetEnumerator() => new AVLEnumerator(this);

        public void IntersectWith(IEnumerable<E> other)
        {
            HashSet<E> otherSet = other.ToHashSet();

            ExceptWith(this.Where(e => !otherSet.Contains(e)).ToList());
        }

        public bool IsProperSubsetOf(IEnumerable<E> other) => IsSubsetOf(other) && !IsSupersetOf(other);

        public bool IsProperSupersetOf(IEnumerable<E> other) => IsSupersetOf(other) && !IsSubsetOf(other);

        public bool IsSubsetOf(IEnumerable<E> other) => this.All(e => other.Contains(e));

        public bool IsSupersetOf(IEnumerable<E> other) => other.All(e => Contains(e));

        public bool Overlaps(IEnumerable<E> other) => other.Any(e => Contains(e));

        public bool Remove(E item)
        {
            AVLInnerNode<E> node = findNode(item, (n, elem) => Equals(n.Element, elem));

            if(node == null)
                return false;

            deleteNode(node);
            return true;
        }

        public bool SetEquals(IEnumerable<E> other) => IsSubsetOf(other) && IsSupersetOf(other);

        public void SymmetricExceptWith(IEnumerable<E> other)
        {
            foreach(E e in other)
                _ = Contains(e) ? Remove(e) : Add(e);
        }

        public void UnionWith(IEnumerable<E> other)
        {
            foreach(E e in other)
                _ = Add(e);
        }

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

        // Searches for node that satisfies given predicate with given value.
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

        // Restores balancing on a path from given node to the root.
        private void balance(IAVLNode<E> node)
        {
            while(node.Height > 0)
            {
                AVLInnerNode<E> theNode = node as AVLInnerNode<E>;
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
            int leftHeight = node.Left?.Height ?? 0;
            int rightHeight = node.Right?.Height ?? 0;

            return leftHeight - rightHeight;
        }

        private interface IAVLNode<T>
        {
            int Height
            {
                get;
            }

            IAVLNode<T> Left
            {
                get; set;
            }

            IAVLNode<T> Right
            {
                get; set;
            }

            IAVLNode<T> Parent
            {
                get; set;
            }

            // Searches in its subtree for the node with minimal value.
            IAVLNode<T> Minimum
            {
                get;
            }

            // Searches in its subtree for the node with maximal value.
            IAVLNode<T> Maximum
            {
                get;
            }
        }

        private class AVLInnerNode<T> : IAVLNode<T>
        {
            private AVLInnerNode<T> left;
            private AVLInnerNode<T> right;

            public int Height
            {
                get; private set;
            }

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

            public IAVLNode<T> Parent
            {
                get; set;
            }

            IAVLNode<T> IAVLNode<T>.Minimum => Minimum;

            IAVLNode<T> IAVLNode<T>.Maximum => Maximum;

            // Value in the node.
            public T Element
            {
                get; set;
            }

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
                int leftHeight = left?.Height ?? 0;
                int rightHeight = right?.Height ?? 0;

                Height = 1 + Math.Max(leftHeight, rightHeight);
            }
        }

        private class AVLHeaderNode<T> : IAVLNode<T>
        {
            private AVLInnerNode<T> inner;

            public int Height => 0;

            IAVLNode<T> IAVLNode<T>.Parent
            {
                get => Parent;
                set => Parent = value as AVLInnerNode<T>;
            }

            public IAVLNode<T> Left
            {
                get => null;
                set
                {
                }
            }

            public IAVLNode<T> Right
            {
                get => null;
                set
                {
                }
            }

            public AVLInnerNode<T> Parent
            {
                get => inner;

                set
                {
                    inner = value;

                    if(inner != null)
                        inner.Parent = this;
                }
            }

            public IAVLNode<T> Minimum => Parent == null ? (IAVLNode<T>)this : Parent.Minimum;

            public IAVLNode<T> Maximum => Parent == null ? (IAVLNode<T>)this : Parent.Maximum;

            public AVLHeaderNode() => Parent = null;
        }

        private class AVLEnumerator : IEnumerator<E>
        {
            private readonly AVLTree<E> tree;
            private IAVLNode<E> currentNode;

            public E Current =>
                currentNode is AVLInnerNode<E> node ? node.Element : throw new InvalidOperationException();

            object IEnumerator.Current => Current;

            public AVLEnumerator(AVLTree<E> tree)
            {
                this.tree = tree;
                Reset();
            }

            public bool MoveNext()
            {
                if(currentNode == null)
                    return false;

                if(currentNode.Height > 0)
                {
                    if(currentNode.Right != null)
                        currentNode = currentNode.Right.Minimum;
                    else
                    {
                        while(currentNode.Parent.Height > 0 && currentNode.Parent.Left != currentNode)
                            currentNode = currentNode.Parent;

                        currentNode = currentNode.Parent;
                    }
                }
                else
                    currentNode = currentNode.Minimum;

                if((currentNode?.Height ?? 0) == 0)
                {
                    currentNode = null;
                    return false;
                }

                return true;
            }

            public void Reset() => currentNode = tree.tree;

            public void Dispose()
            {
            }
        }
    }
}
