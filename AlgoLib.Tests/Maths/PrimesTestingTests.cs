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
    public void TestPrimeFermat_WhenPrimeLong_ThenTrue()
    {
        // when
        bool result = 2131L.TestPrimeFermat();
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void TestPrimeFermat_WhenComposite_ThenFalse()
    {
        // when
        bool result = 1001.TestPrimeFermat(); // 1001 = 7 * 11 * 13
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeFermat_WhenCompositeSquareOfPrime_ThenFalse()
    {
        // when
        bool result = 3481L.TestPrimeFermat(); // 3481 = 59 ^ 2
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeFermat_WhenCompositeCarmichaelNumber_ThenFalse()
    {
        // when
        bool result = 41041.TestPrimeFermat(); // 41041 = 7 * 11 * 13 * 41
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
    public void TestPrimeMiller_WhenPrimeLong_ThenTrue()
    {
        // when
        bool result = 2131L.TestPrimeMiller();
        // then
        result.Should().BeTrue();
    }

    [Test]
    public void TestPrimeMiller_WhenComposite_ThenFalse()
    {
        // when
        bool result = 1001.TestPrimeMiller();
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeMiller_WhenCompositeSquareOfPrime_ThenFalse()
    {
        // when
        bool result = 3481L.TestPrimeMiller();
        // then
        result.Should().BeFalse();
    }

    [Test]
    public void TestPrimeMiller_WhenCompositeCarmichaelNumber_ThenFalse()
    {
        // when
        bool result = 41041.TestPrimeMiller();
        // then
        result.Should().BeFalse();
    }

    #endregion
}
