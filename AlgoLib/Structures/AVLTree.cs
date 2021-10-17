using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Structures
{
    public class AVLTree<T> : ISet<T>, IReadOnlyCollection<T>
    {
        private readonly IComparer<T> comparer;
        private readonly AVLHeaderNode<T> tree = new AVLHeaderNode<T>();

        public int Count
        {
            get; private set;
        }

        public bool IsReadOnly => false;

        private AVLInnerNode<T> Root
        {
            get => tree.Parent;
            set => tree.Parent = value;
        }

        public AVLTree(IComparer<T> comparer = null) => this.comparer = comparer ?? Comparer<T>.Default;

        public AVLTree(IEnumerable<T> enumerable, IComparer<T> comparer = null) : this(comparer)
        {
            foreach(T item in enumerable)
                _ = Add(item);
        }

        public override string ToString()
        {
            string repr = "{|";
            IEnumerator<T> enumerator = GetEnumerator();
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

        public bool Add(T item)
        {
            AVLInnerNode<T> node_parent =
                findNode(item, (n, e) => search(n, e) == null
                                         || comparer.Compare(search(n, e).Element, e) == 0);

            if(node_parent == null)
            {
                Root = new AVLInnerNode<T>(item);
                ++Count;
                return true;
            }

            AVLInnerNode<T> theNode = search(node_parent, item);

            if(theNode != null)
                return false;

            AVLInnerNode<T> newNode = new AVLInnerNode<T>(item);

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

        public bool Contains(T item) =>
            Count > 0 && findNode(item, (node, elem) => Equals(node.Element, elem)) != null;

        public void CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();

        public void ExceptWith(IEnumerable<T> other)
        {
            foreach(T e in other)
                _ = Remove(e);
        }

        public IEnumerator<T> GetEnumerator() => new AVLEnumerator(this);

        public void IntersectWith(IEnumerable<T> other)
        {
            HashSet<T> otherSet = other.ToHashSet();

            ExceptWith(this.Where(e => !otherSet.Contains(e)).ToList());
        }

        public bool IsProperSubsetOf(IEnumerable<T> other) => IsSubsetOf(other) && !IsSupersetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) => IsSupersetOf(other) && !IsSubsetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other) => this.All(e => other.Contains(e));

        public bool IsSupersetOf(IEnumerable<T> other) => other.All(e => Contains(e));

        public bool Overlaps(IEnumerable<T> other) => other.Any(e => Contains(e));

        public bool Remove(T item)
        {
            AVLInnerNode<T> node = findNode(item, (n, elem) => Equals(n.Element, elem));

            if(node == null)
                return false;

            deleteNode(node);
            return true;
        }

        public bool SetEquals(IEnumerable<T> other) => IsSubsetOf(other) && IsSupersetOf(other);

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            foreach(T e in other)
                _ = Contains(e) ? Remove(e) : Add(e);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            foreach(T e in other)
                _ = Add(e);
        }

        void ICollection<T>.Add(T item) => Add(item);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private bool isLeftChild(AVLInnerNode<T> node) =>
            node.Parent.Height > 0 && node.Parent.Left == node;

        private bool isRightChild(AVLInnerNode<T> node) =>
            node.Parent.Height > 0 && node.Parent.Right == node;

        // Determines the subtree where given value might be present:
        // - node if element is in it
        // - left child if element is less than node's element
        // - right child if element is greater than node's element
        private AVLInnerNode<T> search(AVLInnerNode<T> node, T element)
        {
            int comparison = comparer.Compare(element, node.Element);

            return comparison < 0 ? node.Left : comparison > 0 ? node.Right : node;
        }

        // Searches for node that satisfies given predicate with given value.
        private AVLInnerNode<T> findNode(T element, Func<AVLInnerNode<T>, T, bool> predicate)
        {
            AVLInnerNode<T> node = Root;

            while(node != null && !predicate(node, element))
                node = search(node, element);

            return node;
        }

        // Removes inner node from the tree.
        private void deleteNode(AVLInnerNode<T> node)
        {
            if(node.Left != null && node.Right != null)
            {
                AVLInnerNode<T> succ = node.Right.Minimum;
                T temp = succ.Element;

                succ.Element = node.Element;
                node.Element = temp;
                deleteNode(succ);
            }
            else
            {
                AVLInnerNode<T> child = node.Left ?? node.Right;

                if(node.Parent.Height > 0)
                {
                    IAVLNode<T> nodeParent = node.Parent;

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
        private void replaceNode(AVLInnerNode<T> node1, AVLInnerNode<T> node2)
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
        private void rotate(AVLInnerNode<T> node)
        {
            if(isRightChild(node))
            {
                AVLInnerNode<T> upperNode = node.Parent as AVLInnerNode<T>;

                upperNode.Right = node.Left;
                replaceNode(upperNode, node);
                node.Left = upperNode;
            }
            else if(isLeftChild(node))
            {
                AVLInnerNode<T> upperNode = node.Parent as AVLInnerNode<T>;

                upperNode.Left = node.Right;
                replaceNode(upperNode, node);
                node.Right = upperNode;
            }
        }

        // Restores balancing on a path from given node to the root.
        private void balance(IAVLNode<T> node)
        {
            while(node.Height > 0)
            {
                AVLInnerNode<T> theNode = node as AVLInnerNode<T>;
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
        private int countBalance(AVLInnerNode<T> node)
        {
            int leftHeight = node.Left?.Height ?? 0;
            int rightHeight = node.Right?.Height ?? 0;

            return leftHeight - rightHeight;
        }

        private interface IAVLNode<TItem>
        {
            int Height
            {
                get;
            }

            IAVLNode<TItem> Left
            {
                get; set;
            }

            IAVLNode<TItem> Right
            {
                get; set;
            }

            IAVLNode<TItem> Parent
            {
                get; set;
            }

            // Searches in its subtree for the node with minimal value.
            IAVLNode<TItem> Minimum
            {
                get;
            }

            // Searches in its subtree for the node with maximal value.
            IAVLNode<TItem> Maximum
            {
                get;
            }
        }

        private class AVLInnerNode<TItem> : IAVLNode<TItem>
        {
            private AVLInnerNode<TItem> left;
            private AVLInnerNode<TItem> right;

            public int Height
            {
                get; private set;
            }

            IAVLNode<TItem> IAVLNode<TItem>.Left
            {
                get => Left;
                set => Left = value as AVLInnerNode<TItem>;
            }

            IAVLNode<TItem> IAVLNode<TItem>.Right
            {
                get => Right;
                set => Right = value as AVLInnerNode<TItem>;
            }

            public IAVLNode<TItem> Parent
            {
                get; set;
            }

            IAVLNode<TItem> IAVLNode<TItem>.Minimum => Minimum;

            IAVLNode<TItem> IAVLNode<TItem>.Maximum => Maximum;

            // Value in the node.
            public TItem Element
            {
                get; set;
            }

            public AVLInnerNode<TItem> Left
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

            public AVLInnerNode<TItem> Right
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

            public AVLInnerNode<TItem> Minimum => Left == null ? this : Left.Minimum;

            public AVLInnerNode<TItem> Maximum => Right == null ? this : Right.Maximum;

            public AVLInnerNode(TItem element)
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

        private class AVLHeaderNode<TItem> : IAVLNode<TItem>
        {
            private AVLInnerNode<TItem> inner;

            public int Height => 0;

            IAVLNode<TItem> IAVLNode<TItem>.Parent
            {
                get => Parent;
                set => Parent = value as AVLInnerNode<TItem>;
            }

            public IAVLNode<TItem> Left
            {
                get => null;
                set
                {
                }
            }

            public IAVLNode<TItem> Right
            {
                get => null;
                set
                {
                }
            }

            public AVLInnerNode<TItem> Parent
            {
                get => inner;

                set
                {
                    inner = value;

                    if(inner != null)
                        inner.Parent = this;
                }
            }

            public IAVLNode<TItem> Minimum => Parent == null ? (IAVLNode<TItem>)this : Parent.Minimum;

            public IAVLNode<TItem> Maximum => Parent == null ? (IAVLNode<TItem>)this : Parent.Maximum;

            public AVLHeaderNode() => Parent = null;
        }

        private class AVLEnumerator : IEnumerator<T>
        {
            private readonly AVLTree<T> tree;
            private IAVLNode<T> currentNode;

            public T Current =>
                currentNode is AVLInnerNode<T> node ? node.Element : throw new InvalidOperationException();

            object IEnumerator.Current => Current;

            public AVLEnumerator(AVLTree<T> tree)
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
