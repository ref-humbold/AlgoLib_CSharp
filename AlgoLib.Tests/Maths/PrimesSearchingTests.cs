using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Maths;

// Tests: Algorithms for searching for prime numbers.
[TestFixture]
public class PrimesSearchingTests
{
    [Test]
    public void FindPrimes_WhenMinGreaterThanMax_ThenEmpty()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(100, 30);
        // then
        result.Should().BeEmpty();
    }

    [Test]
    public void FindPrimes_WhenSingleArgument_ThenMinIsZero()
    {
        // when
        IEnumerable<int> result1 = PrimesSearching.FindPrimes(100);
        IEnumerable<int> result2 = PrimesSearching.FindPrimes(0, 100);
        // then
        result1.Should().Equal(result2);
    }

    [Test]
    public void FindPrimes_WhenMaxIsComposite_ThenAllPrimes()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(100);
        // then
        result.Should().Equal(2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59,
                              61, 67, 71, 73, 79, 83, 89, 97);
    }

    [Test]
    public void FindPrimes_WhenMaxIsPrime_ThenMaxExclusive()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(67);
        // then
        result.Should().Equal(2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61);
    }

    [Test]
    public void FindPrimes_WhenMaxIsTwo_ThenEmpty()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(2);
        // then
        result.Should().BeEmpty();
    }

    [Test]
    public void FindPrimes_WhenMaxIsThree_ThenSingleElement()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(3);
        // then
        result.Should().Equal(2);
    }

    [Test]
    public void FindPrimes_WhenMaxIsFour_ThenAllPrimes()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(4);
        // then
        result.Should().Equal(2, 3);
    }

    [Test]
    public void FindPrimes_WhenRange_ThenPrimesBetween()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(30, 200);
        // then
        result.Should().Equal(31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101,
                              103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167,
                              173, 179, 181, 191, 193, 197, 199);
    }

    [Test]
    public void FindPrimes_WhenMinIsTwo_ThenTwoIncluded()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(2, 30);
        // then
        result.Should().Equal(2, 3, 5, 7, 11, 13, 17, 19, 23, 29);
    }

    [Test]
    public void FindPrimes_WhenMinIsThree_ThenTwoNotIncluded()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(3, 30);
        // then
        result.Should().Equal(3, 5, 7, 11, 13, 17, 19, 23, 29);
    }

    [Test]
    public void FindPrimes_WhenMaxIsFourthPowerOfPrime_ThenAllPrimesBetween()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(9, 81);
        // then
        result.Should().Equal(11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61,
                              67, 71, 73, 79);
    }

    [Test]
    public void FindPrimes_WhenMinIsLessThanSquareRootOfMax_ThenPrimesBetween()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(5, 150);
        // then
        result.Should().Equal(5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67,
                              71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137,
                              139, 149);
    }

    [Test]
    public void FindPrimes_WhenMinAndMaxArePrimes_ThenMinInclusiveAndMaxExclusive()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(137, 317);
        // then
        result.Should().Equal(137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197,
                              199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271,
                              277, 281, 283, 293, 307, 311, 313);
    }

    [Test]
    public void FindPrimes_WhenMinEqualsMaxAndPrime_ThenEmpty()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(41, 41);
        // then
        result.Should().BeEmpty();
    }

    [Test]
    public void FindPrimes_WhenMinEqualsMaxAndComposite_ThenEmpty()
    {
        // when
        IEnumerable<int> result = PrimesSearching.FindPrimes(91, 91);
        // then
        result.Should().BeEmpty();
    }
}
