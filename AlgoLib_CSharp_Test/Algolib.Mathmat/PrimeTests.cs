// Tests: Algorithms for prime numbers
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Mathmat
{
    [TestFixture]
    public class PrimeTests
    {
        #region TestFermat

        [Test]
        public void TestFermat_WhenZero_ThenFalse()
        {
            bool result = Primes.TestFermat(0);

            result.Should().BeFalse();
        }

        [Test]
        public void TestFermat_WhenOne_ThenFalse()
        {
            bool result = Primes.TestFermat(1);

            result.Should().BeFalse();
        }

        [Test]
        public void TestFermat_WhenTwo_ThenTrue()
        {
            bool result = Primes.TestFermat(2);

            result.Should().BeTrue();
        }

        [Test]
        public void TestFermat_WhenPrime_ThenTrue()
        {
            bool result = Primes.TestFermat(1013);

            result.Should().BeTrue();
        }

        [Test]
        public void TestFermat_WhenComposite_ThenFalse()
        {
            bool result = Primes.TestFermat(1001);

            result.Should().BeFalse();
        }

        [Test]
        public void TestFermat_WhenCarmichaelNumber_ThenFalse()
        {
            bool result = Primes.TestFermat(1105);  // 1105 = 5 * 13 * 17 is a Carmichael number

            result.Should().BeFalse();
        }

        #endregion
        #region TestMiller

        [Test]
        public void TestMiller_WhenZero_ThenFalse()
        {
            bool result = Primes.TestMiller(0);

            result.Should().BeFalse();
        }

        [Test]
        public void TestMiller_WhenOne_ThenFalse()
        {
            bool result = Primes.TestMiller(1);

            result.Should().BeFalse();
        }

        [Test]
        public void TestMiller_WhenTwo_ThenTrue()
        {
            bool result = Primes.TestMiller(2);

            result.Should().BeTrue();
        }

        [Test]
        public void TestMiller_WhenPrime_ThenTrue()
        {
            bool result = Primes.TestMiller(1013);

            result.Should().BeTrue();
        }

        [Test]
        public void TestMiller_WhenComposite1_ThenFalse()
        {
            bool result = Primes.TestMiller(1001);

            result.Should().BeFalse();
        }

        [Test]
        public void TestMiller_WhenComposite2_ThenFalse()
        {
            bool result = Primes.TestMiller(1105);

            result.Should().BeFalse();
        }

        #endregion
    }
}
