using System;
using NUnit.Framework;

namespace AlgoLib.Text;

// Tests: Algorithms for edit distance.
[TestFixture]
public class EditDistanceTest
{
    private const double Precision = 1e-6;

    #region CountLevenshtein

    [Test]
    public void CountLevenshtein_WhenDifferentText_ThenDistance()
    {
        // given
        var source = "qwertyuiop";
        var destination = "wertzuiopsx";

        // when
        double result = source.CountLevenshtein(destination);

        // then
        Assert.That(result, Is.EqualTo(4.0).Within(Precision));
    }

    [Test]
    public void CountLevenshtein_WhenSameText_ThenZero()
    {
        // given
        var text = "qwertyuiop";

        // when
        double result = text.CountLevenshtein(text);

        // then
        Assert.That(result, Is.Zero);
    }

    [Test]
    public void CountLevenshtein_WhenEmptySource_ThenSumOfInsertions()
    {
        // given
        var text = "qwertyuiop";
        var insertionCost = 2.0;

        // when
        double result = string.Empty.CountLevenshtein(text, insertionCost);

        // then
        Assert.That(result, Is.EqualTo(text.Length * insertionCost).Within(Precision));
    }

    [Test]
    public void CountLevenshtein_WhenEmptyDestination_ThenSumOfDeletions()
    {
        // given
        var text = "qwertyuiop";
        var deletionCost = 2.0;

        // when
        double result = text.CountLevenshtein(string.Empty, 1.0, deletionCost);

        // then
        Assert.That(result, Is.EqualTo(text.Length * deletionCost).Within(Precision));
    }

    [Test]
    public void CountLevenshtein_WhenNegativeCost_ThenArgumentException()
    {
        // when
        Action action = () => "a".CountLevenshtein("b", 1.0, 1.0, -1.0);

        // then
        Assert.That(action, Throws.ArgumentException);
    }

    #endregion
    #region CountLcs

    [Test]
    public void CountLcs_WhenDifferentText_ThenDistance()
    {
        // given
        var source = "qwertyuiop";
        var destination = "wertzuiopsx";

        // when
        double result = source.CountLcs(destination);

        // then
        Assert.That(result, Is.EqualTo(5.0).Within(Precision));
    }

    [Test]
    public void CountLcs_WhenSameText_ThenZero()
    {
        // given
        var text = "qwertyuiop";

        // when
        double result = text.CountLcs(text);

        // then
        Assert.That(result, Is.Zero);
    }

    [Test]
    public void CountLcs_WhenEmptySource_ThenSumOfInsertions()
    {
        // given
        var text = "qwertyuiop";
        var insertionCost = 2.0;

        // when
        double result = string.Empty.CountLcs(text, insertionCost);

        // then
        Assert.That(result, Is.EqualTo(text.Length * insertionCost).Within(Precision));
    }

    [Test]
    public void CountLcs_WhenEmptyDestination_ThenSumOfDeletions()
    {
        // given
        var text = "qwertyuiop";
        var deletionCost = 2.0;

        // when
        double result = text.CountLcs(string.Empty, 1.0, deletionCost);

        // then
        Assert.That(result, Is.EqualTo(text.Length * deletionCost).Within(Precision));
    }

    [Test]
    public void CountLcs_WhenNegativeCost_ThenArgumentException()
    {
        // when
        Action action = () => "a".CountLcs("b", 1.0, -1.0);

        // then
        Assert.That(action, Throws.ArgumentException);
    }

    #endregion
    #region CountHamming

    [Test]
    public void CountHamming_WhenDifferentText_ThenDistance()
    {
        // given
        var source = "qwertyuiop";
        var destination = "qvertzuimp";
        var substitutionCost = 2.0;

        // when
        double result = source.CountHamming(destination, substitutionCost);

        // then
        Assert.That(result, Is.EqualTo(3 * substitutionCost).Within(Precision));
    }

    [Test]
    public void CountHamming_WhenSameText_ThenZero()
    {
        // given
        var text = "qwertyuiop";

        // when
        double result = text.CountHamming(text);

        // then
        Assert.That(result, Is.Zero);
    }

    [Test]
    public void CountHamming_WhenEmptyText_ThenZero()
    {
        // given
        var text = string.Empty;

        // when
        double result = text.CountHamming(text);

        // then
        Assert.That(result, Is.Zero);
    }

    [Test]
    public void CountHamming_WhenDifferentLength_ThenArgumentException()
    {
        // when
        Action action = () => "qwerty".CountHamming("asdf");

        // then
        Assert.That(action, Throws.ArgumentException);
    }

    [Test]
    public void CountHamming_WhenNegativeCost_ThenArgumentException()
    {
        // when
        Action action = () => "a".CountHamming("b", -1.0);

        // then
        Assert.That(action, Throws.ArgumentException);
    }

    #endregion
}
