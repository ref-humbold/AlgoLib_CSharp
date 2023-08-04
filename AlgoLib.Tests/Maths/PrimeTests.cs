// Tests: Algorithms for prime numbers
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Maths
{
    [TestFixture]
    public class PrimeTests
    {
        #region FindPrimes

        [Test]
        public void FindPrimes_WhenMinGreaterThanMax_ThenEmpty()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(100, 30);
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void FindPrimes_WhenSingleArgument_ThenMinIsZero()
        {
            // when
            IEnumerable<int> result1 = Primes.FindPrimes(100);
            IEnumerable<int> result2 = Primes.FindPrimes(0, 100);
            // then
            result1.Should().Equal(result2);
        }

        [Test]
        public void FindPrimes_WhenMaxIsComposite_ThenAllPrimes()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(100);
            // then
            result.Should().Equal(2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59,
                                  61, 67, 71, 73, 79, 83, 89, 97);
        }

        [Test]
        public void FindPrimes_WhenMaxIsPrime_ThenMaxExclusive()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(67);
            // then
            result.Should().Equal(2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61);
        }

        [Test]
        public void FindPrimes_WhenMaxIsTwo_ThenEmpty()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(2);
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void FindPrimes_WhenMaxIsThree_ThenSingleElement()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(3);
            // then
            result.Should().Equal(2);
        }

        [Test]
        public void FindPrimes_WhenMaxIsFour_ThenAllPrimes()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(4);
            // then
            result.Should().Equal(2, 3);
        }

        [Test]
        public void FindPrimes_WhenRange_ThenPrimesBetween()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(30, 200);
            // then
            result.Should().Equal(31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101,
                                  103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167,
                                  173, 179, 181, 191, 193, 197, 199);
        }

        [Test]
        public void FindPrimes_WhenMinIsTwo_ThenTwoIncluded()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(2, 30);
            // then
            result.Should().Equal(2, 3, 5, 7, 11, 13, 17, 19, 23, 29);
        }

        [Test]
        public void FindPrimes_WhenMinIsThree_ThenTwoNotIncluded()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(3, 30);
            // then
            result.Should().Equal(3, 5, 7, 11, 13, 17, 19, 23, 29);
        }

        [Test]
        public void FindPrimes_WhenMaxIsFourthPowerOfPrime_ThenAllPrimesBetween()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(9, 81);
            // then
            result.Should().Equal(11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61,
                                  67, 71, 73, 79);
        }

        [Test]
        public void FindPrimes_WhenMinIsLessThanSquareRootOfMax_ThenPrimesBetween()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(5, 150);
            // then
            result.Should().Equal(5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67,
                                  71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137,
                                  139, 149);
        }

        [Test]
        public void FindPrimes_WhenMinAndMaxArePrimes_ThenMinInclusiveAndMaxExclusive()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(137, 317);
            // then
            result.Should().Equal(137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197,
                                  199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271,
                                  277, 281, 283, 293, 307, 311, 313);
        }

        [Test]
        public void FindPrimes_WhenMinEqualsMaxAndPrime_ThenEmpty()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(41, 41);
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void FindPrimes_WhenMinEqualsMaxAndComposite_ThenEmpty()
        {
            // when
            IEnumerable<int> result = Primes.FindPrimes(91, 91);
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
