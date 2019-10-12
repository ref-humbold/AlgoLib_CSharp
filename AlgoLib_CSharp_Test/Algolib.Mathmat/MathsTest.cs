using NUnit.Framework;

namespace Algolib.Mathmat
{
    [TestFixture]
    public class MathsTest
    {
        [Test]
        public void GCD_WhenNumbersAreComposite_ThenReturnsGCD()
        {
            long number1 = 161;
            long number2 = 46;

            long result = Maths.GCD(number1, number2);

            Assert.AreEqual(23, result);
        }

        [Test]
        public void GCD_WhenNumbersArePrime_ThenReturnsOne()
        {
            long number1 = 127;
            long number2 = 41;

            long result = Maths.GCD(number1, number2);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GCD_WhenNumbersAreMutuallyPrime_ThenReturnsOne()
        {
            long number1 = 119;
            long number2 = 57;

            long result = Maths.GCD(number1, number2);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GCD_WhenNumber1IsMultipleOfNumber2_ThenReturnsNumber2()
        {
            long number1 = 272;
            long number2 = 34;

            long result = Maths.GCD(number1, number2);

            Assert.AreEqual(number2, result);
        }

        [Test]
        public void GCD_WhenNumber2IsZero_ThenReturnsNumber1()
        {
            long number1 = 96;
            long number2 = 0;

            long result = Maths.GCD(number1, number2);

            Assert.AreEqual(number1, result);
        }

        [Test]
        public void LCM_WhenNumbersAreComposite_ThenReturnsLCM()
            {
                long number1 = 161;
                long number2 = 46;

                long result = Maths.LCM(number1, number2);

                Assert.AreEqual(322, result);
            }

        [Test]
        public void LCM_WhenNumbersArePrime_ThenReturnsProduct()
            {
                long number1 = 127;
                long number2 = 41;

                long result = Maths.LCM(number1, number2);

                Assert.AreEqual(5207, result);
            }

        [Test]
        public void LCM_WhenNumbersAreMutuallyPrime_ThenReturnsProduct()
            {
                long number1 = 119;
                long number2 = 57;

                long result = Maths.LCM(number1, number2);

                Assert.AreEqual(6783, result);
            }

        [Test]
        public void LCM_WhenNumber1IsMultipleOfNumber2_ThenReturnsNumber1()
            {
                long number1 = 272;
                long number2 = 34;

                long result = Maths.LCM(number1, number2);

                Assert.AreEqual(number1, result);
            }

        [Test]
        public void LCM_WhenOneOfNumbersIsZero_ThenReturnsZero()
            {
                long number1 = 96;
                long number2 = 0;

                long result = Maths.LCM(number1, number2);

                Assert.AreEqual(0, result);
        }
    }
}
