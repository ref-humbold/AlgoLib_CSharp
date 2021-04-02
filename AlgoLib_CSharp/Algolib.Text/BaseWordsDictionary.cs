﻿// Structure of base words dictionary using Karp-Miller-Rosenberg algorithm
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Text
{
    public class BaseWordsDictionary
    {
        private readonly Dictionary<(int, int), int> factors = new Dictionary<(int, int), int>();

        public string Text
        {
            get;
        }

        public BaseWordsDictionary(string text)
        {
            Text = text;
            create();
        }

        /// <summary>Retrieves code of a substring denoted by specified range.</summary>
        /// <param name="range">Range in the text</param>
        /// <returns>Code of the substring</returns>
        /// <exception cref="IndexOutOfRangeException">If start or end of the range are invalid</exception>
        public (int, int) this[Range range]
        {
            get
            {
                int startIndex = range.Start.GetOffset(Text.Length);
                int endIndex = range.End.GetOffset(Text.Length);

                if(startIndex >= Text.Length)
                    throw new IndexOutOfRangeException($"Index out of range {range.Start}");

                if(endIndex > Text.Length)
                    throw new IndexOutOfRangeException($"Index out of range {range.End}");

                if(endIndex <= startIndex)
                    return (0, 0);

                if(factors.TryGetValue((startIndex, endIndex), out int code))
                    return (code, 0);

                int n = getMaxLength(endIndex - startIndex);
                return (factors[(startIndex, startIndex + n)],
                         factors[(endIndex - n, endIndex)]);
            }
        }

        // Builds a base words map using Karp-Miller-Rosenberg algorithm
        private void create()
        {
            int codeValue = extend(1, 0, (i, length) => new int[] { Text[i],
                                                                    1 + Text[i],
                                                                    i, i + length });

            for(int currentLength = 2; currentLength <= Text.Length; currentLength *= 2)
            {
                codeValue = extend(currentLength, codeValue,
                                   (i, length) => new int[] { factors[(i, i + length / 2)],
                                                              factors[(i + length / 2, i + length)],
                                                              i, i + length });
            }
        }

        // Encodes substring of given length using already counted factors
        private int extend(int length, int codeValue, Func<int, int, int[]> func)
        {
            (int, int) previousCode = (0, 0);
            List<int[]> codes = Enumerable.Range(0, Text.Length - length + 1)
                .Select(i => func.Invoke(i, length))
                .OrderBy(c => c, new CodesComparer())
                .ToList();

            foreach(int[] code in codes)
            {
                (int, int) codePair = (code[0], code[1]);

                if(!Equals(previousCode, codePair))
                {
                    ++codeValue;
                    previousCode = codePair;
                }

                factors[(code[2], code[3])] = codeValue;
            }

            return codeValue;
        }

        private int getMaxLength(int n)
        {
            int prev = 0;
            int power = 1;

            while(power < n)
            {
                prev = power;
                power *= 2;
            }

            return prev;
        }

        private class CodesComparer : Comparer<int[]>
        {
            public override int Compare(int[] a1, int[] a2)
            {
                for(int i = 0; i < Math.Min(a1.Length, a2.Length); ++i)
                {
                    int compareInts = a1[i].CompareTo(a2[i]);

                    if(compareInts != 0)
                        return compareInts;
                }

                return 0;
            }
        }
    }
}
