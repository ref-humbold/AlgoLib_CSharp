using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgoLib.Text;

// Tests: Structure of trie tree.
[TestFixture]
public class TrieTests
{
    private readonly List<string> texts = ["abcd", "ab", "xyz"];
    private Trie testObject;

    [SetUp]
    public void SetUp() => testObject = new Trie(texts);

    [Test]
    public void Count_WhenEmpty_ThenZero()
    {
        // given
        testObject = new Trie();

        // when
        int result = testObject.Count;

        // then
        Assert.That(result, Is.Zero);
    }

    [Test]
    public void Count_WhenNotEmpty_ThenNumberOfTexts()
    {
        // when
        int result = testObject.Count;

        // then
        Assert.That(result, Is.EqualTo(texts.Count));
    }

    [Test]
    public void Add_WhenPresent_ThenNothingChanged()
    {
        // given
        var text = "abcd";

        // when
        testObject.Add(text);

        // then
        Assert.That(testObject.Contains(text), Is.True);
        Assert.That(testObject.Count, Is.EqualTo(texts.Count));
    }

    [Test]
    public void Add_WhenAbsent_ThenAdded()
    {
        // given
        var text = "abxx";

        // when
        testObject.Add(text);

        // then
        Assert.That(testObject.Contains(text), Is.True);
        Assert.That(testObject.Count, Is.EqualTo(texts.Count + 1));
    }

    [Test]
    public void Add_WhenAbsentPrefix_ThenAdded()
    {
        // given
        var text = "xy";

        // when
        testObject.Add(text);

        // then
        Assert.That(testObject.Contains(text), Is.True);
        Assert.That(testObject.Count, Is.EqualTo(texts.Count + 1));
    }

    [Test]
    public void AddRange_WhenPresentAndAbsent_ThenAbsentAdded()
    {
        // given
        List<string> textsToAdd = ["abxx", "x", "abcdef", "xyz"];

        // when
        testObject.AddRange(textsToAdd);

        // then
        foreach(string text in textsToAdd)
            Assert.That(testObject.Contains(text), Is.True);

        Assert.That(testObject.Count, Is.EqualTo(texts.Concat(textsToAdd).Distinct().Count()));
    }

    [Test]
    public void Clear_WhenNotEmpty_ThenEmpty()
    {
        // when
        testObject.Clear();

        // then
        Assert.That(testObject.Count, Is.Zero);
    }

    [Test]
    public void Contains_WhenPresent_ThenTrue()
    {
        // when
        bool result = testObject.Contains("abcd");

        // then
        Assert.That(result, Is.True);
    }

    [Test]
    public void Contains_WhenAbsent_ThenFalse()
    {
        // when
        bool result = testObject.Contains("abxx");

        // then
        Assert.That(result, Is.False);
    }

    [Test]
    public void Contains_WhenAbsentPrefix_ThenFalse()
    {
        // when
        bool result = testObject.Contains("xy");

        // then
        Assert.That(result, Is.False);
    }

    [Test]
    public void Remove_WhenPresent_ThenRemoved()
    {
        // given
        var text = "abcd";

        // when
        testObject.Remove(text);

        // then
        Assert.That(testObject.Contains(text), Is.False);
        Assert.That(testObject.Count, Is.EqualTo(texts.Count - 1));
    }

    [Test]
    public void Remove_WhenAbsent_ThenNothingChanged()
    {
        // given
        var text = "abxx";

        // when
        testObject.Remove(text);

        // then
        Assert.That(testObject.Contains(text), Is.False);
        Assert.That(testObject.Count, Is.EqualTo(texts.Count));
    }

    [Test]
    public void Remove_WhenAbsentPrefix_ThenNothingChanged()
    {
        // given
        var text = "xy";

        // when
        testObject.Remove(text);

        // then
        Assert.That(testObject.Contains("xyz"), Is.True);
        Assert.That(testObject.Contains(text), Is.False);
        Assert.That(testObject.Count, Is.EqualTo(texts.Count));
    }

    [Test]
    public void RemoveRange_WhenPresentAndAbsent_ThenPresentRemoved()
    {
        // given
        List<string> textsToRemove = ["abxx", "x", "abcdef", "xyz"];

        // when
        testObject.RemoveRange(textsToRemove);

        // then
        foreach(string text in textsToRemove)
            Assert.That(testObject.Contains(text), Is.False);

        Assert.That(testObject.Count, Is.EqualTo(texts.Count(t => !textsToRemove.Contains(t))));
    }
}
