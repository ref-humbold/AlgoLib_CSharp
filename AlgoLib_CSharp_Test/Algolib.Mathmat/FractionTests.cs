using NUnit.Framework;
using System;

namespace Algolib.Mathmat
{
    [TestFixture]
    public class FractionTests
    {
        [Test]
        public void Constructor_WhenNumeratorAndDenominatorAreDivisible_ThenNormalized()
        {
            // when
            Fraction result = new Fraction(32, 104);
            // then
            Assert.AreEqual(Fraction.Of(4, 13), result);
        }

        [Test]
        public void Constructor_WhenOnlyNumerator_ThenDenominatorEqualsOne()
        {
            // given
            int value = 29;
            // when
            Fraction result = new Fraction(value);
            // then
            Assert.AreEqual(value, (int)result);
        }

        [Test]
        public void Constructor_WhenDenominatorIsZero_ThenDivideByZeroException()
        {
            // when
            TestDelegate testDelegate = () => _ = new Fraction(1, 0);
            // then
            Assert.Throws<DivideByZeroException>(testDelegate);
        }

        [Test]
        public void Constructor_WhenNumeratorIsNegative_ThenNegativeFraction()
        {
            // when
            Fraction result = new Fraction(-4, 11);
            // then
            Assert.True(result < 0L);
        }

        [Test]
        public void Constructor_WhenDenominatorIsNegative_ThenNegativeFraction()
        {
            // when
            Fraction result = new Fraction(4, -11);
            // then
            Assert.True(result < 0L);
        }

        [Test]
        public void Constructor_WhenNumeratorAndDenominatorAreNegative_ThenPositiveFraction()
        {
            // when
            Fraction result = new Fraction(-4, -11);
            // then
            Assert.True(result > 0L);
        }

        [Test]
        public void OperatorAddition_WhenFraction_ThenDenominatorEqualsLCM()
        {
            // when
            Fraction result = Fraction.Of(1, 2) + Fraction.Of(5, 7);
            // then
            Assert.AreEqual(Fraction.Of(17, 14), result);
        }

        [Test]
        public void OperatorSubtraction_WhenFraction_ThenNormalized()
        {
            // when
            Fraction result = Fraction.Of(1, 2) - Fraction.Of(3, 10);
            // then
            Assert.AreEqual(Fraction.Of(1, 5), result);
        }

        [Test]
        public void OperatorMultiplication_WhenFraction_ThenNormalized()
        {
            // when
            Fraction result = Fraction.Of(3, 7) * Fraction.Of(5, 12);
            // then
            Assert.AreEqual(Fraction.Of(5, 28), result);
        }

        [Test]
        public void OperatorDivision_WhenFraction_ThenNormalized()
        {
            // when
            Fraction result = Fraction.Of(9, 14) / Fraction.Of(2, 5);
            // then
            Assert.AreEqual(Fraction.Of(45, 28), result);
        }

        [Test]
        public void OperatorInversion_WhenProperFraction_ThenInverted()
        {
            // when
            Fraction result = ~Fraction.Of(23, 18);
            // then
            Assert.AreEqual(Fraction.Of(18, 23), result);
        }

        [Test]
        public void OperatorInversion_WhenZero_ThenDivideByZeroException()
        {
            // when
            TestDelegate testDelegate = () => _ = ~Fraction.Of(0);
            // then
            Assert.Throws<DivideByZeroException>(testDelegate);
        }
    }
}
