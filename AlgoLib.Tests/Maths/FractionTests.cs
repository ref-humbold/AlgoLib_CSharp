// Tests: Structure of fraction
using System;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Maths
{
    [TestFixture]
    public class FractionTests
    {
        #region Of

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
            result.As<IComparable<int>>().Should().BeLessThan(0);
        }

        [Test]
        public void Of_WhenDenominatorIsNegative_ThenNegativeFraction()
        {
            // when
            var result = Fraction.Of(4, -11);
            // then
            result.As<IComparable<int>>().Should().BeLessThan(0);
        }

        [Test]
        public void Of_WhenNumeratorAndDenominatorAreNegative_ThenPositiveFraction()
        {
            // when
            var result = Fraction.Of(-4, -11);
            // then
            result.As<IComparable<int>>().Should().BeGreaterThan(0);
        }

        #endregion
        #region cast operators

        [Test]
        public void OperatorDouble_WhenToDouble_ThenDobuleValue()
        {
            // when
            double result = (double)Fraction.Of(-129, 20);
            // then
            result.Should().Be(-6.45);
        }

        [Test]
        public void OperatorInt_WhenToInt_ThenIntegerValueRoundedTowardsZero()
        {
            // when
            int result = (int)Fraction.Of(-129, 20);
            // then
            result.Should().Be(-6);
        }

        [Test]
        public void OperatorInt_WhenFromInt_ThenFractionValue()
        {
            // when
            Fraction result = 18;
            // then
            result.Should().Be(Fraction.Of(18));
        }

        #endregion
        #region comparison operators

        [Test]
        public void OperatorEqual_WhenSameNormalizedFraction_ThenTrue()
        {
            // when
            bool result = Fraction.Of(9, 15) == Fraction.Of(3, 5);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorEqual_WhenEqualToInt_ThenTrue()
        {
            // given
            int integer = 25;
            // when
            bool result1 = Fraction.Of(125, 5) == integer;
            bool result2 = integer == Fraction.Of(125, 5);
            // then
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEqual_WhenDifferentFraction_ThenTrue()
        {
            // when
            bool result = Fraction.Of(9, 14) != Fraction.Of(3, 5);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorLess_WhenSameDenominatorAndGreaterNumerator_ThenTrue()
        {
            // when
            bool result = Fraction.Of(9, 14) < Fraction.Of(17, 14);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorLess_WhenLessThanInt_ThenTrue()
        {
            // when
            bool result = Fraction.Of(-31, 6) < -4;
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreater_WhenSameNumeratorAndGreaterDenominator_ThenTrue()
        {
            // when
            bool result = Fraction.Of(9, 14) > Fraction.Of(9, 26);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreater_WhenGreaterThanLong_ThenTrue()
        {
            // when
            bool result = Fraction.Of(11, 3) > 2L;
            // then
            result.Should().BeTrue();
        }

        #endregion
        #region unary operators

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

        #endregion
        #region binary operators

        [Test]
        public void OperatorPlus_WhenFraction_ThenDenominatorEqualsLowestCommonMultiple()
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

        #endregion
        #region CompareTo

        [Test]
        public void CompareTo_WhenSameNormalizedFraction_ThenZero()
        {
            // when
            int result = Fraction.Of(-35, 14).CompareTo(Fraction.Of(5, -2));
            // then
            result.Should().Be(0);
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
        public void CompareTo_WhenInt_ThenCompared()
        {
            // when
            int result = Fraction.Of(-25, 7).CompareTo(-2);
            // then
            result.Should().BeLessThan(0);
        }

        [Test]
        public void CompareTo_WhenLong_ThenCompared()
        {
            // when
            int result = Fraction.Of(25, 7).CompareTo(2L);
            // then
            result.Should().BeGreaterThan(0);
        }

        #endregion

        [Test]
        public void ToString_ThenStringRepresentation()
        {
            // when
            string result = Fraction.Of(4, -19).ToString();
            // then
            result.Should().Be("-4/19");
        }
    }
}
