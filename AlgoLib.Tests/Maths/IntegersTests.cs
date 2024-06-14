using System;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Maths;

// Tests: Algorithms for basic mathematical operations on integers.
[TestFixture]
public class IntegersTests
{
    #region Gcd

    [Test]
    public void Gcd_WhenNumbersAreComposite_ThenGreatestCommonDivisor()
    {
        // when
        int result = Integers.Gcd(161, 46);

        // then
        result.Should().Be(23);
    }

    [Test]
    public void Gcd_WhenNumbersArePrime_ThenOne()
    {
        // when
        long result = Integers.Gcd(127L, 41L);

        // then
        result.Should().Be(1L);
    }

    [Test]
    public void Gcd_WhenNumbersAreMutuallyPrime_ThenOne()
    {
        // when
        int result = Integers.Gcd(119, 57);

        // then
        result.Should().Be(1);
    }

    [Test]
    public void Gcd_WhenOneOfNumbersIsMultipleOfAnother_ThenLessNumber()
    {
        // given
        int number = 34;

        // when
        int result = Integers.Gcd(number, number * 6);

        // then
        result.Should().Be(number);
    }

    [Test]
    public void Gcd_WhenOneOfNumbersIsZero_ThenAnotherNumber()
    {
        // given
        int number = 96;

        // when
        int result = Integers.Gcd(number, 0);

        // then
        result.Should().Be(number);
    }

    #endregion
    #region Lcm

    [Test]
    public void Lcm_WhenNumbersAreComposite_ThenLowestCommonMultiple()
    {
        // when
        int result = Integers.Lcm(161, 46);

        // then
        result.Should().Be(322);
    }

    [Test]
    public void Lcm_WhenNumbersArePrime_ThenProduct()
    {
        // when
        long result = Integers.Lcm(127L, 41L);

        // then
        result.Should().Be(5207L);
    }

    [Test]
    public void Lcm_WhenNumbersAreMutuallyPrime_ThenProduct()
    {
        // when
        int result = Integers.Lcm(119, 57);

        // then
        result.Should().Be(6783);
    }

    [Test]
    public void Lcm_WhenOneOfNumbersIsMultipleOfAnother_ThenGreaterNumber()
    {
        // given
        int number = 34;

        // when
        int result = Integers.Lcm(number, number * 6);

        // then
        result.Should().Be(number * 6);
    }

    [Test]
    public void Lcm_WhenOneOfNumbersIsZero_ThenZero()
    {
        // when
        int result = Integers.Lcm(96, 0);

        // then
        result.Should().Be(0);
    }

    #endregion
    #region Multiply

    [Test]
    public void Multiply_WhenFirstFactorIsZero_ThenZero()
    {
        // when
        int result = Integers.Multiply(0, 14);

        // then
        result.Should().Be(0);
    }

    [Test]
    public void Multiply_WhenSecondFactorIsZero_ThenZero()
    {
        // when
        int result = Integers.Multiply(14, 0);

        // then
        result.Should().Be(0);
    }

    [Test]
    public void Multiply_WhenFactorsAreZero_ThenZero()
    {
        // when
        int result = Integers.Multiply(0, 0);

        // then
        result.Should().Be(0);
    }

    [Test]
    public void Multiply_WhenFactorsArePositive_ThenResultIsPositive()
    {
        // when
        long result = Integers.Multiply(3, 10);

        // then
        result.Should().Be(30);
    }

    [Test]
    public void Multiply_WhenFirstFactorIsNegativeAndSecondFactorIsPositive_ThenResultIsNegative()
    {
        // when
        int result = Integers.Multiply(-3, 10);

        // then
        result.Should().Be(-30);
    }

    [Test]
    public void Multiply_WhenFirstFactorIsPositiveAndSecondFactorIsNegative_ThenResultIsNegative()
    {
        // when
        int result = Integers.Multiply(3, -10);

        // then
        result.Should().Be(-30);
    }

