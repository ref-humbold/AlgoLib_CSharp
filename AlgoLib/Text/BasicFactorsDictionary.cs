using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Text;

/// <summary>Structure of basic factors dictionary using Karp-Miller-Rosenberg algorithm.</summary>
public class BasicFactorsDictionary
{
    private readonly Dictionary<(int, int), int> factors = [];

    public string Text { get; }

    public BasicFactorsDictionary(string text)
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

            if(startIndex < 0 || startIndex >= Text.Length || endIndex < 0 || endIndex > Text.Length)
                throw new IndexOutOfRangeException(
                    "Start and end indices must reflect the location inside the text");

            if(endIndex <= startIndex)
                throw new ArgumentException("The range is empty");

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

    // Builds basic factors dictionary using Karp-Miller-Rosenberg algorithm.
    private void create()
    {
        int codeValue = extend(1, 0, (i, _) => new ExtensionCode(Text[i], 1 + Text[i], i));

        for(var currentLength = 2; currentLength <= Text.Length; currentLength *= 2)
            codeValue = extend(
                currentLength, codeValue,
                (i, length) => new ExtensionCode(
                    factors[(i, i + length / 2)], factors[(i + length / 2, i + length)], i));
    }

    // Encodes substring of given length using already counted factors.
    private int extend(int length, int codeValue, Func<int, int, ExtensionCode> func)
    {
        ExtensionCode[] codes = Enumerable.Range(0, Text.Length - length + 1)
                                          .Select(i => func.Invoke(i, length))
                                          .OrderBy(c => c)
                                          .Prepend(new ExtensionCode(0, 0, -1))
                                          .ToArray();

        for(var i = 1; i < codes.Length; ++i)
        {
            if(codes[i] != codes[i - 1])
                ++codeValue;

            factors[(codes[i].Index, codes[i].Index + length)] = codeValue;
        }

        return codeValue;
    }

    private readonly record struct ExtensionCode(int PrefixCode, int SuffixCode, int Index) :
        IComparable<ExtensionCode>
    {
        public bool Equals(ExtensionCode other) =>
            PrefixCode == other.PrefixCode && SuffixCode == other.SuffixCode;

        public override int GetHashCode() => HashCode.Combine(PrefixCode, SuffixCode);

        public int CompareTo(ExtensionCode other)
        {
            int comparePrefixCode = PrefixCode.CompareTo(other.PrefixCode);

            return comparePrefixCode != 0
                    ? comparePrefixCode
                    : SuffixCode.CompareTo(other.SuffixCode);
        }
    }
}
