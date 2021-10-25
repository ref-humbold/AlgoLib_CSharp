// Tests: Structure of fraction
using System;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Mathmat
{
    [TestFixture]
    public class FractionTests
    {
        [Test]
        public void Of_WhenNumeratorAndDenominatorAreDivisible_ThenNormalized()
        {
            // when
            var result = Fraction.Of(32, 104);
            // then
            result.Should().Be(Fraction.Of(4, 13));
        }

        [Test]
        public void Of_WhenOnlyNumerator_ThenDenominatorEqualsOne()
        {
            // when
            var result = Fraction.Of(29);
            // then
            result.Should().Be(Fraction.Of(29, 1));
        }

        [Test]
        public void Of_WhenDenominatorIsZero_ThenArithmeticException()
        {
            // when
            Action action = () => _ = Fraction.Of(1, 0);
            // then
            action.Should().Throw<ArithmeticException>();
        }

        [Test]
        public void Of_WhenNumeratorIsNegative_ThenNegativeFraction()
        {
            // when
            var result = Fraction.Of(-4, 11);
            // then
            result.As<IComparable<double>>().Should().BeLessThan(0);
        }

        [Test]
        public void Of_WhenDenominatorIsNegative_ThenNegativeFraction()
        {
            // when
            var result = Fraction.Of(4, -11);
            // then
            result.As<IComparable<double>>().Should().BeLessThan(0);
        }

        [Test]
        public void Of_WhenNumeratorAndDenominatorAreNegative_ThenPositiveFraction()
        {
            // when
            var result = Fraction.Of(-4, -11);
            // then
            result.As<IComparable<double>>().Should().BeGreaterThan(0);
        }

        [Test]
        public void OperatorEqual_WhenSameButNormalization_ThenTrue()
        {
            // when
            bool result = Fraction.Of(20, 12) == Fraction.Of(5, 3);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorUnaryPlus_ThenCopied()
        {
            // given
            var fraction = Fraction.Of(23, 18);
            // when
            Fraction result = +fraction;
            // then
            result.Should().NotBeSameAs(fraction);
            result.Should().Be(Fraction.Of(23, 18));
        }

        [Test]
        public void OperatorUnaryMinus_ThenNegated()
        {
            // when
            Fraction result = -Fraction.Of(23, 18);
            // then
            result.Should().Be(Fraction.Of(-23, 18));
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
            Action action = () => _ = Fraction.Of(9, 14) / Fraction.Of(0);
            // then
            action.Should().Throw<DivideByZeroException>();
        }

        [Test]
        public void CompareTo_WhenFraction_ThenCompared()
        {
            // when
            int result = Fraction.Of(25, 7).CompareTo(Fraction.Of(3, 2));
            // then
            result.Should().BeGreaterThan(0);
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