    [Test]
    public void Multiply_WhenFactorsAreNegative_ThenResultIsPositive()
    {
        // when
        long result = Integers.Multiply(-3L, -10L);

        // then
        result.Should().Be(30L);
    }

    [Test]
    public void Multiply_WhenModuloAndFactorsArePositive()
    {
        // when
        int result = Integers.Multiply(547, 312, 10000);

        // then
        result.Should().Be(664);
    }

    [Test]
    public void Multiply_WhenModuloIsPositiveAndFirstFactorIsNegative()
    {
        // when
        int result = Integers.Multiply(-547, 312, 10000);

        // then
        result.Should().Be(9336);
    }

    [Test]
    public void Multiply_WhenModuloIsPositiveAndSecondFactorIsNegative()
    {
        // when
        int result = Integers.Multiply(547, -312, 10000);

        // then
        result.Should().Be(9336);
    }

    [Test]
    public void Multiply_WhenModuloIsPositiveAndFactorsAreNegative()
    {
        // when
        long result = Integers.Multiply(-547L, -312L, 10000L);

        // then
        result.Should().Be(664L);
    }

    [Test]
    public void Multiply_WhenModuloIsNegative_ThenArithmeticException()
    {
        // when
        Action action = () => _ = Integers.Multiply(547, 312, -10000);

        // then
        action.Should().Throw<ArithmeticException>();
    }

    #endregion
    #region Power

    [Test]
    public void Power_WhenBaseIsZero_ThenZero()
    {
        // when
        int result = Integers.Power(0, 14);

        // then
        result.Should().Be(0);
    }

    [Test]
    public void Power_WhenExponentIsZero_ThenOne()
    {
        // when
        int result = Integers.Power(14, 0);

        // then
        result.Should().Be(1);
    }

    [Test]
    public void Power_WhenBaseAndExponentAreZero_ThenNotFiniteNumberException()
    {
        // when
        Action action = () => _ = Integers.Power(0, 0);

        // then
        action.Should().Throw<NotFiniteNumberException>();
    }

    [Test]
    public void Power_WhenBaseAndExponentArePositive_ThenResultIsPositive()
    {
        // when
        int result = Integers.Power(3, 10);

        // then
        result.Should().Be(59049);
    }

    [Test]
    public void Power_WhenBaseIsNegativeAndExponentIsEven_ThenResultIsPositive()
    {
        // when
        int result = Integers.Power(-3, 10);

        // then
        result.Should().Be(59049);
    }

    [Test]
    public void Power_WhenBaseIsNegativeAndExponentIsOdd_ThenResultIsNegative()
    {
        // when
        long result = Integers.Power(-3L, 9L);

        // then
        result.Should().Be(-19683L);
    }

    [Test]
    public void Power_WhenExponentIsNegative_ThenArithmeticException()
    {
        // when
        Action action = () => _ = Integers.Power(3, -10);

        // then
        action.Should().Throw<ArithmeticException>();
    }

    [Test]
    public void Power_WhenModuloAndBaseArePositive()
    {
        // when
        int result = Integers.Power(5, 11, 10000);

        // then
        result.Should().Be(8125);
    }

    [Test]
    public void Power_WhenModuloIsPositiveAndBaseIsNegativeAndExponentIsOdd()
    {
        // when
        int result = Integers.Power(-5, 11, 10000);

        // then
        result.Should().Be(1875);
    }

    [Test]
    public void Power_WhenModuloIsPositiveAndBaseIsNegativeAndExponentIsEven()
    {
        // when
        long result = Integers.Power(-5L, 12L, 10000L);

        // then
        result.Should().Be(625L);
    }

    [Test]
    public void Power_WhenModuloIsNegative_ThenArithmeticException()
    {
        // when
        Action action = () => _ = Integers.Power(5, 11, -10000);

        // then
        action.Should().Throw<ArithmeticException>();
    }

    #endregion
}
