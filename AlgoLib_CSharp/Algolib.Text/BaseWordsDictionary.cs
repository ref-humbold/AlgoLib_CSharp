// Structure of base words dictionary using Karp-Miller-Rosenberg algorithm
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Text
{
    public class BaseWordsDictionary
    {
        private readonly Dictionary<Tuple<int, int>, int> factors = new Dictionary<Tuple<int, int>, int>();

        public string Text { get; }

        public BaseWordsDictionary(string text)
        {
            Text = text;
            create();
        }

        public Tuple<int, int> Code(int startIndex) => Code(startIndex, Text.Length);

        public Tuple<int, int> Code(int startIndex, int endIndex)
        {
            if(startIndex < 0 || startIndex >= Text.Length)
                throw new IndexOutOfRangeException($"Index out of range {startIndex}");

            if(endIndex < 0 || endIndex > Text.Length)
                throw new IndexOutOfRangeException($"Index out of range {endIndex}");

            if(endIndex <= startIndex)
                return Tuple.Create(0, 0);

            int code;

            if(factors.TryGetValue(Tuple.Create(startIndex, endIndex), out code))
                return Tuple.Create(code, 0);

            int n = getMaxLength(endIndex - startIndex);
            return Tuple.Create(factors[Tuple.Create(startIndex, startIndex + n)],
                           factors[Tuple.Create(endIndex - n, endIndex)]);
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
                                   (i, length) => new int[] { factors[Tuple.Create(i, i + length / 2)],
                                                              factors[Tuple.Create(i + length / 2, i + length)],
                                                              i, i + length });
            }
        }

        // Encodes substring of given length using already counted factors
        private int extend(int length, int codeValue, Func<int, int, int[]> func)
        {
            Tuple<int, int> previousCode = Tuple.Create(0, 0);
            List<int[]> codes = Enumerable.Range(0, Text.Length - length + 1)
                .Select(i => func.Invoke(i, length))
                .OrderBy(c => c, new CodesComparer())
                .ToList();

            foreach(int[] code in codes)
            {
                Tuple<int, int> codeTuple = Tuple.Create(code[0], code[1]);

                if(!Equals(previousCode, codeTuple))
                {
                    ++codeValue;
                    previousCode = codeTuple;
                }

                factors[Tuple.Create(code[2], code[3])] = codeValue;
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
