using System.Linq;
using NUnit.Framework;

namespace AlgoLib.Sequences;

// Tests: Algorithm for longest common subsequence.
[TestFixture]
public class LongestCommonSubsequenceTests
{
    [Test]
    public void CountLcsLength_WhenEmptyText_ThenZero()
    {
        // when
        int result = LongestCommonSubsequence.CountLcsLength("qwertyuiop", string.Empty);

        // then
        Assert.That(result, Is.Zero);
    }

    [Test]
    public void CountLcsLength_WhenRepeatedSingleElement_ThenOne()
    {
        // when
        int result = LongestCommonSubsequence.CountLcsLength("abcde", "eeee");

        // then
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void CountLcsLength_WhenSameCharacterText_ThenShorterLength()
    {
        // given
        string text = "xxxx";

        // when
        int result = LongestCommonSubsequence.CountLcsLength(text + text, text);

        // then
        Assert.That(result, Is.EqualTo(text.Length));
    }

    [Test]
    public void CountLcsLength_WhenSameText_ThenTextLength()
    {
        // given
        string text = "qwertyuiop";

        // when
        int result = LongestCommonSubsequence.CountLcsLength(text, text);

        // then
        Assert.That(result, Is.EqualTo(text.Length));
    }

    [Test]
    public void CountLcsLength_WhenSubtext_ThenSubtextLength()
    {
        // when
        int result = LongestCommonSubsequence.CountLcsLength("qwertyuiop", "zxqwertyasdfuiopcvb");

        // then
        Assert.That(result, Is.EqualTo("qwertyuiop".Length));
    }

    [Test]
    public void CountLcsLength_WhenDifferent_ThenZero()
    {
        // when
        int result = LongestCommonSubsequence.CountLcsLength("qwertyuiop", "asdfghjkl");

        // then
        Assert.That(result, Is.Zero);
    }

    [Test]
    public void CountLcsLength_WhenCommonSubtext_ThenCommonSubtextLength()
    {
        // when
        int result = LongestCommonSubsequence.CountLcsLength("qwertyuiop", "zxrtyasdfuiopcvb");

        // then
        Assert.That(result, Is.EqualTo("rtyuiop".Length));
    }

    [Test]
    public void CountLcsLength_WhenSameElementSequence_ThenShorterLength()
    {
        // given
        var sequence = Enumerable.Repeat(11, 25).ToList();

        // when
        int result = LongestCommonSubsequence.CountLcsLength(sequence, sequence.Concat(sequence).ToList());

        // then
        Assert.That(result, Is.EqualTo(sequence.Count));
    }

    [Test]
    public void CountLcsLength_WhenSameSequence_ThenSequenceLength()
    {
        // given
        var sequence = "qwertyuiop".Select(c => (int)c).ToList();

        // when
        int result = LongestCommonSubsequence.CountLcsLength(sequence, sequence);

        // then
        Assert.That(result, Is.EqualTo(sequence.Count));
    }

    [Test]
    public void CountLcsLength_WhenCommonSubsequence_ThenCommonSubsequenceLength()
    {
        // when
        int result = LongestCommonSubsequence.CountLcsLength(
            "qwertyuiop".Select(c => (int)c).ToList(),
            "zxrtyasdfuiopcvb".Select(c => (int)c).ToList());

        // then
        Assert.That(result, Is.EqualTo("rtyuiop".Length));
    }
}
