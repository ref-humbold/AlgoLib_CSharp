// Tests: Algorithms for prime numbers
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Mathmat
{
    [TestFixture]
    public class PrimeTests
    {
        #region Find

        [Test]
        public void Find_WhenMinGreaterThanMax_ThenEmpty()
        {
            // when
            IEnumerable<int> result = Primes.Find(100, 30);
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Find_WhenSingleArgument_ThenMinIsZero()
        {
            // when
            IEnumerable<int> result1 = Primes.Find(100);
            IEnumerable<int> result2 = Primes.Find(0, 100);
            // then
            result1.Should().Equal(result2);
        }

        [Test]
        public void Find_WhenMaxIsComposite_ThenAllPrimes()
        {
            // when
            IEnumerable<int> result = Primes.Find(100);
            // then
            result.Should().Equal(2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59,
                                  61, 67, 71, 73, 79, 83, 89, 97);
        }

        [Test]
        public void Find_WhenMaxIsComposite_ThenMaxExclusive()
        {
            // when
            IEnumerable<int> result = Primes.Find(67);
            // then
            result.Should().Equal(2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61);
        }

        [Test]
        public void Find_WhenMaxIsTwo_ThenEmpty()
        {
            // when
            IEnumerable<int> result = Primes.Find(2);
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Find_WhenRange_ThenPrimesBetween()
        {
            // when
            IEnumerable<int> result = Primes.Find(30, 200);
            // then
            result.Should().Equal(31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101,
                                  103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167,
                                  173, 179, 181, 191, 193, 197, 199);
        }

        [Test]
        public void Find_WhenMinIsLessThanSquareRootOfMax_ThenPrimesBetween()
        {
            // when
            IEnumerable<int> result = Primes.Find(5, 150);
            // then
            result.Should().Equal(5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67,
                                  71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137,
                                  139, 149);
        }

        [Test]
        public void Find_WhenMinAndMaxArePrimes_ThenMinInclusiveAndMaxExclusive()
        {
            // when
            IEnumerable<int> result = Primes.Find(137, 317);
            // then
            result.Should().Equal(137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197,
                                  199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271,
                                  277, 281, 283, 293, 307, 311, 313);
        }

        [Test]
        public void Find_WhenMinEqualsMaxAndPrime_ThenEmpty()
        {
            // when
            IEnumerable<int> result = Primes.Find(41, 41);
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Find_WhenMinEqualsMaxAndComposite_ThenEmpty()
        {
            // when
            IEnumerable<int> result = Primes.Find(91, 91);
            // then
            result.Should().BeEmpty();
        }

        #endregion
        #region TestFermat

        [Test]
        public void TestFermat_WhenZero_ThenFalse()
        {
            // when
            bool result = Primes.TestFermat(0);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void TestFermat_WhenOne_ThenFalse()
        {
            // when
            bool result = Primes.TestFermat(1);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void TestFermat_WhenTwo_ThenTrue()
        {
            // when
            bool result = Primes.TestFermat(2);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void TestFermat_WhenPrime_ThenTrue()
        {
            // when
            bool result = Primes.TestFermat(1013);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void TestFermat_WhenComposite_ThenFalse()
        {
            // when
            bool result = Primes.TestFermat(1001);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void TestFermat_WhenCarmichaelNumber_ThenFalse()
        {
            // when
            bool result = Primes.TestFermat(1105);  // 1105 = 5 * 13 * 17 is a Carmichael number
            // then
            result.Should().BeFalse();
        }

        #endregion
        #region TestMiller

        [Test]
        public void TestMiller_WhenZero_ThenFalse()
        {
            // when
            bool result = Primes.TestMiller(0);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void TestMiller_WhenOne_ThenFalse()
        {
            // when
            bool result = Primes.TestMiller(1);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void TestMiller_WhenTwo_ThenTrue()
        {
            // when
            bool result = Primes.TestMiller(2);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void TestMiller_WhenPrime_ThenTrue()
        {
            // when
            bool result = Primes.TestMiller(1013);
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void TestMiller_WhenComposite1_ThenFalse()
        {
            // when
            bool result = Primes.TestMiller(1001);
            // then
            result.Should().BeFalse();
        }

        [Test]
        public void TestMiller_WhenComposite2_ThenFalse()
        {
            // when
            bool result = Primes.TestMiller(1105);
            // then
            result.Should().BeFalse();
        }

        #endregion
    }
}
