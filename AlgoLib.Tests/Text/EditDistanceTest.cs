﻿using System;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Text
{
    [TestFixture]
    public class EditDistanceTest
    {
        private const double precision = 1e-6;

        #region CountLevenshtein

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
            double result = "".CountLevenshtein(text, insertionCost, 1.0, 1.0);
            // then
            result.Should().BeApproximately(text.Length * insertionCost, precision);
        }

        [Test]
        public void CountLevenshtein_WhenEmptyDestination_ThenSumOfDeletions()
        {
            // given
            string text = "qwertyuiop";
            double deletionCost = 2.0;
            // when
            double result = text.CountLevenshtein("", 1.0, deletionCost, 1.0);
            // then
            result.Should().BeApproximately(text.Length * deletionCost, precision);
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
            double result = "".CountLcs(text, insertionCost, 1.0);
            // then
            result.Should().BeApproximately(text.Length * insertionCost, precision);
        }

        [Test]
        public void CountLcs_WhenEmptyDestination_ThenSumOfDeletions()
        {
            // given
            string text = "qwertyuiop";
            double deletionCost = 2.0;
            // when
            double result = text.CountLcs("", 1.0, deletionCost);
            // then
            result.Should().BeApproximately(text.Length * deletionCost, precision);
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
            string text = "";
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
}