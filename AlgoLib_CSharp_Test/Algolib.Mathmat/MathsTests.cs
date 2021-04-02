using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Mathmat
{
    // Tests: Algorithms for basic mathematics
    [TestFixture]
    public class MathsTests
    {
        #region GCD

        [Test]
        public void GCD_WhenNumbersAreComposite_ThenGCD()
        {
            // when
            long result = Maths.GCD(161, 46);
            // then
            result.Should().Be(23);
        }

        [Test]
        public void GCD_WhenNumbersArePrime_ThenOne()
        {
            // when
            long result = Maths.GCD(127, 41);
            // then
            result.Should().Be(1);
        }

        [Test]
        public void GCD_WhenNumbersAreMutuallyPrime_ThenOne()
        {
            // when
            long result = Maths.GCD(119, 57);
            // then
            result.Should().Be(1);
        }

        [Test]
        public void GCD_WhenOneNumberIsMultipleOfAnother_ThenLessNumber()
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
        public void GCD_WhenOneNumberIsZero_ThenAnother()
        {
            // given
            long number = 96;
            // when
            long result = Maths.GCD(number, 0);
            // then
            result.Should().Be(number);
        }

        #endregion
        #region LCM

        [Test]
        public void LCM_WhenNumbersAreComposite_ThenLCM()
        {
            // when
            long result = Maths.LCM(161, 46);
            // then
            result.Should().Be(322);
        }

        [Test]
        public void LCM_WhenNumbersArePrime_ThenProduct()
        {
            // when
            long result = Maths.LCM(127, 41);
            // then
            result.Should().Be(5207);
        }

        [Test]
        public void LCM_WhenNumbersAreMutuallyPrime_ThenProduct()
        {
            // when
            long result = Maths.LCM(119, 57);
            // then
            result.Should().Be(6783);
        }

        [Test]
        public void LCM_WhenOneNumberIsMultipleOfAnother_ThenGreaterNumber()
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
        public void LCM_WhenOneOfNumbersIsZero_ThenZero()
        {
            // when
            long result = Maths.LCM(96, 0);
            // then
            result.Should().Be(0);
        }

        #endregion
    }
}
