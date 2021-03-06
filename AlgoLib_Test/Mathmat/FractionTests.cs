﻿// Tests: Structure of fraction
using System;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Mathmat
{
    [TestFixture]
    public class FractionTests
    {
        [Test]
        public void Constructor_WhenNumeratorAndDenominatorAreDivisible_ThenNormalized()
        {
            // when
            Fraction result = Fraction.Of(32, 104);
            // then
            result.Should().Be(Fraction.Of(4, 13));
        }

        [Test]
        public void Constructor_WhenOnlyNumerator_ThenDenominatorEqualsOne()
        {
            // when
            Fraction result = Fraction.Of(29);
            // then
            result.Should().Be(Fraction.Of(29, 1));
        }

        [Test]
        public void Constructor_WhenDenominatorIsZero_ThenArithmeticException()
        {
            // when
            Action action = () => _ = Fraction.Of(1, 0);
            // then
            action.Should().Throw<ArithmeticException>();
        }

        [Test]
        public void Constructor_WhenNumeratorIsNegative_ThenNegativeFraction()
        {
            // when
            Fraction result = Fraction.Of(-4, 11);
            // then
            result.As<IComparable<double>>().Should().BeLessThan(0);
        }

        [Test]
        public void Constructor_WhenDenominatorIsNegative_ThenNegativeFraction()
        {
            // when
            Fraction result = Fraction.Of(4, -11);
            // then
            result.As<IComparable<double>>().Should().BeLessThan(0);
        }

        [Test]
        public void Constructor_WhenNumeratorAndDenominatorAreNegative_ThenPositiveFraction()
        {
            // when
            Fraction result = Fraction.Of(-4, -11);
            // then
            result.As<IComparable<double>>().Should().BeGreaterThan(0);
        }

        [Test]
        public void OperatorPlus_WhenFraction_ThenDenominatorEqualsLCM()
        {
            // when
            Fraction result = Fraction.Of(1, 2) + Fraction.Of(5, 7);
            // then
            result.Should().Be(Fraction.Of(17, 14));
        }

        [Test]
        public void OperatorMinus_WhenFraction_ThenNormalized()
        {
            // when
            Fraction result = Fraction.Of(1, 2) - Fraction.Of(3, 10);
            // then
            result.Should().Be(Fraction.Of(1, 5));
        }

        [Test]
        public void OperatorAsterisk_WhenFraction_ThenNormalized()
        {
            // when
            Fraction result = Fraction.Of(3, 7) * Fraction.Of(5, 12);
            // then
            result.Should().Be(Fraction.Of(5, 28));
        }

        [Test]
        public void OperatorSlash_WhenFraction_ThenNormalized()
        {
            // when
            Fraction result = Fraction.Of(9, 14) / Fraction.Of(2, 5);
            // then
            result.Should().Be(Fraction.Of(45, 28));
        }

        [Test]
        public void OperatorSlash_WhenByZero_ThenDivideByZeroException()
        {
            // when
            Action action = () => _ = Fraction.Of(9, 14) / 0;
            // then
            action.Should().Throw<DivideByZeroException>();
        }

        [Test]
        public void OperatorTilde_WhenProperFraction_ThenInverted()
        {
            // when
            Fraction result = ~Fraction.Of(23, 18);
            // then
            result.Should().Be(Fraction.Of(18, 23));
        }

        [Test]
        public void OperatorTilde_WhenZero_ThenInvalidOperationException()
        {
            // when
            Action action = () => _ = ~Fraction.Of(0);
            // then
            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void CompareTo_WhenDouble_ThenCompared()
        {
            // when
            int result = Fraction.Of(25, 7).CompareTo(1.5);
            // then
            result.Should().BeGreaterThan(0);
        }

        [Test]
        public void CompareTo_WhenInteger_ThenCompared()
        {
            // when
            int result = Fraction.Of(-25, 7).CompareTo(-2);
            // then
            result.Should().BeLessThan(0);
        }
    }
}
