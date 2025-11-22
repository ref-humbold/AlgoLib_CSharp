using System;
using NUnit.Framework;

namespace AlgoLib.Maths;

// Tests: Structure Constructor fraction.
[TestFixture]
public class FractionTests
{
    #region constructor

    [Test]
    public void Constructor_WhenDefault_ThenZero()
    {
        // when
        Fraction result = new();

        // then
        Assert.That(result, Is.EqualTo(new Fraction(0)));
    }

    [Test]
    public void Constructor_WhenNumeratorAndDenominatorAreDivisible_ThenNormalized()
    {
        // when
        Fraction result = new(32, 104);

        // then
        Assert.That(result, Is.EqualTo(new Fraction(4, 13)));
    }

    [Test]
    public void Constructor_WhenOnlyNumerator_ThenDenominatorEqualsOne()
    {
        // when
        Fraction result = new(29);

        // then
        Assert.That(result, Is.EqualTo(new Fraction(29)));
    }

    [Test]
    public void Constructor_WhenDenominatorIsZero_ThenDivideByZeroException()
    {
        // when
        Action action = () => _ = new Fraction(1, 0);

        // then
        Assert.That(action, Throws.TypeOf<DivideByZeroException>());
    }

    [Test]
    public void Constructor_WhenNumeratorIsNegative_ThenNegativeFraction()
    {
        // when
        Fraction result = new(-4, 11);

        // then
        Assert.That(result, Is.LessThan(0));
    }

    [Test]
    public void Constructor_WhenDenominatorIsNegative_ThenNegativeFraction()
    {
        // when
        Fraction result = new(4, -11);

        // then
        Assert.That(result, Is.LessThan(0));
    }

    [Test]
    public void Constructor_WhenNumeratorAndDenominatorAreNegative_ThenPositiveFraction()
    {
        // when
        Fraction result = new(-4, -11);

        // then
        Assert.That(result, Is.GreaterThan(0));
    }

    #endregion
    #region cast operators

    [Test]
    public void OperatorDouble_WhenToDouble_ThenDoubleValue()
    {
        // when
        var result = (double)new Fraction(-129, 20);

        // then
        Assert.That(result, Is.EqualTo(-6.45));
    }

    [Test]
    public void OperatorDecimal_WhenToDouble_ThenDecimalValue()
    {
        // when
        var result = (decimal)new Fraction(-129, 20);

        // then
        Assert.That(result, Is.EqualTo(-6.45m));
    }

    [Test]
    public void OperatorInt_WhenToInt_ThenIntegerValueRoundedTowardsZero()
    {
        // when
        var result = (int)new Fraction(-129, 20);

        // then
        Assert.That(result, Is.EqualTo(-6));
    }

    [Test]
    public void OperatorInt_WhenFromInt_ThenFractionValue()
    {
        // when
        Fraction result = 18;

        // then
        Assert.That(result, Is.EqualTo(new Fraction(18)));
    }

    #endregion
    #region comparison operators

    [Test]
    public void OperatorEqual_WhenSameNormalizedFraction_ThenTrue()
    {
        // when
        bool result = new Fraction(9, 15) == new Fraction(3, 5);

        // then
        Assert.That(result, Is.True);
    }

    [Test]
    public void OperatorEqual_WhenEqualToInt_ThenTrue()
    {
        // given
        var integer = 25;

        // when
        bool result1 = new Fraction(125, 5) == integer;
        bool result2 = integer == new Fraction(125, 5);

        // then
        Assert.That(result1, Is.True);
        Assert.That(result2, Is.True);
    }

    [Test]
    public void OperatorNotEqual_WhenDifferentFraction_ThenTrue()
    {
        // when
        bool result = new Fraction(9, 14) != new Fraction(3, 5);

        // then
        Assert.That(result, Is.True);
    }

    [Test]
    public void OperatorLess_WhenSameDenominatorAndGreaterNumerator_ThenTrue()
    {
        // when
        bool result = new Fraction(9, 14) < new Fraction(17, 14);

        // then
        Assert.That(result, Is.True);
    }

    [Test]
    public void OperatorLess_WhenLessThanInt_ThenTrue()
    {
        // when
        bool result = new Fraction(-31, 6) < -4;

        // then
        Assert.That(result, Is.True);
    }

