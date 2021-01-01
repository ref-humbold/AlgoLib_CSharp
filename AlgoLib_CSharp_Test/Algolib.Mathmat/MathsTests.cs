using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Mathmat
{
    // Tests: Algorithms for basic mathematics
    [TestFixture]
    public class MathsTests
    {
        [Test]
        public void GCD_WhenNumbersAreComposite_ThenReturnsGCD()
        {
            // when
            long result = Maths.GCD(161, 46);
            // then
            result.Should().Be(23);
        }

        [Test]
        public void GCD_WhenNumbersArePrime_ThenReturnsOne()
        {
            // when
            long result = Maths.GCD(127, 41);
            // then
            result.Should().Be(1);
        }

        [Test]
        public void GCD_WhenNumbersAreMutuallyPrime_ThenReturnsOne()
        {
            // when
            long result = Maths.GCD(119, 57);
            // then
            result.Should().Be(1);
        }

        [Test]
        public void GCD_WhenNumber1IsMultipleOfNumber2_ThenReturnsNumber2()
        {
            // given
            long number1 = 272;
            long number2 = 34;
            // when
            long result = Maths.GCD(number1, number2);
            // then
            result.Should().Be(number2);
        }

        [Test]
        public void GCD_WhenNumber2IsZero_ThenReturnsNumber1()
        {
            // given
            long number1 = 96;
            long number2 = 0;
            // when
            long result = Maths.GCD(number1, number2);
            // then
            result.Should().Be(number1);
        }

        [Test]
        public void LCM_WhenNumbersAreComposite_ThenReturnsLCM()
        {
            // when
            long result = Maths.LCM(161, 46);
            // then
            result.Should().Be(322);
        }

        [Test]
        public void LCM_WhenNumbersArePrime_ThenReturnsProduct()
        {
            // when
            long result = Maths.LCM(127, 41);
            // then
            result.Should().Be(5207);
        }

        [Test]
        public void LCM_WhenNumbersAreMutuallyPrime_ThenReturnsProduct()
        {
            // when
            long result = Maths.LCM(119, 57);
            // then
            result.Should().Be(6783);
        }

        [Test]
        public void LCM_WhenNumber1IsMultipleOfNumber2_ThenReturnsNumber1()
        {
            // given
            long number1 = 272;
            long number2 = 34;
            // when
            long result = Maths.LCM(number1, number2);
            // then
            result.Should().Be(number1);
        }

        [Test]
        public void LCM_WhenOneOfNumbersIsZero_ThenReturnsZero()
        {
            // given
            long number1 = 96;
            long number2 = 0;
            // when
            long result = Maths.LCM(number1, number2);
            // then
            result.Should().Be(0);
        }
    }
}
