using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Maths;

// Tests: Algorithms for testing prime numbers.
[TestFixture]
public class PrimesTestingTests
{
    #region TestPrimeFermat

    [Test]
    public void TestPrimeFermat_WhenZero_ThenFalse()
    {
        // when
        bool result = 0.TestPrimeFermat();
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeFermat_WhenOne_ThenFalse()
    {
        // when
        bool result = 1.TestPrimeFermat();
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeFermat_WhenTwo_ThenTrue()
    {
        // when
        bool result = 2.TestPrimeFermat();
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void TestPrimeFermat_WhenPrime_ThenTrue()
    {
        // when
        bool result = 1013.TestPrimeFermat();
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void TestPrimeFermat_WhenComposite1_ThenFalse()
    {
        // when
        bool result = 1001.TestPrimeFermat();
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeFermat_WhenComposite2_ThenFalse()
    {
        // when
        bool result = 41041.TestPrimeFermat(); // 41041 = 7 * 11 * 13 * 41 is a Carmichael number
        // then
        result.Should().BeFalse();
    }

    #endregion
    #region TestPrimeMiller

    [Test]
    public void TestPrimeMiller_WhenZero_ThenFalse()
    {
        // when
        bool result = 0.TestPrimeMiller();
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeMiller_WhenOne_ThenFalse()
    {
        // when
        bool result = 1.TestPrimeMiller();
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeMiller_WhenTwo_ThenTrue()
    {
        // when
        bool result = 2.TestPrimeMiller();
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void TestPrimeMiller_WhenPrime_ThenTrue()
    {
        // when
        bool result = 1013.TestPrimeMiller();
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void TestPrimeMiller_WhenComposite1_ThenFalse()
    {
        // when
        bool result = 1001.TestPrimeMiller();
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeMiller_WhenComposite2_ThenFalse()
    {
        // when
        bool result = 41041.TestPrimeMiller();
        // then
        result.Should().BeFalse();
    }

    #endregion
}
