using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Structures
{
    public class AvlTree<T> : ISet<T>, IReadOnlyCollection<T>
    {
        private readonly IComparer<T> comparer;
        private readonly AvlHeaderNode<T> tree = new();

        public int Count
        {
            get;
            private set;
        }

        public bool IsReadOnly => false;

        private AvlInnerNode<T> Root
        {
            get => tree.Parent;
            set => tree.Parent = value;
        }

        public AvlTree(IComparer<T> comparer = null) => this.comparer = comparer ?? Comparer<T>.Default;

        public AvlTree(IEnumerable<T> enumerable, IComparer<T> comparer = null)
            : this(comparer)
        {
            foreach(T item in enumerable)
                _ = Add(item);
        }

        public override string ToString() =>
            $"{{|{string.Join(", ", this.Select(elem => elem.ToString()))}|}}";

        public bool Add(T item)
        {
            AvlInnerNode<T> node_parent =
                findNode(item, (n, e) =>
                    search(n, e) == null || comparer.Compare(search(n, e).Element, e) == 0);

            if(node_parent == null)
            {
                Root = new AvlInnerNode<T>(item);
                ++Count;
                return true;
            }

            AvlInnerNode<T> theNode = search(node_parent, item);

            if(theNode != null)
                return false;

            var newNode = new AvlInnerNode<T>(item);

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

        public IEnumerator<T> GetEnumerator() => new AvlEnumerator(this);

        public void IntersectWith(IEnumerable<T> other)
        {
            var otherSet = other.ToHashSet();

            ExceptWith(this.Where(e => !otherSet.Contains(e)).ToList());
        }

        public bool IsProperSubsetOf(IEnumerable<T> other) =>
            IsSubsetOf(other) && !IsSupersetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) =>
            IsSupersetOf(other) && !IsSubsetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other) => this.All(e => other.Contains(e));

        public bool IsSupersetOf(IEnumerable<T> other) => other.All(e => Contains(e));

        public bool Overlaps(IEnumerable<T> other) => other.Any(e => Contains(e));

        public bool Remove(T item)
        {
            AvlInnerNode<T> node = findNode(item, (n, elem) => Equals(n.Element, elem));

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

        private static bool isLeftChild(AvlInnerNode<T> node) =>
            node.Parent.Height > 0 && node.Parent.Left == node;

        private static bool isRightChild(AvlInnerNode<T> node) =>
            node.Parent.Height > 0 && node.Parent.Right == node;

        // Counts current node balance.
        private static int countBalance(AvlInnerNode<T> node)
        {
            int leftHeight = node.Left?.Height ?? 0;
            int rightHeight = node.Right?.Height ?? 0;

            return leftHeight - rightHeight;
        }

        // Determines the subtree where given value might be present:
        // - node if element is in it
        // - left child if element is less than node's element
        // - right child if element is greater than node's element
        private AvlInnerNode<T> search(AvlInnerNode<T> node, T element)
        {
            int comparison = comparer.Compare(element, node.Element);

            return comparison < 0 ? node.Left : comparison > 0 ? node.Right : node;
        }

        // Searches for node that satisfies given predicate with given value.
        private AvlInnerNode<T> findNode(T element, Func<AvlInnerNode<T>, T, bool> predicate)
        {
            AvlInnerNode<T> node = Root;

            while(node != null && !predicate(node, element))
                node = search(node, element);

            return node;
        }

        // Removes inner node from the tree.
        private void deleteNode(AvlInnerNode<T> node)
        {
            if(node.Left != null && node.Right != null)
            {
                AvlInnerNode<T> succ = node.Right.Minimum;
                (node.Element, succ.Element) = (succ.Element, node.Element);
                deleteNode(succ);
            }
            else
            {
                AvlInnerNode<T> child = node.Left ?? node.Right;

                if(node.Parent.Height > 0)
                {
                    IAvlNode<T> nodeParent = node.Parent;

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
        private void replaceNode(AvlInnerNode<T> node1, AvlInnerNode<T> node2)
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
        private void rotate(AvlInnerNode<T> node)
        {
            if(isRightChild(node))
            {
                var upperNode = node.Parent as AvlInnerNode<T>;

                upperNode.Right = node.Left;
                replaceNode(upperNode, node);
                node.Left = upperNode;
            }
            else if(isLeftChild(node))
            {
                var upperNode = node.Parent as AvlInnerNode<T>;

                upperNode.Left = node.Right;
                replaceNode(upperNode, node);
                node.Right = upperNode;
            }
        }

        // Restores balancing on a path from given node to the root.
        private void balance(IAvlNode<T> node)
        {
            while(node.Height > 0)
            {
                var theNode = node as AvlInnerNode<T>;
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

        private interface IAvlNode<TItem>
        {
            int Height
            {
                get;
            }

            IAvlNode<TItem> Left
            {
                get; set;
            }

            IAvlNode<TItem> Right
            {
                get; set;
            }

            IAvlNode<TItem> Parent
            {
                get; set;
            }

            // Searches in its subtree for the node with minimal value.
            IAvlNode<TItem> Minimum
            {
                get;
            }

            // Searches in its subtree for the node with maximal value.
            IAvlNode<TItem> Maximum
            {
                get;
            }
        }

        private class AvlInnerNode<TItem> : IAvlNode<TItem>
        {
            private AvlInnerNode<TItem> left;
            private AvlInnerNode<TItem> right;

            public int Height
            {
                get; private set;
            }

            IAvlNode<TItem> IAvlNode<TItem>.Left
            {
                get => Left;
                set => Left = value as AvlInnerNode<TItem>;
            }

            IAvlNode<TItem> IAvlNode<TItem>.Right
            {
                get => Right;
                set => Right = value as AvlInnerNode<TItem>;
            }

            public IAvlNode<TItem> Parent
            {
                get; set;
            }

            IAvlNode<TItem> IAvlNode<TItem>.Minimum => Minimum;

            IAvlNode<TItem> IAvlNode<TItem>.Maximum => Maximum;

            // Value in the node.
            public TItem Element
            {
                get; set;
            }

            public AvlInnerNode<TItem> Left
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

            public AvlInnerNode<TItem> Right
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

            public AvlInnerNode<TItem> Minimum => Left == null ? this : Left.Minimum;

            public AvlInnerNode<TItem> Maximum => Right == null ? this : Right.Maximum;

            public AvlInnerNode(TItem element)
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

        private class AvlHeaderNode<TItem> : IAvlNode<TItem>
        {
            private AvlInnerNode<TItem> inner;

            public int Height => 0;

            IAvlNode<TItem> IAvlNode<TItem>.Parent
            {
                get => Parent;
                set => Parent = value as AvlInnerNode<TItem>;
            }

            public IAvlNode<TItem> Left
            {
                get => null;
                set
                {
                }
            }

            public IAvlNode<TItem> Right
            {
                get => null;
                set
                {
                }
            }

            public AvlInnerNode<TItem> Parent
            {
                get => inner;
                set
                {
                    inner = value;

                    if(inner != null)
                        inner.Parent = this;
                }
            }

            public IAvlNode<TItem> Minimum => Parent == null ? this : Parent.Minimum;

            public IAvlNode<TItem> Maximum => Parent == null ? this : Parent.Maximum;

            public AvlHeaderNode() => Parent = null;
        }

        private sealed class AvlEnumerator : IEnumerator<T>
        {
            private readonly AvlTree<T> tree;
            private IAvlNode<T> currentNode;

            public T Current =>
                currentNode is AvlInnerNode<T> node
                    ? node.Element
                    : throw new InvalidOperationException();

            object IEnumerator.Current => Current;

            public AvlEnumerator(AvlTree<T> tree)
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
