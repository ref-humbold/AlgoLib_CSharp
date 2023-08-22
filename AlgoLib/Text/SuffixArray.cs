// Structure of suffix array (with longest common prefix)
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Text
{
    public class SuffixArray
    {
        private readonly List<int> suffixArray;
        private readonly List<int> inverseArray = new();
        private readonly List<int> lcpArray = new();

        public string Text { get; }

        public int Count => Text.Length;

        public SuffixArray(string text)
        {
            Text = text;
            suffixArray = createArray(Text.Select(c => +c).ToList());
            initInverseArray();
            initLcpArray();
        }

        /// <summary>Gets text suffix at given index.</summary>
        /// <param name="index">Index in this suffix array.</param>
        /// <returns>Text suffix at the index.</returns>
        public string this[Index index]
        {
            get
            {
                int i = index.GetOffset(suffixArray.Count);

                return i < 0 || i >= Count
                    ? throw new IndexOutOfRangeException("Suffix array index out of range")
                    : Text[suffixArray[i]..];
            }
        }

        /// <summary>Finds suffix in text for given index in this suffix array.</summary>
        /// <param name="index">Index in the suffix array.</param>
        /// <returns>Index in the text where the suffix begins.</returns>
        public int IndexAt(Index index)
        {
            int i = index.GetOffset(suffixArray.Count);

            return i < 0 || i >= Count
                ? throw new IndexOutOfRangeException("Suffix array index out of range")
                : suffixArray[index];
        }

        /// <summary>Finds index in this suffix array for given text suffix.</summary>
        /// <param name="index">Index in the text where suffix begins.</param>
        /// <returns>Index of suffix in the suffix array.</returns>
        public int IndexOf(Index index)
        {
            int i = index.GetOffset(suffixArray.Count);

            return i < 0 || i >= Count
                ? throw new IndexOutOfRangeException("Text index out of range")
                : inverseArray[index];
        }

        /// <summary>Counts length of the longest common prefix of given suffixes.</summary>
        /// <param name="index1">Index in text where first suffix begins.</param>
        /// <param name="index2">Index in text where second suffix begins.</param>
        /// <returns>Length of the longest common prefix.</returns>
        public int CountLCP(Index index1, Index index2)
        {
            int i1 = index1.GetOffset(suffixArray.Count);
            int i2 = index2.GetOffset(suffixArray.Count);

            if(i1 < 0 || i1 >= Count || i2 < 0 || i2 >= Count)
                throw new IndexOutOfRangeException("Text index out of range");

            if(i1 == i2)
                return Count - i1;

            int j1 = Math.Min(inverseArray[i1], inverseArray[i2]);
            int j2 = Math.Max(inverseArray[i1], inverseArray[i2]);

            try
            {
                return Enumerable.Range(j1 + 1, j2 - j1)
                                 .Select(j => lcpArray[j])
                                 .Min();
            }
            catch(InvalidOperationException)
            {
                return lcpArray[j1 + 1];
            }
        }

        private static int getElement(List<int> v, int i) => i < v.Count ? v[i] : 0;

        private static bool lessOrEqual(params int[] elements)
        {
            for(int i = 0; i < elements.Length; i += 2)
                if(elements[i] < elements[i + 1])
                    return true;
                else if(elements[i] > elements[i + 1])
                    return false;

            return true;
        }

        private static List<int> merge(List<int> t0, List<int> sa0, List<int> t12, List<int> sa12)
        {
            var sa = new List<int>();
            int length2 = (t0.Count + 2) / 3, length1 = (t0.Count + 1) / 3;
            int index0 = 0, index12 = length2 - length1;

            while(index0 < sa0.Count && index12 < sa12.Count)
            {
                int pos12 = sa12[index12] < length2 ? sa12[index12] * 3 + 1 : (sa12[index12] - length2) * 3 + 2;
                int pos0 = sa0[index0];

                bool cond = sa12[index12] < length2
                    ? lessOrEqual(getElement(t0, pos12), getElement(t0, pos0),
                                  getElement(t12, sa12[index12] + length2), getElement(t12, pos0 / 3))
                    : lessOrEqual(getElement(t0, pos12), getElement(t0, pos0),
                                  getElement(t0, pos12 + 1), getElement(t0, pos0 + 1),
                                  getElement(t12, sa12[index12] - length2 + 1), getElement(t12, pos0 / 3 + length2));

                if(cond)
                {
                    sa.Add(pos12);
                    ++index12;
                }
                else
                {
                    sa.Add(pos0);
                    ++index0;
                }
            }

            while(index12 < sa12.Count)
            {
                sa.Add(sa12[index12] < length2 ? sa12[index12] * 3 + 1 : (sa12[index12] - length2) * 3 + 2);
                ++index12;
            }

            while(index0 < sa0.Count)
            {
                sa.Add(sa0[index0]);
                ++index0;
            }

            return sa;
        }

        private static void sortIndices(List<int> indices, List<int> values, int shift)
        {
            var buckets = new SortedDictionary<int, Queue<int>>();
            int j = 0;

            foreach(int i in indices)
            {
                int v = getElement(values, i + shift);

                buckets.TryAdd(v, new Queue<int>());
                buckets[v].Enqueue(i);
            }

            foreach(Queue<int> q in buckets.Values)
                while(q.Count > 0)
                {
                    indices[j] = q.Dequeue();
                    ++j;
                }
        }

        private void initInverseArray()
        {
            inverseArray.AddRange(Enumerable.Range(0, Count));

            for(int i = 0; i < Count; ++i)
                inverseArray[suffixArray[i]] = i;
        }

        private void initLcpArray()
        {
            lcpArray.AddRange(Enumerable.Range(0, Count));

            for(int i = 0, len = 0; i < Count; ++i)
            {
                if(inverseArray[i] >= 1)
                {
                    int j = suffixArray[inverseArray[i] - 1];

                    while(i + len < Count && j + len < Count && Text[i + len] == Text[j + len])
                        ++len;

                    lcpArray[inverseArray[i]] = len;
                }

                if(len > 0)
                    --len;
            }
        }

        private List<int> createArray(List<int> txt)
        {
            if(txt.Count < 2)
                return new List<int> { 0 };

            int length2 = (txt.Count + 2) / 3, length1 = (txt.Count + 1) / 3, length0 = txt.Count / 3;
            int length02 = length0 + length2;
            var indices12 = new List<int>();

            for(int i = 0; i < txt.Count + length2 - length1; ++i)
                if(i % 3 != 0)
                    indices12.Add(i);

            sortIndices(indices12, txt, 2);
            sortIndices(indices12, txt, 1);
            sortIndices(indices12, txt, 0);

            int code = 0;
            (int, int, int) last = (int.MaxValue, int.MaxValue, int.MaxValue);
            var text12 = Enumerable.Repeat(0, length02).ToList();

            foreach(int i in indices12)
            {
                (int, int, int) elems = (getElement(txt, i), getElement(txt, i + 1),
                                         getElement(txt, i + 2));

                if(last != elems)
                {
                    ++code;
                    last = elems;
                }

                if(i % 3 == 1)
                    text12[i / 3] = code;
                else
                    text12[i / 3 + length2] = code;
            }

            var sa0 = new List<int>();
            List<int> sa12;

            if(code < length02)
            {
                sa12 = createArray(text12);

                for(int i = 0; i < sa12.Count; ++i)
                    text12[sa12[i]] = i + 1;
            }
            else
            {
                sa12 = Enumerable.Repeat(0, length02).ToList();

                for(int i = 0; i < text12.Count; ++i)
                    sa12[text12[i] - 1] = i;
            }

            foreach(int i in sa12)
                if(i < length2)
                    sa0.Add(3 * i);

            sortIndices(sa0, txt, 0);
            return merge(txt, sa0, text12, sa12);
        }
    }
}
