using NUnit.Framework;
using FluentAssertions;

namespace Algolib.Mathmat
{
    // Tests: Algorithms for prime numbers
    [TestFixture]
    public class PrimeTests
    {
        #region TestFermat

        [Test]
        public void TestFermat_WhenZero_ThenReturnsFalse()
        {
            bool result = Primes.TestFermat(0);

            result.Should().BeFalse();
        }

        [Test]
        public void TestFermat_WhenOne_ThenReturnsFalse()
        {
            bool result = Primes.TestFermat(1);

            result.Should().BeFalse();
        }

        [Test]
        public void TestFermat_WhenTwo_ThenReturnsTrue()
        {
            bool result = Primes.TestFermat(2);

            result.Should().BeTrue();
        }

        [Test]
        public void TestFermat_WhenPrime_ThenReturnsTrue()
        {
            bool result = Primes.TestFermat(1013);

            result.Should().BeTrue();
        }

        [Test]
        public void TestFermat_WhenComposite_ThenReturnsFalse()
        {
            bool result = Primes.TestFermat(1001);

            result.Should().BeFalse();
        }

        [Test]
        public void TestFermat_WhenCarmichaelNumber_ThenReturnsFalse()
        {
            bool result = Primes.TestFermat(1105);  // 1105 = 5 * 13 * 17 is a Carmichael number

            result.Should().BeFalse();
        }

        #endregion
        #region TestMiller

        [Test]
        public void TestMiller_WhenZero_ThenReturnsFalse()
        {
            bool result = Primes.TestMiller(0);

            result.Should().BeFalse();
        }

        [Test]
        public void TestMiller_WhenOne_ThenReturnsFalse()
        {
            bool result = Primes.TestMiller(1);

            result.Should().BeFalse();
        }

        [Test]
        public void TestMiller_WhenTwo_ThenReturnsTrue()
        {
            bool result = Primes.TestMiller(2);

            result.Should().BeTrue();
        }

        [Test]
        public void TestMiller_WhenPrime_ThenReturnsTrue()
        {
            bool result = Primes.TestMiller(1013);

            result.Should().BeTrue();
        }

        [Test]
        public void TestMiller_WhenComposite1_ThenReturnsFalse()
        {
            bool result = Primes.TestMiller(1001);

            result.Should().BeFalse();
        }

        [Test]
        public void TestMiller_WhenComposite2_ThenReturnsFalse()
        {
            bool result = Primes.TestMiller(1105);

            result.Should().BeFalse();
        }

        #endregion
    }
}
