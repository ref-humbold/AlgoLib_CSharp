using System;
using NUnit.Framework;

namespace AlgoLib.Text;

// Tests: Structure of suffix array.
[TestFixture]
public class SuffixArrayTests
{
    private const string Text = "mississippi";
    private SuffixArray testObject;

    [SetUp]
    public void SetUp() => testObject = new SuffixArray(Text);

    [Test]
    public void Text_ThenText()
    {
        // when
        string result = testObject.Text;

        // then
        Assert.That(result, Is.EqualTo(Text));
    }

    [Test]
    public void Count_ThenNumberOfElements()
    {
        // when
        int result = testObject.Count;

        // then
        Assert.That(result, Is.EqualTo(11));
    }

    [Test]
    public void Indexer_WhenInRange_ThenSuffix()
    {
        // when
        string result0 = testObject[0];
        string result1 = testObject[3];
        string result2 = testObject[6];
        string result3 = testObject[^2];

        // then
        Assert.That(result0, Is.EqualTo("i"));
        Assert.That(result1, Is.EqualTo("ississippi"));
        Assert.That(result2, Is.EqualTo("ppi"));
        Assert.That(result3, Is.EqualTo("ssippi"));
    }

    [Test]
    public void Indexer_WhenOutOfRange_ThenIndexOutOfBoundsException()
    {
        // when
        Action action = () => _ = testObject[20];

        // then
        Assert.That(action, Throws.TypeOf<IndexOutOfRangeException>());
    }

    [Test]
    public void IndexAt_WhenInRange_ThenIndexInText()
    {
        // when
        int result0 = testObject.IndexAt(0);
        int result1 = testObject.IndexAt(3);
        int result2 = testObject.IndexAt(6);
        int result3 = testObject.IndexAt(^2);

        // then
        Assert.That(result0, Is.EqualTo(10));
        Assert.That(result1, Is.EqualTo(1));
        Assert.That(result2, Is.EqualTo(8));
        Assert.That(result3, Is.EqualTo(5));
    }

    [Test]
    public void IndexAt_WhenOutOfRange_ThenIndexOutOfBoundsException()
    {
        // when
        Action action = () => _ = testObject.IndexAt(20);

        // then
        Assert.That(action, Throws.TypeOf<IndexOutOfRangeException>());
    }

    [Test]
    public void IndexOf_WhenInRange_ThenIndexInArray()
    {
        // when
        int result0 = testObject.IndexOf(0);
        int result1 = testObject.IndexOf(3);
        int result2 = testObject.IndexOf(6);
        int result3 = testObject.IndexOf(^2);

        // then
        Assert.That(result0, Is.EqualTo(4));
        Assert.That(result1, Is.EqualTo(8));
        Assert.That(result2, Is.EqualTo(7));
        Assert.That(result3, Is.EqualTo(5));
    }

    [Test]
    public void IndexOf_WhenOutOfRange_ThenIndexOutOfBoundsException()
    {
        // when
        Action action = () => _ = testObject.IndexOf(20);

        // then
        Assert.That(action, Throws.TypeOf<IndexOutOfRangeException>());
    }

    [Test]
    public void CountLcp_WhenSameSuffix_ThenLengthOfPrefix()
    {
        // when
        int result = testObject.CountLcp(4, 4);

        // then
        Assert.That(result, Is.EqualTo(7));
    }

    [Test]
    public void CountLcp_WhenFirstEarlierThanSecondSuffix_ThenLengthOfPrefix()
    {
        // when
        int result = testObject.CountLcp(1, ^1);

        // then
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void CountLcp_WhenFirstFurtherThanSecondSuffix_ThenLengthOfPrefix()
    {
        // when
        int result = testObject.CountLcp(^2, 6);

        // then
        Assert.That(result, Is.Zero);
    }

    [Test]
    public void CountLcp_WhenSwapSuffix_ThenSameLength()
    {
        // when
        int result0 = testObject.CountLcp(2, 5);
        int result1 = testObject.CountLcp(5, 2);

        // then
        Assert.That(result0, Is.EqualTo(3));
        Assert.That(result1, Is.EqualTo(result0));
    }
}
