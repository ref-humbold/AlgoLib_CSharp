﻿using FluentAssertions;
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

        #endregion
    }
}