    [Test]
    public void OperatorGreater_WhenSameNumeratorAndGreaterDenominator_ThenTrue()
    {
        // when
        bool result = new Fraction(9, 14) > new Fraction(9, 26);

        // then
        Assert.That(result, Is.True);
    }

    [Test]
    public void OperatorGreater_WhenGreaterThanLong_ThenTrue()
    {
        // when
        bool result = new Fraction(11, 3) > 2L;

        // then
        Assert.That(result, Is.True);
    }

    #endregion
    #region unary operators

    [Test]
    public void OperatorUnaryPlus_ThenCopied()
    {
        // given
        Fraction fraction = new(23, 18);

        // when
        Fraction result = +fraction;

        // then
        Assert.That(result, Is.Not.SameAs(fraction));
        Assert.That(result, Is.EqualTo(new Fraction(23, 18)));
    }

    [Test]
    public void OperatorUnaryMinus_ThenNegated()
    {
        // when
        Fraction result = -new Fraction(23, 18);

        // then
        Assert.That(result, Is.EqualTo(new Fraction(-23, 18)));
    }

    [Test]
    public void OperatorTilde_WhenProperFraction_ThenInverted()
    {
        // when
        Fraction result = ~new Fraction(23, 18);

        // then
        Assert.That(result, Is.EqualTo(new Fraction(18, 23)));
    }

    [Test]
    public void OperatorTilde_WhenZero_ThenInvalidOperationException()
    {
        // when
        Action action = () => _ = ~new Fraction(0);

        // then
        Assert.That(action, Throws.InvalidOperationException);
    }

    #endregion
    #region binary operators

    [Test]
    public void OperatorPlus_WhenFraction_ThenDenominatorEqualsLowestCommonMultiple()
    {
        // when
        Fraction result = new Fraction(1, 2) + new Fraction(5, 7);

        // then
        Assert.That(result, Is.EqualTo(new Fraction(17, 14)));
    }

    [Test]
    public void OperatorMinus_WhenFraction_ThenNormalized()
    {
        // when
        Fraction result = new Fraction(1, 2) - new Fraction(3, 10);

        // then
        Assert.That(result, Is.EqualTo(new Fraction(1, 5)));
    }

    [Test]
    public void OperatorAsterisk_WhenFraction_ThenNormalized()
    {
        // when
        Fraction result = new Fraction(3, 7) * new Fraction(5, 12);

        // then
        Assert.That(result, Is.EqualTo(new Fraction(5, 28)));
    }

    [Test]
    public void OperatorSlash_WhenFraction_ThenNormalized()
    {
        // when
        Fraction result = new Fraction(9, 14) / new Fraction(2, 5);

        // then
        Assert.That(result, Is.EqualTo(new Fraction(45, 28)));
    }

    [Test]
    public void OperatorSlash_WhenByZero_ThenDivideByZeroException()
    {
        // when
        Action action = () => _ = new Fraction(9, 14) / new Fraction(0);

        // then
        Assert.That(action, Throws.TypeOf<DivideByZeroException>());
    }

    #endregion
    #region CompareTo

    [Test]
    public void CompareTo_WhenSameNormalizedFraction_ThenZero()
    {
        // when
        int result = new Fraction(-35, 14).CompareTo(new Fraction(5, -2));

        // then
        Assert.That(result, Is.Zero);
    }

    [Test]
    public void CompareTo_WhenFraction_ThenCompared()
    {
        // when
        int result = new Fraction(25, 7).CompareTo(new Fraction(3, 2));

        // then
        Assert.That(result, Is.GreaterThan(0));
    }

    [Test]
    public void CompareTo_WhenInt_ThenCompared()
    {
        // when
        int result = new Fraction(-25, 7).CompareTo(-2);

        // then
        Assert.That(result, Is.LessThan(0));
    }

    [Test]
    public void CompareTo_WhenLong_ThenCompared()
    {
        // when
        int result = new Fraction(25, 7).CompareTo(2L);

        // then
        Assert.That(result, Is.GreaterThan(0));
    }

    #endregion

    [Test]
    public void ToString_ThenStringRepresentation()
    {
        // when
        var result = new Fraction(4, -19).ToString();

        // then
        Assert.That(result, Is.EqualTo("-4/19"));
    }
}
