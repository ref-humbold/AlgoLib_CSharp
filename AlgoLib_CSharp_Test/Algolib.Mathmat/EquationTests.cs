// Tests: Structure of linear equation
using NUnit.Framework;
using System;

namespace Algolib.Mathmat
{
    [TestFixture]
    public class EquationTests
    {
        private Equation testObject;

        [SetUp]
        public void SetUp() => testObject = new Equation(new double[] { 2, 3, 0, -2 }, 15);

        [Test]
        public void ToString_ThenStringRepresentation()
        {
            // when
            string result = testObject.ToString();
            // then
            Assert.That(result, Is.EqualTo("2 x_0 + 3 x_1 + -2 x_3 = 15"));
        }

        [Test]
        public void Multiply_WhenConstantIsNonZero_ThenMultiplied()
        {
            // when
            testObject.Multiply(2);
            // then
            Assert.AreEqual(new double[] { 4, 6, 0, -4 }, testObject.Coefficients);
            Assert.AreEqual(30, testObject.Free);
        }

        [Test]
        public void Multiply_WhenConstantIsZero_ThenArithmeticException()
        {
            // when
            TestDelegate testDelegate = () => testObject.Multiply(0);
            // then
            Assert.Throws<ArithmeticException>(testDelegate);
        }

        [Test]
        public void Combine_WhenConstantIsNonZero_ThenCombined()
        {
            // when
            testObject.Combine(new Equation(new double[] { 1, -1, 4, 10 }, 5), -2);
            // then
            Assert.AreEqual(new double[] { 0, 5, -8, -22 }, testObject.Coefficients);
            Assert.AreEqual(5, testObject.Free);
        }

        [Test]
        public void Combine_WhenConstantIsZero_ThenArithmeticException()
        {
            // when
            TestDelegate testDelegate =
                () => testObject.Combine(new Equation(new double[] { 1, -1, 10, 7 }, 5), 0);
            // then
            Assert.Throws<ArithmeticException>(testDelegate);
        }
    }
}
