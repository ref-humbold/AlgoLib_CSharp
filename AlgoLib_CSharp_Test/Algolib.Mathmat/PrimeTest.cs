// Tests: Algorithms for prime numbers
using NUnit.Framework;

namespace Algolib.Mathmat
{
    [TestFixture]
    public class PrimeTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        #region TestFermat

        [Test]
        public void TestFermat_WhenZero_ThenReturnsFalse()
        {
            bool result = Primes.TestFermat(0);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestFermat_WhenOne_ThenReturnsFalse()
        {
            bool result = Primes.TestFermat(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestFermat_WhenTwo_ThenReturnsTrue()
        {
            bool result = Primes.TestFermat(2);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestFermat_WhenPrime_ThenReturnsTrue()
        {
            bool result = Primes.TestFermat(1013);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestFermat_WhenComposite_ThenReturnsFalse()
        {
            bool result = Primes.TestFermat(1001);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestFermat_WhenCarmichaelNumber_ThenReturnsFalse()
        {
            bool result = Primes.TestFermat(1105);  // 1105 = 5 * 13 * 17 is a Carmichael number

            Assert.IsFalse(result);
        }

        #endregion
        #region TestMiller

        [Test]
        public void TestMiller_WhenZero_ThenReturnsFalse()
        {
            bool result = Primes.TestMiller(0);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestMiller_WhenOne_ThenReturnsFalse()
        {
            bool result = Primes.TestMiller(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestMiller_WhenTwo_ThenReturnsTrue()
        {
            bool result = Primes.TestMiller(2);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestMiller_WhenPrime_ThenReturnsTrue()
        {
            bool result = Primes.TestMiller(1013);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestMiller_WhenComposite1_ThenReturnsFalse()
        {
            bool result = Primes.TestMiller(1001);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestMiller_WhenComposite2_ThenReturnsFalse()
        {
            bool result = Primes.TestMiller(1105);

            Assert.IsFalse(result);
        }

        #endregion
    }
}
