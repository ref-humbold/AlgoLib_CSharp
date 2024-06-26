﻿using System;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Text;

// Tests: Algorithms for edit distance.
[TestFixture]
public class EditDistanceTest
{
    private static readonly double Precision = 1e-6;

    #region CountLevenshtein

    [Test]
    public void CountLevenshtein_WhenDifferentText_ThenDistance()
    {
        // given
        string source = "qwertyuiop";
        string destination = "wertzuiopsx";

        // when
        double result = source.CountLevenshtein(destination);

        // then
        result.Should().BeApproximately(4.0, Precision);
    }

    [Test]
    public void CountLevenshtein_WhenSameText_ThenZero()
    {
        // given
        string text = "qwertyuiop";

        // when
        double result = text.CountLevenshtein(text);

        // then
        result.Should().Be(0.0);
    }

    [Test]
    public void CountLevenshtein_WhenEmptySource_ThenSumOfInsertions()
    {
        // given
        string text = "qwertyuiop";
        double insertionCost = 2.0;

        // when
        double result = string.Empty.CountLevenshtein(text, insertionCost, 1.0, 1.0);

        // then
        result.Should().BeApproximately(text.Length * insertionCost, Precision);
    }

    [Test]
    public void CountLevenshtein_WhenEmptyDestination_ThenSumOfDeletions()
    {
        // given
        string text = "qwertyuiop";
        double deletionCost = 2.0;

        // when
        double result = text.CountLevenshtein(string.Empty, 1.0, deletionCost, 1.0);

        // then
        result.Should().BeApproximately(text.Length * deletionCost, Precision);
    }

    [Test]
    public void CountLevenshtein_WhenNegativeCost_ThenArgumentException()
    {
        // when
        Action action = () => "a".CountLevenshtein("b", 1.0, 1.0, -1.0);

        // then
        action.Should().Throw<ArgumentException>();
    }

    #endregion
    #region CountLcs

    [Test]
    public void CountLcs_WhenDifferentText_ThenDistance()
    {
        // given
        string source = "qwertyuiop";
        string destination = "wertzuiopsx";

        // when
        double result = source.CountLcs(destination);

        // then
        result.Should().BeApproximately(5.0, Precision);
    }

    [Test]
    public void CountLcs_WhenSameText_ThenZero()
    {
        // given
        string text = "qwertyuiop";

        // when
        double result = text.CountLcs(text);

        // then
        result.Should().Be(0.0);
    }

    [Test]
    public void CountLcs_WhenEmptySource_ThenSumOfInsertions()
    {
        // given
        string text = "qwertyuiop";
        double insertionCost = 2.0;

        // when
        double result = string.Empty.CountLcs(text, insertionCost, 1.0);

        // then
        result.Should().BeApproximately(text.Length * insertionCost, Precision);
    }

    [Test]
    public void CountLcs_WhenEmptyDestination_ThenSumOfDeletions()
    {
        // given
        string text = "qwertyuiop";
        double deletionCost = 2.0;

        // when
        double result = text.CountLcs(string.Empty, 1.0, deletionCost);

        // then
        result.Should().BeApproximately(text.Length * deletionCost, Precision);
    }

    [Test]
    public void CountLcs_WhenNegativeCost_ThenArgumentException()
    {
        // when
        Action action = () => "a".CountLcs("b", 1.0, -1.0);

        // then
        action.Should().Throw<ArgumentException>();
    }

    #endregion
    #region CountHamming

    [Test]
    public void CountHamming_WhenDifferentText_ThenDistance()
    {
        // given
        string source = "qwertyuiop";
        string destination = "qvertzuimp";
        double substitutionCost = 2.0;

        // when
        double result = source.CountHamming(destination, substitutionCost);

        // then
        result.Should().BeApproximately(3 * substitutionCost, Precision);
    }

    [Test]
    public void CountHamming_WhenSameText_ThenZero()
    {
        // given
        string text = "qwertyuiop";

        // when
        double result = text.CountHamming(text);

        // then
        result.Should().Be(0.0);
    }

    [Test]
    public void CountHamming_WhenEmptyText_ThenZero()
    {
        // given
        string text = string.Empty;

        // when
        double result = text.CountHamming(text);

        // then
        result.Should().Be(0.0);
    }

    [Test]
    public void CountHamming_WhenDifferentLength_ThenArgumentException()
    {
        // when
        Action action = () => "qwerty".CountHamming("asdf");

        // then
        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void CountHamming_WhenNegativeCost_ThenArgumentException()
    {
        // when
        Action action = () => "a".CountHamming("b", -1.0);

        // then
        action.Should().Throw<ArgumentException>();
    }

    #endregion
}
