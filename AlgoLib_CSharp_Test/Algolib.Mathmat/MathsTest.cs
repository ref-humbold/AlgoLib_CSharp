// Tests: Algorithms for basic mathematics
using NUnit.Framework;

namespace Algolib.Mathmat
{
    [TestFixture]
    public class MathsTest
    {
        [Test]
        public void GCD_WhenNumbersAreComposite_ThenReturnsGCD()
        {
            // when
            long result = Maths.GCD(161, 46);
            // then
            Assert.AreEqual(23, result);
        }

        [Test]
        public void GCD_WhenNumbersArePrime_ThenReturnsOne()
        {
            // when
            long result = Maths.GCD(127, 41);
            // then
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GCD_WhenNumbersAreMutuallyPrime_ThenReturnsOne()
        {
            // when
            long result = Maths.GCD(119, 57);
            // then
            Assert.AreEqual(1, result);
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
            Assert.AreEqual(number2, result);
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
            Assert.AreEqual(number1, result);
        }

        [Test]
        public void LCM_WhenNumbersAreComposite_ThenReturnsLCM()
        {
            // when
            long result = Maths.LCM(161, 46);
            // then
            Assert.AreEqual(322, result);
        }

        [Test]
        public void LCM_WhenNumbersArePrime_ThenReturnsProduct()
        {
            // when
            long result = Maths.LCM(127, 41);
            // then
            Assert.AreEqual(5207, result);
        }

        [Test]
        public void LCM_WhenNumbersAreMutuallyPrime_ThenReturnsProduct()
        {
            // when
            long result = Maths.LCM(119, 57);
            // then
            Assert.AreEqual(6783, result);
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
            Assert.AreEqual(number1, result);
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
            Assert.AreEqual(0, result);
        }
    }
}
