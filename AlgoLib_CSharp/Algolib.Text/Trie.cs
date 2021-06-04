// Structure of trie tree
using System.Collections.Generic;

namespace Algolib.Text
{
    public class Trie
    {
        private TrieNode tree = new TrieNode();

        public int Count
        {
            get;
            private set;
        }

        public Trie() => Count = 0;

        public Trie(IEnumerable<string> texts) : this()
        {
            foreach(string text in texts)
                Add(text);
        }

        public void Add(string text)
        {
            TrieNode node = tree;

            foreach(char character in text)
            {
                node.Add(character, new TrieNode());
                node = node[character];
            }

            if(!node.Terminus)
            {
                ++Count;
                node.Terminus = true;
            }
        }

        public void Clear()
        {
            tree = new TrieNode();
            Count = 0;
        }

        public bool Contains(string text)
        {
            TrieNode node = tree;

            foreach(char character in text)
            {
                node = node[character];

                if(node == null)
                    return false;
            }

            return node.Terminus;
        }

        public void Remove(string text)
        {
            if(text.Length > 0)
                removeNode(text, tree, 0);
        }

        private bool removeNode(string text, TrieNode node, int i)
        {
            if(i == text.Length && node.Terminus)
            {
                --Count;
                node.Terminus = false;
            }
            else if(i < text.Length)
            {
                TrieNode nextNode = node[text[i]];

                if(nextNode != null && removeNode(text, nextNode, i + 1))
                    node.Remove(text[i]);
            }

            return !node.Terminus && node.Count == 0;
        }

        private class TrieNode
        {
            private readonly Dictionary<char, TrieNode> children = new Dictionary<char, TrieNode>();

            internal int Count => children.Count;

            internal bool Terminus
            {
                get;
                set;
            }

            internal TrieNode() => Terminus = false;

            internal TrieNode this[char c]
            {
                get
                {
                    children.TryGetValue(c, out TrieNode node);
                    return node;
                }
            }

            internal void Add(char c, TrieNode node) => children.TryAdd(c, node);

            internal void Remove(char c) => children.Remove(c);
        }
    }
}
