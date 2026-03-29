using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Text;

/// <summary>Structure of base words dictionary using Karp-Miller-Rosenberg algorithm.</summary>
public class BaseWordsDictionary
{
    private readonly Dictionary<(int, int), int> factors = [];

    public string Text { get; }

    public BaseWordsDictionary(string text)
    {
        Text = text;
        create();
    }

    /// <summary>Retrieves code of substring denoted by given indices range.</summary>
    /// <param name="range">The range of indices in the text.</param>
    /// <returns>The code of the substring.</returns>
    public (int CodeStart, int CodeEnd) this[Range range]
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
            return (factors[(startIndex, startIndex + n)], factors[(endIndex - n, endIndex)]);
        }
    }

    private static int getMaxLength(int n)
    {
        var prev = 0;
        var power = 1;

        while(power < n)
        {
            prev = power;
            power *= 2;
        }

        return prev;
    }

    // Builds base words dictionary using Karp-Miller-Rosenberg algorithm.
    private void create()
    {
        int codeValue = extend(
            1, 0, (i, _) =>
                new ExtensionCode(Text[i], 1 + Text[i], i));

        for(var currentLength = 2; currentLength <= Text.Length; currentLength *= 2)
            codeValue = extend(
                currentLength, codeValue,
                (i, length) => new ExtensionCode(
                    factors[(i, i + length / 2)], factors[(i + length / 2, i + length)], i));
    }

    // Encodes substring of given length using already counted factors.
    private int extend(int length, int codeValue, Func<int, int, ExtensionCode> func)
    {
        var previousCode = (0, 0);
        List<ExtensionCode> codes = Enumerable.Range(0, Text.Length - length + 1)
                                              .Select(i => func.Invoke(i, length))
                                              .OrderBy(c => c)
                                              .ToList();

        foreach(ExtensionCode code in codes)
        {
            (int, int) codePair = (code.PrefixCode, code.SuffixCode);

            if(!Equals(previousCode, codePair))
            {
                ++codeValue;
                previousCode = codePair;
            }

            factors[(code.Index, code.Index + length)] = codeValue;
        }

        return codeValue;
    }

    private record ExtensionCode(int PrefixCode, int SuffixCode, int Index) :
        IComparable<ExtensionCode>
    {
        public int CompareTo(ExtensionCode other)
        {
            int comparePrefixCode = PrefixCode.CompareTo(other.PrefixCode);

            if(comparePrefixCode != 0)
                return comparePrefixCode;

            int compareSuffixCode = SuffixCode.CompareTo(other.SuffixCode);

            if(compareSuffixCode != 0)
                return compareSuffixCode;

            return Index.CompareTo(other.Index);
        }
    }
}
