using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Maths;

// Tests: Algorithms for testing prime numbers.
[TestFixture]
public class PrimesTestingTests
{
    #region TestPrimeFermat

    // 1001 = 7 * 11 * 13 ; 3481 = 59 ^ 2
    [Test]
    public void TestPrimeFermat_WhenIntNotPrime_ThenFalse(
        [Values(0, 1, 77, 1001, 3481)] int number)
    {
        // when
        bool result = number.TestPrimeFermat();

        // then
        result.Should().BeFalse();
    }

    // 41041 = 7 * 11 * 13 * 41 ; 73627 = 17 * 61 * 71
    [Test]
    public void TestPrimeFermat_WhenLongNotPrime_ThenFalse(
        [Values(41041L, 73627L)] long number)
    {
        // when
        bool result = number.TestPrimeFermat();

        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeFermat_WhenIntIsPrime_ThenTrue(
        [Values(2, 107, 1013)] int number)
    {
        // when
        bool result = number.TestPrimeFermat();

        // then
        result.Should().BeTrue();
    }

    [Test]
    public void TestPrimeFermat_WhenLongIsPrime_ThenTrue(
        [Values(2131L, 6199L)] long number)
    {
        // when
        bool result = number.TestPrimeFermat();

        // then
        result.Should().BeTrue();
    }

    #endregion
    #region TestPrimeMiller

    [Test]
    public void TestPrimeMiller_WhenIntNotPrime_ThenFalse(
        [Values(0, 1, 77, 1001, 3481)] int number)
    {
        // when
        bool result = number.TestPrimeMiller();

        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeMiller_WhenLongNotPrime_ThenFalse(
        [Values(41041L, 73627L)] long number)
    {
        // when
        bool result = number.TestPrimeMiller();

        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeMiller_WhenIntIsPrime_ThenTrue(
        [Values(2, 107, 1013)] int number)
    {
        // when
        bool result = number.TestPrimeMiller();

        // then
        result.Should().BeTrue();
    }

    [Test]
    public void TestPrimeMiller_WhenLongIsPrime_ThenTrue(
        [Values(2131L, 6199L)] long number)
    {
        // when
        bool result = number.TestPrimeMiller();

        // then
        result.Should().BeTrue();
    }

    #endregion
}
