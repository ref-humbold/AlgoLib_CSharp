// Structure of suffix array
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Text
{
    public class SuffixArray
    {
        private readonly List<int> suffixArray;
        private readonly List<int> inverseArray = new List<int>();
        private readonly List<int> lcpArray = new List<int>();

        public int Count
        {
            get;
        }

        public string Text
        {
            get;
        }

        public SuffixArray(string text)
        {
            Text = text;
            Count = text.Length;
            suffixArray = createArray(Text.Select(c => +c).ToList());
            initInverseArray();
            initLcpArray();
        }

        public string this[int i]
        {
            get
            {
                if(i < 0 || i >= Count)
                    throw new IndexOutOfRangeException("Suffix array index out of range");

                return Text.Substring(suffixArray[i]);
            }
        }

        /**
         * @param i index in suffix array
         * @return index in text where suffix begins
         */

        public int IndexAt(int i)
        {
            if(i < 0 || i >= Count)
                throw new IndexOutOfRangeException("Suffix array index out of range");

            return suffixArray[i];
        }

        /**
         * @param suf index in text where suffix begins
         * @return index of suffix in suffix array
         */

        public int IndexOf(int suf)
        {
            if(suf < 0 || suf >= Count)
                throw new IndexOutOfRangeException("Text index out of range");

            return inverseArray[suf];
        }

        /**
         * @param suf1 index in text where first suffix begins
         * @param suf2 index in text where second suffix begins
         * @return longest common prefix of both suffices
         */

        public int CountLCP(int suf1, int suf2)
        {
            if(suf1 < 0 || suf1 >= Count || suf2 < 0 || suf2 >= Count)
                throw new IndexOutOfRangeException("Text index out of range");

            if(suf1 == suf2)
                return Count - suf1;

            int i1 = Math.Min(inverseArray[suf1], inverseArray[suf2]);
            int i2 = Math.Max(inverseArray[suf1], inverseArray[suf2]);
            int res = lcpArray[i1 + 1];

            for(int i = i1 + 2; i <= i2; ++i)
                res = Math.Min(res, lcpArray[i]);

            return res;
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
            List<int> indices12 = new List<int>();

            for(int i = 0; i < txt.Count + length2 - length1; ++i)
                if(i % 3 != 0)
                    indices12.Add(i);

            sortIndices(indices12, txt, 2);
            sortIndices(indices12, txt, 1);
            sortIndices(indices12, txt, 0);

            int code = 0;
            (int, int, int) last = (int.MaxValue, int.MaxValue, int.MaxValue);
            List<int> text12 = Enumerable.Repeat(0, length02).ToList();

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

            List<int> sa0 = new List<int>();
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

        private List<int> merge(List<int> t0, List<int> sa0, List<int> t12,
                                    List<int> sa12)
        {
            List<int> sa = new List<int>();
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

        private void sortIndices(List<int> indices, List<int> values, int shift)
        {
            SortedDictionary<int, Queue<int>> buckets = new SortedDictionary<int, Queue<int>>();
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

        private int getElement(List<int> v, int i) => i < v.Count ? v[i] : 0;

        private bool lessOrEqual(params int[] elements)
        {
            for(int i = 0; i < elements.Length; i += 2)
                if(elements[i] < elements[i + 1])
                    return true;
                else if(elements[i] > elements[i + 1])
                    return false;

            return true;
        }
    }
}
