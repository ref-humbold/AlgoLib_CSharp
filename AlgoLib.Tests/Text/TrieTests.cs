using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Text;

// Tests: Structure of trie tree.
[TestFixture]
public class TrieTests
{
    private readonly List<string> texts = new() { "abcd", "ab", "xyz" };
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
        result.Should().Be(0);
    }

    [Test]
    public void Count_WhenNotEmpty_ThenNumberOfTexts()
    {
        // when
        int result = testObject.Count;
        // then
        result.Should().Be(texts.Count);
    }

    [Test]
    public void Add_WhenPresent_ThenNothingChanged()
    {
        // given
        string text = "abcd";
        // when
        testObject.Add(text);
        // then
        testObject.Contains(text).Should().BeTrue();
        testObject.Count.Should().Be(texts.Count);
    }

    [Test]
    public void Add_WhenAbsent_ThenAdded()
    {
        // given
        string text = "abxx";
        // when
        testObject.Add(text);
        // then
        testObject.Contains(text).Should().BeTrue();
        testObject.Count.Should().Be(texts.Count + 1);
    }

    [Test]
    public void Add_WhenAbsentPrefix_ThenAdded()
    {
        // given
        string text = "xy";
        // when
        testObject.Add(text);
        // then
        testObject.Contains(text).Should().BeTrue();
        testObject.Count.Should().Be(texts.Count + 1);
    }

    [Test]
    public void AddRange_WhenPresentAndAbsent_ThenAbsentAdded()
    {
        // given
        var textsToAdd = new List<string> { "abxx", "x", "abcdef", "xyz" };
        // when
        testObject.AddRange(textsToAdd);
        // then
        foreach(string text in textsToAdd)
            testObject.Contains(text).Should().BeTrue();

        testObject.Count.Should().Be(texts.Concat(textsToAdd).Distinct().Count());
    }

    [Test]
    public void Clear_WhenNotEmpty_ThenEmpty()
    {
        // when
        testObject.Clear();
        // then
        testObject.Count.Should().Be(0);
    }

    [Test]
    public void Contains_WhenPresent_ThenTrue()
    {
        // when
        bool result = testObject.Contains("abcd");
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void Contains_WhenAbsent_ThenFalse()
    {
        // when
        bool result = testObject.Contains("abxx");
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void Contains_WhenAbsentPrefix_ThenFalse()
    {
        // when
        bool result = testObject.Contains("xy");
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void Remove_WhenPresent_ThenRemoved()
    {
        // given
        string text = "abcd";
        // when
        testObject.Remove(text);
        // then
        testObject.Contains(text).Should().BeFalse();
        testObject.Count.Should().Be(texts.Count - 1);
    }

    [Test]
    public void Remove_WhenAbsent_ThenNothingChanged()
    {
        // given
        string text = "abxx";
        // when
        testObject.Remove(text);
        // then
        testObject.Contains(text).Should().BeFalse();
        testObject.Count.Should().Be(texts.Count);
    }

    [Test]
    public void Remove_WhenAbsentPrefix_ThenNothingChanged()
    {
        // given
        string text = "xy";
        // when
        testObject.Remove(text);
        // then
        testObject.Contains("xyz").Should().BeTrue();
        testObject.Contains(text).Should().BeFalse();
        testObject.Count.Should().Be(texts.Count);
    }

    [Test]
    public void RemoveRange_WhenPresentAndAbsent_ThenPresentRemoved()
    {
        // given
        var textsToRemove = new List<string> { "abxx", "x", "abcdef", "xyz" };
        // when
        testObject.RemoveRange(textsToRemove);
        // then
        foreach(string text in textsToRemove)
            testObject.Contains(text).Should().BeFalse();

        testObject.Count.Should().Be(texts.Where(t => !textsToRemove.Contains(t)).Count());
    }
}
